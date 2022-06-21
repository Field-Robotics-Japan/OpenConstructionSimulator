using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.DriveTrain
{
    [System.Serializable]
    public class CylinderDrivenJoint
    {
        public Transform targetJoint;
        public Transform cylinderJoint;
        public Transform rodJoint;
        public float minExtendRange;
        public float maxExtendRange;
        public bool inverse;

        private float _initialLength = 1.0f;

        public float Length { get => (cylinderJoint.position - rodJoint.position).magnitude / _initialLength; }

        public void Init() => _initialLength = (cylinderJoint.position - rodJoint.position).magnitude;

        public void RotateJoint(float speed)
        {
            if (!inverse)
            {
                if (Length < minExtendRange && speed < 0) return;
                else if (maxExtendRange < Length && 0 < speed) return;
            }
            if (inverse)
            {
                if (Length < minExtendRange && speed > 0) return;
                else if (maxExtendRange < Length && 0 > speed) return;
            }

            targetJoint.Rotate(Vector3.left * speed);
        }

        public void SmoothRotateJoint(float speed)
        {

        }
    }
}