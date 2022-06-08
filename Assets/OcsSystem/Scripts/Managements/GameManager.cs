using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.GameSystem
{
    public class GameManager : MonoBehaviour
    {
        private Ocs.Input.InputActions _input;

        private void Awake() => this._input = new Ocs.Input.InputActions();
        private void OnEnable() => this._input.Enable();
        private void OnDisable() => this._input.Disable();
        private void OnDestroy() => this._input.Dispose();
        private void Start() => this._input.GameManager.QuitGame.started += context => QuitGame();

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        }
    }
}
