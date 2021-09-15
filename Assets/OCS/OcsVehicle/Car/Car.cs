using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private List<AxleInfo> _axleInfos;
        [SerializeField] private float _maxMotorTorque;
        [SerializeField] private float _maxBrakeTorque;
        [SerializeField] private float _maxSteeringAngle;

        private float _accelInput;
        private float _brakeInput;
        private float _steeringInput;
        private bool _reverseGear;

        public float AccelInput { get => _accelInput; set => _accelInput = value; }
        public float BrakeInput { set => _brakeInput = value; }
        public float SteeringInput { set => _steeringInput = value; }
        public bool ReverseGear { set => _reverseGear = value; }

        private void Drive(float motor, float brake, float steering)
        {
            if (this._reverseGear) motor *= -1;
            foreach (AxleInfo axleInfo in this._axleInfos)
            {
                axleInfo.Drive(motor, brake, steering);
            }
        }

        private void FixedUpdate()
        {
            Drive(this._maxMotorTorque   * this._accelInput,
                  this._maxBrakeTorque   * this._brakeInput,
                  this._maxSteeringAngle * this._steeringInput);
        }
    }
}
