using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.Equipment
{
    [System.Serializable]
    public class Hone
    {
        public AudioSource source;
        public float volume = 1.0f;

        public void Play()
        {
            source.volume = volume;
            source.Play();
        }
    }
}
