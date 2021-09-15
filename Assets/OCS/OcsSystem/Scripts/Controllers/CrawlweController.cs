using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    public class CrawlweController : MonoBehaviour
    {
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
            this._input.BackhoeDriver.LeftReverse.started += context => this._leftCrawler.ReverseGear = false;
            this._input.BackhoeDriver.RightReverse.started += context => this._rightCrawler.ReverseGear = false;
        }

        private void OnEnable() => this._input.Enable();
        private void OnDestroy() => this._input.Dispose();

        private void OnDisable()
        {
            this._input.Disable();
            this._leftCrawler.TargetVelocity = 0.0f;
            this._rightCrawler.TargetVelocity = 0.0f;
        }

        void Update()
        {
            this._leftCrawler.TargetVelocity = this._input.BackhoeDriver.LeftForward.ReadValue<float>();
            this._rightCrawler.TargetVelocity = this._input.BackhoeDriver.RightForward.ReadValue<float>();
        }
    }
}
