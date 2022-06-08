using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManeger : MonoBehaviour
{
    public GameObject stagePanel;

    // Start is called before the first frame update
    void Start()
    {
        stagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stageSelect()
    {
        stagePanel.SetActive(true);
    }
}
