using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Ocs.GameSystem{
    public class SceneControl : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        //[SerializeField] private Color loadToColor = Color.black;
        //[SerializeField] private float fadeRate = 1.0f;

        public void SwitchScene()
        {
            //Initiate.Fade(_sceneName, loadToColor, fadeRate);
            SceneManager.LoadScene(this._sceneName);
        }

        public void StageTransition()
        {
            SceneManager.LoadScene(GameSetting.setting.stage);
        }

        public void QuitScene()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
        }
    }

}//end namespace Ocs.GameSystem
