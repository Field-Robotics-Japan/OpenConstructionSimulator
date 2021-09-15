using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    public class Crawler : MonoBehaviour
    {
        [SerializeField] private List<HingeJoint> _wheelJoints;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _motorTorque;

        private float _targetVelocity;
        private bool _reverseGear = false;
        public float TargetVelocity { set => _targetVelocity = value; }
        public bool ReverseGear { get => _reverseGear; set => _reverseGear = value; }

        JointMotor jointMotor;

        private void Drive(float velocity)
        {
            if (this._reverseGear) velocity *= -1;
            foreach (HingeJoint wheel in this._wheelJoints)
            {
                jointMotor.targetVelocity = velocity;
                wheel.motor = jointMotor;
            }
        }

        private void Start()
        {
            jointMotor = new JointMotor();
            jointMotor.force = this._motorTorque;
            jointMotor.freeSpin = false;
        }

        private void FixedUpdate()
        {
            Drive(this._maxVelocity * this._targetVelocity);
        }
    }
}
