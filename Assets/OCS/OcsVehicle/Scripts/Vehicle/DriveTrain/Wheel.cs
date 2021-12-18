using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.DriveTrain
{
    [System.Serializable]
    public class Wheel
    {
        public WheelCollider leftWheel;
        public Transform leftWheelMesh;
        public WheelCollider rightWheel;
        public Transform rightWheelMesh;
        public bool isMotor;
        public bool isBrake;
        public bool isSteering;

        public void UpdateMeshTransform()
        {
            Vector3 position;
            Quaternion rotation;

            leftWheel.GetWorldPose(out position, out rotation);
            leftWheelMesh.transform.position = position;
            leftWheelMesh.transform.rotation = rotation;
            rightWheel.GetWorldPose(out position, out rotation);
            rightWheelMesh.transform.position = position;
            rightWheelMesh.transform.rotation = rotation;
        }

        public void Drive(float motor, float brake)
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
        }

        public void Steering(float angle)
        {
            if (isSteering)
            {
                leftWheel.steerAngle = angle;
                rightWheel.steerAngle = angle;
            }
        }
    }
}
