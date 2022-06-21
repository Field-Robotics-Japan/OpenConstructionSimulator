using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Unity.Robotics.ROSTCPConnector;
using Float64 = RosMessageTypes.Std.Float64Msg;

namespace Ocs.Vehicle.Controller
{
    public class WheelLoaderController : MonoBehaviour
    {
        [SerializeField] private WheelLoader _vehicle;
        private Ocs.Input.VehicleInput _input;
        [Header("- Topic Name -")]
        [SerializeField] private string wheelDrive_topic = "wheelLoader/wheel";
        [SerializeField] private string steer_topic = "wheelLoader/steer";
        [SerializeField] private string boom_topic = "wheelLoader/boom";
        [SerializeField] private string bucket_topic = "wheelLoader/bucket";
        private float wheel_input, steer_input, boom_input, bucket_input;

#if UNITY_EDITOR
        [Header("- Debug -")]
        [SerializeField] private bool _debug_forceManualMode = false;
#endif


        private void Awake()
        {
            this._input = new Ocs.Input.VehicleInput();
        }

        private void Start()
        {
            // Callback
            this._input.Car.ShiftUp.started += context =>{
                if(_vehicle.ownership){
                    this._vehicle.ReverseGear = !this._vehicle.ReverseGear;
                }
            };
            //this._input.Car.ShiftDown.started += context => this._vehicle.ReverseGear = true;
            this._input.Equipment.Light.started += context =>{
                if(_vehicle.ownership){
                    this._vehicle.SwitchLight();
                }
                
            };
            this._input.Equipment.Hone.started += context =>{
                if(_vehicle.ownership){
                    this._vehicle.PlayHone();
                }
                
            };
            this._input.Equipment.LeftWinker.started += context =>{
                if(_vehicle.ownership){
                    this._vehicle.SwitchLeftWinker();
                }
            };
            this._input.Equipment.RightWinker.started += context =>{
                if(_vehicle.ownership){
                    this._vehicle.SwitchRightWinker();
                }
            };

            //ros
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>(this.wheelDrive_topic, wheel_callback);
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>(this.steer_topic, steer_callback);
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>(this.boom_topic, boom_callback);
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>(this.bucket_topic, bucket_callback);

        }

        private void OnEnable() => this._input.Enable();
        private void OnDestroy() => this._input.Dispose();

        private void OnDisable()
        {
            this._input.Disable();
            this._vehicle.AccelInput = 0.0f;
            this._vehicle.BrakeInput = 1.0f;
            this._vehicle.SteerInput = 0.0f;
        }

        void Update()
        {
#if UNITY_EDITOR
            if (_debug_forceManualMode)
            {
                this._vehicle.AccelInput = this._input.Car.Accel.ReadValue<float>();
                this._vehicle.BrakeInput = this._input.Car.Brake.ReadValue<float>();
                this._vehicle.SteerInput = this._input.Car.Steering.ReadValue<Vector2>()[0];
                this._vehicle.BoomInput = -this._input.Backhoe.Lever1.ReadValue<Vector2>()[1];
                this._vehicle.BucketInput = this._input.Backhoe.Lever1.ReadValue<Vector2>()[0];
                return;
            }
#endif
            if (_vehicle.automation){ //ros
                //acceel, brake 
                if(wheel_input >= 0){
                    this._vehicle.AccelInput = wheel_input;
                    this._vehicle.BrakeInput = 0;
                }else if(wheel_input < 0){
                    this._vehicle.AccelInput = 0;
                    this._vehicle.BrakeInput = System.Math.Abs(wheel_input);
                }
                //steer
                this._vehicle.SteerInput = steer_input;
                //boom
                this._vehicle.BoomInput = boom_input;
                //bucket
                this._vehicle.BucketInput = bucket_input; 
            }else{ //controller
                if(_vehicle.ownership){
                    this._vehicle.AccelInput = this._input.Car.Accel.ReadValue<float>();
                    this._vehicle.BrakeInput = this._input.Car.Brake.ReadValue<float>();
                    this._vehicle.SteerInput = this._input.Car.Steering.ReadValue<Vector2>()[0];
                    this._vehicle.BoomInput = -this._input.Backhoe.Lever1.ReadValue<Vector2>()[1];
                    this._vehicle.BucketInput = this._input.Backhoe.Lever1.ReadValue<Vector2>()[0];
                }
                
            }
        }

        void wheel_callback(Float64 wheel_message)
        {
            wheel_input = (float)wheel_message.data;
        }

        void steer_callback(Float64 steer_message)
        {
            steer_input = (float)steer_message.data;
        }

        void boom_callback(Float64 boom_message)
        {
            boom_input = (float)boom_message.data;
        }

        void bucket_callback(Float64 bucket_message)
        {
            bucket_input = (float)bucket_message.data;
        }
    }
}
