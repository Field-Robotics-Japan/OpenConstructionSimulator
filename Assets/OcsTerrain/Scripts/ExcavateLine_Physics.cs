using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavateLine_Physics : MonoBehaviour
{
    [SerializeField] private TerrainManager _terrainManager;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private bool _debugMode = false;

#if UNITY_EDITOR
    private DeformTools.TagHelper _tagHelper = new DeformTools.TagHelper();
#endif
    private string _terrainTagName = "Terrain";

    // Start is called before the first frame update
    void Start()
    {
        this._terrainManager = GameObject.FindGameObjectWithTag(this._terrainTagName).GetComponent<TerrainManager>();
        this._rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != _terrainTagName) return;

        Vector3 force = collision.impulse / Time.fixedDeltaTime / (float)collision.contacts.Length;

        force = Vector3.Dot(force, _rigidbody.velocity.normalized)*_rigidbody.velocity.normalized;

        //if (force.magnitude < 10.0f) return;
        Vector3[] targets = GetExcavateArea(collision, force);
        this._terrainManager.ExcavateWithSand(targets);
    }

    private Vector3[] GetExcavateArea(Collision collision, Vector3 force)
    {
        List<Vector3> DeformVerts = new List<Vector3>();

        for(int i = 0; i < collision.contacts.Length; i++)
        {
            float j = 0.0f;
            for(j = 0.0f; j <= force.magnitude*0.05f && j<=0.8f; j += 0.1f)
            {
                DeformVerts.Add(collision.contacts[i].point - force.normalized * j - Vector3.up * 0.01f + this.transform.right*(Random.value-0.5f)*0.5f);
            }
            DeformVerts.Add(collision.contacts[i].point - collision.contacts[i].normal * 0.1f - Vector3.up * 0.01f);
#if UNITY_EDITOR
            if (_debugMode)
            {
                Debug.DrawLine(collision.contacts[i].point, collision.contacts[i].point - force.normalized*j, Color.white, 0.1f, false);
                Debug.DrawLine(collision.contacts[i].point, collision.contacts[i].point - collision.contacts[i].normal*0.1f, Color.white, 0.1f, false);
            }
#endif
        }
        return DeformVerts.ToArray();
    }
}
