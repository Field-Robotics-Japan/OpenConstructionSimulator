# WheelLoaderについて

OCSで使用するWheelLoaderの情報をまとめています。  
OcsVehicleでは参考用にWheelLoaderを１台収録しているのでご確認ください。

※改善点があれば[Issue](https://github.com/Field-Robotics-Japan/OcsVehicle/issues)へのコメントをよろしくお願い致します。

<img src="https://user-images.githubusercontent.com/26988372/139441632-1f9a3354-7143-4739-9cbf-21ec85f49ebf.gif" width=90%>

## 操作方法

重機の操作はUnityのInputSystemを使用していますが、必要に応じて[キーコンフィグ](../../../Scripts/InputSystem/VehicleInput.inputactions)を修正してください。

<img src="../../HowToPlay/WheelLoader.png" width=70%>

## Prefabの基本構成

WheelLoaderは下記のような構成を基本としています。  
必要に応じてリンクの追加や親子関係の変更をしてください。

- WheelLoader(root)
  - MainBody
    - SubBody
      - Boom
        - Bucket
      - Hub.FL
        - Wheel.FL
      - Hub.FR
        - Wheel.FR
    - Hub.RL
      - Wheel.RL
    - Hub.RR
      - Wheel.RR
  - Equipments(optional)
    - Lights
    - Winkers
    - Hone

## コンポーネント一覧

WheelLoaderに使用しているC#スクリプトの一覧です。  
下記以外にもRigidBodyなど様々なコンポーネントを使用しているので、詳しくはOcsVehicleに収録しているモデルを参照してください。

- [WheelLoader.cs](../../../Scripts/Vehicle/WheelLoader.cs)
- [WheelLoaderController.cs](../../../Scripts/Controller/WheelLoaderController.cs)
