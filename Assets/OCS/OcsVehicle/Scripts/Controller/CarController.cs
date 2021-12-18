using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.Controller
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private Car _vehicle;

        private Ocs.Input.VehicleInput _input;

        private void Awake()
        {
            this._input = new Ocs.Input.VehicleInput();
        }

        private void Start()
        {
            // Callback
            this._input.Car.ShiftUp.started += context => this._vehicle.ReverseGear = false;
            this._input.Car.ShiftDown.started += context => this._vehicle.ReverseGear = true;
            this._input.Equipment.Light.started += context => this._vehicle.SwitchLight();
            this._input.Equipment.Hone.started += context => this._vehicle.PlayHone();
            this._input.Equipment.LeftWinker.started += context => this._vehicle.SwitchLeftWinker();
            this._input.Equipment.RightWinker.started += context => this._vehicle.SwitchRightWinker();
        }

        private void OnEnable() => this._input.Enable();
        private void OnDestroy() => this._input.Dispose();

        private void OnDisable()
        {
            this._input.Disable();
            this._vehicle.AccelInput = 0.0f;
            this._vehicle.BrakeInput = 1.0f;
            this._vehicle.SteerInput = 0.0f;
        }

        void Update()
        {
            this._vehicle.AccelInput = this._input.Car.Accel.ReadValue<float>();
            this._vehicle.BrakeInput = this._input.Car.Brake.ReadValue<float>();
            this._vehicle.SteerInput = this._input.Car.Steering.ReadValue<Vector2>()[0];
        }
    }
}
