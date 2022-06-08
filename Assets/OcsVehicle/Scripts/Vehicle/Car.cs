using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ocs.Vehicle.Equipment;

namespace Ocs.Vehicle
{
    public class Car : Vehicle
    {
        [Header("- DriveTrain Setting -")]
        [SerializeField] protected List<DriveTrain.Wheel> _driveTrains;
        [SerializeField] protected float _maxAccelTorque = 500.0f;
        [SerializeField] protected float _maxBrakeTorque = 1000.0f;
        [SerializeField] protected float _maxSteerAngle = 30.0f;

        public float MaxAccelTorque { get => _maxAccelTorque; }
        public float MaxBrakeTorque { get => _maxBrakeTorque; }
        public float MaxSteerAngle { get => _maxSteerAngle; }

        public float AccelInput { get; set; }
        public float BrakeInput { get; set; }
        public float SteerInput { get; set; }
        public bool ReverseGear { get; set; }

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


        protected virtual void Update()
        {
            vehicleStateUpdate();

            foreach(DriveTrain.Wheel driveTrain in this._driveTrains)
            {
                if(!ReverseGear) driveTrain.Drive(MaxAccelTorque * AccelInput, MaxBrakeTorque * BrakeInput);
                if(ReverseGear) driveTrain.Drive(MaxAccelTorque * -AccelInput, MaxBrakeTorque * BrakeInput);
                driveTrain.Steering(MaxSteerAngle * SteerInput);
                driveTrain.UpdateMeshTransform();
            }

            foreach(Equipment.Winker winker in this._winkers)
            {
                winker.Blinky(this._winkerFrequency, this._winkerIntensity);
            }
        }
    }
}
