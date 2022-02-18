using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatus : MonoBehaviour
{
    private int PlayerNumber;//プレイヤーの番号
    public string Name;//名前
    public List<string> HabItem;//持っているアイテム
    private int Goalcount = 0;//ゴールした数
    private int PX,PY;//プレイヤーのマス座標
    public GameObject Play;
    
    [SerializeField]
    private Dropdown dropdown;

    public GameObject dice;                         //ダイスを取得
    private bool dicestart = true;                  //ダイスを回す

    public int initialX, initialY;
    public days[] week;                             //Massの縦列のオブジェクトの取得・一番下で二次元配列にしている
    public int step = 0;                            //プレイヤーのターン手順
    private bool stop;                              //プレイヤーのターン手順のストッパー
    public float speed = 0.5f;                      //プレイヤー移動速度
    private float currentTime = 0f;
    public bool nextturn;                           //次のプレイヤーの番にする
    public bool Goalup;                             //自分のターンにゴールしたという宣言

    private int xplay;                              //選択したマス座標を取得
    private int yplay;
    private int Switchnum = 0;                      //switch構文の切り替え

    private int[] way;                //マスの上下左右のマス座標 0:上 1:下 2:左 3:右
    private int[] XLoot;              //移動するマスを入れる(とりあえず最大10マス移動可能)
    private int[] YLoot;

    private int Move = 0;                           //ダイスの出目
    private int diceconter;


    void Start()
    {
        dicestart = true;                        //初期化
        way = new int[4];
        XLoot = new int[10];
        YLoot = new int[10];
        PlayerMass(initialX, initialY);         //プレイヤーを初期位置にに
    }

    
    void Update()
    {
        switch (step)
        {
            case 0:
                //動かない状態
                stop = false;
                break;

            case 1://ダイスを回す
                if (dicestart) {                                    //一回しか反応しない
                    dice.GetComponent<imamuraDice>().OnDiceSpin();  //ダイスを回す
                    dicestart = false;
                }
                
                if (stop == true)                                   //ストップを押されたら
                {
                    Move = dice.GetComponent<imamuraDice>().StopDice();//ダイスを止める
                    Debug.Log(Move);
                    step = 2;
                    stop = false;
                    dicestart = true;
                }
                break;

            case 2://ダイスのマス分移動出来るところを設定する
                MoveSelect(Move);                   //マスの選択
                if (stop == true)                   //選択が終了したら
                {
                    step = 3;
                    stop = false;
                }
                break;

            case 3://プレイヤーの移動

                currentTime += Time.deltaTime;      //プレイヤーの移動が一歩ずつ進むように
                if (currentTime > speed)
                {
                    MovePlayer();                   //一歩進める
                    currentTime = 0f;
                }

                if (stop == true)                   //移動が終了したら
                {
                    step = 4;
                    stop = false;
                }
                break;

            case 4://ゴール＆マスの効果
                if (week[yplay].day[xplay].GetComponent<Mass>().Goal == true)   //もしゴールマスに止まったら
                {
                    Goaladd();                                                  //ゴール数を1上げる
                    Itemobtain("ゴール");                                       //ドロップダウンにゴールを追加
                    Goalup = true;                                              //ゴールをした際の宣言
                    stop = true;
                }
                if (week[yplay].day[xplay].GetComponent<Mass>().Open == false)  //止まったマスが空いていなかったら
                {
                    GetComponent<MassEffect>().Effects( week[yplay].day[xplay].GetComponent<Mass>().Day);//マスの効果の発動
                    week[yplay].day[xplay].GetComponent<Mass>().Open = true;    //マスを開けた状態にする
                }
                
                if (stop == true)                      //マスの処理が終了したら
                {
                    step = 5;
                    stop = false;
                }
                break;

            case 5://次の人の番に
                nextturn = true;        //プレイヤーのターンを終了する
                step = 0;
                break;
        }
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

    public void Itemobtain(string Item)//こいつをアイテムの名前で呼ぶとドロップダウンに入る
    {
        HabItem.Add(Item);
        dropdown.options.Add(new Dropdown.OptionData { text = Item + DictionaryManager.ItemDictionary[Item][0] + "P" });
        dropdown.RefreshShownValue();
    }

    public void ItemInfoGet(string Item)
    {
        Debug.Log(HabItem[0]);

        //  Debug.Log(Item+ItemDectionari.ItemDictionary[Item]);

        // Play.ItemDectionari.DectionariyInfo(Item);
        Debug.Log(DictionaryManager.ItemDictionary[Item][0]);


    }

    public void Itemadd(string IName)//アイテムの取得
    {
        HabItem.Add(IName);

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
        return HabItem[num];
    }
    /*
    public int GetItemPoint(int num)//持っているアイテムのポイント
    {
        return ItemPoint[num];
    }
    */
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
    

    private void MoveSelect(int dice)//プレイヤーの移動の選択
    {

        switch (Switchnum)
        {
            case 0://移動のための初期設定
                xplay = PlayerX();//選択の中心マスを入れる(最初なのでプレイヤーのいるマスを入れる)
                yplay = PlayerY();
                diceconter = dice;//移動出来るマスの数を入れる
                XLoot[diceconter] = xplay;//足元のマスを順番に記憶する
                YLoot[diceconter] = yplay;
                week[yplay].day[xplay].GetComponent<Mass>().Decisionon();//プレイヤーの足元を決定マスに変える
                Switchnum = 1;
                break;

            case 1://移動出来るマスを表示する
                way[0] = yplay - 1; way[1] = yplay + 1; way[2] = xplay - 1; way[3] = xplay + 1;//選択の中心マスの四方の座標を入れる 0:上 1:下 2:左 3:右
                for (int i = 0; i < 2; i++)
                {
                    if (0 <= way[i] && way[i] < week.Length && week[way[i]].day[xplay].GetComponent<Mass>().invalid == false && (XLoot[diceconter + 1], YLoot[diceconter + 1]) != (xplay, way[i]))//選択中心マスの上下にマスは存在して一つ前に選択していないマスか
                    {
                        week[way[i]].day[xplay].GetComponent<Mass>().Selecton();//マスを選択出来るというimageを表示させる
                    }
                }
                for (int i = 2; i < 4; i++)
                {
                    if (0 <= way[i] && way[i] < week[0].day.Length && week[yplay].day[way[i]].GetComponent<Mass>().invalid == false && (XLoot[diceconter + 1], YLoot[diceconter + 1]) != (way[i], yplay))//選択中心マスの左右にマスは存在して一つ前に選択していないマスか
                    {
                        week[yplay].day[way[i]].GetComponent<Mass>().Selecton();//マスを選択出来るというimageを表示させる
                    }
                }
                if ((xplay, yplay) == (0, 1) || (xplay, yplay) == (13, 0) || (xplay, yplay) == (0, 9) || (xplay, yplay) == (12, 9))
                {//選択中心マスがワープマスにある時に反応
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
                Warpdecision(0, 1); //右上ワープが選択された時に反応
                Warpdecision(13, 0);//左上ワープが選択された時に反応
                Warpdecision(0, 9); //右下ワープが選択された時に反応
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

    private void Warpdecision(int x, int y)//ワープ先を選択した時
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

    private void MovePlayer()//プレイヤーの移動
    {
        int oneLoot = 0;//そのマスが移動の際一回しか通らないならtrue
        switch (Switchnum)
        {
            case 0:
                xplay = PlayerX();//プレイヤーのマス座標
                yplay = PlayerY();
                diceconter = Move;
                Switchnum = 1;
                break;

            case 1:
                for (int i = 0; i < Move + 1; i++)//移動順番のマスがもう一度同じマスを通らないならoneLootがMove-1になる
                {
                    if ((xplay, yplay) != (XLoot[i], YLoot[i]))
                    {
                        oneLoot++;
                    }
                }
                if (Move == oneLoot)//移動マスが同じマスを通らないなら決定マスが消える
                {
                    week[yplay].day[xplay].GetComponent<Mass>().Decisionoff();//足元の決定マス消去
                }
                else
                {
                    XLoot[diceconter] = -1;//すでに通ったところが反応しないようにする
                    YLoot[diceconter] = -1;
                }
                diceconter--;//移動するマス目数を一つ減らす
                PlayerMass(XLoot[diceconter], YLoot[diceconter]);//プレイヤーをLootに記憶させた順番に移動させる

                if (xplay == XLoot[diceconter] && yplay > YLoot[diceconter]) { Debug.Log("上" + diceconter); }//上に移動の時に反応(アニメーション用？)
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

    private void PlayerMass(int x, int y)//プレイヤーをマス座標移動させる(日付ワープに使える)
    {
        transform.position = week[y].day[x].transform.position;//指定したマスの上にプレイヤーを移動する
        SetPlayerMass(x, y);//プレイヤーがどのマスにいるか記憶する
    }


    public void stopon()//ダイスを止める
    {
        stop = true;
    }

}


[System.Serializable]
public class days//weekの子・横列のオブジェクトの取得
{
    public GameObject[] day;
}