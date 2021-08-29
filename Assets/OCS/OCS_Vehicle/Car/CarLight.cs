using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [RequireComponent(typeof(Car))]
    public class CarLight : MonoBehaviour
    {
        [SerializeField] private List<LightInfo> _lightInfos;
        public List<LightInfo> LightInfos { get => _lightInfos; }

        // Start is called before the first frame update
        void Start()
        {
            foreach (LightInfo lightInfo in this._lightInfos)
            {
                lightInfo.Switch(false);
            }
        }
    }
}
