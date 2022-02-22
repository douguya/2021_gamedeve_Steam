using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGRK : MonoBehaviour
{
    public GameObject BaseMass;//一番左上のrayを飛ばすためのオブジェクトの取得

    public ChildArray[] tate;
    void Start()
    {
        Debug.Log(tate.Length);
        Debug.Log(tate[0].yoko.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class ChildArray
{
    public int[] yoko;
}
