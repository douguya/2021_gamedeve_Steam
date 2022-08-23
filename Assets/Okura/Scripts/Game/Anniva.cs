using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anniva : MonoBehaviour
{
    [SerializeField]
    string Itemname;
    [SerializeField]
    int InstantiateTimes;
    [SerializeField]
    BGMManager BGM;

    PlayerStatasIMamura Playerstatus;

    private void Awake()
    {
        Playerstatus = GetComponent<PlayerStatasIMamura>();
        BGM = GameObject.Find("BGM").GetComponent<BGMManager>();
        InstantiateTimes = 0;
    }

    //��{�ƂȂ�A�A�C�e���݂̂��l������^�C�v�̋L�O��
    public void GetItemandPoint() {
        Playerstatus.ItemobtainToRPC(Itemname);
    }

    //Instance���o�Ă�����
    public void AppearInstances(){
        GameObject Instance = (GameObject)Resources.Load(Itemname);

        Instantiate(Instance, new Vector3(200 * InstantiateTimes,0,650), Quaternion.identity);
        InstantiateTimes++;
    }

    //BGM���ς����
    public void BGMChange() {
        BGM.BGMsetandplay(Itemname);
    }
}
