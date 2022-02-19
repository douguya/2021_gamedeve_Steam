using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassEffect : MonoBehaviour
{
    //public GameObject[] Player = new GameObject[4];//プレイヤーオブジェクト取得
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Effects(string day)
    {
        switch(DictionaryManager.DayEffectictDictionary[day][0, 1])
        {
            case "アイテム":
                GetItem( DictionaryManager.DayEffectictDictionary[day][0, 2]);
                step();
                break;

            case "アイコン":
                step();
                break;

            case "BGM":
                step();
                break;

            case "背景":
                step();
                break;

            case "":
                step();
                break;
        }
        
        

    }
    
    private void step()
    {
        GetComponent<PlayerStatus>().stopon();
    }

    private void GetItem(string Iname)
    {
        GetComponent<PlayerStatus>().Itemobtain(Iname);
    }

}
