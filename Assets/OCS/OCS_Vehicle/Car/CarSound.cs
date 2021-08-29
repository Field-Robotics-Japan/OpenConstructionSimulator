using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [RequireComponent(typeof(Car))]
    public class CarSound : MonoBehaviour
    {
        [SerializeField] private Car _car;    
        [SerializeField] private AudioSource _engineSound;
        [SerializeField] private float _idleVolume;


        // Start is called before the first frame update
        void Start()
        {
            this._engineSound.Play();
        }

        // Update is called once per frame
        void Update()
        {
            this._engineSound.volume = this._idleVolume;
            this._engineSound.pitch = 0.5f + this._car.AccelInput / 2;
        }
    }
}
