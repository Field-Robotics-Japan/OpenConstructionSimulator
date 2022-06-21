using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
[RequireComponent(typeof(TerrainCollider))]
public class DeformableTerrain : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Terrain _terrain;

    [SerializeField] private float _terrainDepth;

    [Header("Informations(No need to input)")]
    
    [SerializeField] public Vector3 _offset;

    [SerializeField] private SandManager _sandManager;

    [SerializeField] private TerrainData _terrainData;

    [SerializeField] public Vector3 _terrainSize;

    [SerializeField] private Vector3 _dimensionRatio;

    [SerializeField] private int _terrainHeightmapResolution;

    [SerializeField] private bool _isHeightmapChanged;

    [Header("Debug")]
    [SerializeField] private bool _noDeform;

    private float[,] _heightmap;
    private float[,] _originalHeightmap;

    private void Awake()
    {
        _sandManager = UnityEngine.Object.FindObjectOfType<SandManager>();

        _terrain = GetComponent<Terrain>();
        _terrainData = _terrain.terrainData;
        _terrainSize = _terrain.terrainData.size;
        _terrainHeightmapResolution = this._terrain.terrainData.heightmapResolution;
        _heightmap = _terrain.terrainData.GetHeights(0, 0, _terrainHeightmapResolution, _terrainHeightmapResolution);
        _originalHeightmap = _terrain.terrainData.GetHeights(0, 0, _terrainHeightmapResolution, _terrainHeightmapResolution);
        _dimensionRatio = new Vector3(_terrainHeightmapResolution / _terrainSize.x,
                                           1 / _terrainSize.y,
                                           _terrainHeightmapResolution / _terrainSize.z);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        SetOffset(_terrainDepth);
        _offset = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    private void OnEnable()
    {
        StartCoroutine(this.UpdateTerrainCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsValidIndex(int z, int x)
    {
        if (z < 0 || z >= _heightmap.GetLength(0) || x < 0 || x >= _heightmap.GetLength(1)) return false;
        return true;
    }

    private void SetOffset(float offset)
    {
        for (int i = 0; i < this._terrainHeightmapResolution; i++)
        {
            for (int j = 0; j < this._terrainHeightmapResolution; j++)
            {
                _heightmap[j, i] += offset * _dimensionRatio.y;
            }
        }
        _terrain.terrainData.SetHeightsDelayLOD(0, 0, _heightmap);

        Vector3 pos = this.transform.position;
        pos.y -= _terrainDepth;
        this.transform.position = pos;
    }

    public float GetHeight(Vector3 pos)
    {
        pos -= _offset;
        int pos_z = (int)(pos.z * (_terrainHeightmapResolution - 1) / _terrainSize.z);
        int pos_x = (int)(pos.x * (_terrainHeightmapResolution - 1) / _terrainSize.x);
        return GetHeight(pos_z, pos_x);
    }

    public float GetHeight(int z, int x)
    {
        return IsValidIndex(z, x) ? _heightmap[z, x]*_terrainSize.y : 0.0f;
    }

    public void SetHeight(Vector3 pos, float height)
    {
        if (_noDeform) return;

        pos -= _offset;

        int pos_z = (int)(pos.z * (_terrainHeightmapResolution - 1) / _terrainSize.z);
        int pos_x = (int)(pos.x * (_terrainHeightmapResolution - 1) / _terrainSize.x);
        SetHeight(pos_z, pos_x, height);
    }

    public void SetHeight(Vector3 pos_start, Vector3 pos_end, float height)
    {
        if (_noDeform) return;

        pos_start -= _offset;
        pos_end -= _offset;

        int pos_z_start, pos_z_end;
        if (pos_start.z < pos_end.z)
        {
            pos_z_start = (int)(pos_start.z * (_terrainHeightmapResolution - 1) / _terrainSize.z);
            pos_z_end = (int)(pos_end.z * (_terrainHeightmapResolution - 1) / _terrainSize.z);
        }
        else
        {
            pos_z_end = (int)(pos_start.z * (_terrainHeightmapResolution - 1) / _terrainSize.z);
            pos_z_start = (int)(pos_end.z * (_terrainHeightmapResolution - 1) / _terrainSize.z);
        }

        int pos_x_start, pos_x_end;
        if (pos_start.x < pos_end.x)
        {
            pos_x_start = (int)(pos_start.x * (_terrainHeightmapResolution - 1) / _terrainSize.x);
            pos_x_end = (int)(pos_end.x * (_terrainHeightmapResolution - 1) / _terrainSize.x);
        }
        else
        {
            pos_x_end = (int)(pos_start.x * (_terrainHeightmapResolution - 1) / _terrainSize.x);
            pos_x_start = (int)(pos_end.x * (_terrainHeightmapResolution - 1) / _terrainSize.x);
        }

        for (int i = pos_z_start; i <= pos_z_end; i++)
            for (int j = pos_x_start; j <= pos_x_end; j++)
                SetHeight(i, j, height);
    }

    private void SetHeight(int z, int x, float height)
    {
        height -= _offset.y;
        float h = height / _terrainSize.y;
        for (int i = -1; i < 2; i++)
            for (int j = -1; j < 2; j++)
                if (IsValidIndex(z + i, x + j))
                    _heightmap[z + i, x + j] = h;
    }

    private IEnumerator UpdateTerrainCoroutine()
    {
        for (; ; )
        {
            if (_isHeightmapChanged)
            {
                _terrain.terrainData.SetHeightsDelayLOD(0, 0, _heightmap);
                _isHeightmapChanged = false;
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
        //yield break;
    }

    public void OnHeightmapChanged()
    {
        _isHeightmapChanged = true;
    }

    private void OnApplicationQuit()
    {
        RestoreTerrain();
    }

    public void RestoreTerrain()
    {
        _terrain.terrainData.SetHeightsDelayLOD(0, 0, _originalHeightmap);
    }
}
