# OcsTerrain

## Overview

Mesh/Terrain deformation tools for OpenConstructionSim.

## Installation

Clone this repo into your unity project.

```bash
cd [YourProjects]/Assets/~
git clone https://github.com/Field-Robotics-Japan/OcsTerrain.git
```

## Setup Environment
### Construction Site

1. Create your construction site using Terrain.
1. Attach [TerrainManager.cs](./Scripts/TerrainManager.cs) to the Terrain you have created.
1. Enter the parameters.

### Excavation Bucket

1. Add an empty child object to the excavation bucket.
1. Attach [ExcavateLine.cs](./Scripts/ExcavateLine.cs) to the object.
1. Enter the parameters.
1. Adjust the position of the object to the tip of the bucket. (Where the ExcavateLine can collide with the Terrain)

### Layer Setting

If the terrain and bucket collide, they will not work properly, so use the layer settings to prevent them from colliding with each other.  
※Do not release the collision of soil particles. 
