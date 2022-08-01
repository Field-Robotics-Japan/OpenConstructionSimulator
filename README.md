<p align="center">
  <img src="https://user-images.githubusercontent.com/26988372/133429919-b7c8d779-7036-488e-8b17-1340bd41ea3d.png" />
</p>

## Description
Open Construction Simulator (OCS) is a free construction simulator.  
It is developed based on the game engine "Unity" and provides a simulation environment for excavation and earth transportation using heavy machinery.

**note**  
The version maintained in this repository is a **Demo** version.  
[The Full version](https://github.com/qoopen0815/OpenConstructionSimulator/releases) is distributed only as a binary due to license restrictions.

![222_Trim](https://user-images.githubusercontent.com/26988372/133398942-6b8ef0e1-ac1b-4119-a4f6-ea16bbeaaa40.gif)

In this repository, following packages are utilized.
Please check them if you have more interests.

- [UnitySensors](https://github.com/Field-Robotics-Japan/UnitySensors) : Sensor packages available for Unity
- [OcsSystem](https://github.com/qoopen0815/OcsSystem) : System management package
- [OcsTerrain](https://github.com/qoopen0815/OcsTerrain) : Terrain control package
- [OcsVehicle](https://github.com/qoopen0815/OcsVehicle) : Vehicle control package

## Installation
### 1．Installation of unity(2020.3.6f1)
First, install UnityHub with following links.
- Windows, Mac : https://unity3d.com/jp/get-unity/download
- Linux : Goto https://unity3d.com/get-unity/download and click "Download Unity Hub" button to get latest `UnityHub.AppImage`.  
  Then add execution permission for `UnityHub.AppImage` by following command.
  ```bash
  $ sudo chmod +x UnityHub.AppImage
  ```
  Then, run the UnityHub.AppImage
   ```bash
   $ ./UnityHub.AppImage
   ```
   Please certificate the LICENSE for Unity on UnityHub application (you can use them free !)

After that, choose and install Unity Editor (version : `2020.3.6f1`) from archive.  
https://unity3d.com/get-unity/download/archive

**For Windows**  
You have `.exe` file from above link. Just run them.

**For Linux**  
1. Right click on `Unity Hub` button on your desired Unity Editor version, and click "Copy Lilnk Location".
2. Run `UnityHub.AppImage` by setting copied link location as the argument. Here is the example for `2020.3.6f1` version.
   ```bash
   $ ./UnityHub.AppImage unityhub://2020.3.6f1/338bb68529b2
   ```
   If you need any other version, the procedure is same.
   After above commands, the UnityHub will start to install desird version's Unity Editor!

### 4.Open project
Finally, please open `OpenConstructionSimulator` package from UnityHub. (It takes more than 5 minuites at the first time, in the case)

### 5. Select the Scene file
There are Demo Scene file in `Asset/OpenConstructionSim/Scenes/DemoEnv.unity`.  
Please open the Scene file you want.
