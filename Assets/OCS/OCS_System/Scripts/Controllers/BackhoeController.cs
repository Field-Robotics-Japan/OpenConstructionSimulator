using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    public class BackhoeController : MonoBehaviour
    {
        [SerializeField] private Backhoe _vehicle;
        [SerializeField] private Crawler _leftCrawler;
        [SerializeField] private Crawler _rightCrawler;

        private Ocs.Input.InputActions _input;

        private void Awake()
        {
            this._input = new Ocs.Input.InputActions();
        }

        private void Start()
        {
            // Callback
            this._input.BackhoeDriver.LeftReverse.started += context => this._leftCrawler.ReverseGear = !this._leftCrawler.ReverseGear;
            this._input.BackhoeDriver.RightReverse.started += context => this._rightCrawler.ReverseGear = !this._rightCrawler.ReverseGear;
        }

        private void OnEnable() => this._input.Enable();
        private void OnDestroy() => this._input.Dispose();

        private void OnDisable()
        {
            this._input.Disable();
            this._vehicle.BaseJointInput = 0.0f;
            this._vehicle.BoomJointInput = 0.0f;
            this._vehicle.ArmJointInput = 0.0f;
            this._vehicle.EndJointInput = 0.0f;
            this._leftCrawler.TargetVelocity = 0.0f;
            this._rightCrawler.TargetVelocity = 0.0f;
        }

        void Update()
        {
            this._vehicle.BaseJointInput = this._input.BackhoeDriver.LeftStick.ReadValue<Vector2>()[0];
            this._vehicle.BoomJointInput = this._input.BackhoeDriver.RightStick.ReadValue<Vector2>()[1];
            this._vehicle.ArmJointInput = this._input.BackhoeDriver.LeftStick.ReadValue<Vector2>()[1];
            this._vehicle.EndJointInput = this._input.BackhoeDriver.RightStick.ReadValue<Vector2>()[0];
            this._leftCrawler.TargetVelocity = this._input.BackhoeDriver.LeftForward.ReadValue<float>();
            this._rightCrawler.TargetVelocity = this._input.BackhoeDriver.RightForward.ReadValue<float>();
        }
    }
}
