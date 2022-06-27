using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.DriveTrain
{
    [System.Serializable]
    public class CylinderDrivenJoint_Physics : MonoBehaviour
    {
        enum Axis
        {
            X,
            Y,
            Z
        }

        [Header("Parameters")]

        [SerializeField]
        private float _power;

        [SerializeField]
        private Axis axis;

        [SerializeField]
        private float _minVelocity;

        [SerializeField]
        private float _minAngle = 0.0f;

        [SerializeField]
        private float _maxAngle = 90.0f;

        [SerializeField]
        private bool _inverse_motor;

        [SerializeField]
        private bool _inverse_sensor;

        [SerializeField]
        private bool _useLimitsForRotation = false;

        [Header("Informations(No need to input)")]
        [SerializeField]
        private HingeJoint _joint;

        private JointMotor _motor;
        private JointLimits _limit;

        [SerializeField]
        private Transform _this_transform;

        [SerializeField]
        private Transform _target_transform;

        [SerializeField]
        private bool _use_angle_offset;

        [SerializeField]
        private float _angle_offset;

        [SerializeField]
        private float _angle_now;

        [SerializeField]
        private float _minAngle_now;

        [SerializeField]
        private float _maxAngle_now;

#if UNITY_EDITOR
        [Header("Debug")]

        [SerializeField]
        bool _debug_switch = false;

        [SerializeField]
        float _debug_speed = 0.0f;
#endif

        private void Awake()
        {
            _joint = GetComponent<HingeJoint>();

            _joint.useMotor = true;
            _joint.useLimits = true;

            _this_transform = this.transform;
            _target_transform = _joint.connectedBody.transform;
            return;
        }

        private void Start()
        {
            if (_useLimitsForRotation)
            {
                _minAngle_now = 0.0f;
                _maxAngle_now = 0.0f;
                SetLimit(ref _minAngle_now, ref _maxAngle_now);
            }
            else
            {
                SetLimit(ref _minAngle, ref _maxAngle);
            }

            _motor.force = _power;
            _motor.targetVelocity = 0.0f;
            _joint.motor = _motor;

            _angle_offset = CalcAngle();
        }

        public void Init()
        {
            return;
        }

        public void RotateJoint(float speed)
        {
            if (Mathf.Abs(speed) < _minVelocity) speed = 0.0f;
            if (_inverse_motor) speed = -speed;

            _angle_now = CalcAngle() - (_use_angle_offset?_angle_offset:0.0f);
            if (!_inverse_sensor) _angle_now = -_angle_now;
            _angle_now = Mathf.Clamp(_angle_now, _minAngle, _maxAngle);

            _motor = _joint.motor;
            if (_useLimitsForRotation)
            {
                if (speed > 0)
                {
                    _maxAngle_now += _maxAngle_now - 1.0f < _angle_now ? 1.0f : 0.0f;
                    _minAngle_now = _angle_now - 1.0f;
                }
                else if (speed < 0)
                {
                    _minAngle_now -= _minAngle_now + 1.0f > _angle_now ? 1.0f : 0.0f;
                    _maxAngle_now = _angle_now + 1.0f;
                }
                SetLimit(ref _minAngle_now, ref _maxAngle_now);
            }
            _motor.targetVelocity = speed;
            _joint.motor = _motor;
        }
        private float CalcAngle()
        {
            Vector3 axisVec;
            switch (axis)
            {
                case Axis.X:
                    axisVec = _this_transform.right;
                    break;
                case Axis.Y:
                    axisVec = _this_transform.up;
                    break;
                case Axis.Z:
                    axisVec = _this_transform.forward;
                    break;
                default:
                    axisVec = Vector3.zero;
                    break;
            }
            float angle = Vector3.SignedAngle(_this_transform.forward, _target_transform.forward, axisVec);
            return angle;
        }

        private void SetLimit(ref float minAngle, ref float maxAngle)
        {
            if (_useLimitsForRotation)
            {
                minAngle = Mathf.Clamp(minAngle, _minAngle, _maxAngle);
                maxAngle = Mathf.Clamp(maxAngle, _minAngle, _maxAngle);
            }
            _limit.min = minAngle;
            _limit.max = maxAngle;
            _joint.limits = _limit;
        }


#if UNITY_EDITOR
        private void Update()

        {
            if (_debug_switch)
            {
                RotateJoint(_debug_speed);
            }
        }
#endif
    }
}
