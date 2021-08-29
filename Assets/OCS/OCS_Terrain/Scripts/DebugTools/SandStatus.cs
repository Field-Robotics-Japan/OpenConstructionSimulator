using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandStatus : MonoBehaviour
{
    [SerializeField] private Material _activeColor;
    [SerializeField] private Material _inactiveColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "ActiveGrain") this.GetComponent<Renderer>().material.color = this._activeColor.color;
        else if (this.tag == "InActiveGrain") this.GetComponent<Renderer>().material.color = this._inactiveColor.color;
    }
}
