using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DeformableSand : MonoBehaviour
{
    [SerializeField] private float _sleepThreshold = 10f;


#if UNITY_EDITOR
    private DeformTools.TagHelper _tagHelper = new DeformTools.TagHelper();
#endif

    private string _terrainTagName = "Terrain";
    private string _bucketTagName = "Bucket";
    private string _activeTagName = "ActiveSand";
    private string _inactiveTagName = "InactiveSand";

    private Rigidbody _rigidBody;
    private GameObject _targetTerrain;
    private bool _isDeformable;

    private void Awake()
    {
#if UNITY_EDITOR
        // Tag setting
        this._tagHelper.AddTag(this._activeTagName);
        this._tagHelper.AddTag(this._inactiveTagName);
#endif
        this.gameObject.tag = this._activeTagName;
    }

    // Start is called before the first frame update
    private void Start()
    {
        this._rigidBody = this.GetComponent<Rigidbody>();
        this._rigidBody.sleepThreshold = this._sleepThreshold;
        this._targetTerrain = GameObject.FindGameObjectWithTag(this._terrainTagName);
        this._isDeformable = true;
    }

    private void Update()
    {
        if (this._rigidBody.IsSleeping() && this._isDeformable)
        {
            this._targetTerrain.GetComponent<TerrainManager>().AddDestroyObj(this.gameObject);
        }

        if (this.transform.position.y < -5.0f)
        {
            this._isDeformable = false;
            this._targetTerrain.GetComponent<TerrainManager>().AddDestroyObj(this.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == this._terrainTagName)
        {
            this.gameObject.tag = this._inactiveTagName;
            this._isDeformable = true;
        }
        if (collision.gameObject.tag == this._inactiveTagName)
        {
            this.gameObject.tag = this._inactiveTagName;
            this._isDeformable = true;
        }
        if (collision.gameObject.tag == this._bucketTagName)
        {
            this.gameObject.tag = this._activeTagName;
            this._isDeformable = false;
        }
        if (collision.gameObject.tag == this._activeTagName)
        {
            this.gameObject.tag = this._activeTagName;
            this._isDeformable = false;
        }
    }
}
