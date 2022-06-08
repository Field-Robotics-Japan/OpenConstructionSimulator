using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ocs.GameSystem
{
    public class PanelChange : MonoBehaviour
    {
        public GameObject BasePanel;
        public GameObject NextPanel;

        void Start()
        {

        }

        public void SwitchingView()
        {
            BasePanel.SetActive(false);
            NextPanel.SetActive(true);
        }

    }
}
