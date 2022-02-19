using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sugorokuManager : MonoBehaviour
{
    private int XGoal, YGoal;                       //ゴールの座標
    
    public GameObject[] Player = new GameObject[4]; //プレイヤーオブジェクト取得
    public Width[] height=new Width[10];                             //Massの縦列のオブジェクトの取得・一番下で二次元配列にしている
    private int Playerturn = 0;                     //プレイヤーの手番管理
    
    private int Playcount = 0;                      //プレイヤーの参加人数
    private int play = 0;                           //誰の番か


    private bool gamestart = false;

    void Start()
    {
    
        


        GoalDecision();//ゴールの選択

    }


    void Update()
    {

        if (gamestart)
        {
            switch (Playerturn)
            {
                case 0:
                    Player[play].GetComponent<PlayerStatus>().step = 1;             //プレイヤーをコントロール出来るようにする
                    Playerturn = 1;
                    break;

                case 1:
                    if (Player[play].GetComponent<PlayerStatus>().Goalup == true)   //もしこの手番にゴールしていたら
                    {
                        Player[play].GetComponent<PlayerStatus>().Goalup = false;   //ゴール宣言取り消し
                        GoalAgain();                                                //ゴールの再設置
                    }
                    if (Player[play].GetComponent<PlayerStatus>().GetGaol() == 4)   //ゴールした数が４なら
                    {
                        Playerturn = 3;                                             //ゲーム終了
                    }
                    if (Player[play].GetComponent<PlayerStatus>().nextturn == true) //プレイヤーがターンを終了していたら
                    {
                        Player[play].GetComponent<PlayerStatus>().nextturn = false;
                        Playerturn = 2;
                        play++;                                                    //次のプレイヤーの番にする
                    }
                    break;

                case 2:
                    Playerturn = 0;
                    if (play >= Playcount)//プレイヤー参加人数を超えたら
                    {
                        play = 0;     //プレイヤー0の手番になる
                    }
                    break;

                case 3:
                    //ゲーム終了
                    Debug.Log("ゲーム終了");
                    break;
            }
        }
    }


    private void GoalClear()//全てのマスのゴールを消す
    {
        for (int i = 0; i < height.Length; i++)
        {
            for (int l = 0; l < height[0].width.Length; l++)
            {
               // Debug.Log(height[i].width[l]);
                height[i].width[l].GetComponent<Mass>().GoalOff();//ゴールを消していく
            }
        }
    }

    private void GoalDecision()//初めてゴールを出現させる
    {
        int Week, Day;                                               //ランダムなゴールの場所を入れる
        GoalClear();                                                 //全てのマスのゴールを消す
        do {
            Week = Random.Range(0, height.Length);                  //week・横の列のランダム
            Day = Random.Range(0, height[0].width.Length);          //day・縦の列のランダム
        } while (height[Week].width[Day].GetComponent<Mass>().invalid == true);//ランダムに選んだマスが存在しているものを見つけるまで繰り返す
        height[Week].width[Day].GetComponent<Mass>().GoalOn();      //ゴールの設置
        XGoal = Day; YGoal = Week;                                  //ゴール配列番号を記憶

    }

    public void GoalAgain()                                         //ゴールの再設置(同じ月にならないように)
    {
        int Week, Day;                                              //ランダムなゴールの場所を入れる
        GoalClear();                                                //全てのマスのゴールを消す
        do
        {
            Week = Random.Range(0, height.Length);                  //横の列のランダム
            Day = Random.Range(0, height[0].width.Length);          //縦の列のランダム
        } while (height[Week].width[Day].GetComponent<Mass>().invalid == true && MonthCount(Day, Week) == true);//選んだマスが存在しているもの＆同じ月じゃないものを見つけるまで繰り返す
        height[Week].width[Day].GetComponent<Mass>().GoalOn();      //ゴールの設置
        XGoal = Day; YGoal = Week;                                  //ゴール配列番号を記憶
    }

    private bool MonthCount(int x, int y)//ゴールと同じ月か判断する
    {
        if (WhichMonth(XGoal, YGoal) == WhichMonth(x,y))//同じ月ならtrue
        {
            return true;
        }
        else//違う月ならfalse
        {
            return false;
        }
    }
    private int WhichMonth(int x,int y)//x,yが何月にいるのか調べる
    {
        int Month = 0;
        if (x < height[0].width.Length/2 && y < height.Length/2) { Month = 1; }//左上の月にいるかどうか
        if (height[0].width.Length/2 <= x && y < height.Length/2) { Month = 2; }//左上の月にいるかどうか
        if (x < height[0].width.Length/2 && height.Length/2 < y) { Month = 3; }//左上の月にいるかどうか
        if (height[0].width.Length/2 <= x && height.Length/2 < y) { Month = 4; }//左上の月にいるかどうか
        return Month;
         
    }


    public void StartOfimitation()
    {
        gamestart = (gamestart == false);//反転

    }






}
[System.Serializable]
public class Width//weekの子・横列のオブジェクトの取得
{
    public  GameObject[] width;




}