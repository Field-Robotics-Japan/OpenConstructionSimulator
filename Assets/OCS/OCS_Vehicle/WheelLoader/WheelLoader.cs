using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [RequireComponent(typeof(Car))]
    public class WheelLoader : MonoBehaviour
    {
        [SerializeField] private HingeJoint _steerJoint;
        [SerializeField] private Transform _bucketLink;
        [SerializeField] private Transform _bucket;
        [SerializeField] private float _controlSpeed;

        private float _heightInput;
        private float _rotateInput;

        JointSpring spring;

        public float SteerInput { set => spring.targetPosition = value; }
        public float HeightInput { set => _heightInput = value * _controlSpeed; }
        public float RotateInput { set => _rotateInput = value * _controlSpeed; }

        private void Start()
        {
            spring = this._steerJoint.spring;
        }

        private void Update()
        {
            this._steerJoint.spring = spring;
            Height(this._heightInput, this._bucketLink, this._bucket);
            Pitch(this._rotateInput, this._bucket);
        }

        private void Height(float input, Transform joint1, Transform joint2)
        {
            joint1.Rotate(Vector3.forward * input);
            joint2.Rotate(Vector3.forward * -input);
        }

        private void Pitch(float input, Transform joint)
        {
            joint.Rotate(Vector3.forward * input);
        }
    }
}
