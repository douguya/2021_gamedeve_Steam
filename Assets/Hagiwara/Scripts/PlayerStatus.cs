using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerStatus : MonoBehaviour
{
    private int PlayerNumber;//プレイヤーの番号
    private string Name;//名前
    private List<string> ItemName = new List<string>();//持っているアイテムの名前
    private List<int> ItemPoint = new List<int>();//持っているアイテムのポイント
    private int Goalcount = 0;//ゴールした数
    private int PX,PY;//プレイヤーのマス座標

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    

    public PlayerStatus(int Pnum, string n, int G)
    {
        PlayerNumber = Pnum; Name = n; Goalcount = G;
    }

    public void SetName(string n)//名前の再設定
    {
        Name = n;
    }

    public void Goaladd()//ゴールの数プラス
    {
        Goalcount++;
    }

    public void Itemadd(string IName, int IPoint)//アイテムの取得
    {
        ItemName.Add(IName);
        ItemPoint.Add(IPoint);
    }

    public void SetPlayerMass(int x,int y)//プレイヤーがどのマスにいるか記憶
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
        return ItemName[num];
    }

    public int GetItemPoint(int num)//持っているアイテムのポイント
    {
        return ItemPoint[num];
    }

    public int GetGaol()//ゴールした数
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
