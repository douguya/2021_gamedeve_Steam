using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Player
{
    private int PlayerNumber;//プレイヤーの番号
    private string Name;//名前
    private List<string> ItemName = new List<string>();//持っているアイテムの名前
    private List<int> ItemPoint = new List<int>();//持っているアイテムのポイント
    private int GoalNum = 0;//ゴールした数

    public Player(int Pnum,string n,int G)
    {
        PlayerNumber = Pnum; Name = n; GoalNum = G;
    }

    public void SetName(string n)//名前の再設定
    {
        Name = n;
    }

    public void Goaladd()//ゴールの数プラス
    {
        GoalNum++;
    }

    
}

public class PlayerStatus : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
