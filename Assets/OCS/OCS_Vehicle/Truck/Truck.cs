using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [RequireComponent(typeof(Car))]
    public class Truck : MonoBehaviour
    {
        [SerializeField] private Transform _truckBed;
        [SerializeField] private float _controlSpeed;

        private float _truckbedJointInput;
        public float TruckbedJointInput { get => _truckbedJointInput; set => _truckbedJointInput = value; }

        private void Update()
        {
            Vector3 newJointRotate = this._truckBed.localRotation.eulerAngles + Vector3.forward * this._controlSpeed * this._truckbedJointInput;
            newJointRotate.z = System.Math.Min(newJointRotate.z, 60.0f);
            newJointRotate.z = System.Math.Max(newJointRotate.z, 0.0f);
            this._truckBed.localRotation = Quaternion.Euler(newJointRotate);
        }
    }
}
