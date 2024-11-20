# Virtual-CNS  
VRChatでの通信、航法、監視システム

[![GitHub license](https://img.shields.io/github/license/SeaeeesSan/SimpleFolderIcon)](https://github.com/SeaeeesSan/SimpleFolderIcon/blob/master/LICENSE)

---

## 必要要件
- [VRChat Creator Companion(VCC)](https://github.com/vrchat-community/creator-companion) 経由でインストールする [**UdonSharp 1.x**](https://github.com/vrchat-community/UdonSharp)  
- [UdonToolkit](https://github.com/orels1/UdonToolkit/)  

---

## はじめに  
1. **VRChat Creator Companion** を使用して、UdonSharpを含むVRChatワールド用のUnityプロジェクトを作成します。  
2. Unityプロジェクトを開きます。  
3. メニューの「Window」→「Package Manager」を開きます。  
4. 「+」ボタンをクリックし、`Add package from git URL` を選択します。  
5. 以下のURLを入力し、「Add」ボタンをクリックします：  
   - 通常版: `git+https://github.com/VirtualAviationJapan/Virtual-CNS.git?path=/Packages/jp.virtualaviation.virtual-cns`  
   - アルファ版: `git+https://github.com/VirtualAviationJapan/Virtual-CNS.git?path=/Packages/jp.virtualaviation.virtual-cns#alpha`  

---

### 追加資料(整備中)
以下に、 **このスクリプトの機能** の簡単な説明を追加しています(LLMを用いて作成している為その点はご留意下さい)

| **スクリプト名**                              | **説明**                                                                                       |
|---------------------------------------------|---------------------------------------------------------------------------------------------|
| **ATISGenerator**                           | ATIS（自動ターミナル情報サービス）の音声データ生成を管理するスクリプト。                          |
| **ATISPlayer**                              | ATISの音声再生を制御するスクリプト。                                                          |
| **ATCRadar**                                | 航空交通管制用のレーダーディスプレイを制御するスクリプト。                                      |
| **AirbandDatabase**                         | 無線通信の周波数データを管理するスクリプト。                                                  |
| **CDU**                                     | フライトマネジメントシステム（FMS）の操作インターフェースを提供するスクリプト。                   |
| **CDUFunction**                             | CDUの基本的な操作を定義するスクリプト。                                                      |
| **CDUFunction_RadioTuning**                 | ラジオの周波数調整を行うためのCDU機能スクリプト。                                              |
| **FMC**                                     | フライト管理コンピュータの動作を制御するメインスクリプト。                                      |
| **FlightModeControlUnit**                   | フライトモードの切り替えや制御を行うユニット。                                                |
| **AreaOnly**                                | 特定のエリアでの機能制限や有効化を管理するスクリプト。                                          |
| **ComponentInjectAttribute**                | 自動でコンポーネントをインジェクトするための属性スクリプト。                                    |
| **MaterialOfAttribute**                     | 特定のレンダラーのマテリアルを指定する属性スクリプト。                                          |
| **UdonSharpComponentInjectAttribute**       | UdonSharpコンポーネントの自動注入を管理する属性スクリプト。                                    |
| **InputRelay**                              | 入力イベントを他のスクリプトにリレーするスクリプト。                                           |
| **OneShotCamera**                           | 1回のレンダリングで無効化されるカメラスクリプト。                                              |
| **SetReplacementShader**                    | カメラに置き換え用シェーダーを適用するスクリプト。                                             |
| **UniversalTextDriver**                     | 複数のテキストコンポーネントに一括して文字列を適用するためのスクリプト。                         |
| **CDIAnimationDriver**                      | コース偏差指示器（CDI）のアニメーション制御を行うスクリプト。                                   |

---

| **Virtual-CNS スクリプト名**            | **Inari依存関係**                                     | **Toolkit依存関係**                                     |
|----------------------------------------|-----------------------------------------------------|-------------------------------------------------------|
| **ATIS関連**                           |                                                     |                                                       |
| ATISGenerator.cs                       |                                                     |                                                       |
| ATISPlayer.cs                          |                                                     |                                                       |
| **無線通信関連**                       |                                                     |                                                       |
| Transceiver.cs                         |                                                     | `NetworkedTrigger` (Toolkit)                          |
| Receiver.cs                            |                                                     |                                                       |
| Transmitter.cs                         |                                                     |                                                       |
| **CDU（コントロール・ディスプレイユニット）関連** |                                                     |                                                       |
| CDU.cs                                 |                                                     |                                                       |
| CDUFunction.cs                         |                                                     |                                                       |
| CDUFunction_RadioTuning.cs             |                                                     |                                                       |
| **フライト管理システム（FMS）関連**   |                                                     |                                                       |
| FMC.cs                                 |                                                     |                                                       |
| FlightModeControlUnit.cs               |                                                     |                                                       |
| **カスタム属性関連**                   |                                                     |                                                       |
| ComponentInjectAttribute.cs            |                                                     |                                                       |
| MaterialOfAttribute.cs                 |                                                     |                                                       |
| UdonSharpComponentInjectAttribute.cs   |                                                     |                                                       |
| **ユーティリティ・操作系**             |                                                     |                                                       |
| InputRelay.cs                          |                                                     | `UniversalAction` (Toolkit)                           |
| OneShotCamera.cs                       |                                                     | `PlatformTrigger` (Toolkit)                           |
| SetReplacementShader.cs                |                                                     |                                                       |
| **テキスト・画像処理系**               |                                                     |                                                       |
| UniversalTextDriver.cs                 | `MultiTextLoader`, `AbstractImageDownloader` (Inari)|                                                       |
| **インジケーター**                     |                                                     |                                                       |
| CDIAnimationDriver.cs                  | `AnimatorDriver` (Inari)                            |                                                       |

依存関係については、こちらで見るのを推奨→ `https://github.com/Unity-Technologies/com.unity.search.extensions.git?path=package`
