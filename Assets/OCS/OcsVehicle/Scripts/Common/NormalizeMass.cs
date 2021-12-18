using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizeMass : MonoBehaviour
{
    //private void Apply(Transform root)
    //{
    //    var j = root.GetComponent<Joint>();

    //    // Apply the inertia scaling if possible
    //    if (j & amp; &amp; j.connectedBody)
    //    {
    //        // Make sure that both of the connected bodies will be moved by the solver with equal speed
    //        j.massScale = j.connectedBody.mass / root.GetComponent<Rigidbody>().mass;
    //        j.connectedMassScale = 1f;
    //    }

    //    // Continue for all children...
    //    for (int childId = 0; childId < root.childCount; ++childId)
    //    {
    //        Apply(root.GetChild(childId));
    //    }
    //}

    //public void Start()
    //{
    //    Apply(this.transform);
    //}
}
