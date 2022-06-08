using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ocs.GameSystem
{
    public class TimeZoneManager : MonoBehaviour
    {
        [SerializeField] private Button NoonButton;
        [SerializeField] private GameObject Noon;
        private Image NoonImage;
        [SerializeField] private Button EveningButton;
        [SerializeField] private GameObject Evening;
        private Image EveningImage;
        [SerializeField] private Button NightButton;
        [SerializeField] private GameObject Night;
        private Image NightImage;

        // Start is called before the first frame update
        void Start()
        {
            //firstSelectedButton.Select();
            NoonImage = NoonButton.GetComponent<Image>();
            EveningImage = EveningButton.GetComponent<Image>();
            NightImage = NightButton.GetComponent<Image>();

        }

        // Update is called once per frame
        void Update()
        {
            switch(GameSetting.setting.timeZone)
            {
                case StageSetting.TimeZone.Noon:
                    switch_Noon();
                    break;
                case StageSetting.TimeZone.Evening:
                    switch_Evening();
                    break;
                case StageSetting.TimeZone.Night:
                    switch_Night();
                    break;
                default:
                    break;
            }
        }

        public void NoonClicked()
        {
            GameSetting.setting.timeZone = StageSetting.TimeZone.Noon;
        }

        public void EveningClicked()
        {
            GameSetting.setting.timeZone = StageSetting.TimeZone.Evening;
        }

        public void NightClicked()
        {
            GameSetting.setting.timeZone = StageSetting.TimeZone.Night;
        }

        private void switch_Noon()
        {
            NoonImage.enabled = true;
            Noon.SetActive (true);

            EveningImage.enabled = false;
            Evening.SetActive (false);
            NightImage.enabled = false;
            Night.SetActive (false);
        }

        private void switch_Evening()
        {
            EveningImage.enabled = true;
            Evening.SetActive (true);

            NoonImage.enabled = false;
            Noon.SetActive (false);
            NightImage.enabled = false;
            Night.SetActive (false);
        }

        private void switch_Night()
        {
            NightImage.enabled = true;
            Night.SetActive (true);

            NoonImage.enabled = false;
            Noon.SetActive (false);
            EveningImage.enabled = false;
            Evening.SetActive (false);
        }


    }
}


