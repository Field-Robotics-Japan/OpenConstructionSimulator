using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0F;
    [SerializeField] private float _rotateSpeed = 1.0F;

    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, this.transform.parent.position, this._moveSpeed);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.transform.parent.rotation, this._rotateSpeed);
    }
}
