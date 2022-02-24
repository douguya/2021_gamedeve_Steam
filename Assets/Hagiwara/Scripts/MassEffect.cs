using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassEffect : MonoBehaviour
{
    //public GameObject[] Player = new GameObject[4];//プレイヤーオブジェクト取得
    public GameObject daycomment;

    public float speed = 0.5f;                      //プレイヤー移動速度
    private float currentTime = 0f;
    private int Count = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Effects(string day)
    {

        GetComponent<PlayerStatus>().daycomment(day);
        
        Debug.Log(DictionaryManager.DayEffectictDictionary[day][0]);
        Debug.Log("殺す"+DictionaryManager.EffectictCategoryDictionary[DictionaryManager.DayEffectictDictionary[day][0]][0]); 
       // Debug.Log("RRRRRRRRRRRRR" + DictionaryManager.DayEffectictDictionary[day][0,0]);
        switch (DictionaryManager.EffectictCategoryDictionary [DictionaryManager.DayEffectictDictionary[day][0]] [0])
        {
            case "アイテム":
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                GetItem( DictionaryManager.EffectictCategoryDictionary[DictionaryManager.DayEffectictDictionary[day][0]][1]);
                break;

           // case "アイコン":

                //break;

          //  case "BGM":
           //     break;


         //   case "背景":
         //       break;

         //   case "波":
                
         //       break;

         //   case "":
         //       break;

            default:
                GetItem("汎用カレンダー");
                break;
        }
        
        

    }
    
    private void step()
    {
       // GetComponent<PlayerStatus>().stopon();
    }

    private void GetItem(string Iname)
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaa"+Iname);
        GetComponent<PlayerStatus>().Itemobtain(Iname);
    }

}
