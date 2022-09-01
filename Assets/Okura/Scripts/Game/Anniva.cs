using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anniva : MonoBehaviour
{
    [SerializeField]
    string Itemname;
    [SerializeField]
    GameObject Instance;
    [SerializeField]
    float InstanceyValue;
    [SerializeField]
    BGMManager BGM;

    PlayerStatasIMamura Playerstatus;

    private void Awake()
    {
        Playerstatus = GetComponent<PlayerStatasIMamura>();
        BGM = GameObject.Find("BGM").GetComponent<BGMManager>();
    }

    //基本となる、アイテムのみを獲得するタイプの記念日
    public void GetItemandPoint() {
        Playerstatus.ItemobtainToRPC(Itemname);
    }

    //Instanceが出てくるやつ
    public void AppearInstances()
    {
        StartCoroutine(Instance.GetComponent<AnimationController>().StartAnimation(InstanceyValue));
    }

    //BGMが変わるやつ
    public void BGMChange() {
        BGM.BGMsetandplay(Itemname);
    }
}
