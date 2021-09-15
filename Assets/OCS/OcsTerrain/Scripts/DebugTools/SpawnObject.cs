using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private bool _isRepeat;
    [SerializeField] private float _dt;
    [SerializeField] private GameObject _prefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject sand = Instantiate(this._prefab, this.transform.position, Quaternion.identity, this.transform);
        if (this._isRepeat) StartCoroutine(FuncCoroutine());
    }

    IEnumerator FuncCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this._dt);
            GameObject sand = Instantiate(this._prefab, this.transform.position, Quaternion.identity, this.transform);
        }
    }
}
