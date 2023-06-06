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
        [SerializeField] private Transform _base;
        [SerializeField] private CylinderDrivenJoint _boom;
        [SerializeField] private CylinderDrivenJoint _arm;
        [SerializeField] private CylinderDrivenJoint _end;
        [SerializeField] private float _controlSpeed;

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
            this._base.Rotate(Vector3.up * BaseInput * this._controlSpeed);
            this._boom.RotateJoint(BoomInput * this._controlSpeed);
            this._arm.RotateJoint(ArmInput * this._controlSpeed);
            this._end.RotateJoint(EndInput * this._controlSpeed);
        }
    }
}
