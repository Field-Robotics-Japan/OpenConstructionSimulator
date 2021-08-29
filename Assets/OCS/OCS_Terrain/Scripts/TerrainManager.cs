using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Terrain))]
[RequireComponent(typeof(TerrainCollider))]
public class TerrainManager : MonoBehaviour
{
    [SerializeField] private bool _isRestoreFirstTerrain;
    [SerializeField] private GameObject _sandPrefab;
    [SerializeField] public float _terrainDepth;
    [SerializeField] private float _deformSmoothLevel = 1.0f;

    private GameObject _prefabBox;
    private Terrain _terrain;
    private Vector3 _terrainSize;
    private Vector3 _dimentionRatio;
    private float[,] _terrainHeightmap;
    private float[,] _originalHeightmap;
    private int _terrainHeightmapResolution;
    private List<Vector3> _sandGeneratePoints = new List<Vector3>();
    private List<GameObject> _destroyObjects = new List<GameObject>();

#if UNITY_EDITOR
    private DeformTools.TagHelper _tagHelper = new DeformTools.TagHelper();
#endif
    private string _terrainTagName = "Terrain";

    private void Awake()
    {
#if UNITY_EDITOR
        // Tag setting
        this._tagHelper.AddTag(this._terrainTagName);
#endif
        this.gameObject.tag = this._terrainTagName;
    }

    // Start is called before the first frame update
    private void Start()
    {
        this._terrain = this.gameObject.GetComponent<Terrain>();
        this._terrainSize = this._terrain.terrainData.size;
        this._terrainHeightmapResolution = this._terrain.terrainData.heightmapResolution;
        this._originalHeightmap = this._terrain.terrainData.GetHeights(0, 0, this._terrainHeightmapResolution, this._terrainHeightmapResolution);
        this._terrainHeightmap  = this._terrain.terrainData.GetHeights(0, 0, this._terrainHeightmapResolution, this._terrainHeightmapResolution);
        this._dimentionRatio = new Vector3(this._terrainHeightmapResolution / this._terrainSize.x,
                                           1 / this._terrainSize.y,
                                           this._terrainHeightmapResolution / this._terrainSize.z);
        this._prefabBox = new GameObject("GeneratedSands");

        // Depth setting
        this.transform.position += new Vector3(0.0f, -this._terrainDepth, 0.0f);
        this.OffsetHeightMap(this._terrainHeightmap, this._terrainDepth);
    }

    private void FixedUpdate()
    {
        foreach (GameObject destroyObject in this._destroyObjects)
        {
            Destroy(destroyObject);
        }
        this._destroyObjects.Clear();
        this._terrain.terrainData.SetHeightsDelayLOD(0, 0, this._terrainHeightmap);
        this.GenerateSand();
        this._sandGeneratePoints.Clear();
    }

    private void OnApplicationQuit()
    {
        if (this._isRestoreFirstTerrain)
        {
            this._terrain.terrainData.SetHeightsDelayLOD(0, 0, this._originalHeightmap);
        }
    }

    private void OffsetHeightMap(float[,] heightmap, float offset)
    {
        for (int i = 0; i < this._terrainHeightmapResolution; i++)
        {
            for (int j = 0; j < this._terrainHeightmapResolution; j++)
            {
                heightmap[j, i] = heightmap[j, i] + offset * this._dimentionRatio.y;
            }
        }
    }

    private void GenerateSand()
    {
        if (this._sandGeneratePoints != null && this._sandGeneratePoints.Count > 0)
        {
            foreach (Vector3 point in this._sandGeneratePoints)
            {
                GameObject sand = Instantiate(this._sandPrefab, point, Quaternion.identity, _prefabBox.transform);
            }
        }
    }

    public void AddDestroyObj(GameObject obj)
    {
        this._destroyObjects.Add(obj);
        Vector3 pos = this.ToTerrainPosition(obj.transform.position);
        if (this.ToTerrainPositionY(obj.transform.position.y) > this._terrainHeightmap[(int)pos.z, (int)pos.x]) this._terrainHeightmap[(int)pos.z, (int)pos.x] = this.ToTerrainPositionY(obj.transform.position.y);
    }

    public float GetHeightmap(Vector3 position)
    {
        Vector3 terrainPos = this.ToTerrainPosition(position);
        return this._terrainHeightmap[(int)terrainPos.z, (int)terrainPos.x];
    }

