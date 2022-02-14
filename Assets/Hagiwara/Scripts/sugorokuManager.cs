using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sugorokuManager : MonoBehaviour
{
    private int XGoal, YGoal;//ゴールの座標
    
    public GameObject dice;//ダイスを取得
    public GameObject[] Player = new GameObject[4];//プレイヤーオブジェクト取得
    public days[] week;//Massの縦列のオブジェクトの取得・一番下で二次元配列にしている
    private int Playerturn = 0;//どのプレイヤー番か
    private int step = 0;//プレイヤーのターン手順
    private bool stop; //プレイヤーのターン手順のストッパー
    public float speed = 0.5f;//プレイヤー移動速度
    private float currentTime = 0f;

    private int xplay;//プレイヤーのマス座標を取得
    private int yplay;
    private int Switchnum = 0;//switch構文の切り替え

    private int[] way = new int[4];//マスの上下左右のマス座標 0:上 1:下 2:左 3:右
    private int[] XLoot = new int [10];//移動するマスを入れる(とりあえず最大10マス移動可能)
    private int[] YLoot = new int[10];
    
    int Move = 0;//ダイスの出目
    private int diceconter;//ダイスの残り数
    
    void Start()
    {
        PlayerMass(0, 0, 1);//プレイヤー0をマス(0,1)に移動させる
        PlayerMass(1, 13, 0);//プレイヤー1をマス(0,1)に移動させる
        PlayerMass(2, 0, 9);//プレイヤー2をマス(0,1)に移動させる
        PlayerMass(3, 12, 9);//プレイヤー3をマス(0,1)に移動させる

        GoalDecision();//ゴールの選択

    }


    void Update()
    {

        
        
        switch (step) {
            case 0://ダイスを回す
                Move = 6;//この二行はテスト用
                step = 1;
                if (stop == true)
                {
                    /*
                    Move = dice.GetComponent<imamuraDice>().StopDice();
                    Debug.Log(Move);
                    step = 1;
                    stop = false;
                    */
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
                if (currentTime > speed)
                {
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
        int Week, Day;//ランダムなゴールの場所を入れる
        do {
            Week = Random.Range(0, week.Length);//week・横の列のランダム
            Day = Random.Range(0, week[0].day.Length);//day・縦の列のランダム
        } while (week[Week].day[Day].GetComponent<Mass>().invalid == true);//ランダムに選んだマスが存在しているものを見つけるまで繰り返す
        week[Week].day[Day].GetComponent<Mass>().GoalOn();//ゴールの設置
        XGoal = Day; YGoal = Week;//ゴール配列番号を記憶

    }

    private void GoalAgain()//ゴールの再設置(同じ月にならないように)
    {
        int Week, Day;//ランダムなゴールの場所を入れる
        do{
            Week = Random.Range(0, week.Length);//week・横の列のランダム
            Day = Random.Range(0, week[0].day.Length);//day・縦の列のランダム
        } while (week[Week].day[Day].GetComponent<Mass>().invalid == true && MonthCount(Day, Week) == true);//選んだマスが存在しているもの＆同じ月じゃないものを見つけるまで繰り返す
        week[Week].day[Day].GetComponent<Mass>().GoalOn();//ゴールの設置
        XGoal = Day; YGoal = Week;//ゴール配列番号を記憶
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
        if (x < week[0].day.Length/2 && y < week.Length/2) { Month = 1; }//左上の月にいるかどうか
        if (week[0].day.Length/2 <= x && y < week.Length/2) { Month = 2; }//左上の月にいるかどうか
        if (x < week[0].day.Length/2 && week.Length/2 < y) { Month = 3; }//左上の月にいるかどうか
        if (week[0].day.Length/2 <= x && week.Length/2 < y) { Month = 4; }//左上の月にいるかどうか
        return Month;
         
    }


    private void GoalClear()//全てのマスのゴールを消す
    {
        for (int i = 0; i < week.Length; i++)
        {
            for (int l = 0; l < week[0].day.Length; l++)
            {
                week[i].day[l].GetComponent<Mass>().GoalOff();//ゴールを消していく
            }
        }
    }

    

    private void PlayerMass(int P, int x, int y)//プレイヤーをマス座標移動させる(日付ワープに使える)
    {
        Player[P].transform.position = transform.InverseTransformPoint(week[y].day[x].transform.position);  //指定したマスの上にプレイヤーを移動する
        Player[P].GetComponent<PlayerStatus>().SetPlayerMass(x, y);//プレイヤーがどのマスにいるか記憶する
    }


    public void DiceBotton()//ダイスを止める
    {
        stop = true;
    }

    private void MoveSelect(int Pnum, int dice)//プレイヤーの移動の選択
    {
        
        switch (Switchnum) {
            case 0://移動のための初期設定
                xplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerX();//選択の中心マスを入れる(最初なのでプレイヤーのいるマスを入れる)
                yplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerY();
                diceconter = dice;//移動出来るマスの数を入れる
                XLoot[diceconter] = xplay;//足元のマスを順番に記憶する
                YLoot[diceconter] = yplay;
                week[yplay].day[xplay].GetComponent<Mass>().Decisionon();//プレイヤーの足元を決定マスに変える
                Switchnum = 1;
                break;

            case 1://移動出来るマスを表示する
                way[0] = yplay - 1; way[1] = yplay + 1; way[2] = xplay - 1; way[3] = xplay + 1;//選択の中心マスの四方の座標を入れる 0:上 1:下 2:左 3:右
                for (int i = 0;i < 2; i++)
                {
                    if (0 <= way[i] && way[i] < week.Length && week[way[i]].day[xplay].GetComponent<Mass>().invalid == false && (XLoot[diceconter+1], YLoot[diceconter+1]) != (xplay,way[i]))//選択中心マスの上下にマスは存在して一つ前に選択していないマスか
                    {
                        week[way[i]].day[xplay].GetComponent<Mass>().Selecton();//マスを選択出来るというimageを表示させる
                    }
                }
                for (int i = 2; i < 4; i++)
                {
                    if (0 <= way[i] && way[i] < week[0].day.Length && week[yplay].day[way[i]].GetComponent<Mass>().invalid == false && (XLoot[diceconter + 1], YLoot[diceconter + 1]) != (way[i],yplay))//選択中心マスの左右にマスは存在して一つ前に選択していないマスか
                    {
                        week[yplay].day[way[i]].GetComponent<Mass>().Selecton();//マスを選択出来るというimageを表示させる
                    }
                }
                if ((xplay,yplay) == (0,1) || (xplay, yplay) == (13, 0) || (xplay, yplay) == (0, 9) || (xplay, yplay) == (12, 9)) {//選択中心マスがワープマスにある時に反応
                    week[1].day[0].GetComponent<Mass>().Selecton();
                    week[0].day[13].GetComponent<Mass>().Selecton();
                    week[9].day[0].GetComponent<Mass>().Selecton();
                    week[9].day[12].GetComponent<Mass>().Selecton();
                }
                week[yplay].day[xplay].GetComponent<Mass>().Selectoff();
                Switchnum = 2;
                break;

            case 2://選択出来るマスがクリックされたその反応

                for (int i = 0; i < 2; i++)
                {
                    if (0 <= way[i] && way[i] < week.Length && week[way[i]].day[xplay].GetComponent<Mass>().walk == true)//選択中心マスの上下にマスは存在してクリックされたか
                    {
                        diceconter--;//移動出来るマス数を一つ減らす
                        yplay = way[i];//選択中心マスをクリックしたマスに移す
                        XLoot[diceconter] = xplay;//移動決定したマスを順番に記憶する
                        YLoot[diceconter] = yplay;
                        clearSelect();//選択できるマスの全消去
                    }
                }
                
                for (int i = 2; i < 4; i++)
                {
                    if (0 <= way[i] && way[i] < week[0].day.Length && week[yplay].day[way[i]].GetComponent<Mass>().walk == true)//選択中心マスの左右にマスは存在してクリックされたか
                    {
                        diceconter--;//移動出来るマス数を一つ減らす
                        xplay = way[i];//選択中心マスをクリックしたマスに移す
                        XLoot[diceconter] = xplay;//移動決定したマスを順番に記憶する
                        YLoot[diceconter] = yplay;
                        clearSelect();//選択できるマスの全消去
                    }
                }
                Warpdecision(0, 1);//右上ワープが選択された時に反応
                Warpdecision(13, 0);//左上ワープが選択された時に反応
                Warpdecision(0, 9);//右下ワープが選択された時に反応
                Warpdecision(12, 9);//左下ワープが選択された時に反応

                if (diceconter > 0)
                {
                    Switchnum = 1;
                }
                else
                {
                    Switchnum = 0;
                    Debug.Log("選択終了");
                    stop = true;
                }
                
                break;
        }
    }

    

    private void Warpdecision(int x,int y)//ワープ先を選択した時
    {
        if (week[y].day[x].GetComponent<Mass>().walk == true)
        {
            diceconter--;//移動出来るマス数を一つ減らす
            xplay = x;//選択中心マスをクリックしたマスに移す
            yplay = y;
            XLoot[diceconter] = xplay;//移動決定したマスを順番に記憶する
            YLoot[diceconter] = yplay;
            clearSelect();//選択できるマスの全消去
        }
    }

    private void clearSelect()//選択できるマスの全消去
    {
        for (int i = 0; i < week.Length; i++)
        {
            for (int l = 0; l < week[0].day.Length; l++)
            {
                week[i].day[l].GetComponent<Mass>().Selectoff();//マスを選択出来るというimageを消す
                week[i].day[l].GetComponent<Mass>().walk = false;//クリックされたという判定を消す
            }
        }
    }
    
    private void MovePlayer(int Pnum)//プレイヤーの移動
    {
        int oneLoot = 0;//そのマスが移動の際一回しか通らないならtrue
        switch (Switchnum)
        {
            case 0:
                xplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerX();//プレイヤーのマス座標
                yplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerY();
                diceconter = Move;
                Switchnum = 1;
                break;

            case 1:
                for(int i=0;i< Move+1; i++)//移動順番のマスがもう一度同じマスを通らないならoneLootがMove-1になる
                {
                    if ((xplay,yplay) != (XLoot[i],YLoot[i]))
                    {
                        oneLoot++;
                    }
                }
                if(Move == oneLoot)//移動マスが同じマスを通らないなら決定マスが消える
                {
                    week[yplay].day[xplay].GetComponent<Mass>().Decisionoff();//足元の決定マス消去
                }
                else
                {
                    XLoot[diceconter] = -1;//すでに通ったところが反応しないようにする
                    YLoot[diceconter] = -1;
                }
                diceconter--;//移動するマス目数を一つ減らす
                PlayerMass(Pnum, XLoot[diceconter], YLoot[diceconter]);//プレイヤーをLootに記憶させた順番に移動させる
                
                if (xplay == XLoot[diceconter] && yplay > YLoot[diceconter]) { Debug.Log("上"+diceconter); }//上に移動の時に反応(アニメーション用？)
                if (xplay == XLoot[diceconter] && yplay < YLoot[diceconter]) { Debug.Log("下" + diceconter); }
                if (xplay > XLoot[diceconter] && yplay == YLoot[diceconter]) { Debug.Log("左" + diceconter); }
                if (xplay < XLoot[diceconter] && yplay == YLoot[diceconter]) { Debug.Log("右" + diceconter); }
                
                xplay = XLoot[diceconter];//プレイヤーのいるマスを記憶
                yplay = YLoot[diceconter];

                if (diceconter == 0)
                {
                    Debug.Log("終わってる");
                    week[yplay].day[xplay].GetComponent<Mass>().Decisionoff();//足元の決定マス消去
                    Switchnum = 0;
                    stop = true;
                }
                break;
        }
        
    }

}
[System.Serializable]
public class days//weekの子・横列のオブジェクトの取得
{
    public GameObject[] day;
}