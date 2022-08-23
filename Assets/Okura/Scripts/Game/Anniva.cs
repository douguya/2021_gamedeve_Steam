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

    //基本となる、アイテムのみを獲得するタイプの記念日
    public void GetItemandPoint() {
        Playerstatus.ItemobtainToRPC(Itemname);
    }

    //Instanceが出てくるやつ
    public void AppearInstances(){
        GameObject Instance = (GameObject)Resources.Load(Itemname);

        Instantiate(Instance, new Vector3(200 * InstantiateTimes,0,650), Quaternion.identity);
        InstantiateTimes++;
    }

    //BGMが変わるやつ
    public void BGMChange() {
        BGM.BGMsetandplay(Itemname);
    }
}
