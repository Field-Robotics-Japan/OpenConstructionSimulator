using UnityEngine;
using System.Collections;

public class findObject : MonoBehaviour 
{
     
    void Start () 
    {
        // typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
        foreach (Ocs.GameSystem.ModeManeger obj in UnityEngine.Object.FindObjectsOfType(typeof(Ocs.GameSystem.ModeManeger)))
        {
            Debug.Log(obj.Automation);
            // シーン上に存在するオブジェクトならば処理.
            /*if (obj.activeInHierarchy)
            {
                // GameObjectの名前を表示.
                Debug.Log(obj.name);
            }*/
        }
    }
    
}
