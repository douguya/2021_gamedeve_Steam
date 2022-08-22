using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Video;


public class I_Player_3D : MonoBehaviourPunCallbacks
{
    public int PlayerNumber;                      //プレイヤー番号
    

    public int XPlayer_position;                  //プレイヤーの現在の横の位置
    public int YPlayer_position;                  //プレイヤーの現在の縦の位置

    int Xcenter, Ycenter;                         //選択できるマスの中心マス

    public List<Anniversary_Item> Hub_Items = new List<Anniversary_Item>();

    public int Move_Point = 0;                    //プレイヤーの移動できる歩数 
    private int select_Point = 0;                 //マスを選択できる数
    private bool[] Player_warpMove = new bool[11];//プレイヤーの移動方法

    public int Goalcount = 0;

    private int[] XPlayer_Loot = new int[11];     //選択したマスを記憶する
    private int[] YPlayer_Loot = new int[11];


    public GameObject GameManager;                //GameManagerオブジェクトの取得
    private I_game_manager Manager;                 //I_game_managerを取得

    public GameObject DiceButton;                           //ダイスを止める為のオブジェクト取得
    public GameObject ButtonText;                           //ダイスのテキストオブジェクト取得
    private bool DiceStrat = true;                          //ボタンがダイスの開始かストップか

    // 以下MannequinPlayer空の引用=====================================================================
    public Anniversary_Item_Master ItemMaster;
    public GameObject ItemBlock;//アイテムリストのUGI

    // =====================================================================

