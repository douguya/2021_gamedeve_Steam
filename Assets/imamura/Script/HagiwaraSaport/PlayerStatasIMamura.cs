using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatasIMamura : MonoBehaviour
{
    private int PlayerNumber;//プレイヤーの番号
    private string Name;//名前
    public List<string> HabItem ;//持っているアイテム
    private int Goalcount = 0; //ゴールした数
    private int PX, PY;//プレイヤーのマス座標
    public GameObject Play;

    [SerializeField]
    private Dropdown dropdown;


    void Start()
    {

    }


    void Update()
    {

    }


    public PlayerStatasIMamura(int Pnum, string n, int G)
    {
        PlayerNumber = Pnum; Name = n; Goalcount = G;
    }
    public void Itemobtain(string Item)
    {
        HabItem.Add(Item);
        dropdown.options.Add(new Dropdown.OptionData { text = Item + DectionariManager.ItemDictionary[Item][0] + "P" });
        dropdown.RefreshShownValue();
    }

    
    public void ItemInfoGet(string Item)
    {
        Debug.Log(HabItem[0]);

        //  Debug.Log(Item+ItemDectionari.ItemDictionary[Item]);

       // Play.ItemDectionari.DectionariyInfo(Item);
        Debug.Log(DectionariManager.ItemDictionary[Item][0]);


    }
    




    public void SetName(string n)//名前の再設定
    {
        Name = n;
    }

    public void Goaladd()//ゴールの数プラス
    {
        Goalcount++;
    }

    public void Itemadd(string IName)//アイテムの取得
    {
        HabItem.Add(IName);
        
    }

    public void SetPlayerMass(int x, int y)//プレイヤーがどのマスにいるか記憶
    {
        PX = x;
        PY = y;
    }

    public int GetPlayerNumber()//プレイヤー番号の出力
    {
        return PlayerNumber;
    }

    public string GetName()//名前の出力
    {
        return Name;
    }

    public string GetItemName(int num)//持っているアイテムの名前
    {
        return HabItem[num];
    }

    /*
    public int GetItemPoint(int num)//持っているアイテムのポイント
    {
        return ItemPoint[num];
    }
    */
    public int GetGaol()//ゴールした数の取得
    {
        return Goalcount;
    }

    public int PlayerX()//プレイヤーのマス座標Xを出力
    {
        return PX;
    }
    public int PlayerY()//プレイヤーのマス座標Yを出力
    {
        return PY;
    }
}


