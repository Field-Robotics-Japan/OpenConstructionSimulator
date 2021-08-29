using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private Car _vehicle;
        [SerializeField] private CarLight _lights;

        private Ocs.Input.InputActions _input;

        private void Awake()
        {
            this._input = new Ocs.Input.InputActions();
        }

        private void Start()
        {
            // Callback
            this._input.CarDriver.ShiftUp.started += context => this._vehicle.ReverseGear = false;
            this._input.CarDriver.ShiftDown.started += context => this._vehicle.ReverseGear = true;
            if (this._lights != null) this._input.CarDriver.Light.started += context => this._lights.LightInfos[1].Switch();
        }

        private void OnEnable() => this._input.Enable();
        private void OnDestroy() => this._input.Dispose();

        private void OnDisable()
        {
            this._input.Disable();
            this._vehicle.AccelInput = 0.0f;
            this._vehicle.BrakeInput = 1.0f;
            this._vehicle.SteeringInput = 0.0f;
        }

        void Update()
        {
            this._vehicle.AccelInput = this._input.CarDriver.Accel.ReadValue<float>();
            this._vehicle.BrakeInput = this._input.CarDriver.Brake.ReadValue<float>();
            this._vehicle.SteeringInput = this._input.CarDriver.Steering.ReadValue<Vector2>()[0];
        }
    }
}
