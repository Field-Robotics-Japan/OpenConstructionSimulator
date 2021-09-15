using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    public class Crawler : MonoBehaviour
    {
        [SerializeField] private List<ArticulationInfo> _wheelJoints;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _motorTorque;

        private float _targetVelocity;
        private bool _reverseGear = false;
        public float TargetVelocity { set => _targetVelocity = value; }
        public bool ReverseGear { get => _reverseGear; set => _reverseGear = value; }

        private void Drive(float velocity)
        {
            if (this._reverseGear) velocity *= -1;
            foreach (ArticulationInfo wheel in this._wheelJoints)
            {
                wheel.Rotate(velocity);
            }
        }

        private void Start()
        {
            foreach (ArticulationInfo wheel in this._wheelJoints)
            {
                wheel.Initialize();
            }
        }

        private void FixedUpdate()
        {
            Drive(this._maxVelocity * this._targetVelocity);
        }
    }
}
