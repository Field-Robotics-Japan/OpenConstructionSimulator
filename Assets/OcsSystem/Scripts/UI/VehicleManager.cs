using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Ocs.GameSystem
{
    public class VehicleManager : MonoBehaviour
    {
        //[SerializeField] private EventSystem eventSystem;

        /*[SerializeField] private Button WheelLoaderButton;
        private Image WheelLoaderImage;
        [SerializeField] private Button BackHoeButton;
        private Image BackHoeImage;
        [SerializeField] private Button TruckButton;
        private Image TruckImage;
        */
        [SerializeField] private Toggle WheelLoader_toggle;
        [SerializeField] private Toggle BackHoe_toggle;
        [SerializeField] private Toggle Truck_toggle;

        // Start is called before the first frame update
        void Start()
        {
            //firstSelectedButton.Select();
            WheelLoader_toggle.isOn = GameSetting.setting.WheelLoader.valid;
            BackHoe_toggle.isOn = GameSetting.setting.BackHoe.valid;
            Truck_toggle.isOn = GameSetting.setting.Truck.valid;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void WheelLoaderClicked()
        {
            GameSetting.setting.WheelLoader.valid = WheelLoader_toggle.isOn;
        }

        public void BackHoeClicked()
        {
            GameSetting.setting.BackHoe.valid = BackHoe_toggle.isOn;
        }

        public void TruckClicked()
        {
            GameSetting.setting.Truck.valid = Truck_toggle.isOn;
        }

    }

}