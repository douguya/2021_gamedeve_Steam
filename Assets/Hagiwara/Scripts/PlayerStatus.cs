using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;

using Photon.Realtime;
using System.Threading.Tasks;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerStatus : MonoBehaviourPunCallbacks
{
    private int PlayerNumber;//プレイヤーの番号
    public string Name;//名前
    public List<string> HabItem;//持っているアイテム
    private int Goalcount = 0;//ゴールした数
    private int PX, PY;//プレイヤーのマス座標

    int[,] Position;


    private TextMeshPro nameLabel = default;

    //private ProjectileManager projectileManager;
    public GameObject Play;

    [SerializeField]
   
    private Dropdown dropdown;
    public int PlayerIdVew;　　 //プレイヤーのID
    public string PlayerNameVew;//プレイヤーの名前
    public int HowPlayer;       //何人プレイヤーがいるかの閲覧用
    public Button Botton;　　　 //動作テスト用ボタンへのアクセス用
    public Button StopDiceButton;    //動作テスト用ボタンへのアクセス用
    public Hashtable hashPlayStatus;
   


    public GameObject dice;                         //ダイスを取得
    private bool dicestart = true;                  //ダイスを回す

    // public int initialX, initialY;
    public Width[] week;                             //Massの縦列のオブジェクトの取得・一番下で二次元配列にしている
    public int step = 0;                            //プレイヤーのターン手順
    private bool stop;                              //プレイヤーのターン手順のストッパー
    public float speed = 0.5f;                      //プレイヤー移動速度
    private float currentTime = 0f;
    public bool nextturn;                           //次のプレイヤーの番にする
    public bool Goalup;                             //自分のターンにゴールしたという宣言
    public GameObject Gamemanager;
    private int xplay;                              //選択したマス座標を取得
    private int yplay;
    private int Switchnum = 0;                      //switch構文の切り替え

    private int[] way;                //マスの上下左右のマス座標 0:上 1:下 2:左 3:右
    private int[] XLoot;              //移動するマスを入れる(とりあえず最大10マス移動可能)
    private int[] YLoot;

    private int Move = 0;                           //ダイスの出目
    private bool Selecting = false;
    private bool Moving = false;
    public bool Effecting = false;

    private int diceconter;
    private bool CanSelect=false;

    public SpriteRenderer PlayerSpriteRenderer;
    public  Sprite PlayerSprite;
    public GameObject Dcomment;
  



    private void Awake()
    {
        hashPlayStatus = new Hashtable();


        Debug.Log("Awake:"+hashPlayStatus);
    }

    void Start()
    {

        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();

        Dcomment = GameObject.Find("DayComment");

        nameLabel = transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();

            nameLabel.text = photonView.Owner.NickName;


        // Debug.Log(Position.GetLength());
        Gamemanager = GameObject.Find("GameControl");
        PlayerIdVew = photonView.OwnerActorNr;　　//プレイヤーのIDの同期
        PlayerNameVew = photonView.Owner.NickName;//プレイヤーの名前の同期
        SetPlayernumShorten();                    //アイテムリストUIとプレイヤーの同期



        dice = GameObject.Find("Dice");
        if (photonView.IsMine)//PListがプレイヤーのものなら
        {
            Debug.Log(hashPlayStatus);
            hashPlayStatus["nextturn"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashPlayStatus);
            Debug.Log("Start:" + hashPlayStatus);

            StopDiceButton = GameObject.Find("Stop").GetComponent<Button>();//テスト用ボタンへのアクセス用
            StopDiceButton.GetComponent<Button>().onClick.AddListener(StopDice);//テスト用ボタンへの関数追加 (インスペクターには映らない)
        }

    
        Position = new int[4, 2] {
         {0,0} ,
         {13,0},
         {0,9},
         {0,9},
        };

        for (int loop = 0; loop < 10; loop++)
        {
            week[loop] = Gamemanager.GetComponent<sugorokuManager>().height[loop];

            for (int loop2=0;loop2<14;loop2++) {
                week[loop].width[loop2].GetComponent<Button>().onClick.AddListener(() => MoveSelect_Clicked()); ;

            }


        }
        dicestart = true;                        //初期化
        way = new int[4];
        XLoot = new int[10];
        YLoot = new int[10];
        // PlayerMass(initialX, initialY);         //プレイヤーを初期位置にに
      //  nameLabel.text ="asd";
    }


    private void Update()
    {
        
    }





    public void TurnDice()//ダイスを回す
    {
        

                                //一回しか反応しない
        dice.GetComponent<imamuraDice>().OnDiceSpin();  //ダイスを回す
            dicestart = false;
       



       // Debug.Log(Move);
            step = 2;
            stop = false;
            dicestart = true;
        

    }
 









    public void StopDice()//ダイスを止める ボタンクリックで起動する。
    {
        Move = dice.GetComponent<imamuraDice>().StopDice();//ダイスを止める

        MoveSelect_Setting(Move);
    }


 
   

    public void GoalAnniversaries()//止まった時の処理
    {
        Effecting = true;

     
            if (week[yplay].width[xplay].GetComponent<Mass>().Goal == true)   //もしゴールマスに止まったら
            {
               
                Goaladd();                                                  //ゴール数を1上げる
                Itemobtain("ゴール");                                       //ドロップダウンにゴールを追加
                Goalup = true;                                              //ゴールをした際の宣言
                stop = true;
            }
            if (week[yplay].width[xplay].GetComponent<Mass>().Open == false)  //止まったマスが空いていなかったら
            {
                 
                // Debug.Log("WWWWWWWWWWWWWWWW"+week[yplay].width[xplay].GetComponent<Mass>().Day);
                GetComponent<MassEffect>().Effects(week[yplay].width[xplay].GetComponent<Mass>().Day);//マスの効果の発動
                week[yplay].width[xplay].GetComponent<Mass>().Open = true;    //マスを開けた状態にする
              
            }

        

      
        //  TurnEnd();
        Gamemanager.GetComponent<sugorokuManager>().AfterMoving();
    }
    public void TurnEnd()
    {

        hashPlayStatus["nextturn"] = false;//カスタムプロパティのセッティング　初手なのでfalse
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashPlayStatus);//更新

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

    //  MoveSelect =============================================================================================================================
    public void MoveSelect_Setting(int dice)//プレイヤーの移動の選択の準備
    {
        xplay = PlayerX();//選択の中心マスを入れる(最初なのでプレイヤーのいるマスを入れる)
        yplay = PlayerY();
        diceconter = dice;//移動出来るマスの数を入れる
        XLoot[diceconter] = xplay;//足元のマスを順番に記憶する
        YLoot[diceconter] = yplay;
        week[yplay].width[xplay].GetComponent<Mass>().Decisionon();//プレイヤーの足元を決定マスに変える
        Switchnum = 1;

        MoveSelect_Display();
      
    }





    public void MoveSelect_Display()//移動出来るマスを表示する
    {

        way[0] = yplay - 1; way[1] = yplay + 1; way[2] = xplay - 1; way[3] = xplay + 1;//選択の中心マスの四方の座標を入れる 0:上 1:下 2:左 3:右
        for (int i = 0; i < 2; i++)
        {
            if (0 <= way[i] && way[i] < week.Length && week[way[i]].width[xplay].GetComponent<Mass>().invalid == false && (XLoot[diceconter + 1], YLoot[diceconter + 1]) != (xplay, way[i]))//選択中心マスの上下にマスは存在して一つ前に選択していないマスか
            {
                week[way[i]].width[xplay].GetComponent<Mass>().Selecton();//マスを選択出来るというimageを表示させる
            }
        }
        for (int i = 2; i < 4; i++)
        {
            if (0 <= way[i] && way[i] < week[0].width.Length && week[yplay].width[way[i]].GetComponent<Mass>().invalid == false && (XLoot[diceconter + 1], YLoot[diceconter + 1]) != (way[i], yplay))//選択中心マスの左右にマスは存在して一つ前に選択していないマスか
            {
                week[yplay].width[way[i]].GetComponent<Mass>().Selecton();//マスを選択出来るというimageを表示させる
            }
        }
        if ((xplay, yplay) == (0, 0) || (xplay, yplay) == (13, 0) || (xplay, yplay) == (0, 9) || (xplay, yplay) == (12, 9))
        {//選択中心マスがワープマスにある時に反応

            week[1].width[0].GetComponent<Mass>().Selecton();
            week[0].width[13].GetComponent<Mass>().Selecton();
            week[9].width[0].GetComponent<Mass>().Selecton();
            week[9].width[12].GetComponent<Mass>().Selecton();
        }
        week[yplay].width[xplay].GetComponent<Mass>().Selectoff();
        Switchnum = 2;

        CanSelect = true;

    }








    public void MoveSelect_Clicked()//プレイヤーの移動の選択
    {
        if (CanSelect == true) {
            CanSelect = false;
            for (int i = 0; i < 2; i++)
            {
                //Debug.Log("aaaaaaaaaaaaa0 <" + way[i] + "&& " + way[i] + "<" + week.Length + "&&" + " week[way[i]]+.width[xplay].GetComponent<Mass>().walk ");

                if (0 <= way[i] && way[i] < week.Length && week[way[i]].width[xplay].GetComponent<Mass>().walk == true)//選択中心マスの上下にマスは存在してクリックされたか
                {
                    Debug.Log("aaaaaaaaaaaaa");
                    diceconter--;//移動出来るマス数を一つ減らす
                    yplay = way[i];//選択中心マスをクリックしたマスに移す
                    XLoot[diceconter] = xplay;//移動決定したマスを順番に記憶する
                    YLoot[diceconter] = yplay;
                    clearSelect();//選択できるマスの全消去
                }
            }

            for (int i = 2; i < 4; i++)
            {
                if (0 <= way[i] && way[i] < week[0].width.Length && week[yplay].width[way[i]].GetComponent<Mass>().walk == true)//選択中心マスの左右にマスは存在してクリックされたか
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
                //Debug.Log("99999999999999999999999999999999999999999999999999999999999999999999999999999999999999");
                MoveSelect_Display();
            }
            else
            {
                Switchnum = 0;
                Debug.Log("選択終了");
                PlayerMovement();
            }
        }
        
    }


    //  =============================================================================================================================MoveSelect
    /*

    private void MoveSelect(int dice)//プレイヤーの移動の選択
    {
        Debug.Log("GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG"+"  Start");

        switch (Switchnum)
        {
            case 0://移動のための初期設定
                Debug.Log("GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG" + "S"+ Switchnum);
                xplay = PlayerX();//選択の中心マスを入れる(最初なのでプレイヤーのいるマスを入れる)
                yplay = PlayerY();
                diceconter = dice;//移動出来るマスの数を入れる
                XLoot[diceconter] = xplay;//足元のマスを順番に記憶する
                YLoot[diceconter] = yplay;
                week[yplay].width[xplay].GetComponent<Mass>().Decisionon();//プレイヤーの足元を決定マスに変える
                Switchnum = 1;
                Debug.Log("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH" + "E" + Switchnum);
                break;

            case 1://移動出来るマスを表示する
                Debug.Log("IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII" + "S" + Switchnum);
                way[0] = yplay - 1; way[1] = yplay + 1; way[2] = xplay - 1; way[3] = xplay + 1;//選択の中心マスの四方の座標を入れる 0:上 1:下 2:左 3:右
                for (int i = 0; i < 2; i++)
                {
                    if (0 <= way[i] && way[i] < week.Length && week[way[i]].width[xplay].GetComponent<Mass>().invalid == false && (XLoot[diceconter + 1], YLoot[diceconter + 1]) != (xplay, way[i]))//選択中心マスの上下にマスは存在して一つ前に選択していないマスか
                    {
                        week[way[i]].width[xplay].GetComponent<Mass>().Selecton();//マスを選択出来るというimageを表示させる
                    }
                }
                for (int i = 2; i < 4; i++)
                {
                    if (0 <= way[i] && way[i] < week[0].width.Length && week[yplay].width[way[i]].GetComponent<Mass>().invalid == false && (XLoot[diceconter + 1], YLoot[diceconter + 1]) != (way[i], yplay))//選択中心マスの左右にマスは存在して一つ前に選択していないマスか
                    {
                        week[yplay].width[way[i]].GetComponent<Mass>().Selecton();//マスを選択出来るというimageを表示させる
                    }
                }
                if ((xplay, yplay) == (0, 0) || (xplay, yplay) == (13, 0) || (xplay, yplay) == (0, 9) || (xplay, yplay) == (12, 9))
                {//選択中心マスがワープマスにある時に反応

                    week[1].width[0].GetComponent<Mass>().Selecton();
                    week[0].width[13].GetComponent<Mass>().Selecton();
                    week[9].width[0].GetComponent<Mass>().Selecton();
                    week[9].width[12].GetComponent<Mass>().Selecton();
                }
                week[yplay].width[xplay].GetComponent<Mass>().Selectoff();
                Switchnum = 2;
                Debug.Log("JJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJ" + "E" + Switchnum);
                break;

            case 2://選択出来るマスがクリックされたその反応
                Debug.Log("kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk" + "S" + Switchnum);
                for (int i = 0; i < 2; i++)
                {
                    Debug.Log("aaaaaaaaaaaaa0 <" + way[i] + "&& " + way[i] + "<" + week.Length + "&&" + " week[way[i]]+.width[xplay].GetComponent<Mass>().walk ");

                    if (0 <= way[i] && way[i] < week.Length && week[way[i]].width[xplay].GetComponent<Mass>().walk == true)//選択中心マスの上下にマスは存在してクリックされたか
                    {
                        Debug.Log("aaaaaaaaaaaaa");
                        diceconter--;//移動出来るマス数を一つ減らす
                        yplay = way[i];//選択中心マスをクリックしたマスに移す
                        XLoot[diceconter] = xplay;//移動決定したマスを順番に記憶する
                        YLoot[diceconter] = yplay;
                        clearSelect();//選択できるマスの全消去
                    }
                }

                for (int i = 2; i < 4; i++)
                {
                    if (0 <= way[i] && way[i] < week[0].width.Length && week[yplay].width[way[i]].GetComponent<Mass>().walk == true)//選択中心マスの左右にマスは存在してクリックされたか
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
                    Selecting = false;
                }
                break;
        }
        Debug.Log("LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL" + "E" + Switchnum);
    }





    */



    private void Warpdecision(int x, int y)//ワープ先を選択した時
    {
        if (week[y].width[x].GetComponent<Mass>().walk == true)
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
            for (int l = 0; l < week[0].width.Length; l++)
            {
                week[i].width[l].GetComponent<Mass>().Selectoff();//マスを選択出来るというimageを消す
                week[i].width[l].GetComponent<Mass>().walk = false;//クリックされたという判定を消す
            }
        }
    }









    public async void PlayerMovement()//移動
    {

        Moving = true;
        while (Moving)
        {
            currentTime += Time.deltaTime;      //プレイヤーの移動が一歩ずつ進むように
            if (currentTime > speed)
            {
                MovePlayer();                   //一歩進める
                currentTime = 0f;
                await Task.Delay(500);
            }
        }

        GoalAnniversaries();

    }




    












    private  void MovePlayer()//プレイヤーの移動
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
                    week[yplay].width[xplay].GetComponent<Mass>().Decisionoff();//足元の決定マス消去
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



              //  await Task.Delay(50);

                if (diceconter == 0)
                {
                    Debug.Log("終わってる");
                    week[yplay].width[xplay].GetComponent<Mass>().Decisionoff();//足元の決定マス消去
                    Switchnum = 0;
                    Moving = false;
                }
                break;
        }

    }

    public void PlayerMass(int x, int y)//プレイヤーをマス座標移動させる(日付ワープに使える)
    {
        transform.position = week[y].width[x].transform.position;//指定したマスの上にプレイヤーを移動する
        SetPlayerMass(x, y);//プレイヤーがどのマスにいるか記憶する
    }

    //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝


    public async void SetPlayernumShorten()//アイテムリストUIの同期
    {
        await Task.Delay(50);//一気に複数のプレイヤーとアイテムリストの同期をするための一時停止

 
        int loop = 1;//アイテムリストの初期値
        foreach (var PList in PhotonNetwork.PlayerList)//プレイヤーリストの内容を順番に格納
        {
            if (photonView.CreatorActorNr == PList.ActorNumber)//自分の作成者のIDがPListのIDとイコールなら
            {
               // dropdown.ClearOptions();//移行前のリスト消去
                dropdown = GameObject.Find("Dropdown:Player" + loop).GetComponent<Dropdown>();//プレイヤーの順番に対応したアイテムリストUIとの同期
                dropdown.ClearOptions();//同期したアイテムリストの初期化

                dropdown.options.Add(new Dropdown.OptionData { text = "" + PlayerNameVew });//アイテムリストのラベル付け
                dropdown.RefreshShownValue();//アイテムリストUIの更新

              //  Debug.Log("aaaaaaaaaaaaaaa" + Position[loop - 1, 0]+ Position[loop - 1, 1]);
                PlayerMass(Position[loop-1, 0], Position[loop-1,1]);
                this.name = "Player" + (loop-1);
              //  Debug.Log("aaaaaaaaaaaaaaa" + this.name);
                Gamemanager.GetComponent<sugorokuManager>().Player[loop-1] = this.gameObject;






                //テスト用ボタンのセッティング
          
            }
            loop++;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)//他のプレイヤーがいなくなった時のコールバック
    {

        //アイテムリストUIの更新のための全体初期化
        for (int loop = 1; loop < 5; loop++)
        {
            GameObject.Find("Dropdown:Player" + loop).GetComponent<Dropdown>().ClearOptions();//削除
            GameObject.Find("Dropdown:Player" + loop).GetComponent<Dropdown>().RefreshShownValue();//更新

        }

        SetPlayernumShorten();//改めてのアイテムリストUIの同期
    }
    public override void OnJoinedRoom()//自身がルームに入ったとき
    {
        hashPlayStatus["nextturn"] = false;//カスタムプロパティのセッティング　
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashPlayStatus);//更新        /
   
    
    }

    public void TurnUp()
    {

        hashPlayStatus["nextturn"] = true;//カスタムプロパティのセッティング　初手なのでfalse
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashPlayStatus);//更新

    }


    /*
    public override void OnPlayerPropertiesUpdate(Player player, Hashtable propertiesThatChanged)
    {
        foreach (var p in PhotonNetwork.PlayerList)//プレイヤー全員のカスタムプロパティ：準備状態の集計
        {

            Debug.Log((bool)p.CustomProperties["nextturn"]);
        }

      



    }
    */
       public bool returnhash()
   {

     
        bool kariy= (bool)PhotonNetwork.LocalPlayer.CustomProperties["nextturn"];
        return kariy;// カスタムプロパティのセッティング　初手なのでfalse
        // PhotonNetwork.LocalPlayer.SetCustomProperties(hashPlayStatus);//更新

    }






    public void Itemobtain(string Item)//アイテムを手に入れた場合の関数を呼び出す
    {
        photonView.RPC(nameof(ItemobtainToRPC), RpcTarget.All, Item);
    }



   [PunRPC]
    public void ItemobtainToRPC(string Item)//アイテムを手に入れた場合の関数
    {
        HabItem.Add(Item);//Itemのリストへの追加
        Debug.Log("AAAA"+Item);
        dropdown.options.Add(new Dropdown.OptionData { text = Item + DictionaryManager.ItemDictionary[Item][0] + "P" });//アイテムとそのポイントをアイテムリストUIに追加
        dropdown.RefreshShownValue();//アイテムリストUIの更新
    }


    public void IconChange()
    {
        photonView.RPC(nameof(iconChange), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber);
    }
    
    [PunRPC]
    public void iconChange(int PID)
    {

        if ((int)photonView.CreatorActorNr == PID)
        {
            //PlayerSprite = 
            PlayerSpriteRenderer.sprite = PlayerSprite;
        }
    }


    public void daycomment(string Day)
    {
        photonView.RPC(nameof(Daycomment), RpcTarget.All, Day);
    }

    [PunRPC]
    public void Daycomment(string Day)
    {
        
            Dcomment.GetComponent<DayComment>().DayCommenton(Day);
        
    }



    /*

    void Update()
    {




            switch (step)
            {
                case 0:
                    //動かない状態
                    stop = false;
                    break;

                case 1://ダイスを回す
                    if (dicestart)
                    {                                    //一回しか反応しない
                        dice.GetComponent<imamuraDice>().OnDiceSpin();  //ダイスを回す
                        dicestart = false;
                    }

                    if (stop == true)                                   //ストップを押されたら
                    {
                      //  Move = dice.GetComponent<imamuraDice>().StopDice();//ダイスを止める
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
                    if (week[yplay].width[xplay].GetComponent<Mass>().Goal == true)   //もしゴールマスに止まったら
                    {
                        Goaladd();                                                  //ゴール数を1上げる
                        Itemobtain("ゴール");                                       //ドロップダウンにゴールを追加
                        Goalup = true;                                              //ゴールをした際の宣言
                        stop = true;
                    }
                    if (week[yplay].width[xplay].GetComponent<Mass>().Open == false)  //止まったマスが空いていなかったら
                    {
                       // Debug.Log("WWWWWWWWWWWWWWWW"+week[yplay].width[xplay].GetComponent<Mass>().Day);
                        GetComponent<MassEffect>().Effects(week[yplay].width[xplay].GetComponent<Mass>().Day);//マスの効果の発動
                        week[yplay].width[xplay].GetComponent<Mass>().Open = true;    //マスを開けた状態にする
                    }

                    if (stop == true)                      //マスの処理が終了したら
                    {
                        step = 5;
                        stop = false;
                    }
                    break;

                case 5://次の人の番に

                hashPlayStatus["nextturn"] = true;//カスタムプロパティのセッティング　
                PhotonNetwork.LocalPlayer.SetCustomProperties(hashPlayStatus);//更新        //プレイヤーのターンを終了する
                step = 0;
                    break;
            }
        
        
    }
    */

}




