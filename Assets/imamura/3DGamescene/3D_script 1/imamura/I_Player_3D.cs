using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class I_Player_3D : MonoBehaviourPunCallbacks
{
    public int PlayerNumber;                      //プレイヤー番号


    public int XPlayer_position;                  //プレイヤーの現在の横の位置
    public int YPlayer_position;                  //プレイヤーの現在の縦の位置

    int Xcenter, Ycenter;                         //選択できるマスの中心マス

    public List<Anniversary_Item> Hub_Items = new List<Anniversary_Item>();

    public int Move_Point = 0;                    //プレイヤーの移動できる歩数 
    private int select_Point = 0;                 //マスを選択できる数
    private bool[] Player_warpMove = new bool[51];//プレイヤーの移動方法

    public int Goalcount = 0;

    private int[] XPlayer_Loot = new int[51];     //選択したマスを記憶する
    private int[] YPlayer_Loot = new int[51];


    public GameObject GameManager;                //GameManagerオブジェクトの取得
    private I_game_manager Manager;                 //I_game_managerを取得

    public GameObject DiceButton;                           //ダイスを止める為のオブジェクト取得
    public GameObject ButtonText;                           //ダイスのテキストオブジェクト取得
    private bool DiceStrat = true;                          //ボタンがダイスの開始かストップか
    public bool Effect;                           //エフェクトによる移動かどうか 

    public bool Exchange;                         //場所交換するかどうか 
    public bool Turn_change;                      //ターンを変えるかどうか 
    public int OneMore_Dice;

    public int DiceAdd = 0;

    public int DiceMultiply = 0;
    public int MoveAdd_point = 0;
    public int MoveStop_point = 0;
    private int MovePoint_Count = 0;
    public bool selectwark;
    private bool Effect_ready;

    private bool Effect_start = true;

    private bool MoveStop_Push;

    public bool Guide_on = true;
    private bool Guide_one = true;

    private bool OneMore;

    // 以下MannequinPlayer空の引用=====================================================================
    public Anniversary_Item_Master ItemMaster;
    public GameObject ItemBlock;//アイテムリストのUGI

    // =====================================================================

    private void Awake()
    {
        GameManager= GameObject.FindWithTag("GameController");
        DiceButton=GameObject.FindWithTag("Dice");
        ButtonText=DiceButton.transform.GetChild(0).gameObject;
        Manager = GameManager.GetComponent<I_game_manager>();
        
        DontDestroyOnLoad(this.gameObject);

        if (SceneManager.GetActiveScene().name=="T1")
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        name=""+GetComponent<PhotonView>().CreatorActorNr;
        if (SceneManager.GetActiveScene().name == "Result")
        {
            transform.position = new Vector3(0, 600, 0);
        }
    }


    void Update()
    {

    }

    //プレイヤーの表示
    public void Player_indicate()
    {
        gameObject.SetActive(true);
    }



    //プレイヤーの初期位置設定
    public void Player_position_setting(int Y_position, int X_position)
    {
        Manager = GameManager.GetComponent<I_game_manager>();
        XPlayer_position = X_position;//プレイヤーの現在の縦・横位置
        YPlayer_position = Y_position;
        transform.position = Manager.Week[Y_position].Day[X_position].GetComponent<I_Mass_3D>().transform.position;//プレイヤーの移動
        //Debug.Log("プレイヤーの初期位置 "+Y_position + " : "+ X_position);
    }


    public void Turn_your()
    {
        string Log = Manager.PlayerColouradd("貴方")+"のターンです。";
       
        Manager.Log_Mine(Log);
        Log =Manager.PlayerColouradd(PhotonNetwork.NickName)+"のターンです。";
        Manager.Log_connection_Oter(Log);

        Manager.HowMyTurn=true;
        Manager.Camera.GetComponent<Camera_Mouse>().CameraOwnership();
        photonView.RPC(nameof(ApartmentEffect), RpcTarget.All);
        Dice_ready();
    }



    //ダイスを回す準備
    public void Dice_ready()
    {
        string Text_Announce;

        if (Manager.Player_Turn==PlayerNumber)
        {
        
            Manager.Camera.GetComponent<Camera_Mouse>().CameraReset();
      
        }
        if (Guide_on == true && Guide_one == true)
        {
            GameManager.GetComponent<Guide>().Dice_BottonStart();
            GameManager.GetComponent<Guide>().chat_Start();
        }
        DiceButton.GetComponent<Button>().interactable = true;
        ButtonText.GetComponent<Text>().text = "ダイスを回す";
        gameObject.GetComponent<I_Day_Effect>().DiceSetting();
        if (selectwark)

        {

            ButtonText.GetComponent<Text>().text = "進む";

            Text_Announce = Manager.PlayerColouradd(PhotonNetwork.NickName) + "は" + MoveAdd_point + "マスまで進んでもいいです。";
            Manager.Log_connection(Text_Announce);
        }
        Turn_change = false;
        if(MoveAdd_point != 0 && selectwark == false)
        {
            Text_Announce = Manager.PlayerColouradd(PhotonNetwork.NickName) + "はダイスに+" + MoveAdd_point + "マスまで進んでもいいです。";
            Manager.Log_connection(Text_Announce);
        }
        if (OneMore_Dice > 1 && OneMore == false)
        {
            OneMore = true;
            Text_Announce = Manager.PlayerColouradd(PhotonNetwork.NickName) + "はダイスを" + OneMore_Dice + "回振れます。";
            Manager.Log_connection(Text_Announce);
        }
        if (DiceAdd != 0)
        {
            Text_Announce = Manager.PlayerColouradd(PhotonNetwork.NickName) + "のダイスの出目に+" + DiceAdd + "します。";
            Manager.Log_connection(Text_Announce);
        }
        if (DiceMultiply != 0)
        {
            Text_Announce = Manager.PlayerColouradd(PhotonNetwork.NickName) + "のダイスの出目が×" + DiceMultiply + "します。";
            Manager.Log_connection(Text_Announce);
        }
    }




  [PunRPC] public void ApartmentEffect()
    {
        foreach (var Item in Hub_Items)
        {
           if(Item.ItemName=="マンション")
            {
                Item.ItemPoint++;
                Debug.Log("<color=red>マンション発動</color>");
                ItemBlock.GetComponent<ItemBlock_List_Script>().PuintUpdate();
                var Log =Manager.PlayerColouradd(PhotonNetwork.NickName)+"マンションの資産価値が上がり、1ポイント増加しました。";
                Manager.Log_connection_Oter(Log);

            }
            if (Item.ItemName=="かいわれ大根")
            {
                Item.ItemPoint++;
                Debug.Log("<color=red>カイワレ大根</color>");
                ItemBlock.GetComponent<ItemBlock_List_Script>().PuintUpdate();
                var Log = Manager.PlayerColouradd(PhotonNetwork.NickName)+"かいわれ大根が成長し、1ポイント増加しました。";
                Manager.Log_connection_Oter(Log);
            }

        }

    }
    //ダイスを止めて値を受け取る
    private void Dice_Stop()
    {
        if (OneMore_Dice >= 2)

        {

            OneMore_Dice--;

            DiceAdd += Manager.Output_DiceStop();

            Dice_ready();

        }

        else

        {
            if (Guide_on == true && Guide_one == true)
            {
                GameManager.GetComponent<Guide>().Dice_BottonFinish();
                GameManager.GetComponent<Guide>().chat_Finish();
            }
            Move_Point = Manager.Output_DiceStop() + DiceAdd;
            
            if (DiceMultiply != 0)

            {

                Move_Point *= DiceMultiply;

            }

            if (DiceAdd != 0 || DiceMultiply != 0)
            {
                string Text_Announce = Manager.PlayerColouradd(PhotonNetwork.NickName) + "の移動出来る合計は..." + Move_Point + "です。";
                Manager.Log_connection(Text_Announce);
            }

            MoveStop_point = Move_Point;

            Move_Point += MoveAdd_point;

            MovePoint_Count = 0;

            MoveAdd_point = 0;

            DiceAdd = 0;

            DiceMultiply = 0;

            OneMore = false;

            //Debug.Log("歩数：" + Move_Point); 
            MoveSelect();

        }
    }

    public void DicePush()
    {
        if (PlayerNumber == Manager.Player_Turn)
        {
            if (selectwark)
            {
                selectwark = false;
                DiceButton.GetComponent<Button>().interactable = false;
                ButtonText.GetComponent<Text>().text = "移動を選択";
                Move_Point = MoveAdd_point;
                MovePoint_Count = 0;
                MoveAdd_point = 0;
                DiceAdd = 0;
                DiceMultiply = 0;
                //Debug.Log("歩数：" + Move_Point); 
                MoveSelect();
            }
            else
            {
                if (MoveStop_Push)
                {
                    MoveStop_Push = false;
                    select_Point = 0;
                    DiceButton.GetComponent<Button>().interactable = false;
                    ButtonText.GetComponent<Text>().text = "移動を選択";
                    for (int week = 0; week < Manager.Week.Length; week++)
                    {
                        for (int day = 0; day < Manager.Week[0].Day.Length; day++)
                        {
                            photonView.RPC(nameof(Output_SelectClear), RpcTarget.All, week, day);
                        }
                    }
                    StartCoroutine(PlayerMove_Coroutine(MovePoint_Count, true));//プレイヤーの移動開始 
                }
                else
                {          
                        ButtonText.GetComponent<Text>().text = "移動を選択";
                        if (OneMore_Dice <= 1)
                        {
                            DiceButton.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            ButtonText.GetComponent<Text>().text = "ダイスを回す";
                        }

                        StartCoroutine(Dice_Coroutine());

                        DiceStrat = true;
                }
            }
        }
    }


    IEnumerator Dice_Coroutine()
    {
        Manager.Dice.GetComponent<newRotate>().Dice_shuffle();

        yield return new WaitForSeconds(0.5f);     //1秒待つ

        Dice_Stop();//ダイスを止めて値を受け取る 
    }

    public void another_turn()
    {
        ButtonText.GetComponent<Text>().text = "他プレイヤーのターン";
    }





    //選択できるマスの表示の初期設定
    public void MoveSelect()
    {
        if (Guide_on == true && Guide_one == true)
        {
            GameManager.GetComponent<Guide>().MassSelecet_Start();
            GameManager.GetComponent<Guide>().warp_BottonStart();
        }
        Effect_start = true;
        Xcenter = XPlayer_position;                 //選択の中心となるマスを設定
        Ycenter = YPlayer_position;
        YPlayer_Loot[0] = Ycenter;                  //プレイヤーの現在のマスを記憶する
        XPlayer_Loot[0] = Xcenter;
        photonView.RPC(nameof(Output_decisionSetting), RpcTarget.All, Ycenter, Xcenter);//現在のマスを移動決定したマスにする
        select_Point = Move_Point;                  //選択できる数にダイスの目を入れる
        MoveSelect_Display();                       //選択できるマスの表示
    }

    //選択できるマスの表示
    private void MoveSelect_Display()
    {
        int[] Select = new int[4];                                              //選択の中心となるマスの四方を設定する

                            
        photonView.RPC(nameof(Output_SelectClear), RpcTarget.All, Ycenter, Xcenter);//選択の中心となるマスの選択マス(黄色やつ)非表示
        Select[0] = Xcenter - 1; Select[1] = Xcenter + 1;                       //選択の中心となるマスの左右を入れる
        for (int way = 0; way < 2; way++)
        {
            //選択の中心となるマスの左右が存在し移動決定されたマスでない時
            if (0 <= Select[way] && Select[way] < Manager.Week[0].Day.Length && Manager.Week[Ycenter].Day[Select[way]].GetComponent<I_Mass_3D>().decision == false)
            {
                Manager.Week[Ycenter].Day[Select[way]].layer = LayerMask.NameToLayer("Default");
                photonView.RPC(nameof(Output_SelectSetting), RpcTarget.All, Ycenter, Select[way]);
            }
        }

        Select[2] = Ycenter - 1; Select[3] = Ycenter + 1;                        //選択の中心となるマスの上下を入れる
        for (int way = 2; way < Select.Length; way++)
        {
            //選択の中心となるマスの上下が存在し移動決定されたマスでない時
            if (0 <= Select[way] && Select[way] < Manager.Week.Length && Manager.Week[Select[way]].Day[Xcenter].GetComponent<I_Mass_3D>().decision == false)
            {
                Manager.Week[Select[way]].Day[Xcenter].layer = LayerMask.NameToLayer("Default");                     //移動決定したマスを表示
                photonView.RPC(nameof(Output_SelectSetting), RpcTarget.All, Select[way], Xcenter);
            }
        }

        if (Manager.Week[Ycenter].Day[Xcenter].GetComponent<I_Mass_3D>().warp == true)//選択の中心となるマスがワープマスなら
        {
            for (int week = 0; week < Manager.Week.Length; week++)
            {
                for (int day = 0; day < Manager.Week[0].Day.Length; day++)
                {
                    if (Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().warp == true)
                    {
                        Manager.Week[week].Day[day].layer = LayerMask.NameToLayer("Default");
                                           //選択できるマスを表示
                        photonView.RPC(nameof(Output_SelectSetting), RpcTarget.All, week, day);
                    }
                }
            }
        }
    }

    //選択できるマスから移動決定する
    public void MoveSelect_Clicked()
    {
        if (Guide_on == true && Guide_one == true)
        {
            Guide_one = false;
            GameManager.GetComponent<Guide>().MassSelecet_Finish();
            GameManager.GetComponent<Guide>().warp_BottonFinish();
        }
        if (Effect == false)

        {
            for (int week = 0; week < Manager.Week.Length; week++)
            {
                for (int day = 0; day < Manager.Week[0].Day.Length; day++)
                {

                    photonView.RPC(nameof(Output_SelectClear), RpcTarget.All, week, day);                                          //全ての選択マス(黄色やつ)非表示
                    if (Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().On_Click)        //マスがクリックされたものか
                    {
                        //Debug.Log("決定したマス！");
                      
                        photonView.RPC(nameof(Output_decisionSetting), RpcTarget.AllViaServer, week, day);
                        select_Point--;                                                      //プレイヤーの移動できる歩数を1つ減らす
                        MoveStop_point--;
                        MovePoint_Count++;
                        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().On_Click = false;//クリックされた反応を消す
                        YPlayer_Loot[Move_Point - select_Point] = week;                      //移動決定したマスを記憶する
                        XPlayer_Loot[Move_Point - select_Point] = day;
                        //中心マスがワープマスでそこからワープマスに移動したら
                        if (Manager.Week[Ycenter].Day[Xcenter].GetComponent<I_Mass_3D>().warp == true && Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().warp == true)
                        {
                            Player_warpMove[Move_Point - select_Point] = true;              //ワープのモーションをするようにする
                                                                                            //   Debug.Log("モーション");
                        }
                        //Debug.Log("行動基準:"+ (Move_Point - select_Point));
                        Ycenter = week; Xcenter = day;                                      //選択の中心マスをクリックされたマスに移す
                        if (Move_Point - select_Point - 2 >= 0)
                        {
                            Manager.Week[YPlayer_Loot[Move_Point - select_Point - 2]].Day[XPlayer_Loot[Move_Point - select_Point - 2]].GetComponent<I_Mass_3D>().decision = false;
                        }
                    }
                }
            }
            if (MoveStop_point <= 0)
            {
                MoveStop_Push = true;
                DiceButton.GetComponent<Button>().interactable = true;
                ButtonText.GetComponent<Text>().text = "ここで止まる";
            }
            if (select_Point > 0)         //まだ移動できる歩数があるなら
            {
                MoveSelect_Display();     //選択できるマスの表示
            }
            else
            {
                MoveStop_Push = false;
                DiceButton.GetComponent<Button>().interactable = false;
                ButtonText.GetComponent<Text>().text = "移動";
                // Debug.Log("行動終了");
                StartCoroutine(PlayerMove_Coroutine(Move_Point, true));//プレイヤーの移動開始
            }
        }
        else

        {

            //日付の効果による移動 

            for (int week = 0; week < Manager.Week.Length; week++)

            {

                for (int day = 0; day < Manager.Week[0].Day.Length; day++)

                {

                 
                    photonView.RPC(nameof(Output_SelectClear), RpcTarget.All, week, day);

                    if (Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().On_Click)        //マスがクリックされたものか 

                    {

                        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().On_Click = false;//クリックされた反応を消す 

                        Player_warpMove[1] = true;

                        YPlayer_Loot[1] = week;                      //移動決定したマスを記憶する 

                        XPlayer_Loot[1] = day;

                    }

                }

            }



            Effect = false;

            StartCoroutine(PlayerMove_Coroutine(1, false));//プレイヤーの移動開始 

        }

    }

    //プレイヤーの移動
    IEnumerator PlayerMove_Coroutine(int MovePoint, bool Effect)
    {
        for (int Move = 1; Move < MovePoint + 1; Move++)//プレイヤーの決定分だけ移動
        {
            if (Player_warpMove[Move] == true)      //ワープするか
            {
                Player_warpMove[Move] = false;
                photonView.RPC(nameof(Output_AnimationWarpUp), RpcTarget.AllViaServer);  //ワープのアニメーション
                yield return new WaitForSeconds(0.4f);     //1秒待つ
                photonView.RPC(nameof(Output_AnimationStop), RpcTarget.AllViaServer);     //ビデオの再生 

                photonView.RPC(nameof(Output_PlayerMove), RpcTarget.AllViaServer, YPlayer_Loot[Move], XPlayer_Loot[Move]);//ワープのアニメーションと移動 

                yield return new WaitForSeconds(0.1f);     //0.1秒待つ 
            }
            else
            {
                if (YPlayer_Loot[Move] < YPlayer_Loot[Move - 1] && YPlayer_Loot[Move - 1] != 5)
                {
                    photonView.RPC(nameof(Output_AnimationUp), RpcTarget.AllViaServer); //上移動のアニメーション
                }
                else if (YPlayer_Loot[Move] < YPlayer_Loot[Move - 1] && YPlayer_Loot[Move - 1] == 5)
                {
                    photonView.RPC(nameof(Output_AnimationUpMonth), RpcTarget.AllViaServer); //上移動で月を跨ぐアニメーション
                }
                if (YPlayer_Loot[Move] > YPlayer_Loot[Move - 1] && YPlayer_Loot[Move - 1] != 4)
                {
                    photonView.RPC(nameof(Output_AnimationDown), RpcTarget.AllViaServer); //下移動のアニメーション
                }
                else if (YPlayer_Loot[Move] > YPlayer_Loot[Move - 1] && YPlayer_Loot[Move - 1] == 4)
                {
                    photonView.RPC(nameof(Output_AnimationDownMonth), RpcTarget.AllViaServer);//下移動で月を跨ぐアニメーション
                }
                if (XPlayer_Loot[Move] > XPlayer_Loot[Move - 1] && XPlayer_Loot[Move - 1] != 6)
                {
                    photonView.RPC(nameof(Output_AnimationRight), RpcTarget.AllViaServer);//右移動のアニメーション
                }
                else if (XPlayer_Loot[Move] > XPlayer_Loot[Move - 1] && XPlayer_Loot[Move - 1] == 6)
                {
                    photonView.RPC(nameof(Output_AnimationRightMonth), RpcTarget.AllViaServer);//右移動で月を跨ぐアニメーション
                }
                if (XPlayer_Loot[Move] < XPlayer_Loot[Move - 1] && XPlayer_Loot[Move - 1] != 7)
                {
                    photonView.RPC(nameof(Output_AnimationLeft), RpcTarget.AllViaServer);//右移動で月を跨ぐアニメーショ//左移動のアニメーション
                }
                else if (XPlayer_Loot[Move] < XPlayer_Loot[Move - 1] && XPlayer_Loot[Move - 1] == 7)
                {
                    photonView.RPC(nameof(Output_AnimationLeftMonth), RpcTarget.AllViaServer);//左移動で月を跨ぐアニメーション
                }

            }
            photonView.RPC(nameof(Output_Postion), RpcTarget.AllViaServer, XPlayer_Loot[Move], YPlayer_Loot[Move]);
        
            XPlayer_position = XPlayer_Loot[Move];  //プレイヤーの現在の縦・横位置を設定
            YPlayer_position = YPlayer_Loot[Move];
            photonView.RPC(nameof(Output_Playerloot), RpcTarget.OthersBuffered, YPlayer_position, XPlayer_position);

            photonView.RPC(nameof(Output_BGM), RpcTarget.AllViaServer);
            yield return new WaitForSeconds(0.4f);     //1秒待つ

            photonView.RPC(nameof(Output_AnimationStop), RpcTarget.AllViaServer);  //全てのアニメーションを止める 

            photonView.RPC(nameof(Output_PlayerMove), RpcTarget.AllViaServer, YPlayer_Loot[Move], XPlayer_Loot[Move]);                //座標移動 

            yield return new WaitForSeconds(0.3f);     //0.1秒待つ 
        }
        for (int week = 0; week < Manager.Week.Length; week++)
        {
            for (int day = 0; day < Manager.Week[0].Day.Length; day++)
            {
                photonView.RPC(nameof(Output_decisionClear), RpcTarget.AllViaServer, week, day);

            }
        }


        if (Exchange)

        {

            Exchange = false;

            gameObject.GetComponent<I_Day_Effect>().Exchange_Position();


        }
        Manager.Player_Same();

        if (Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Present_Item == true)
        {
            //ここにオリエンテーリングの高得点アイテムの取得処理入れる
            
                
            foreach (var item in ItemMaster.Anniversary_Items)
            {
               if(item.ItemName=="プレゼント")
                {
                    photonView.RPC(nameof(ItemAdd), RpcTarget.All, ItemMaster.Anniversary_Items.IndexOf(item)); //アイテム加算
                 
                    string Log = Manager.PlayerColouradd(PhotonNetwork.NickName)+"がオリエンテーリングの"+item.ItemName+"を入手しました。";
                    Manager.Log_connection(Log);

                }
            }
            Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Present_hid();
        }

        if (Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Goal == true)
        {
            Player_Goal();//ゴールしたときの処理
        }
        else
        {
            if (Turn_change == false)

            {

                if (Effect == true)
                {
                    Effect_start = false;
                    StopI_Day_Effect(); //止まったマスの処理 

                }

            }
        }
        
        if (GetComponent<I_Day_Effect>().Effect_ON)
        {
            GetComponent<I_Day_Effect>().Move_end=true;
        }
              
       
        Turn_change = false;



        //=================================================================================================


    }
    [PunRPC]
    private void Output_Playerloot(int Y, int X)
    {
        XPlayer_position = X;  //プレイヤーの現在の縦・横位置を設定
        YPlayer_position =Y;

    }

    [PunRPC]
    private void Output_BGM()
    {
        Manager.SE.GetComponent<SEManager>().SEsetandplay("WalkSE");

    }


    [PunRPC]  //移動の際の座標移動を出力
    private void Output_PlayerMove(int YPlayer_Loot_Move, int XPlayer_Loot_Move)
    {
        transform.position = Manager.Week[YPlayer_Loot_Move].Day[XPlayer_Loot_Move].GetComponent<I_Mass_3D>().transform.position;//プレイヤーの移動
    }





    [PunRPC]//上移動のアニメーションを出力
    private void Output_AnimationUp()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_up", true);
    }
    [PunRPC] //上移動で月を跨ぐアニメーションを出力
    private void Output_AnimationUpMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_upMonth", true);
    }
    [PunRPC]//下移動のアニメーションを出力
    private void Output_AnimationDown()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_down", true);
    }
    [PunRPC]//下移動で月を跨ぐアニメーションを出力
    private void Output_AnimationDownMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_downMonth", true);
    }
    [PunRPC]//右移動のアニメーションを出力
    private void Output_AnimationRight()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_right", true);
    }
    [PunRPC]//右移動で月を跨ぐアニメーションを出力
    private void Output_AnimationRightMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_rightMonth", true);
    }
    [PunRPC] //左移動のアニメーションを出力
    private void Output_AnimationLeft()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_left", true);
    }
    [PunRPC] //左移動で月を跨ぐアニメーションを出力
    private void Output_AnimationLeftMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_leftMonth", true);
    }
    [PunRPC] //ワープのアニメーションを出力
    private void Output_AnimationWarpUp()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_warpup", true);
    }




    [PunRPC] //移動アニメーションを止めるを出力
    private void Output_AnimationStop()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_up", false);
        gameObject.GetComponent<Animator>().SetBool("Move_down", false);
        gameObject.GetComponent<Animator>().SetBool("Move_right", false);
        gameObject.GetComponent<Animator>().SetBool("Move_left", false);
        gameObject.GetComponent<Animator>().SetBool("Move_upMonth", false);
        gameObject.GetComponent<Animator>().SetBool("Move_downMonth", false);
        gameObject.GetComponent<Animator>().SetBool("Move_rightMonth", false);
        gameObject.GetComponent<Animator>().SetBool("Move_leftMonth", false);
        gameObject.GetComponent<Animator>().SetBool("Move_warpup", false);
    }



    //移動方向に歩数分進む(方向(上下左右), 歩数)
    public void Player_wayMove(string way, int step)
    {
        YPlayer_Loot[0] = YPlayer_position;                  //プレイヤーの現在のマスを記憶する
        XPlayer_Loot[0] = XPlayer_position;
        int Step_Count = step;
        //  Debug.Log(0 + " : " + YPlayer_Loot[0] + ":" + XPlayer_Loot[0]);
        for (int Move = 1; Move < step + 1; Move++)
        {
            switch (way)
            {
                case "上":
                    YPlayer_Loot[Move] = YPlayer_Loot[Move - 1] - 1;
                    XPlayer_Loot[Move] = XPlayer_Loot[Move - 1];
                    break;

                case "下":
                    YPlayer_Loot[Move] = YPlayer_Loot[Move - 1] + 1;
                    XPlayer_Loot[Move] = XPlayer_Loot[Move - 1];
                    break;

                case "右":
                    YPlayer_Loot[Move] = YPlayer_Loot[Move - 1];
                    XPlayer_Loot[Move] = XPlayer_Loot[Move - 1] + 1;
                    break;

                case "左":
                    YPlayer_Loot[Move] = YPlayer_Loot[Move - 1];
                    XPlayer_Loot[Move] = XPlayer_Loot[Move - 1] - 1;
                    break;
            }
            //     Debug.Log(Move + " : " + YPlayer_Loot[Move] + ":" + XPlayer_Loot[Move]);
            if (YPlayer_Loot[Move] < 0 || Manager.Week.Length < YPlayer_Loot[Move])
            {
                Step_Count--;
                YPlayer_Loot[Move] = YPlayer_Loot[Move - 1];
            }
            if (XPlayer_Loot[Move] < 0 || Manager.Week[0].Day.Length < XPlayer_Loot[Move])
            {
                Step_Count--;
                XPlayer_Loot[Move] = XPlayer_Loot[Move - 1];
            }
            if (Manager.Week[YPlayer_Loot[Move]].Day[XPlayer_Loot[Move]].activeInHierarchy == false)
            {
                Step_Count--;
                YPlayer_Loot[Move] = YPlayer_Loot[Move - 1];
                XPlayer_Loot[Move] = XPlayer_Loot[Move - 1];
            }
            //     Debug.Log(Move + " : " + YPlayer_Loot[Move] + ":" + XPlayer_Loot[Move]);
        }
        StartCoroutine(PlayerMove_Coroutine(Step_Count, false));//プレイヤーの移動開始
    }





    [PunRPC]  //選択できるマスを表示して出力(共有すると他プレイヤーもクリック出来る可能性がある為共有しないように頼む)
    private void Output_SelectSetting(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().select_display();
    }

    [PunRPC] //選択できるマスを非表示にして出力
    private void Output_SelectClear(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().select_hidden();
    }

    [PunRPC] //移動決定したマスを表示して出力
    private void Output_decisionSetting(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().decision_display();
    }

    [PunRPC]//移動決定したマスを非表示にして出力
    private void Output_decisionClear(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().decision_hidden();
    }



    [PunRPC ] private void Output_Postion(int Xposition, int Yposition)

    {

        XPlayer_position = Xposition;

        YPlayer_position = Yposition;

    }

    //止まったマスの処理






    //ゴールした時の処理
    public void Player_Goal()
    {
        Output_GoalAnimation();
        photonView.RPC(nameof(Output_GoalCount), RpcTarget.All); //ゴール数を加算
        Manager.Goal_Add();//ゲーム全体のゴール数に加算
        var loop = ItemMaster.Anniversary_Items.Count-1;//最後の位置を取得
        photonView.RPC(nameof(ItemAdd), RpcTarget.All, loop); //アイテム加算
        
        string Log = Manager.PlayerColouradd(PhotonNetwork.NickName)+"が"+ItemMaster.Anniversary_Items[loop].ItemName+"を入手しました。";
        Manager.Log_connection(Log);
    }

   

    //ゴールした時のゴール数を出力
     [PunRPC]private void Output_GoalCount()
    {
        Goalcount++;
    }

    //ゴールのアニメーションの共有お願いします
    private void Output_GoalAnimation()
    {
        photonView.RPC(nameof(Output_GoalAnimation_RPC), RpcTarget.All);
    }
    [PunRPC]
    private void　Output_GoalAnimation_RPC()
    {
        Manager.SE.GetComponent<SEManager>().SEsetandplay("GoalSE");
        Manager.GoalAnimation.GetComponent<Animator>().SetBool("GoalAnimation", true);
    }





    public void GoalAnimation_After()
    {
        if (Manager.Player_Turn == PlayerNumber)
        {
            //Debug.Log(PlayerNumber + "GoalAnimation_After()");
            if (Turn_change == false)
            {
                if (Effect_start == true)
                {
                    //Debug.Log("StopDay_Effect()");
                    StopI_Day_Effect(); //止まったマスの処理
                }
                //Manager.PlayerTurn_change();
            }
            Turn_change = false;
            Effect_start = true;
        }
    }


    //日付の効果発動
    public void Player_DayEffect()
    {
       
        string day = Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Day;//発動する日付を取得
        StartCoroutine(Day_Animation(day));     //ビデオの再生とホップアップの表示
        Effect_ready = true;
        Item_Get(day);                               //ここに日付の効果入れる
        
        //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
    }

    public void Item_Get(string day)
    {
      //  Debug.Log("WWWWWWWWWWWWWWWWWWWWWWWWWWWWW"+day);
        int loop = 0;
        int Itemunum = 0;
          foreach (var Item in ItemMaster.Anniversary_Items)//アイテムリストの内容を順番に格納
          {
            if (Item.Day==day)
            {
                if (Item.ItemName!="ランダムなアイテム"&&Item.ItemName!="落札品") {
                    photonView.RPC(nameof(ItemAdd), RpcTarget.All, loop); //アイテム加算
                    Itemunum=loop;
                    string Log = Manager.PlayerColouradd(PhotonNetwork.NickName)+"が"+Item.ItemName+"を入手しました。";
                    Manager.Log_connection(Log);
                }
          
            }
            loop++;
          }
        //   Debug.Log("あいてむううううううううううううううううううう");
        if (ItemMaster.Anniversary_Items[Itemunum].ItemName!="ランダムなアイテム")
        {
            

           
        }



    }






   //開いたマスを非表示にして出力
   [PunRPC] private void Output_hideCoverClear(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().hideCover_Clear();//マスを開いた表示にする
    }

    //ビデオの再生とホップアップの表示
  

    //ビデオの再生とホップアップの表示
    IEnumerator Day_Animation(string day)
    {
        
        photonView.RPC(nameof(Day_Animation_RPC_1), RpcTarget.AllViaServer, day);//コルーチン回避用
        yield return new WaitForSeconds(8);     //8秒待つ
        photonView.RPC(nameof(Day_Animation_RPC_2), RpcTarget.AllViaServer, day);//コルーチン回避用
                                                        
    }


    [PunRPC]public void Day_Animation_RPC_1(string day)//コルーチン回避用
    {
        
        //   Debug.Log("ムービーーーーーーーーーーーーーーーーーーーー");
        Manager.Output_VideoSetting();  //ビデオを表示 
        Manager.Video_obj.GetComponent<VideoPlayer>().clip = gameObject.GetComponent<I_Day_Effect>().Output_VideoClip(day); //ビデオをオブジェクトVideoに入れる 
        Manager.Output_VideoStart();     //ビデオの再生 
    }

    [PunRPC]
    public void Day_Animation_RPC_2(string day)//コルーチン回避用
    {
        
        if (Manager.Video_obj.activeInHierarchy == true)

        {
            if (Guide_on == true)
            {
                GameManager.GetComponent<Guide>().Hopup_Start();
            }

            Manager.Output_HopUp();         //ホップアップを表示する 

            gameObject.GetComponent<I_Day_Effect>().Output_HopUp_Setting(day);        //ホップアップを日付のものに変更する 

        }

        Manager.Output_VideoFinish();     //ビデオの非表示 



    }








    // 以下MannequinPlayer空の引用=====================================================================
    [PunRPC] public void ItemAdd(int ItemNum)//ItemNum＝マスター登録順の番号
    {
        Hub_Items.Add(ScriptableObject.Instantiate(ItemMaster.Anniversary_Items[ItemNum]));//マスターにあるItemNumのアイテムを生成し、Hubに追加
        ItemBlock.GetComponent<ItemBlock_List_Script>().AddItem(ItemNum);
    }

    [PunRPC] public void ItemLost(int HubItemNum)//HubItemNum＝所持アイテム登録順の番号
    {

        Hub_Items.RemoveAt(HubItemNum);//所持中のHubItemNum番目のアイテムを消去
        ItemBlock.GetComponent<ItemBlock_List_Script>().LostItem(HubItemNum);


    }


    public void ItemAdd_ToConnect(int HubItemNum)//ItemNum＝マスター登録順の番号
    {

        photonView.RPC(nameof(ItemAdd), RpcTarget.All, HubItemNum); //アイテム加算


    }
    public void ItemLost_ToConnect(int HubItemNum)//HubItemNum＝所持アイテム登録順の番号
    {
       
        photonView.RPC(nameof(ItemLost), RpcTarget.All, HubItemNum); //アイテム加算

    }



    // =====================================================================





    public void Player_WarpMove(string Mode, string Day)

    {

        if (Mode == "ワープ")

        {

            YPlayer_Loot[0] = YPlayer_position;                  //プレイヤーの現在のマスを記憶する 

            XPlayer_Loot[0] = XPlayer_position;

            for (int week = 0; week < Manager.Week.Length; week++)

            {

                for (int day = 0; day < Manager.Week[0].Day.Length; day++)

                {

                    if (Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().Day == Day)

                    {

                        YPlayer_Loot[1] = week;

                        XPlayer_Loot[1] = day;

                    }

                }

            }

            if (Manager.Week[YPlayer_Loot[1]].Day[XPlayer_Loot[1]].activeInHierarchy == true)
            {

                Player_warpMove[1] = true;

                StartCoroutine(PlayerMove_Coroutine(1, false));//プレイヤーの移動開始

            }

        }

    }


    public void HopUp_Setting(string day)

    {
        if (Guide_on == true)
        {
            GameManager.GetComponent<Guide>().Hopup_Start();
        }

        Manager.Output_HopUp();         //ホップアップを表示する 

        gameObject.GetComponent<I_Day_Effect>().Output_HopUp_Setting(day);        //ホップアップを日付のものに変更する 

    }
  

    private void StopI_Day_Effect()//止まったマスの処理
    {
    
        if (Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Open == false)//まだ開いてないマスなら
        {
             photonView.RPC(nameof(Output_hideCoverClear), RpcTarget.All, YPlayer_position, XPlayer_position); //マスを開いた表示にする
             Player_DayEffect();//日付の効果
        }
        else
        {
          
          Manager.PlayerTurn_change();
    
           
        }
    }


    public void StertDayEffect()

    {
        if (Guide_on == true)
        {
            GameManager.GetComponent<Guide>().Hopup_Finish();
            Guide_on = false;
            if(Manager.Player_Turn == PlayerNumber)
            {
                GameManager.GetComponent<Guide>().Item_Cstart();
            }
            
        }

        if (Effect_ready)

        {

            Effect_ready = false;

            string day = Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Day;//発動する日付を取得 

            gameObject.GetComponent<I_Day_Effect>().Day_EffectReaction(day);

        }

    }


}


