using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavateLine : MonoBehaviour
{
    [SerializeField] private GameObject _bucketObj;
    [SerializeField] private float _lineWidth;

    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private TerrainManager _terrainManager;

    private GameObject[] _tips;
    private Vector3[] _lineEnd;
    private bool _isDeformable;

#if UNITY_EDITOR
    private DeformTools.TagHelper _tagHelper = new DeformTools.TagHelper();
#endif
    private string _terrainTagName = "Terrain";
    private string _bucketTagName = "Bucket";

    private void Awake()
    {
        this.gameObject.AddComponent<Rigidbody>();
        this.gameObject.AddComponent<CapsuleCollider>();

#if UNITY_EDITOR
        // Tag setting
        this._tagHelper.AddTag(this._bucketTagName);
#endif
        this.gameObject.tag = this._bucketTagName;
        this._bucketObj.tag = this._bucketTagName;
        Transform children = this._bucketObj.GetComponentInChildren<Transform>();
        if (children.childCount != 0)
        {
            foreach (Transform child in children)
            {
                child.tag = this._bucketTagName;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this._rigidbody = this.gameObject.GetComponent<Rigidbody>();
        this._rigidbody.isKinematic = true;
        this._collider = this.gameObject.GetComponent<CapsuleCollider>();
        this._collider.height = this._lineWidth;
        this._collider.isTrigger = true;
        this._collider.radius = 0.01f;
        this._collider.direction = 0;   // Z-axis

        this._terrainManager = GameObject.FindGameObjectWithTag(this._terrainTagName).GetComponent<TerrainManager>();
        this._isDeformable = false;

        this._tips = new GameObject[2] { new GameObject("tip1"), new GameObject("tip2") };
        this._tips[0].transform.parent = this.transform;
        this._tips[0].transform.localPosition = new Vector3(-this._lineWidth / 2, 0, 0);
        this._tips[1].transform.parent = this.transform;
        this._tips[1].transform.localPosition = new Vector3(this._lineWidth / 2, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (this._isDeformable)
        {
            Vector3[] targets = this.GetExcavateArea(this._tips[0].transform.position, this._tips[1].transform.position);
            this._terrainManager.ExcavateWithSand(targets);
        }

        if (this._tips[0].transform.position.y - 0.1f > this._terrainManager.FromTerrainPositionY(this._terrainManager.GetHeightmap(this._tips[0].transform.position)) &&
            this._tips[1].transform.position.y - 0.1f > this._terrainManager.FromTerrainPositionY(this._terrainManager.GetHeightmap(this._tips[1].transform.position)))
        {
            this._isDeformable = false;
        }
        else
        {
            this._isDeformable = true;
        }
    }

    private void OnDisable()
    {
        GameObject.Destroy(this.GetComponent<CapsuleCollider>());
    }

    private Vector3[] GetExcavateArea(Vector3 start, Vector3 end)
    {
        List<Vector3> DeformVerts = new List<Vector3>();
        int loopNum = 17;
        for (float i = 0.0f; i <= 1.0f; i += 1.0f / (float)loopNum)
        {
            DeformVerts.Add(i * start + (1 - i) * end);
        }
        return DeformVerts.ToArray();
    }
}
