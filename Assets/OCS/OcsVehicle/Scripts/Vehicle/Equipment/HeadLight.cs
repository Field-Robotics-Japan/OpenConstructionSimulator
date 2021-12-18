using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.Equipment
{
    [System.Serializable]
    public class HeadLight
    {
        public Light light;
        public void SwitchLight() => light.enabled = !light.enabled;
    }
}
