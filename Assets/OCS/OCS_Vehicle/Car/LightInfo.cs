using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [System.Serializable]
    public class LightInfo
    {
        [SerializeField] private Light leftLight;
        [SerializeField] private Light rightLight;
        private bool _lightState;
        public bool LightState { get => _lightState;}

        public void Switch()
        {
            leftLight.enabled = !leftLight.enabled;
            rightLight.enabled = !rightLight.enabled;
            this._lightState = leftLight.enabled;
        }
        public void Switch(bool state)
        {
            leftLight.enabled = state;
            rightLight.enabled = state;
            this._lightState = leftLight.enabled;
        }
        public void TurnOn()
        {
            leftLight.enabled = true;
            rightLight.enabled = true;
            this._lightState = leftLight.enabled;
        }
        public void TurnOff()
        {
            leftLight.enabled = false;
            rightLight.enabled = false;
            this._lightState = leftLight.enabled;
        }
    }
}