    public bool SetHeightmap(Vector3 position)
    {
        this._terrainHeightmap[this.ToTerrainPositionZ(position.z), this.ToTerrainPositionX(position.x)] = this.ToTerrainPositionY(position.y);
        return true;
    }

    public bool ExcavateWithSand(Vector3[] deformArea)
    {
        float height;
        foreach (Vector3 target in deformArea)
        {
            height = this.FromTerrainPositionY(this._terrainHeightmap[(int)(target.z * this._dimentionRatio.z), (int)(target.x * this._dimentionRatio.x)]) - target.y;
            if (height > 0)
            {
                this._terrainHeightmap[(int)(target.z * this._dimentionRatio.z), (int)(target.x * this._dimentionRatio.x)] = this.ToTerrainPositionY(target.y);
                for (float i = 0.2f; i <= height; i += 0.15f)
                {
                    this._sandGeneratePoints.Add(target + new Vector3(0.0f, i, 0.0f));
                }
            }
        }
        return true;
    }

    public bool ExcavateWithoutSand(Vector3[] deformArea)
    {
        float height;
        foreach (Vector3 target in deformArea)
        {
            height = this.FromTerrainPositionY(this._terrainHeightmap[(int)(target.z * this._dimentionRatio.z), (int)(target.x * this._dimentionRatio.x)]) - target.y;
            if (height > 0)
            {
                this._terrainHeightmap[(int)(target.z * this._dimentionRatio.z), (int)(target.x * this._dimentionRatio.x)] = this.ToTerrainPositionY(target.y);
            }
        }
        return true;
    }

    public bool SmoothDeformation(Vector3 position)
    {
        Vector3 terrainPos = this.ToTerrainPosition(position);
        Vector3 pos = new Vector3();
        float center = this._terrainHeightmap[(int)terrainPos.z, (int)terrainPos.x];
        float gap = this.ToTerrainPositionY(position.y) - center;
        for (int i = this.ToTerrainPositionX(position.x - this._deformSmoothLevel * 3); i < this.ToTerrainPositionX(position.x + this._deformSmoothLevel * 3); i++)
        {
            if (i > this._terrainHeightmapResolution) continue;

            for (int j = this.ToTerrainPositionZ(position.z - this._deformSmoothLevel * 3); j < this.ToTerrainPositionZ(position.z + this._deformSmoothLevel * 3); j++)
            {
                if (j > this._terrainHeightmapResolution) continue;

                pos = new Vector3(i, 0, j);
                if (this._terrainHeightmap[(int)pos.z, (int)pos.x] - center > gap) continue;
                float diff = 2 * gap * Mathf.Exp(-1 * (Mathf.Pow((pos - terrainPos).magnitude, 2)) / (2 * Mathf.Pow(this._deformSmoothLevel, 2)));
                this._terrainHeightmap[(int)pos.z, (int)pos.x] += diff;
            }
        }
        return true;
    }

    public Vector3 ToTerrainPosition(Vector3 position)
    {
        Vector3 tmp = Vector3.Scale(position + new Vector3(0.0f, this._terrainDepth, 0.0f), this._dimentionRatio);
        return new Vector3((int)tmp.x, tmp.y, (int)tmp.z);
    }

    public Vector2Int ToTerrainPosition(Vector2 position)
    {
        Vector2 tmp = Vector2.Scale(position, new Vector2(this._dimentionRatio.x, this._dimentionRatio.z));
        return new Vector2Int((int)tmp.x, (int)tmp.y);
    }

    public int ToTerrainPositionX(float position)
    {
        return (int)(position * this._dimentionRatio.x);
    }


    public float ToTerrainPositionY(float position)
    {
        return (position + this._terrainDepth) * this._dimentionRatio.y;
    }


    public int ToTerrainPositionZ(float position)
    {
        return (int)(position * this._dimentionRatio.z);
    }

    public float FromTerrainPositionX(int position)
    {
        return ((float)position / this._dimentionRatio.x);
    }


    public float FromTerrainPositionY(float position)
    {
        return position / this._dimentionRatio.y - this._terrainDepth;
    }


    public float FromTerrainPositionZ(int position)
    {
        return ((float)position / this._dimentionRatio.z);
    }
}
