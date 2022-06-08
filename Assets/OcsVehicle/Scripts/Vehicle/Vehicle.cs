using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle
{
    public class Vehicle : MonoBehaviour
    {
        [Header("- camera -")]
        [SerializeField] protected Camera _MainCamera;
        [SerializeField] protected Camera _upperCamera;

        public bool ownership{ get; private set; }
        public bool automation{ get; private set; }
        public bool aroundview{ get; private set; }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void vehicleStateUpdate()
        {
            if(this.ownership){
                //get ModeManeger(UI) from hierarchy
                foreach(GameSystem.ModeManeger obj in UnityEngine.Object.FindObjectsOfType(typeof(GameSystem.ModeManeger)))
                {
                    this.automation = obj.Automation;
                    this.aroundview = obj.AroundView;
                }
                
                switchUpperCamera(this.aroundview);
            }
        }

        public void switchUpperCamera(bool mode)
        {
            _upperCamera.enabled = mode;
        }
        public void setOwner(bool mode)
        {
            this.ownership = mode;
            _upperCamera.enabled = false;
            _MainCamera.enabled = mode;
        }
    }
}