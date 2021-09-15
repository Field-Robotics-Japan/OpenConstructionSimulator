using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    public class Backhoe : MonoBehaviour
    {
        [SerializeField] private ArticulationInfo _base;
        [SerializeField] private ArticulationInfo _boom;
        [SerializeField] private ArticulationInfo _arm;
        [SerializeField] private ArticulationInfo _end;

        private float _baseJointInput;
        private float _boomJointInput;
        private float _armJointInput;
        private float _endJointInput;

        public ArticulationInfo Base { get => _base; }
        public ArticulationInfo Boom { get => _boom; }
        public ArticulationInfo Arm { get => _arm; }
        public ArticulationInfo End { get => _end; }

        public float BaseJointInput { set => _baseJointInput = value; }
        public float BoomJointInput { set => _boomJointInput = value; }
        public float ArmJointInput { set => _armJointInput = -value; }
        public float EndJointInput { set => _endJointInput = -value; }

        private void Start()
        {
            this._base.Initialize();
            this._boom.Initialize();
            this._arm.Initialize();
            this._end.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            this._base.Rotate(this._baseJointInput);
            this._boom.Rotate(this._boomJointInput);
            this._arm.Rotate(this._armJointInput);
            this._end.Rotate(this._endJointInput);
        }
    }
}
