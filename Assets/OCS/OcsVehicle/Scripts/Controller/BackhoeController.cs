using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.Controller
{
    public class BackhoeController : MonoBehaviour
    {
        [SerializeField] private Backhoe _vehicle;

        private Ocs.Input.VehicleInput _input;

        private void Awake()
        {
            this._input = new Ocs.Input.VehicleInput();
        }

        private void OnEnable() => this._input.Enable();
        private void OnDestroy() => this._input.Dispose();

        private void OnDisable()
        {
            this._input.Disable();
        }

        private void Start()
        {
            // Callback
            this._input.Crawler.LeftReverse.started += context => this._vehicle.LeftReverse = !this._vehicle.LeftReverse;
            this._input.Crawler.RightReverse.started += context => this._vehicle.RightReverse = !this._vehicle.RightReverse;
            this._input.Equipment.Light.started += context => this._vehicle.SwitchLight();
            this._input.Equipment.Hone.started += context => this._vehicle.PlayHone();
            this._input.Equipment.LeftWinker.started += context => this._vehicle.SwitchLeftWinker();
            this._input.Equipment.RightWinker.started += context => this._vehicle.SwitchRightWinker();
        }

        void Update()
        {
            this._vehicle.LeftCrawlerInput = this._input.Crawler.LeftForward.ReadValue<float>();
            this._vehicle.RightCrawlerInput = this._input.Crawler.RightForward.ReadValue<float>();
            this._vehicle.BaseInput = this._input.Backhoe.Lever0.ReadValue<Vector2>()[0];
            this._vehicle.BoomInput = -this._input.Backhoe.Lever1.ReadValue<Vector2>()[1];
            this._vehicle.ArmInput = this._input.Backhoe.Lever0.ReadValue<Vector2>()[1];
            this._vehicle.EndInput = this._input.Backhoe.Lever1.ReadValue<Vector2>()[0];
        }
    }
}
