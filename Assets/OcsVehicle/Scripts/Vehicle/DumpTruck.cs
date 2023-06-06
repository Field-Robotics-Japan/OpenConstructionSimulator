using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ocs.Vehicle.DriveTrain;

namespace Ocs.Vehicle
{
    public class DumpTruck : Car
    {
        [Header("- Work Setting -")]
        [SerializeField] private CylinderDrivenJoint _work;
        [SerializeField] private float _controlSpeed;
        
        public float WorkJointInput { get; set; }

        protected override void Update()
        {
            base.Update();
            this._work.RotateJoint(WorkJointInput * this._controlSpeed);
        }
    }
}
