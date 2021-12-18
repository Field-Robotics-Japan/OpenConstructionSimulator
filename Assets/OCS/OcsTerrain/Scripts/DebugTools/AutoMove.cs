using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public Vector3 move;
    public enum BaseDir
    {
        world,
        local
    }

    [SerializeField] private BaseDir _base = BaseDir.world;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch(this._base)
        {
            case BaseDir.world:
                this.transform.Translate(move, Space.World);
                break;
            case BaseDir.local:
                this.transform.Translate(move);
                break;
            default:
                break;
        }
    }
}
