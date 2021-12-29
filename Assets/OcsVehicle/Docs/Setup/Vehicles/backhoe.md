# Backhoeについて

OCSで使用するBackhoeの情報をまとめています。  
OcsVehicleでは参考用にBackhoeを１台収録しているのでご確認ください。

※改善点があれば[Issue](https://github.com/Field-Robotics-Japan/OcsVehicle/issues)へのコメントをよろしくお願い致します。

<img src="https://user-images.githubusercontent.com/26988372/139288755-bf3a7cc7-95d9-4437-8a0a-44b3efda29b7.gif" width=90%>

## 操作方法

重機の操作はUnityのInputSystemを使用していますが、必要に応じて[キーコンフィグ](../../../Scripts/InputSystem/VehicleInput.inputactions)を修正してください。

<img src="../../HowToPlay/Backhoe.png" width=70%>

## Prefabの基本構成

Backhoeは下記のような構成を基本としています。  
必要に応じてリンクの追加や親子関係の変更をしてください。

- Backhoe(root)
  - Base
    - Body
      - Boom
        - Arm
          - Bucket
    - LeftCrawler(Prefab: Crawler)
    - RightCrawler(Prefab: Crawler)

Backhoeに使用しているCrawlerはPrefabとして収録しています。  
（球体(Sphere)を6個並べただけですが・・）

- Crawler(root)
  - Sphere x6


## コンポーネント一覧

Backhoeに使用しているC#スクリプトの一覧です。  
下記以外にもRigidBodyなど様々なコンポーネントを使用しているので、詳しくはOcsVehicleに収録しているモデルを参照してください。

- [Backhoe.cs](../../../Scripts/Vehicle/Backhoe.cs)
- [BackhoeController.cs](../../../Scripts/Controller/BackhoeController.cs)
