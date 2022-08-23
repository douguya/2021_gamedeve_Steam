using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Player_3D : MonoBehaviour
{
    public int PlayerNumber;                      //プレイヤー番号

    public int XPlayer_position;                  //プレイヤーの現在の横の位置
    public int YPlayer_position;                  //プレイヤーの現在の縦の位置

    int Xcenter, Ycenter;                         //選択できるマスの中心マス

    public int Move_Point = 0;                    //プレイヤーの移動できる歩数 
    private int select_Point = 0;                 //マスを選択できる数
    private bool[] Player_warpMove = new bool[11];//プレイヤーの移動方法

    public int Goalcount = 0;

    private int[] XPlayer_Loot = new int[11];     //選択したマスを記憶する
    private int[] YPlayer_Loot = new int[11];

    
    public GameObject GameManager;                //GameManagerオブジェクトの取得
    private game_manager Manager;                 //game_managerを取得

    public GameObject DiceButton;                           //ダイスを止める為のオブジェクト取得
    public GameObject ButtonText;                           //ダイスのテキストオブジェクト取得
    private bool DiceStrat = true;                          //ボタンがダイスの開始かストップか


    void Start()
    {

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
        Manager = GameManager.GetComponent<game_manager>();
        XPlayer_position = X_position;//プレイヤーの現在の縦・横位置
        YPlayer_position = Y_position;
        transform.position = Manager.Week[Y_position].Day[X_position].GetComponent<Mass_3D>().transform.position;//プレイヤーの移動
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

        Output_decisionSetting(Ycenter, Xcenter);   //現在のマスを移動決定したマスにする
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
            if (0 <= Select[way] && Select[way] < Manager.Week[0].Day.Length && Manager.Week[Ycenter].Day[Select[way]].GetComponent<Mass_3D>().decision == false)
            {
                Output_SelectSetting(Ycenter, Select[way]);                      //移動決定したマスを表示
            }
        }

        Select[2] = Ycenter - 1; Select[3] = Ycenter + 1;                        //選択の中心となるマスの上下を入れる
        for (int way = 2; way < Select.Length; way++)
        {
            //選択の中心となるマスの上下が存在し移動決定されたマスでない時
            if (0 <= Select[way] && Select[way] < Manager.Week.Length && Manager.Week[Select[way]].Day[Xcenter].GetComponent<Mass_3D>().decision == false)
            {
                Output_SelectSetting(Select[way], Xcenter);                     //移動決定したマスを表示
            }
        }

        if (Manager.Week[Ycenter].Day[Xcenter].GetComponent<Mass_3D>().warp == true)//選択の中心となるマスがワープマスなら
        {
            for (int week = 0; week < Manager.Week.Length; week++)
            {
                for (int day = 0; day < Manager.Week[0].Day.Length; day++)
                {
                    if (Manager.Week[week].Day[day].GetComponent<Mass_3D>().warp == true)
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
                if (Manager.Week[week].Day[day].GetComponent<Mass_3D>().On_Click)        //マスがクリックされたものか
                {
                    //Debug.Log("決定したマス！");
                    select_Point--;                                                      //プレイヤーの移動できる歩数を1つ減らす
                    Manager.Week[week].Day[day].GetComponent<Mass_3D>().On_Click = false;//クリックされた反応を消す
                    YPlayer_Loot[Move_Point - select_Point] = week;                      //移動決定したマスを記憶する
                    XPlayer_Loot[Move_Point - select_Point] = day;
                    //中心マスがワープマスでそこからワープマスに移動したら
                    if (Manager.Week[Ycenter].Day[Xcenter].GetComponent<Mass_3D>().warp == true && Manager.Week[week].Day[day].GetComponent<Mass_3D>().warp == true)
                    {
                        Player_warpMove[Move_Point - select_Point] = true;              //ワープのモーションをするようにする
                        Debug.Log("モーション");
                    }
                    //Debug.Log("行動基準:"+ (Move_Point - select_Point));
                    Ycenter = week; Xcenter = day;                                      //選択の中心マスをクリックされたマスに移す
                    if(Move_Point - select_Point - 2 >= 0)
                    {
                        Manager.Week[YPlayer_Loot[Move_Point - select_Point - 2]].Day[XPlayer_Loot[Move_Point - select_Point - 2]].GetComponent<Mass_3D>().decision = false;
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
                Player_warpMove[Move] = false;
                Output_AnimationWarpUp();           //ワープのアニメーション
                yield return new WaitForSeconds(1);     //1秒待つ
                Output_AnimationStop();
                Output_PlayerMove(Move);              //ワープのアニメーションと移動
                Manager.Player_Same();
                yield return new WaitForSeconds(0.1f);     //0.1秒待つ
            }
            else
            {
                if (YPlayer_Loot[Move] < YPlayer_Loot[Move - 1] && YPlayer_Loot[Move - 1] != 5)
                {
                    Output_AnimationUp();//上移動のアニメーション
                }
                else if (YPlayer_Loot[Move - 1] == 5)
                {
                    Output_AnimationUpMonth();//上移動で月を跨ぐアニメーション
                }
                if (YPlayer_Loot[Move] > YPlayer_Loot[Move - 1] && YPlayer_Loot[Move - 1] != 4)
                {
                    Output_AnimationDown();//下移動のアニメーション
                }
                else if (YPlayer_Loot[Move - 1] == 4)
                {
                    Output_AnimationDownMonth();//下移動で月を跨ぐアニメーション
                }
                if (XPlayer_Loot[Move] > XPlayer_Loot[Move - 1] && XPlayer_Loot[Move - 1] != 6)
                {
                    Output_AnimationRight();//右移動のアニメーション
                }
                else if (XPlayer_Loot[Move - 1] == 6)
                {
                    Output_AnimationRightMonth();//右移動で月を跨ぐアニメーション
                }
                if (XPlayer_Loot[Move] < XPlayer_Loot[Move - 1] && XPlayer_Loot[Move - 1] != 7)
                {
                    Output_AnimationLeft();//左移動のアニメーション
                }
                else if (XPlayer_Loot[Move - 1] == 7)
                {
                    Output_AnimationLeftMonth();//左移動で月を跨ぐアニメーション
                }

            }
            XPlayer_position = XPlayer_Loot[Move];  //プレイヤーの現在の縦・横位置を設定
            YPlayer_position = YPlayer_Loot[Move];
            yield return new WaitForSeconds(1);     //1秒待つ

            Output_AnimationStop();                 //全てのアニメーションを止める
            Output_PlayerMove(Move);                //座標移動
            Manager.Player_Same();
            yield return new WaitForSeconds(0.1f);     //0.1秒待つ
        }
        for (int week = 0; week < Manager.Week.Length; week++)
        {
            for (int day = 0; day < Manager.Week[0].Day.Length; day++)
            {
                Output_decisionClear(week, day);    //全ての移動決定したマスを消す
            }
        }
        if (Effect == true)
        {
            StopDay_Effect(); //止まったマスの処理
        }
    }



    //移動の際の座標移動を出力
    private void Output_PlayerMove(int Move)
    {
        transform.position = Manager.Week[YPlayer_Loot[Move]].Day[XPlayer_Loot[Move]].GetComponent<Mass_3D>().transform.position;//プレイヤーの移動
    }
    
    


    //上移動のアニメーションを出力
    private void Output_AnimationUp()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_up", true);
    }
    //上移動で月を跨ぐアニメーションを出力
    private void Output_AnimationUpMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_upMonth", true);
    }
    //下移動のアニメーションを出力
    private void Output_AnimationDown()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_down", true);
    }
    //下移動で月を跨ぐアニメーションを出力
    private void Output_AnimationDownMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_downMonth", true);
    }
    //右移動のアニメーションを出力
    private void Output_AnimationRight()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_right", true);
    }
    //右移動で月を跨ぐアニメーションを出力
    private void Output_AnimationRightMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_rightMonth", true);
    }
    //左移動のアニメーションを出力
    private void Output_AnimationLeft()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_left", true);
    }
    //左移動で月を跨ぐアニメーションを出力
    private void Output_AnimationLeftMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_leftMonth", true);
    }
    //ワープのアニメーションを出力
    private void Output_AnimationWarpUp()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_warpup", true);
    }
    

    //移動アニメーションを止めるを出力
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





    //選択できるマスを表示して出力(共有すると他プレイヤーもクリック出来る可能性がある為共有しないように頼む)
    private void Output_SelectSetting(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<Mass_3D>().select_display();
    }

    //選択できるマスを非表示にして出力
    private void Output_SelectClear(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<Mass_3D>().select_hidden();
    }

    //移動決定したマスを表示して出力
    private void Output_decisionSetting(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<Mass_3D>().decision_display();
    }

    //移動決定したマスを非表示にして出力
    private void Output_decisionClear(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<Mass_3D>().decision_hidden();
    }





    //止まったマスの処理
    private void StopDay_Effect()
    {
        if (Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<Mass_3D>().Goal == true)
        {
            Player_Goal();//ゴールしたときの処理
        }
        else
        {
            if (Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<Mass_3D>().Open == false)//まだ開いてないマスなら
            {
                Output_hideCoverClear(YPlayer_position, XPlayer_position);//マスを開いた表示にする
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
        Output_GoalCount();//ゴール数を加算
        Manager.Goal_Add();//ゲーム全体のゴール数に加算
        Player_DayEffect();//日付の効果
    }

    //ゴールした時のゴール数を出力
    private void Output_GoalCount()
    {
        Goalcount++;
    }

    //日付の効果発動
    public void Player_DayEffect()
    {
        string day = Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<Mass_3D>().Day;//発動する日付を取得
        StartCoroutine(Day_Animation(day));     //ビデオの再生とホップアップの表示
                                             //ここに日付の効果入れる
        
    }

    //開いたマスを非表示にして出力
    private void Output_hideCoverClear(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<Mass_3D>().hideCover_Clear();//マスを開いた表示にする
    }

    //ビデオの再生とホップアップの表示
    IEnumerator Day_Animation(string day)
    {
        Manager.Output_VideoSetting();
        Manager.Output_HopUp();
        gameObject.GetComponent<Day_Effect>().Output_HopUp_Setting(day);
        Manager.Video_obj.GetComponent<VideoPlayer>().clip = gameObject.GetComponent<Day_Effect>().Output_VideoClip(day);
        Manager.Output_VideoStart();     //ビデオの再生
        yield return new WaitForSeconds(8);     //8秒待つ
        Manager.Output_VideoFinish();     //ビデオの非表示
        Manager.PlayerTurn_change();         //ターンを変える
    }
}
