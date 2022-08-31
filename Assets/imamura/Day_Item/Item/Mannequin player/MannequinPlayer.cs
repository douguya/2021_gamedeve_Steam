 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MannequinPlayer : MonoBehaviour
{
    public int PlayerNumber;
    // Start is called before the first frame update

    public Day_Square_Master DayMaster;
    public Anniversary_Item_Master ItemMaster;
    public List<Anniversary_Item> Hub_Items = new List<Anniversary_Item>();
    public GameObject blocs;
    public GameObject Log;
    public Text JujeText;
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

    public void textadd(string LogText)//日本語50文字
    {
      
      Text texts=  Log.GetComponent<Text_Log>().TextObj.GetComponent<Text>();//ログのテキストを参照で取得
        /*
      JujeText.text=LogText;

      float height = JujeText.preferredWidth;
         Debug.Log(height);
        */

        texts.text=texts.text+"\n"+LogText;//ログのテキスト内容に追加
    }

   public void Dayicon(int num)
    {
        Debug.Log("QQQQQQQQQQQQQQQQQQQ"+DayMaster.Day_Squares[num].Icon);
        Debug.Log("QQQQQQQQQQQQQQQQQQQ"+DayMaster.Day_Squares[num+1].Icon);

        if (DayMaster.Day_Squares[num+1].Icon!=null)
        {
            Debug.Log("MMMMMMMMMMMMMMMMMMM"+DayMaster.Day_Squares[num+1].Icon);
        }
        else
        {
            Debug.Log("TTTTTTTTTTTTTTTTTTTTT"+DayMaster.Day_Squares[num+1].Icon);
        }


    }

}
