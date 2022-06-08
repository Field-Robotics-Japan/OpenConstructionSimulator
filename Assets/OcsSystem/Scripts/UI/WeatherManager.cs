using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ocs.GameSystem
{
    public class WeatherManager : MonoBehaviour
    {
        [SerializeField] private Button SunButton;
        [SerializeField] private GameObject Sun;
        private Image SunImage;
        [SerializeField] private Button RainButton;
        [SerializeField] private GameObject Rain;
        private Image RainImage;

        // Start is called before the first frame update
        void Start()
        {
            //firstSelectedButton.Select();
            SunImage = SunButton.GetComponent<Image>();
            RainImage = RainButton.GetComponent<Image>();

        }

        // Update is called once per frame
        void Update()
        {
            switch(GameSetting.setting.weather)
            {
                case StageSetting.Weather.Sun:
                    switch_Sun();
                    break;
                case StageSetting.Weather.Rain:
                    switch_Rain();
                    break;
                default:
                    break;
            }
        }

        public void SunClicked()
        {
            GameSetting.setting.weather = StageSetting.Weather.Sun;
        }

        public void RainClicked()
        {
            GameSetting.setting.weather = StageSetting.Weather.Rain;
        }

        private void switch_Sun()
        {
            SunImage.enabled = true;
            Sun.SetActive (true);

            RainImage.enabled = false;
            Rain.SetActive (false);
        }

        private void switch_Rain()
        {
            RainImage.enabled = true;
            Rain.SetActive (true);

            SunImage.enabled = false;
            Sun.SetActive (false);
        }

    }
}

