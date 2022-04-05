using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ModeManeger : MonoBehaviour
{

    [SerializeField] private Toggle automation_toggle;
    //[SerializeField] private Toggle aroundView_toggle;

    public bool Automation{get;set;}
    public bool AroundView{get;set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Automation = automation_toggle.isOn;
        //AroundView = aroundView_toggle.isOn;
    }
}
