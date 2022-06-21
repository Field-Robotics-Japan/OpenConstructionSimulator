using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ocs.Vehicle.DriveTrain;
using Ocs.Vehicle.Equipment;

namespace Ocs.Vehicle
{
    public class Backhoe : Crawler
    {
        [Header("- Equipments Setting -")]
        [SerializeField] protected List<HeadLight> _lights;
        [SerializeField] protected float _headLightIntensity = 3.0f;

        [SerializeField] protected List<Winker> _winkers;
        [SerializeField] protected float _winkerFrequency = 5.0f;
        [SerializeField] protected float _winkerIntensity = 3.0f;

        [SerializeField] protected Hone _hone;

        public float HeadLightIntensity { get => _headLightIntensity; }
        public float WinkerFrequency { get => _winkerFrequency; }
        public float WinkerIntensity { get => _winkerIntensity; }

        [Header("- Arm Setting -")]
        [SerializeField] private CylinderDrivenJoint_Physics _base;
        [SerializeField] private CylinderDrivenJoint_Physics _boom;
        [SerializeField] private CylinderDrivenJoint_Physics _arm;
        [SerializeField] private CylinderDrivenJoint_Physics _end;
        [SerializeField] private float _controlSpeed_base;
        [SerializeField] private float _controlSpeed_boom;
        [SerializeField] private float _controlSpeed_arm;
        [SerializeField] private float _controlSpeed_end;

        public float BaseInput { get; set; }
        public float BoomInput { get; set; }
        public float ArmInput { get; set; }
        public float EndInput { get; set; }

        public void SwitchLight()
        {
            foreach (HeadLight light in this._lights)
            {
                light.SwitchLight();
            }
        }
        public void SwitchLeftWinker()
        {
            foreach (Winker winker in this._winkers)
            {
                winker.SwitchLeftWinker();
            }
        }
        public void SwitchRightWinker()
        {
            foreach (Winker winker in this._winkers)
            {
                winker.SwitchRightWinker();
            }
        }
        public void PlayHone()
        {
            _hone.Play();
        }


        private void Start()
        {
            this._boom.Init();
            this._arm.Init();
            this._end.Init();
        }

        
        protected override void Update()
        {
            base.Update();
            this._base.RotateJoint(BaseInput * this._controlSpeed_base);
            this._boom.RotateJoint(BoomInput * this._controlSpeed_boom);
            this._arm.RotateJoint(ArmInput * this._controlSpeed_arm);
            this._end.RotateJoint(EndInput * this._controlSpeed_end);
        }
    }
}
