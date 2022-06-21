using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnSand : MonoBehaviour
{
    public int _id;
    public float _radius;
    public bool _onTerrain;

    public SandManager _sandManager;

    private MeshCollider _mc;
    private Rigidbody _rb;

    private void Awake()
    {
        _mc = GetComponent<MeshCollider>();
        _rb = GetComponent<Rigidbody>();
        _rb.maxDepenetrationVelocity = float.MaxValue;
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        _mc.enabled = false;
    }

    private void OnEnable()
    {
        this.transform.localScale = new Vector3(2 * _radius * 100, 2 * _radius * 100, 2 * _radius * 100);
        _onTerrain = false;

        if(giveMassToNearObj(_radius + _sandManager._maxSandRadius))
        {
            //_sandManager.Dispose(_id);
            _mc.enabled = true;
        }
        else
        {
            _mc.enabled = true;
        }
    }

    private bool giveMassToNearObj(float threshold)
    {
        var targets = GameObject.FindGameObjectsWithTag(this.gameObject.tag);
        
        if (targets.Length == 1) return false;

        foreach (var target in targets)
        {
            if (target == this.gameObject) continue;
            var targetDistance = Vector3.Distance(transform.position, target.transform.position);
            if (targetDistance < threshold)
            {
                target.SendMessage("ReceiveMass", _rb.mass*0.5f);
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain") _onTerrain = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain") _onTerrain = false;
    }

    public void ReceiveMass(float mass)
    {
        float scale = Mathf.Pow((_rb.mass+mass) / _sandManager._sandDensity, 0.33f);
        if (scale > _sandManager._maxSandRadius * 2) scale = _sandManager._maxSandRadius*2;
        _rb.mass = scale * scale * scale * _sandManager._sandDensity;
        scale *= 100.0f;
        this.transform.localScale = new Vector3(scale, scale, scale);
    }
}
