using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [System.Serializable]
    public class ArticulationInfo
    {
        public ArticulationBody articulationBody;
        public float speed;

        ArticulationDrive articulationDrive;

        public void Initialize()
        {
            articulationDrive = articulationBody.xDrive;
        }

        public void Rotate(float input)
        {
            articulationDrive.targetVelocity = input * speed;
            articulationBody.xDrive = articulationDrive;
        }
    }
}
