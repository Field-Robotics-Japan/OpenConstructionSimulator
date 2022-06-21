using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepenetrationVelocityLimitter : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float velocity;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.maxDepenetrationVelocity = velocity;
    }
}