    private void Awake()
    {
        GameManager= GameObject.FindWithTag("GameController");
        DiceButton=GameObject.FindWithTag("Dice");
        ButtonText=DiceButton.transform.GetChild(0).gameObject;
        
            
        
    }
    void Start()
    {
        name=""+GetComponent<PhotonView>().CreatorActorNr;
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



    //ダイスを回す準備
    public void Dice_ready()
    {
        DiceButton.GetComponent<Button>().interactable = true;
        ButtonText.GetComponent<Text>().text = "ダイスを回す";
    }

    //ダイスを止めて値を受け取る
    private void Dice_Stop()
    {
        Move_Point = Manager.Output_DiceStop();
        MoveSelect();
    }

    public void DicePush()
    {
        if (PlayerNumber == Manager.Player_Turn)
        {
            if (DiceStrat)
            {
                //ここでダイスを回す処理
                Manager.Output_DiceStart();
                ButtonText.GetComponent<Text>().text = "ダイスを止める";
                DiceStrat = false;
            }
            else
            {
                Dice_Stop();//ダイスを止めて値を受け取る
                DiceButton.GetComponent<Button>().interactable = false;
                ButtonText.GetComponent<Text>().text = "移動を選択";
                DiceStrat = true;
            }
        }
    }

    public void another_turn()
    {
        ButtonText.GetComponent<Text>().text = "他プレイヤーのターン";
    }





    //選択できるマスの表示の初期設定
    public void MoveSelect()
    {
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

        Output_SelectClear(Ycenter, Xcenter);                                   //選択の中心となるマスの選択マス(黄色やつ)非表示

        Select[0] = Xcenter - 1; Select[1] = Xcenter + 1;                       //選択の中心となるマスの左右を入れる
        for (int way = 0; way < 2; way++)
        {
            //選択の中心となるマスの左右が存在し移動決定されたマスでない時
            if (0 <= Select[way] && Select[way] < Manager.Week[0].Day.Length && Manager.Week[Ycenter].Day[Select[way]].GetComponent<I_Mass_3D>().decision == false)
            {
                Output_SelectSetting(Ycenter, Select[way]);                      //移動決定したマスを表示
            }
        }

        Select[2] = Ycenter - 1; Select[3] = Ycenter + 1;                        //選択の中心となるマスの上下を入れる
        for (int way = 2; way < Select.Length; way++)
        {
            //選択の中心となるマスの上下が存在し移動決定されたマスでない時
            if (0 <= Select[way] && Select[way] < Manager.Week.Length && Manager.Week[Select[way]].Day[Xcenter].GetComponent<I_Mass_3D>().decision == false)
            {
                Output_SelectSetting(Select[way], Xcenter);                     //移動決定したマスを表示
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
                        Output_SelectSetting(week, day);                        //選択できるマスを表示
                    }
                }
            }
        }
    }

    //選択できるマスから移動決定する
    public void MoveSelect_Clicked()
    {
        for (int week = 0; week < Manager.Week.Length; week++)
        {
            for (int day = 0; day < Manager.Week[0].Day.Length; day++)
            {

                Output_SelectClear(week, day);                                           //全ての選択マス(黄色やつ)非表示
                if (Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().On_Click)        //マスがクリックされたものか
                {
                    //Debug.Log("決定したマス！");
                    select_Point--;                                                      //プレイヤーの移動できる歩数を1つ減らす
                    Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().On_Click = false;//クリックされた反応を消す
                    YPlayer_Loot[Move_Point - select_Point] = week;                      //移動決定したマスを記憶する
                    XPlayer_Loot[Move_Point - select_Point] = day;
                    //中心マスがワープマスでそこからワープマスに移動したら
                    if (Manager.Week[Ycenter].Day[Xcenter].GetComponent<I_Mass_3D>().warp == true && Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().warp == true)
                    {
                        Player_warpMove[Move_Point - select_Point] = true;              //ワープのモーションをするようにする
                        Debug.Log("モーション");
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

        if (select_Point > 0)         //まだ移動できる歩数があるなら
        {
            MoveSelect_Display();     //選択できるマスの表示
        }
        else
        {
            Debug.Log("行動終了");
            StartCoroutine(PlayerMove_Coroutine(Move_Point, true));//プレイヤーの移動開始
        }
    }

    //プレイヤーの移動
    IEnumerator PlayerMove_Coroutine(int MovePoint, bool Effect)
    {
        for (int Move = 1; Move < MovePoint + 1; Move++)//プレイヤーの決定分だけ移動
        {
            if (Player_warpMove[Move] == true)      //ワープするか
            {
                photonView.RPC(nameof(Output_AnimationWarpUp), RpcTarget.AllViaServer);  //ワープのアニメーション
                yield return new WaitForSeconds(1);     //1秒待つ
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
                else if (YPlayer_Loot[Move - 1] == 5)
                {
                    photonView.RPC(nameof(Output_AnimationUpMonth), RpcTarget.AllViaServer); //上移動で月を跨ぐアニメーション
                }
                if (YPlayer_Loot[Move] > YPlayer_Loot[Move - 1] && YPlayer_Loot[Move - 1] != 4)
                {
                   photonView.RPC(nameof(Output_AnimationDown), RpcTarget.AllViaServer); //下移動のアニメーション
                }
                else if (YPlayer_Loot[Move - 1] == 4)
                {
                   photonView.RPC(nameof(Output_AnimationDownMonth), RpcTarget.AllViaServer);//下移動で月を跨ぐアニメーション
                }
                if (XPlayer_Loot[Move] > XPlayer_Loot[Move - 1] && XPlayer_Loot[Move - 1] != 6)
                {
                   photonView.RPC(nameof(Output_AnimationRight), RpcTarget.AllViaServer);//右移動のアニメーション
                }
                else if (XPlayer_Loot[Move - 1] == 6)
                {
                   photonView.RPC(nameof(Output_AnimationRightMonth), RpcTarget.AllViaServer);//右移動で月を跨ぐアニメーション
                }
                if (XPlayer_Loot[Move] < XPlayer_Loot[Move - 1] && XPlayer_Loot[Move - 1] != 7)
                {
                   photonView.RPC(nameof(Output_AnimationLeft), RpcTarget.AllViaServer);//右移動で月を跨ぐアニメーショ//左移動のアニメーション
                }
                else if (XPlayer_Loot[Move - 1] == 7)
                {
                   photonView.RPC(nameof(Output_AnimationLeftMonth), RpcTarget.AllViaServer);//左移動で月を跨ぐアニメーション
                }

            }
            XPlayer_position = XPlayer_Loot[Move];  //プレイヤーの現在の縦・横位置を設定
            YPlayer_position = YPlayer_Loot[Move];
            yield return new WaitForSeconds(1);     //1秒待つ

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
        if (Effect == true)
        {
            StopDay_Effect(); //止まったマスの処理
        }
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
        Debug.Log(0 + " : " + YPlayer_Loot[0] + ":" + XPlayer_Loot[0]);
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
            Debug.Log(Move + " : " + YPlayer_Loot[Move] + ":" + XPlayer_Loot[Move]);
            if (YPlayer_Loot[Move] < 0 || Manager.Week.Length < YPlayer_Loot[Move])
            {
                YPlayer_Loot[Move] = YPlayer_Loot[Move - 1];
            }
            if (XPlayer_Loot[Move] < 0 || Manager.Week[0].Day.Length < XPlayer_Loot[Move])
            {
                XPlayer_Loot[Move] = XPlayer_Loot[Move - 1];
            }
            Debug.Log(Move + " : " + YPlayer_Loot[Move] + ":" + XPlayer_Loot[Move]);
        }
        StartCoroutine(PlayerMove_Coroutine(step, false));//プレイヤーの移動開始
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





    //止まったマスの処理
    private void StopDay_Effect()
    {
        if (Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Goal == true)
        {
            Player_Goal();//ゴールしたときの処理
        }
        else
        {
            if (Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Open == false)//まだ開いてないマスなら
            {
               
                photonView.RPC(nameof(Output_hideCoverClear), RpcTarget.All, YPlayer_position, XPlayer_position); //マスを開いた表示にする
                Player_DayEffect();//日付の効果
            }
            else
            {
                Manager.PlayerTurn_change();         //ターンを変える
            }
        }
    }





    //ゴールした時の処理
    public void Player_Goal()
    {
        photonView.RPC(nameof(Output_GoalCount), RpcTarget.All); //ゴール数を加算
        Manager.Goal_Add();//ゲーム全体のゴール数に加算
        Player_DayEffect();//日付の効果
    }

    //ゴールした時のゴール数を出力
     [PunRPC]private void Output_GoalCount()
    {
        Goalcount++;
    }

    //日付の効果発動
    public void Player_DayEffect()
    {
        string day = Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Day;//発動する日付を取得
        StartCoroutine(Day_Animation(day));     //ビデオの再生とホップアップの表示
                                                //ここに日付の効果入れる

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
        Manager.Output_VideoSetting();
        Manager.Output_HopUp();
        gameObject.GetComponent<Day_Effect>().Output_HopUp_Setting(day);
        Manager.Video_obj.GetComponent<VideoPlayer>().clip = gameObject.GetComponent<Day_Effect>().Output_VideoClip(day);
        Manager.Output_VideoStart();     //ビデオの再生 Dayが入るとエラーを吐くのでこう書いた
        yield return new WaitForSeconds(8);     //8秒待つ
        Manager.Output_VideoFinish();     //ビデオの非表示
        Manager.PlayerTurn_change();         //ターンを変える
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
    // =====================================================================

















}


