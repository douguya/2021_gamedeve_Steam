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

    //��{�ƂȂ�A�A�C�e���݂̂��l������^�C�v�̋L�O��
    public void GetItemandPoint() {
        Playerstatus.ItemobtainToRPC(Itemname);
    }
}
