using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class PanelChange : MonoBehaviour
{
    public GameObject PlayingPanel;
    public GameObject SettingPanel;
 
    void Start()
    {
        PlayingPanel.SetActive(true);
        SettingPanel.SetActive(false);
    }
 
    public void PlayingView()
    {
        PlayingPanel.SetActive(true);
        SettingPanel.SetActive(false);
    }
 
    public void SettingView()
    {
        PlayingPanel.SetActive(false);
        SettingPanel.SetActive(true);
    }
}