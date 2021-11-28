﻿using UdonSharp;
using UnityEngine;
using VRC.Udon;
using System;
#if !COMPILER_UDONSHARP && UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UdonSharpEditor;
#endif

namespace MonacaAirfrafts
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public class NavaidDatabase : UdonSharpBehaviour
    {
        public const uint NAVAID_NDB = 1;
        public const uint NAVAID_VOR = 2;
        public const uint NAVAID_DME = 4;
        public const uint NAVAID_ILS = 8;

        public const uint WAYPOINT_WAYPOINT = 0;
        public const uint WAYPOINT_AERDROME = 1;

        public Transform[] transforms = { };
        public uint[] capabilities = { };
        public string[] identities = { };
        public Transform[] dmeTransforms = { };
        public Transform[] glideSlopeTransforms = { };
        public bool[] hideFromMaps = {};

        public Transform[] waypointTransforms = {};
        public string[] waypointIdentities = {};
        public uint[] waypointTypes = {};

        public int Count => transforms.Length;
        public bool _HasCapability(int navaidIndex, uint capability) => (capabilities[navaidIndex] & capability) != 0;
        public bool _IsNDB(int navaidIndex) => _HasCapability(navaidIndex, NAVAID_NDB);
        public bool _IsVOR(int navaidIndex) => _HasCapability(navaidIndex, NAVAID_VOR);
        public bool _IsILS(int navaidIndex) => _HasCapability(navaidIndex, NAVAID_ILS);
        public bool _HasDME(int navaidIndex) => _HasCapability(navaidIndex, NAVAID_DME);

        private void Reset()
        {
            gameObject.name = nameof(NavaidDatabase);
        }

#if !COMPILER_UDONSHARP && UNITY_EDITOR
        public static IEnumerable<T> GetUdonSharpComponentsInScene<T>() where T : UdonSharpBehaviour
        {
            return FindObjectsOfType<UdonBehaviour>()
                .Where(UdonSharpEditorUtility.IsUdonSharpBehaviour)
                .Select(UdonSharpEditorUtility.GetProxyBehaviour)
                .Select(u => u as T)
                .Where(u => u != null);
        }

        public void Setup()
        {
            var rootObjects = gameObject.scene.GetRootGameObjects();
            var navaids = rootObjects.SelectMany(o => o.GetComponentsInChildren<Navaid>()).ToArray();
            transforms = navaids.Select(n => n.transform).ToArray();
            capabilities = navaids.Select(n => (uint)n.capability).ToArray();
            identities = navaids.Select(n => n.identity).ToArray();
            dmeTransforms = navaids.Select(n => n.DmeTransform).ToArray();
            glideSlopeTransforms = navaids.Select(n => n.glideSlope).ToArray();
            hideFromMaps = navaids.Select(n => n.hideFromMap).ToArray();

            var waypoints = rootObjects.SelectMany(o => o.GetComponentsInChildren<Waypoint>());
            waypointTransforms = waypoints.Select(w => w.transform).ToArray();
            waypointIdentities = waypoints.Select(w => w.identity).ToArray();
            waypointTypes = waypoints.Select(w => (uint)w.type).ToArray();
        }
#endif
    }

#if !COMPILER_UDONSHARP && UNITY_EDITOR

    [CustomEditor(typeof(NavaidDatabase))]
    public class NavaidDatabaseEditor : Editor
    {
        public int tabIndex;
        public int navaidIndex;
        public int waypointIndex;

        private readonly string[] tabs = {
            "Navaids",
            "Waypoints",
        };

        private void NavaidGUI()
        {
            var db = target as NavaidDatabase;

            using (new EditorGUILayout.HorizontalScope())
            {
                navaidIndex = EditorGUILayout.Popup(navaidIndex, db.identities.ToArray());

                if (GUILayout.Button("Force Refresh", EditorStyles.miniButton, GUILayout.ExpandWidth(false))) SetupAll();
            }

            var navaid = db.transforms[navaidIndex].GetComponent<Navaid>();

            EditorGUILayout.ObjectField("Navaid", navaid, navaid.GetType(), true);

            var serializedNavaid = new SerializedObject(navaid);
            var property = serializedNavaid.GetIterator();
            property.NextVisible(true);
            while (property.NextVisible(false))
            {
                var disabled = property.name == nameof(Navaid.glideSlope) && !navaid.IsILS;
                using (new EditorGUI.DisabledGroupScope(disabled))
                {
                    EditorGUILayout.PropertyField(property, true);
                }
            }
            serializedNavaid.ApplyModifiedProperties();
        }

        private void WaypointGUI()
        {
            var db = target as NavaidDatabase;

            using (new EditorGUILayout.HorizontalScope())
            {
                waypointIndex = EditorGUILayout.Popup(waypointIndex, db.waypointIdentities.ToArray());

                if (GUILayout.Button("Force Refresh", EditorStyles.miniButton, GUILayout.ExpandWidth(false))) SetupAll();
            }

            var waypoint = db.waypointTransforms[waypointIndex].GetComponent<Waypoint>();

            EditorGUILayout.ObjectField("Waypoint", waypoint, waypoint.GetType(), true);

            var serializedNavaid = new SerializedObject(waypoint);
            var property = serializedNavaid.GetIterator();
            property.NextVisible(true);
            while (property.NextVisible(false))
            {
                var disabled = false;
                using (new EditorGUI.DisabledGroupScope(disabled))
                {
                    EditorGUILayout.PropertyField(property, true);
                }
            }
            serializedNavaid.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            tabIndex = GUILayout.Toolbar(tabIndex, tabs);
            switch (tabIndex)
            {
                case 0:
                    NavaidGUI();
                    break;
                case 1:
                    WaypointGUI();
                    break;
            }
        }

        [InitializeOnLoadMethod]
        static public void RegisterCallback()
        {
            EditorSceneManager.sceneSaving += (_, __) => SetupAll();
        }

        private static void SetupAll()
        {
            foreach (var db in NavaidDatabase.GetUdonSharpComponentsInScene<NavaidDatabase>())
            {
                db.Setup();
                db.ApplyProxyModifications();
                EditorUtility.SetDirty(UdonSharpEditorUtility.GetBackingUdonBehaviour(db));
            }
        }
    }
#endif
}