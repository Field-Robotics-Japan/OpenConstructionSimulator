using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.GameSystem{
    public class StageManager : MonoBehaviour
    {
        public bool isPlaying;

        [Header("Vehicle prefab")]
        [Tooltip("set vehicle prefab")]
        [SerializeField] private GameObject _wheelLoader;
        [SerializeField] private GameObject _backHoe;
        [SerializeField] private GameObject _truck;

        [Header("Time zone setting")]
        [SerializeField] private Light sunLight;
        [SerializeField] private Material _noonSky;
        [SerializeField] private float noonLightIntensity;
        [SerializeField] private Material _eveningSky;
        [SerializeField] private float eveningLightIntensity;
        [SerializeField] private Material _nightSky;
        [SerializeField] private float nightLightIntensity;

        void Awake()
        {

        }

        void Start()
        {
            var set = GameSetting.setting;

            if(isPlaying){
                if(set.WheelLoader.valid){
                    Instantiate(_wheelLoader, set.WheelLoader.position, Quaternion.Euler(set.WheelLoader.rotation));
                }
                if(set.BackHoe.valid){
                    Instantiate(_backHoe, set.BackHoe.position, Quaternion.Euler(set.BackHoe.rotation));
                }
                if(set.Truck.valid){
                    Instantiate(_truck, set.Truck.position, Quaternion.Euler(set.Truck.rotation));
                }
            }
        }

        void OnEnable()
        {
            var set = GameSetting.setting;
            
            if(isPlaying){
                switch (set.timeZone)
                {
                    case StageSetting.TimeZone.Noon:
                        RenderSettings.skybox = _noonSky;
                        sunLight.intensity = noonLightIntensity;
                        break;
                    case StageSetting.TimeZone.Evening:
                        RenderSettings.skybox = _eveningSky;
                        sunLight.intensity = eveningLightIntensity;
                        break;
                    case StageSetting.TimeZone.Night:
                        RenderSettings.skybox = _nightSky;
                        sunLight.intensity = nightLightIntensity;
                        break;
                    default:
                        break;
                }
            }
        }
        
        void Update()
        {
            var set = GameSetting.setting;

            if (!isPlaying)
            {
                switch (set.timeZone)
                {
                    case StageSetting.TimeZone.Noon:
                        RenderSettings.skybox = _noonSky;
                        sunLight.intensity = noonLightIntensity;
                        break;
                    case StageSetting.TimeZone.Evening:
                        RenderSettings.skybox = _eveningSky;
                        sunLight.intensity = eveningLightIntensity;
                        break;
                    case StageSetting.TimeZone.Night:
                        RenderSettings.skybox = _nightSky;
                        sunLight.intensity = nightLightIntensity;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

