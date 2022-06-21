using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Ocs.GameSystem{
    public class SceneControl : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        [Header("-No need to input-")]
        [SerializeField] private DeformableTerrain _terrain;

        //[SerializeField] private Color loadToColor = Color.black;
        //[SerializeField] private float fadeRate = 1.0f;

        private void Awake()
        {
            _terrain = GameObject.FindObjectOfType<DeformableTerrain>();
        }

        public void SwitchScene()
        {
            if(_terrain)
                _terrain.RestoreTerrain();
            //Initiate.Fade(_sceneName, loadToColor, fadeRate);
            SceneManager.LoadScene(this._sceneName);
        }

        public void StageTransition()
        {
            if(_terrain)
                _terrain.RestoreTerrain();
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
