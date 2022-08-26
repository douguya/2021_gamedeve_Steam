 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MannequinPlayer : MonoBehaviour
{
    public int PlayerNumber;
    // Start is called before the first frame update
    public Text text;
    public Anniversary_Item_Master ItemMaster;
    public List<Anniversary_Item> Hub_Items = new List<Anniversary_Item>();
    public GameObject blocs;
    void Start()
    {

    } 

    // Update is called once per frame
    void Update()
    {
       
    }
    public void ItemAdd(int ItemNum)//ItemNum＝マスター登録順の番号
    {
        Hub_Items.Add(ScriptableObject.Instantiate(ItemMaster.Anniversary_Items[ItemNum]));//マスターにあるItemNumのアイテムを生成し、Hubに追加
        blocs.GetComponent<ItemBlock_List_Script>().AddItem(ItemNum);
    }

    public void ItemLost(int HubItemNum)//HubItemNum＝所持アイテム登録順の番号
    {
      
        Hub_Items.RemoveAt(HubItemNum);//所持中のHubItemNum番目のアイテムを消去
        blocs.GetComponent<ItemBlock_List_Script>().LostItem(HubItemNum);


    }

    public void NextMyTurn()
    {
        Debug.Log("AAA"+Hub_Items.Count);
        int num = Hub_Items.Count;
       
        for (int loop=num-1;loop>=0;loop--)
        {
            
            if (Hub_Items[loop].ItemLifespan!="null") 
            {
                Hub_Items[loop].ItemLifespan=(int.Parse(Hub_Items[loop].ItemLifespan)-1).ToString();
                if (int.Parse(Hub_Items[loop].ItemLifespan)<=0){ Hub_Items.Remove(Hub_Items[loop]); }
            }
        }

    }



 

}
