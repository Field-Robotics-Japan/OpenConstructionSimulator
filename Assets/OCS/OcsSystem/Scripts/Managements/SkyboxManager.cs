using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.GameSystem
{
    public class SkyboxManager : MonoBehaviour
    {
        //�@��]�X�s�[�h
        [SerializeField]
        private float _rotateSpeed = 0.5f;
        //�@�X�J�C�{�b�N�X�̃}�e���A��
        private Material _skyboxMaterial;

        // Use this for initialization
        void Start()
        {
            //�@Lighting Settings�Ŏw�肵���X�J�C�{�b�N�X�̃}�e���A�����擾
            this._skyboxMaterial = RenderSettings.skybox;
        }

        // Update is called once per frame
        void Update()
        {
            //�@�X�J�C�{�b�N�X�}�e���A����Rotation�𑀍삵�Ċp�x��ω�������
            this._skyboxMaterial.SetFloat("_Rotation", Mathf.Repeat(this._skyboxMaterial.GetFloat("_Rotation") + this._rotateSpeed * Time.deltaTime, 360f));
        }

        private void OnDisable()
        {
            this._skyboxMaterial.SetFloat("_Rotation", 0);
        }
    }
}
