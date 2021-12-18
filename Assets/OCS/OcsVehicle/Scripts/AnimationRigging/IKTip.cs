using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTip : MonoBehaviour
{
    public Transform target;
    
    // Update is called once per frame
    void Update()
    {
        target.position = this.transform.position;
    }
}
