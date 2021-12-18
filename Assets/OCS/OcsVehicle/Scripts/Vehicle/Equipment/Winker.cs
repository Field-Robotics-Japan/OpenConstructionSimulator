using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.Equipment
{
    [System.Serializable]
    public class Winker
    {
        public Light leftWinker;
        public Light rightWinker;

        public void Blinky(float frequency, float maxIntensity)
        {
            if (leftWinker.enabled) leftWinker.intensity = 0.5f * (Mathf.Sin(Time.time * frequency) + 1.0f) * maxIntensity;
            if (rightWinker.enabled) rightWinker.intensity = 0.5f * (Mathf.Sin(Time.time * frequency) + 1.0f) * maxIntensity;
        }

        public void SwitchLeftWinker()
        {
            leftWinker.enabled = !leftWinker.enabled;
        }

        public void SwitchRightWinker()
        {
            rightWinker.enabled = !rightWinker.enabled;
        }
    }
}
