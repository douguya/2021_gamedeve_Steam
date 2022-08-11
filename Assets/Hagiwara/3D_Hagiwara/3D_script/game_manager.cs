using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class game_manager : MonoBehaviour
{
    public int[] month = new int[4];                        //設置する月を受け取る
    public string[] warp = new string[4];                   //設置するワープの位置を受け取る

    public GameObject[] Player = new GameObject[4];         //プレイヤーオブジェクト取得
    public int joining_Player = 4;                          //参加するプレイヤーの人数を取得
    public string[] Player_InitialPosition = new string[4]; //設置するプレイヤーの初期位置を受け取る
    public int Player_Turn = -1;                             //プレイヤーの現在の手番

    public Days[] Week = new Days[10];                      //Massの縦列のオブジェクトの取得・関数Daysで二次元配列にしている

    private int XGoal = 0, YGoal = 0;                       //ゴールのマスの横・縦の現在位置
    private bool Goal_check;                                //ゴールしたかどうか
    private int Goal_AddCount = 0;                          //ゴールした合計

    private Day_Animation Day_Animation;                    //日付の再生するビデオを受け渡すスクリプト取得
    public GameObject Video_obj;                            //ビデオ再生用のオブジェクト取得

    public GameObject HopUp;                                //ホップアップのオブジェクト取得

    public GameObject Dice;                                 //サイコロのオブジェクト取得


    void Start()
    {
        Day_Animation = GetComponent<Day_Animation>();
        Month_Setting();
        Player_setting();
        Goal_Decision();
        PlayerTurn_change();
        Output_DiceStop();
        //Goal_Again();
    }

    void Update()
    {

    }
    


    [System.Serializable]
    public class Days//weekの子・横列のオブジェクトの取得
    {
        public GameObject[] Day;

    }




    //MonthSettingを呼ぶとmonthから受け取った月の日付をMassのDayに入れる&入れた日付がwarpが受け取ってる日付ならワープを設置する
    private void Month_Setting()
    {
        int Xmonth = 0;//設置するマップ分Xの配列をずらす
        int Ymonth = 0;//設置するマップ分Yの配列をずらす

        for (int block = 0; block < this.month.Length; block++)//指定する月がどのブロックにいるか判別
        {
            switch (block)//それぞれのブロックに指定した日付を入れるようにする
            {
                case 0:
                    Xmonth = 0; Ymonth = 0;
                    break;
                case 1:
                    Xmonth = Week[0].Day.Length / 2; Ymonth = 0;
                    break;
                case 2:
                    Xmonth = 0; Ymonth = Week.Length / 2;
                    break;
                case 3:
                    Xmonth = Week[0].Day.Length / 2; Ymonth = Week.Length / 2;
                    break;
            }
            for (int month = 0; month < 12; month++)//monthに何月か入れる
            {
                if (this.month[block] == month + 1)//指定した月が何月か判別する
                {
                    Day_Setting(month, Ymonth, Xmonth);//マスに日付を入れる
                }
            }
        }

    }

    //MonthSettingから月と入れるマスの場所を受け取りマスに日付を入れる&入れた日付がワープのマスならワープマスにする
    private void Day_Setting(int month, int Ymonth, int Xmonth)
    {
        int nullday = 0;//空白の日付
        int countday = 0;//入れる日付
        int Maxday = 0;//その月の最大日付

        switch (month + 1)
        {
            case 1:
                nullday = 6; Maxday = 31;
                break;
            case 2:
                nullday = 2; Maxday = 28;
                break;
            case 3:
                nullday = 2; Maxday = 31;
                break;
            case 4:
                nullday = 5; Maxday = 30;
                break;
            case 5:
                nullday = 0; Maxday = 31;
                break;
            case 6:
                nullday = 3; Maxday = 30;
                break;
            case 7:
                nullday = 5; Maxday = 31;
                break;
            case 8:
                nullday = 1; Maxday = 31;
                break;
            case 9:
                nullday = 4; Maxday = 30;
                break;
            case 10:
                nullday = 6; Maxday = 31;
                break;
            case 11:
                nullday = 2; Maxday = 30;
                break;
            case 12:
                nullday = 4; Maxday = 31;
                break;
        }
        for (int n = 0; n < 7 - nullday; n++)//第一週目に日付を入れる
        {
            countday++;
            Output_DaySetting(month, countday, Ymonth, Xmonth + nullday + n);//日付を入れる

            for (int i = 0; i < warp.Length; i++)//ワープの追加
            {
                if (warp[i] == month + 1 + "/" + countday)//入れた日付がワープを設置位置だったらワープのマスにする
                {
                    Output_WarpSetting(Ymonth, Xmonth + nullday + n);//ワープ出来るマスの設置
                }
            }
        }

        for (int h = 1; h < Week.Length / 2; h++)//第二週以降の日付を入れる
        {
            for (int w = 0; w < Week[0].Day.Length / 2; w++)
            {
                if (countday < Maxday)
                {
                    countday++;
                    Output_DaySetting(month, countday, Ymonth + h, Xmonth + w);//日付を入れる

                    for (int i = 0; i < warp.Length; i++)//ワープの追加
                    {
                        if (warp[i] == month + 1 + "/" + countday)//入れた日付がワープを設置位置だったらワープのマスにする
                        {
                            Output_WarpSetting(Ymonth + h, Xmonth + w);//ワープ出来るマスの設置
                        }
                    }
                }
            }
        }
    }

    //マスに日付を入れる結果出力
    private void Output_DaySetting(int month, int countday, int week, int day)
    {
        Week[week].Day[day].GetComponent<Mass_3D>().Day = month + 1 + "/" + countday;//日付を入れる
        Week[week].Day[day].GetComponent<Mass_3D>().hideCover_setting();             //hideCover(青いやつ)の表示
    }

    //ワープ出来るマスの設置出力
    private void Output_WarpSetting(int week, int day)
    {
        Week[week].Day[day].GetComponent<Mass_3D>().warp_setting();//ワープマスに設定
    }






    //プレイヤーの初期位置設定
    private void Player_setting()
    {
        for (int player = 0; player < joining_Player; player++)//プレイヤーの人数分
        {
            for (int week = 0; week < Week.Length; week++)
            {
                for (int day = 0; day < Week[0].Day.Length; day++)
                {
                    if (Player_InitialPosition[player] == Week[week].Day[day].GetComponent<Mass_3D>().Day)
                    {
                        Output_PlayerSetting(player, week, day);//プレイヤーの初期位置設定
                    }
                }
            }
        }
    }

    //プレイヤーの初期位置設定の結果出力
    private void Output_PlayerSetting(int player, int week, int day)
    {
        Player[player].GetComponent<Player_3D>().Player_indicate();                 //プレイヤーの表示
        Player[player].GetComponent<Player_3D>().Player_position_setting(week, day);//プレイヤーを初期位置へ
    }






    private void Goal_Decision()//初めてゴールを出現させる
    {
        int week, day;                                            //ランダムなゴールの場所を入れる
        do
        {
            week = Random.Range(0, Week.Length);                  //week・横の列のランダム
            day = Random.Range(0, Week[0].Day.Length);            //day・縦の列のランダム
        } while (Week[week].Day[day].GetComponent<Mass_3D>().Day == "null");//ランダムに選んだマスが存在しているものを見つけるまで繰り返す

        Output_GoalSetting(week, day);

    }

    public void Goal_Again()                                         //ゴールの再設置(同じ月にならないように)
    {
        int week, day;                                               //ランダムなゴールの場所を入れる
        for (int w = 0; w < Week.Length; w++)
        {
            for (int d = 0; d < Week[0].Day.Length; d++)
            {
                Output_GoalClear(w, d);                                   //全てのゴールを消す
            }
        }
        do
        {
            week = Random.Range(0, Week.Length);                    //横の列のランダム
            day = Random.Range(0, Week[0].Day.Length);              //縦の列のランダム
        } while (Week[week].Day[day].GetComponent<Mass_3D>().Day == "null" && MonthCount(day, week) == true);//選んだマスに日付があるか＆同じ月じゃないものを見つけるまで繰り返す

        Output_GoalSetting(week, day);
    }

    private bool MonthCount(int day, int week)//ゴールと同じ月か判断する
    {
        if (Month_Block(XGoal, YGoal) == Month_Block(day, week))//同じ月ならtrue
        {
            return true;
        }
        else//違う月ならfalse
        {
            return false;
        }
    }
    private int Month_Block(int day, int week)//day,weekが何月にいるのか調べる
    {
        int Month = 0;
        if (day < Week[0].Day.Length / 2 && week < Week.Length / 2) { Month = 1; }//左上のブロックにいるかどうか
        if (Week[0].Day.Length / 2 <= day && week < Week.Length / 2) { Month = 2; }//右上のブロックにいるかどうか
        if (day < Week[0].Day.Length / 2 && Week.Length / 2 < week) { Month = 3; }//左下の月にいるかどうか
        if (Week[0].Day.Length / 2 <= day && Week.Length / 2 < week) { Month = 4; }//右下の月にいるかどうか
        return Month;
    }

    //ゴールを設置する結果出力
    private void Output_GoalSetting(int week, int day)
    {
        XGoal = day; YGoal = week;
        Week[week].Day[day].GetComponent<Mass_3D>().Goal_setting();
    }

    //ゴールを消す結果出力
    private void Output_GoalClear(int week, int day)
    {
        Week[week].Day[day].GetComponent<Mass_3D>().Goal_Clear();
    }



    public void Output_DiceStart()
    {
        Dice.GetComponent<newRotate>().RotateStart();
    }

    public int Output_DiceStop()
    {
        Dice.GetComponent<newRotate>().newDiceStop();
        return Dice.GetComponent<newRotate>().DiceNum;
    }




    //プレイヤーターンを切り替える
    public void PlayerTurn_change()
    {

        if(Goal_check == true)//誰かがゴールしていたら
        {
            Goal_Again();//ゴールの再設置
            Goal_check = false;
        }

        Output_PlayerTurn();//プレイヤーのターンを変える
        if (joining_Player <= Player_Turn)
        {
            Player_Turn = 0;
        }

        for(int turn = 0; turn < Player.Length; turn++)
        {
            Output_anotherTurn(turn);//他プレイヤーのターンのボタンテキストを変更
        }

        Player[Player_Turn].GetComponent<Player_3D>().Dice_ready();
        
        Debug.Log("プレイヤー：" + Player_Turn);
    }

    //プレイヤーのターンを追加して出力
    private void Output_PlayerTurn()
    {
        Player_Turn++;
    }

    //他プレイヤーのターンの際、ボタンテキストを変える出力
    private void Output_anotherTurn(int player)
    {
        Player[player].GetComponent<Player_3D>().another_turn();
    }


    //マスクリックしたらplayerに飛ばす
    public void Player_select()
    {
        //Debug.Log("クリックしたやつマネージャーに飛んだ");
        Player[Player_Turn].GetComponent<Player_3D>().MoveSelect_Clicked();
    }


    









    //ゴールしたらの処理
    public void Goal_Add()
    {
        Output_GoalAdd();                       //ゴールした全体数に加える
        for (int week = 0; week < Week.Length; week++)
        {
            for (int day = 0; day < Week[0].Day.Length; day++)
            {
                Output_GoalClear(week, day);    //全てのゴールマスを消す
            }
        }
        Goal_check = true;                      //ゴールの再設置をするようにする
        if(Goal_AddCount >= 4)                  //全体で4回ゴールしたら
        {
            Output_GameFinish();                //ゲーム終了の処理
        }
    }

    //ゴールした全体数に加えて出力
    private void Output_GoalAdd()
    {
        Goal_AddCount++;
    }

    //ゲーム終了の処理を出力
    private void Output_GameFinish()
    {
        Debug.Log("ゲーム終了");
    }



    //日付のビデオを再生する出力
    public void Output_VideoStart(string day)
    {
        Video_obj.SetActive(true);                      //ビデオを表示にする
        Video_obj.GetComponent<VideoPlayer>().clip = Day_Animation.play_video("1/1");
        //Video_obj.GetComponent<VideoPlayer>().clip = Day_Animation.play_video(day); //本来こっちだがビデオが揃ってないので上ので代用
        Video_obj.GetComponent<VideoPlayer>().Play();   //ビデオの再生
    }

    //日付のビデオを非表示にする出力
    public void Output_VideoFinish()
    {
        Video_obj.SetActive(false);
    }

    //ホップアップの表示の出力
    public void Output_HopUp()
    {
        HopUp.SetActive(true); 
    }

    //ホップアップの非表示
    public void HopUp_hid()
    {
        HopUp.SetActive(false);
    }
}
