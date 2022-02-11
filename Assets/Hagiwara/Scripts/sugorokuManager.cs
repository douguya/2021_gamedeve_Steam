using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sugorokuManager : MonoBehaviour
{
    private GameObject[,] mass = new GameObject[14,10];//マスの格納
    private int XGoal,YGoal;//ゴールの座標
    public GameObject obj;//プレハブのMassを取得
    public GameObject June;
    public GameObject July;
    public GameObject August;
    public GameObject September;
    public GameObject[] Player = new GameObject[4];

    void Start()
    {
        float x = -2.25f;
        float y = 1.2f;
        int count = 0;
        for(int i =0;i < 5; i++)//6月の生成
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
        Player[1].transform.position = transform.InverseTransformPoint(mass[13,0].transform.position); PlayerMass(1, 13, 1);
        Player[2].transform.position = transform.InverseTransformPoint(mass[0, 9].transform.position); PlayerMass(2, 0, 9);
        Player[3].transform.position = transform.InverseTransformPoint(mass[12,9].transform.position); PlayerMass(3, 12, 9);

        GoalDecision();//ゴールの選択
       
    }

    
    void Update() 
    {
        
        int Playerturn = -1;
        int Move = 0;
        do
        {
            Playerturn++;
            if(3 < Playerturn)
            {
                Playerturn = 0;
            }
            Move = 5;//ダイスを回す
            MoveSelect(Playerturn, Move);//ダイスのマス分移動出来るところを設定する
            //移動する

        } while (Player[Playerturn].GetComponent<PlayerStatus>().GetGaol()==4);
        
    }

    private void GoalDecision()//初めてゴールを出現させる
    {
        int vertical, beside;
        do {
            vertical = Random.Range(0, 14);
            beside = Random.Range(0, 10);
        } while (mass[vertical, beside].GetComponent<Mass>().invalid == true );//ランダムに選んだマスが無効化じゃないものを探す
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

    private bool MonthCount(int x,int y)//ゴールと同じ月か判断する
    {
        bool Jach = false;
        int BeforeMonth = 0,NextMonth =0;

        if (XGoal < 7 && YGoal < 5) { BeforeMonth = 1; }
        if (7 <= XGoal&& YGoal < 5) { BeforeMonth = 2; }
        if (XGoal < 7 && 5 < YGoal) { BeforeMonth = 3; }
        if (7 <= XGoal&& 5 < YGoal) { BeforeMonth = 4; }

        if (x < 7 && y < 5) { NextMonth = 1; }
        if (7 <= x && y < 5) { NextMonth = 2; }
        if (x < 7 && 5 < y) { NextMonth = 3; }
        if (7 <= x && 5 < y) { NextMonth = 4; }

        if(BeforeMonth == NextMonth)
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

    private void PlayerMass(int P,int x,int y)//プレイヤーのマス座標を記憶させる
    {
        Player[P].GetComponent<PlayerStatus>().SetPlayerMass(x, y);
    }


    private void MoveSelect(int Pnum, int dice)//プレイヤーの移動
    {
        int x, y;
        int[] way = new int[4];//0:上 1:下 2:左 3:右
        bool[] walk = new bool[4];//onClickされたかどうか調べる
        x = Player[Pnum].GetComponent<PlayerStatus>().PlayerX();//プレイヤーのマス座標を取得
        y = Player[Pnum].GetComponent<PlayerStatus>().PlayerY();
        do
        {
            way[0] = y - 1; way[1] = y + 1; way[2] = x - 1; way[3] = x + 1;//マスの前後左右を確認

            if (0 <= way[0] && way[0] <= 13)
            {
                if (mass[x, way[0]].GetComponent<Mass>().Loot == false)//ルート決定済みでないマスを選択できるように
                {
                    mass[x, way[0]].GetComponent<Mass>().Selecton();
                    walk[0] = mass[x, way[0]].GetComponent<Mass>().walk;
                }
            }
            if (0 <= way[1] && way[1] <= 13)
            {
                if (mass[x, way[1]].GetComponent<Mass>().Loot == false)
                {
                    mass[x, way[1]].GetComponent<Mass>().Selecton();
                    walk[1] = mass[x, way[0]].GetComponent<Mass>().walk;
                }
            }
            if (0 <= way[2] && way[2] <= 9)
            {
                if (mass[way[2], y].GetComponent<Mass>().Loot == false)
                {
                    mass[way[2], y].GetComponent<Mass>().Selecton();
                    walk[2] = mass[x, way[0]].GetComponent<Mass>().walk;
                }
            }
            if (0 <= way[3] && way[3] <= 9)
            {
                if (mass[way[3], y].GetComponent<Mass>().Loot == false)
                {
                    mass[way[3], y].GetComponent<Mass>().Selecton();
                    walk[3] = mass[x, way[0]].GetComponent<Mass>().walk;
                }
            }

            //onclickされたら選択マス消してダイスを1減らす
            if(walk[0] == true)
            {
                dice--;
                y = way[0];
                clearSelect();
            }
            if (walk[1] == true)
            {
                dice--;
                y = way[1];
                clearSelect();
            }
            if (walk[2] == true)
            {
                dice--;
                x = way[2];
                clearSelect();
            }
            if (walk[3] == true)
            {
                dice--;
                x = way[3];
                clearSelect();
            }

        } while (dice <= 0);
    }

    private void clearSelect()
    {
        for (int i = 0; i < 14; i++)
        {
            for (int l = 0; l < 10; l++)
            {
                mass[i, l].GetComponent<Mass>().Selectoff();
            }
        }
    }





}
