using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.GameSystem{
    public class CameraControl : MonoBehaviour
    {

        [SerializeField] private ModeManeger mode;
        [SerializeField] private Ocs.Vehicle.WheelLoader _wheelLoader;
        [SerializeField] private Ocs.Vehicle.Backhoe _backHoe;
        [SerializeField] private Ocs.Vehicle.DumpTruck _truck;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        void Update()
        {
            var set = GameSetting.setting;
            if(mode.AroundView){
                if(set.WheelLoader.valid){
                    _wheelLoader.switchUpperCamera(true);
                }
                if(set.BackHoe.valid){
                    _backHoe.switchUpperCamera(true);
                }
                if(set.Truck.valid){
                    _truck.switchUpperCamera(true);
                }
            }else{
                if(set.WheelLoader.valid){
                    _wheelLoader.switchUpperCamera(false);
                }
                if(set.BackHoe.valid){
                    _backHoe.switchUpperCamera(false);
                }
                if(set.Truck.valid){
                    _truck.switchUpperCamera(false);
                }
            }
        }

    }

}//end namespace Ocs.GameSystem

