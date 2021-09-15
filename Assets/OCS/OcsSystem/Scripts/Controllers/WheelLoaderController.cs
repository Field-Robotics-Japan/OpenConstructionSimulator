using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [RequireComponent(typeof(CarController))]
    public class WheelLoaderController : MonoBehaviour
    {
        [SerializeField] private WheelLoader _vehicle;

        private Ocs.Input.InputActions _input;

        private void Awake()
        {
            this._input = new Ocs.Input.InputActions();
        }

        private void OnEnable() => this._input.Enable();

        private void OnDisable() => this._input.Disable();

        private void OnDestroy() => this._input.Dispose();

        void Update()
        {
            this._vehicle.SteerInput = -this._input.CarDriver.Steering.ReadValue<Vector2>()[0] * 15;
            this._vehicle.HeightInput = this._input.CarDriver.Joint0.ReadValue<float>();
            this._vehicle.RotateInput = this._input.CarDriver.Joint1.ReadValue<float>();
        }
    }
}
