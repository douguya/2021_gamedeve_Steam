using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anniva : MonoBehaviour
{
    [SerializeField]
    string Itemname;

    PlayerStatasIMamura Playerstatus;

    private void Awake()
    {
        Playerstatus = GetComponent<PlayerStatasIMamura>();
    }

    //基本となる、アイテムのみを獲得するタイプの記念日
    public void GetItemandPoint() {
        Playerstatus.ItemobtainToRPC(Itemname);
    }
}
