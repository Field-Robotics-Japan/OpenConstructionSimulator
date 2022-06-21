using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Excavator : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _minForceToExcavate;
    [SerializeField] private LayerMask _terrainLayer;

    [Header("Informations(No need to input)")]
    [SerializeField] private DeformableTerrain _deformableTerrain;
    [SerializeField] private SandManager _sandManager;

    [SerializeField] private Rigidbody _rigidbody;

    [Header("Debug")]
    [SerializeField] private bool _showForceArrow;
    [SerializeField] private bool _isExcavating;

    private void Awake()
    {
        _deformableTerrain = Object.FindObjectOfType<DeformableTerrain>();
        _sandManager = Object.FindObjectOfType<SandManager>();

        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Terrain") return;

        if (collision.impulse.magnitude == 0) return;
        Vector3 force = -collision.impulse / Time.fixedDeltaTime / (float)collision.contacts.Length;

        if (force.magnitude < _minForceToExcavate) return;
        //force.y = 0;

        _isExcavating = true;

        foreach (ContactPoint cp in collision.contacts)
        {
            Vector3 pos_cp = cp.point;

            float i = 0.0f;
            float radius = 0.0f;

            while (i < _sandManager._maxSandRadius*2)
            {
                radius = Random.Range(_sandManager._minSandRadius, _sandManager._maxSandRadius);
                i += radius;

                Vector3 pos_sp = pos_cp + force.normalized * i;

                RaycastHit hit;
                if(Physics.Raycast(new Vector3(pos_sp.x, _deformableTerrain._terrainSize.y + _deformableTerrain._offset.y, pos_sp.z), Vector3.down, out hit, _deformableTerrain._terrainSize.y, _terrainLayer))
                {
                    if(hit.collider.tag == "Terrain")
                    {
                        float height_sp = _deformableTerrain._terrainSize.y + _deformableTerrain._offset.y - hit.distance;
                        while(height_sp > pos_cp.y)
                        {
                            float spawnRadius = Random.Range(_sandManager._minSandRadius, radius);
                            _deformableTerrain.SetHeight(pos_sp, height_sp - spawnRadius*2);
                            _sandManager.Spawn(new Vector3(pos_sp.x, height_sp, pos_sp.z));
                            height_sp -= spawnRadius*2;
                        }
                    }
                }
                i += radius;
            }

#if UNITY_EDITOR
            if (_showForceArrow)
            {
                Debug.DrawLine(pos_cp, pos_cp+force.normalized, Color.white);
            }
#endif
        }
        

        _deformableTerrain.OnHeightmapChanged();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Terrain") return;
        _isExcavating = false;
    }
}
