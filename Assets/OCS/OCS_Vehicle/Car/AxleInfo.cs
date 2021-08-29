using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool isMotor;
        public bool isBrake;
        public bool isSteering;

        public void ApplyLocalPositionToVisuals(WheelCollider collider)
        {
            if (collider.transform.childCount == 0) return;

            Transform visualWheel = collider.transform.GetChild(0);

            Vector3 position;
            Quaternion rotation;
            collider.GetWorldPose(out position, out rotation);

            visualWheel.transform.position = position;
            visualWheel.transform.rotation = rotation;
        }

        public void Drive(float motor, float brake, float steering)
        {
            if (isMotor)
            {
                leftWheel.motorTorque = motor;
                rightWheel.motorTorque = motor;
            }
            if (isBrake)
            {
                leftWheel.brakeTorque = brake;
                rightWheel.brakeTorque = brake;
            }
            if (isSteering)
            {
                leftWheel.steerAngle = steering;
                rightWheel.steerAngle = steering;
            }
            ApplyLocalPositionToVisuals(leftWheel);
            ApplyLocalPositionToVisuals(rightWheel);
        }
    }
}

