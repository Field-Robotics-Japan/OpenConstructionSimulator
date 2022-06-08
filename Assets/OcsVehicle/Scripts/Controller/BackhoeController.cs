using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Unity.Robotics.ROSTCPConnector;
using Float64 = RosMessageTypes.Std.Float64Msg;

namespace Ocs.Vehicle.Controller
{
    public class BackhoeController : MonoBehaviour
    {
        [SerializeField] private Backhoe _vehicle;
        private Ocs.Input.VehicleInput _input;

        [Header("- Topic Name -")]
        [SerializeField] private string leftCrawler_topic = "backHoe/leftCrawler";
        [SerializeField] private string rightCrawler_topic = "backHoe/rightCrawler";
        [SerializeField] private string base_topic = "backHoe/wheel";
        [SerializeField] private string boom_topic = "backHoe/boom";
        [SerializeField] private string arm_topic = "backHoe/arm";
        [SerializeField] private string end_topic = "backHoe/end";
        private float leftCrawler_input, rightCrawler_input, base_input, boom_input, arm_input, end_input;


        private void Awake()
        {
            this._input = new Ocs.Input.VehicleInput();
        }

        private void OnEnable() => this._input.Enable();
        private void OnDestroy() => this._input.Dispose();

        private void OnDisable()
        {
            this._input.Disable();
        }

        private void Start()
        {
            // Callback
            this._input.Crawler.LeftReverse.started += context =>{
                if(_vehicle.ownership){
                    this._vehicle.LeftReverse = !this._vehicle.LeftReverse;
                }
            };
            this._input.Crawler.RightReverse.started += context =>{
                if(_vehicle.ownership){
                    this._vehicle.RightReverse = !this._vehicle.RightReverse;
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
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>(this.leftCrawler_topic, leftCrawler_callback);
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>(this.rightCrawler_topic, rightCrawler_callback);
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>(this.base_topic, base_callback);
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>(this.boom_topic, boom_callback);
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>(this.arm_topic, arm_callback);
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>(this.end_topic, end_callback);
        }

        void Update()
        {
            if(this._vehicle.automation){ //ros
                //left crawler
                    if(leftCrawler_input >= 0){
                        this._vehicle.LeftReverse = true;
                        this._vehicle.LeftCrawlerInput = leftCrawler_input;
                    }else if(leftCrawler_input < 0){
                        this._vehicle.LeftReverse = false;
                        this._vehicle.LeftCrawlerInput = System.Math.Abs(leftCrawler_input);
                    }
                    //right crawler
                    if(rightCrawler_input >= 0){
                        this._vehicle.RightReverse = true;
                        this._vehicle.RightCrawlerInput = rightCrawler_input;
                    }else if(rightCrawler_input < 0){
                        this._vehicle.RightReverse = false;
                        this._vehicle.RightCrawlerInput = System.Math.Abs(rightCrawler_input);
                    }
                    //base
                    this._vehicle.BaseInput = base_input;
                    //boom
                    this._vehicle.BoomInput = boom_input;
                    //arm
                    this._vehicle.ArmInput = arm_input;
                    //end
                    this._vehicle.EndInput = end_input;
            }else{ //controller
                if(this._vehicle.ownership){
                    this._vehicle.LeftCrawlerInput = this._input.Crawler.LeftForward.ReadValue<float>();
                    this._vehicle.RightCrawlerInput = this._input.Crawler.RightForward.ReadValue<float>();
                    this._vehicle.BaseInput = this._input.Backhoe.Lever0.ReadValue<Vector2>()[0];
                    this._vehicle.BoomInput = -this._input.Backhoe.Lever1.ReadValue<Vector2>()[1];
                    this._vehicle.ArmInput = this._input.Backhoe.Lever0.ReadValue<Vector2>()[1];
                    this._vehicle.EndInput = this._input.Backhoe.Lever1.ReadValue<Vector2>()[0];
                }
            }        
        }

        void leftCrawler_callback(Float64 message)
        {
            leftCrawler_input = (float)message.data;
        }

        void rightCrawler_callback(Float64 message)
        {
            rightCrawler_input = (float)message.data;
        }

        void base_callback(Float64 message)
        {
            base_input = (float)message.data;
        }

        void boom_callback(Float64 message)
        {
            boom_input = (float)message.data;
        }

        void arm_callback(Float64 message)
        {
            arm_input = (float)message.data;
        }

        void end_callback(Float64 message)
        {
            end_input = (float)message.data;
        }
    }
}