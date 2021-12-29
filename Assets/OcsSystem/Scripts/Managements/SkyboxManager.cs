using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.GameSystem
{
    public class SkyboxManager : MonoBehaviour
    {
        //　回転スピード
        [SerializeField]
        private float _rotateSpeed = 0.5f;
        //　スカイボックスのマテリアル
        private Material _skyboxMaterial;

        // Use this for initialization
        void Start()
        {
            //　Lighting Settingsで指定したスカイボックスのマテリアルを取得
            this._skyboxMaterial = RenderSettings.skybox;
        }

        // Update is called once per frame
        void Update()
        {
            //　スカイボックスマテリアルのRotationを操作して角度を変化させる
            this._skyboxMaterial.SetFloat("_Rotation", Mathf.Repeat(this._skyboxMaterial.GetFloat("_Rotation") + this._rotateSpeed * Time.deltaTime, 360f));
        }

        private void OnDisable()
        {
            this._skyboxMaterial.SetFloat("_Rotation", 0);
        }
    }
}
