using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandManager : MonoBehaviour
{

    [System.Serializable] struct Sand
    {
        public Rigidbody rb;
        public SpawnSand sandScript;
        public bool isActivated;
        public float time;
    }

    [Header("Parameters")]
    [SerializeField] private float _updateInterval = 0.1f;
    [SerializeField] public float _minSandRadius = 0.1f;
    [SerializeField] public float _maxSandRadius = 0.3f;
    [SerializeField] public float _sandDensity = 100.0f;
    [SerializeField] private GameObject _sandPrefab;
    [SerializeField] private int _maxSandCount;
    [SerializeField] private float _minAltitude = 0.0f;

    [SerializeField] private LayerMask _sandLayer;

    [Header("Informations(No need to input)")]
    [SerializeField] private DeformableTerrain _deformableTerrain;
    [SerializeField] private int _sandCount;
    [SerializeField] Sand[] _sand;

    private float _time_old;

    private void Awake()
    {
        _deformableTerrain = Object.FindObjectOfType<DeformableTerrain>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SandInit();
        _time_old = Time.time;
    }

    private void SandInit()
    {
        _sand = new Sand[_maxSandCount];
        for (int i = 0; i < _maxSandCount; i++)
        {
            var newSand = GameObject.Instantiate(_sandPrefab, Vector3.zero, Quaternion.identity);
            
            newSand.transform.parent = this.gameObject.transform;
            newSand.SetActive(false);
            _sand[i].rb = newSand.GetComponent<Rigidbody>();
            _sand[i].sandScript = newSand.GetComponent<SpawnSand>();
            _sand[i].isActivated = false;
            _sand[i].sandScript._id = i;
            _sand[i].sandScript._sandManager = this;
        }
        _sandCount = 0;
    }

    public bool Spawn(Vector3 pos)
    {
        if (_sandCount >= _maxSandCount) return false;
        float radius = Random.Range(_minSandRadius, _maxSandRadius);
        return Spawn(pos, radius);
    }

    public bool Spawn(Vector3 pos, float radius)
    {
        if (_sandCount >= _maxSandCount) return false;

        for (int i = 0; i < _maxSandCount; i++)
        {
            if (!_sand[i].isActivated)
            {
                _sand[i].rb.gameObject.transform.position = pos;
                _sand[i].rb.mass = radius * radius * radius * _sandDensity;
                _sand[i].rb.maxAngularVelocity = 1.0f;
                _sand[i].isActivated = true;
                _sand[i].time = Time.time;
                _sand[i].sandScript._radius = radius;
                _sand[i].rb.gameObject.SetActive(true);
                _sandCount++;
                return true;
            }
        }

        return false;
    }

    public bool Dispose(int i)
    {
        if (!_sand[i].isActivated) return false;
        _sand[i].rb.gameObject.SetActive(false);
        _sand[i].isActivated = false;
        _sandCount--;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _time_old < _updateInterval) return;

        bool terrainUpdate = false;

        for(int i = 0;i < _maxSandCount; i++)
        {
            if (!_sand[i].isActivated) continue;
            if (_sand[i].rb.transform.position.y < _minAltitude)
            {
                _sand[i].rb.velocity = Vector3.zero;
                Dispose(i);
                continue;
            }
            if (_sand[i].rb.IsSleeping() && _sand[i].sandScript._onTerrain)
            {
                if (Time.time - _sand[i].time < 5.0f) continue;
                _sand[i].time = Time.time;
                RaycastHit hit;
                if (Physics.Raycast(_sand[i].rb.transform.position + Vector3.up*_deformableTerrain._terrainSize.y, Vector3.down, out hit, transform.position.y + _deformableTerrain._terrainSize.y + _maxSandRadius, ~_sandLayer))
                {
                    if(hit.collider.tag == "Terrain")
                    {
                        _deformableTerrain.SetHeight(_sand[i].rb.transform.position, _sand[i].rb.transform.position.y);
                        Dispose(i);
                        terrainUpdate = true;
                    }
                }
            }
            else
            {
                _sand[i].time = Time.time;
            }
        }

        if (terrainUpdate)
        {
            _deformableTerrain.OnHeightmapChanged();
        }

        /*
        for(int i = 0; i < 50; i++)
        {
            Spawn(new Vector3(50.0f*Random.value, 10.0f, 50.0f * Random.value));
        }
        */

        _time_old = Time.time;
    }
}
