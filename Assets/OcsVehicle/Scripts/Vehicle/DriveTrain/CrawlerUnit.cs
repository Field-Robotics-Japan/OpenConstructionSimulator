using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.DriveTrain
{
    public class CrawlerUnit : MonoBehaviour
    {
        [SerializeField] private List<HingeJoint> _colliderJoints;
        [SerializeField] private float _motorTorque = Mathf.Infinity;
        [SerializeField] private MeshRenderer _mesh;
        [SerializeField] private float _animationSpeedGain = 0.001f;

        JointMotor motor;

        private void Start()
        {
            if (this._colliderJoints.Count == 0) return;
            motor = this._colliderJoints[0].motor;
            motor.force = this._motorTorque;

            Drive(0);
        }

        public void Drive(float velocity)
        {
            this._mesh.material.SetFloat("_ScrollY", -this._animationSpeedGain * velocity);
            motor.targetVelocity = velocity;
            foreach (HingeJoint joint in this._colliderJoints)
            {
                joint.motor = motor;
            }
        }
    }
}
