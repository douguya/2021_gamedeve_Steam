using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sugorokuManager : MonoBehaviour
{
    private GameObject[,] mass = new GameObject[14, 10];//マスの格納
    private int XGoal, YGoal;//ゴールの座標
    public GameObject obj;//プレハブのMassを取得
    public GameObject June;
    public GameObject July;
    public GameObject August;
    public GameObject September;
    public GameObject dice;//ダイスを取得
    public GameObject[] Player = new GameObject[4];//プレイヤーオブジェクト取得
    private int Playerturn = 0;//どのプレイヤー番か
    private int step = 0;//プレイヤーのターン手順
    private bool stop; //プレイヤーのターン手順のストッパー
    private float span = 2f;//プレイヤー移動速度
    private float currentTime = 0f;

    private int xplay;//プレイヤーのマス座標を取得
    private int yplay;
    private int MoveSelectnum = 0;//移動選択の切り替え
    private int[] way = new int[4];//0:上 1:下 2:左 3:右
    private bool[] walk = new bool[4];//onClickされたかどうか調べる
    int Move = 0;//ダイスの出目
    private int diceconter;//ダイスの残り数
    
    void Start()
    {
        float x = -2.25f;
        float y = 1.2f;
        int count = 0;
        for (int i = 0; i < 5; i++)//6月の生成
        {
            for (int l = 0; l < 7; l++)
            {
                mass[l, i] = Instantiate(obj, new Vector3(x, y, 0.0f), Quaternion.identity);//マスを指定座標への生成and格納
                mass[l, i].transform.parent = June.transform;//6月の子として生成
                x = x + 0.75f;
                count++;
            }
            y = y - 0.6f;
            x = -2.25f;
        }

        y = 1.2f;
        for (int i = 0; i < 5; i++)//7月の生成
        {
            for (int l = 7; l < 14; l++)
            {
                mass[l, i] = Instantiate(obj, new Vector3(x, y, 0.0f), Quaternion.identity);//マスを指定座標への生成and格納
                mass[l, i].transform.parent = July.transform;//7月の子として生成
                x = x + 0.75f;
                count++;
            }
            y = y - 0.6f;
            x = -2.25f;
        }

        y = 1.2f;
        for (int i = 5; i < 10; i++)//8月の生成
        {
            for (int l = 0; l < 7; l++)
            {
                mass[l, i] = Instantiate(obj, new Vector3(x, y, 0.0f), Quaternion.identity);//マスを指定座標への生成and格納
                mass[l, i].transform.parent = August.transform;//8月の子として生成
                x = x + 0.75f;
                count++;
            }
            y = y - 0.6f;
            x = -2.25f;
        }

        y = 1.2f;
        for (int i = 5; i < 10; i++)//9月の生成
        {
            for (int l = 7; l < 14; l++)
            {
                mass[l, i] = Instantiate(obj, new Vector3(x, y, 0.0f), Quaternion.identity);//マスを指定座標への生成and格納
                mass[l, i].transform.parent = September.transform;//9月の子として生成
                x = x + 0.75f;
                count++;
            }
            y = y - 0.6f;
            x = -2.25f;
        }
        June.transform.position = new Vector3(-2.9f, 1.6f, 0);//6月の移動
        July.transform.position = new Vector3(2.9f, 1.6f, 0);//7月の移動
        August.transform.position = new Vector3(-2.9f, -2.4f, 0);//8月の移動
        September.transform.position = new Vector3(2.9f, -2.4f, 0);//9月の移動

        invalid();//いらないマスの無効化

        Player[0].transform.position = transform.InverseTransformPoint(mass[0, 1].transform.position); PlayerMass(0, 0, 1);//プレイヤーの配置とマス座標の記憶
        Player[1].transform.position = transform.InverseTransformPoint(mass[13, 0].transform.position); PlayerMass(1, 13, 1);
        Player[2].transform.position = transform.InverseTransformPoint(mass[0, 9].transform.position); PlayerMass(2, 0, 9);
        Player[3].transform.position = transform.InverseTransformPoint(mass[12, 9].transform.position); PlayerMass(3, 12, 9);

        GoalDecision();//ゴールの選択

    }


    void Update()
    {

        
        
        switch (step) {
            case 0://ダイスを回す
                //dice.GetComponent<imamuraDice>().OnDiceSpin();
                if(stop == true)
                {
                    Move = dice.GetComponent<imamuraDice>().StopDice();
                    Debug.Log(Move);
                    step = 1;
                    stop = false;
                }
                
                break;
            case 1://ダイスのマス分移動出来るところを設定する
                MoveSelect(Playerturn, Move);
                if(stop == true)
                {
                    step = 2;
                    stop = false;
                }
                break;
            case 2://プレイヤーの移動
                
                currentTime += Time.deltaTime;

                if (currentTime > span)
                {
                    //Debug.LogFormat("{0}秒経過", span);
                    MovePlayer(Playerturn);
                    currentTime = 0f;
                }
                
                if (stop == true)
                {
                    step = 3;
                    stop = false;
                }
                break;

            case 3://ゴール＆マスの効果
                //ゴール＆マスの効果
                step = 4;
                break;

            case 4://次の人の番に
                Playerturn++;
                if (3 < Playerturn)
                {
                    Playerturn = 0;
                }
                step = 0;
                break;
        }
        
        
    }

    private void GoalDecision()//初めてゴールを出現させる
    {
        int vertical, beside;
        do {
            vertical = Random.Range(0, 14);
            beside = Random.Range(0, 10);
        } while (mass[vertical, beside].GetComponent<Mass>().invalid == true);//ランダムに選んだマスが無効化じゃないものを探す
        mass[vertical, beside].GetComponent<Mass>().Goal = true;//ゴールの設置
        XGoal = vertical; YGoal = beside;//ゴール位置の記憶

    }

    private void GoalAgain()//ゴールの再設置
    {
        int vertical, beside;
        do
        {
            vertical = Random.Range(0, 14);
            beside = Random.Range(0, 10);
        } while (mass[vertical, beside].GetComponent<Mass>().invalid == true && MonthCount(vertical, beside) == true);//選んだマスが無効化＆同じ月じゃないものを探す
        mass[vertical, beside].GetComponent<Mass>().Goal = true;//ゴールの設置
        XGoal = vertical; YGoal = beside;//ゴール位置の記憶
    }

    private bool MonthCount(int x, int y)//ゴールと同じ月か判断する
    {
        bool Jach = false;
        int BeforeMonth = 0, NextMonth = 0;

        if (XGoal < 7 && YGoal < 5) { BeforeMonth = 1; }
        if (7 <= XGoal && YGoal < 5) { BeforeMonth = 2; }
        if (XGoal < 7 && 5 < YGoal) { BeforeMonth = 3; }
        if (7 <= XGoal && 5 < YGoal) { BeforeMonth = 4; }

        if (x < 7 && y < 5) { NextMonth = 1; }
        if (7 <= x && y < 5) { NextMonth = 2; }
        if (x < 7 && 5 < y) { NextMonth = 3; }
        if (7 <= x && 5 < y) { NextMonth = 4; }

        if (BeforeMonth == NextMonth)
        {
            Jach = true;
        }
        else
        {
            Jach = false;
        }
        return Jach;
    }

    private void GoalClear()//ゴールを消す
    {

        for (int i = 0; i < 14; i++)
        {
            for (int l = 0; l < 10; l++)
            {
                mass[i, l].GetComponent<Mass>().Goal = false;
            }
        }
    }

    private void invalid()//いらないマスの無効化
    {
        mass[0, 0].GetComponent<Mass>().invalid = true;//5/29
        mass[1, 0].GetComponent<Mass>().invalid = true;//5/30
        mass[2, 0].GetComponent<Mass>().invalid = true;//5/31
        mass[5, 4].GetComponent<Mass>().invalid = true;//7/1
        mass[6, 4].GetComponent<Mass>().invalid = true;//7/2

        mass[7, 0].GetComponent<Mass>().invalid = true;//6/26
        mass[8, 0].GetComponent<Mass>().invalid = true;//6/27
        mass[9, 0].GetComponent<Mass>().invalid = true;//6/28
        mass[10, 0].GetComponent<Mass>().invalid = true;//6/29
        mass[11, 0].GetComponent<Mass>().invalid = true;//6/30

        mass[0, 5].GetComponent<Mass>().invalid = true;//7/31
        mass[4, 9].GetComponent<Mass>().invalid = true;//9/1
        mass[5, 9].GetComponent<Mass>().invalid = true;//9/2
        mass[6, 9].GetComponent<Mass>().invalid = true;//9/3

        mass[7, 5].GetComponent<Mass>().invalid = true;//8/28
        mass[8, 5].GetComponent<Mass>().invalid = true;//8/29
        mass[9, 5].GetComponent<Mass>().invalid = true;//8/30
        mass[10, 5].GetComponent<Mass>().invalid = true;//8/31
        mass[13, 9].GetComponent<Mass>().invalid = true;//10/1

    }

    private void PlayerMass(int P, int x, int y)//プレイヤーのマス座標を記憶させる
    {
        Player[P].GetComponent<PlayerStatus>().SetPlayerMass(x, y);
    }
    public void DiceBotton()//ダイスを止める
    {
        stop = true;
    }

    private void MoveSelect(int Pnum, int dice)//プレイヤーの移動の選択
    {
        
        switch (MoveSelectnum) {
            case 0:
                xplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerX();
                yplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerY();
                diceconter = dice;
                mass[xplay, yplay].GetComponent<Mass>().Decisionon();
                mass[xplay, yplay].GetComponent<Mass>().Loot = true; 
                MoveSelectnum = 1;
                break;

            case 1:
                way[0] = yplay - 1; way[1] = yplay + 1; way[2] = xplay - 1; way[3] = xplay + 1;
                if (0 <= way[0] && way[0] <= 10 && mass[xplay, way[0]].GetComponent<Mass>().invalid == false && mass[xplay, way[0]].GetComponent<Mass>().Loot == false)
                {
                    mass[xplay, way[0]].GetComponent<Mass>().Selecton();
                }
                if (0 <= way[1] && way[1] <= 10 && mass[xplay, way[1]].GetComponent<Mass>().invalid == false && mass[xplay, way[1]].GetComponent<Mass>().Loot == false)
                {
                    mass[xplay, way[1]].GetComponent<Mass>().Selecton();
                }
                if (0 <= way[2] && way[2] <= 13 && mass[way[2], yplay].GetComponent<Mass>().invalid == false && mass[way[2], yplay].GetComponent<Mass>().Loot == false)
                {
                    mass[way[2], yplay].GetComponent<Mass>().Selecton();
                }
                if (0 <= way[3] && way[3] <= 13 && mass[way[3], yplay].GetComponent<Mass>().invalid == false && mass[way[3], yplay].GetComponent<Mass>().Loot == false)
                {
                    mass[way[3], yplay].GetComponent<Mass>().Selecton();
                }
                MoveSelectnum = 2;
                break;

            case 2:
                if (0 <= way[0] && way[0] <= 10 && mass[xplay, way[0]].GetComponent<Mass>().walk == true)
                {
                    diceconter--;
                    yplay = way[0];
                    clearSelect();
                }
                if (0 <= way[1] && way[1] <= 10 && mass[xplay, way[1]].GetComponent<Mass>().walk == true)
                {
                    diceconter--;
                    yplay = way[1];
                    clearSelect();
                }
                if (0 <= way[2] && way[2] <= 13 && mass[way[2], yplay].GetComponent<Mass>().walk == true)
                {
                    diceconter--;
                    xplay = way[2];
                    clearSelect();
                }
                if (0 <= way[3] && way[3] <= 13 && mass[way[3], yplay].GetComponent<Mass>().walk == true)
                {
                    diceconter--;
                    xplay = way[3];
                    clearSelect();
                }
                if(diceconter > 0)
                {
                    MoveSelectnum = 1;
                }
                else
                {
                    MoveSelectnum = 0;
                    Debug.Log("選択終了");
                    stop = true;
                }
                
                break;
        }
    }

    private void clearSelect()//選択できるマスの全消去
    {
        for (int i = 0; i < 14; i++)
        {
            for (int l = 0; l < 10; l++)
            {
                mass[i, l].GetComponent<Mass>().Selectoff();
                mass[i, l].GetComponent<Mass>().walk = false;
            }
        }
    }

    /*
    private void MoveSelect(int Pnum)//プレイヤーの移動
    {
        
    }
    */

    private void MovePlayer(int Pnum)//プレイヤーの移動
    {
        
        xplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerX();//プレイヤーのマス座標
        yplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerY();
        way[0] = yplay - 1; way[1] = yplay + 1; way[2] = xplay - 1; way[3] = xplay + 1;
        mass[xplay, yplay].GetComponent<Mass>().Loot = false;//足元のLoot消す
        mass[xplay, yplay].GetComponent<Mass>().Decisionoff();//決定マス消去
        bool stoper = false;
        if (0 <= way[0] && way[0] <= 10 && mass[xplay, way[0]].GetComponent<Mass>().Loot == true && stoper == false)//四方にLootがあるか探して移動する
        {
            Move--;
            Player[Pnum].transform.position = transform.InverseTransformPoint(mass[xplay, way[0]].transform.position);
            PlayerMass(Pnum, xplay, way[0]);
            yplay = way[0];
            stoper = true;
        }
        if (0 <= way[1] && way[1] <= 10 && mass[xplay, way[1]].GetComponent<Mass>().Loot == true && stoper == false)
        {
            Move--;
            Player[Pnum].transform.position = transform.InverseTransformPoint(mass[xplay, way[1]].transform.position);
            PlayerMass(Pnum, xplay, way[1]);
            yplay = way[1];
            stoper = true;
        }
        if (0 <= way[2] && way[2] <= 13 && mass[way[2], yplay].GetComponent<Mass>().Loot == true && stoper == false)
        {
            Move--;
            Player[Pnum].transform.position = transform.InverseTransformPoint(mass[way[2], yplay].transform.position);
            PlayerMass(Pnum, way[2], yplay);
            xplay = way[2];
            stoper = true;
        }
        if (0 <= way[3] && way[3] <= 13 && mass[way[3], yplay].GetComponent<Mass>().Loot == true && stoper == false)
        {
            Move--;
            Player[Pnum].transform.position = transform.InverseTransformPoint(mass[way[3], yplay].transform.position);
            PlayerMass(Pnum, way[3], yplay);
            xplay = way[3];
            stoper = true;
        }
        //Debug.Log(Move);
        if (Move == 0)
        {
            Debug.Log("終わってる");
            mass[xplay, yplay].GetComponent<Mass>().Loot = false;//足元のLoot消す
            mass[xplay, yplay].GetComponent<Mass>().Decisionoff();//決定マス消去
            stop = true;
        }
    }

}
