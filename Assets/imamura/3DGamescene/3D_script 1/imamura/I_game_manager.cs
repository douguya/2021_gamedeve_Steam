using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


public class I_game_manager : MonoBehaviourPunCallbacks
{

    public int[] month = new int[4];                        //設置する月を受け取る
    public string[] warp = new string[4];                   //設置するワープの位置を受け取る

    public List<GameObject> Player = new List<GameObject>();
    public List<GameObject> ItemList = new List<GameObject>();
    public int joining_Player = 0;                          //参加するプレイヤーの人数を取得
    public string[] Player_InitialPosition = new string[4]; //設置するプレイヤーの初期位置を受け取る
    public int Player_Turn = -1;                             //プレイヤーの現在の手番

    public Days[] Week = new Days[10];                      //Massの縦列のオブジェクトの取得・関数Daysで二次元配列にしている

    private int XGoal = 0, YGoal = 0;                       //ゴールのマスの横・縦の現在位置
    private bool Goal_check;                                //ゴールしたかどうか
    private int Goal_AddCount = 0;                          //ゴールした合計

    private I_Day_Animation Day_Animation;                    //日付の再生するビデオを受け渡すスクリプト取得
    public GameObject Video_obj;                            //ビデオ再生用のオブジェクト取得

    public GameObject HopUp;                                //ホップアップのオブジェクト取得

    public GameObject Dice;                                 //サイコロのオブジェクト取得

    public Hashtable hashtable = new Hashtable();//カスタムプロパティのリスト

    //　以下今村========================================================================================//

    private bool GameStart=false;
    public GameObject GameStartButton;
    public GameObject SceneManager;


    //  ここまで=========================================================================================//
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Day_Animation = GetComponent<I_Day_Animation>();
        Month_Setting();
       
        //Goal_Again();
    }
   public void Gamestart()
    {
        Goal_Decision();
        
      //  PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);//変更したカスタムプロパティの更新
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PlayerTurn_change();
        GameStartButton.SetActive(false);
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
        Week[week].Day[day].GetComponent<I_Mass_3D>().Day = month + 1 + "/" + countday;//日付を入れる
        Week[week].Day[day].GetComponent<I_Mass_3D>().hideCover_setting();             //hideCover(青いやつ)の表示
        Week[week].Day[day].GetComponent<I_Mass_3D>().DayText_setting(countday);
    }
   
  

    //ワープ出来るマスの設置出力
    private void Output_WarpSetting(int week, int day)
    {
        Week[week].Day[day].GetComponent<I_Mass_3D>().warp_setting();//ワープマスに設定
    }






    //プレイヤーの初期位置設定
    public void Player_setting(int playernum)
    {   


            for (int week = 0; week < Week.Length; week++)
            {
                for (int day = 0; day < Week[0].Day.Length; day++)
                {
                    if (Player_InitialPosition[playernum] == Week[week].Day[day].GetComponent<I_Mass_3D>().Day)
                    {
                        Output_PlayerSetting(playernum, week, day);//プレイヤーの初期位置設定
                    }
                }
            }
        
    }

    //アイテムリストの初期位置設定
    public void ItemList_setting(GameObject List,int num)
    {


        var ListTransform = List.GetComponent<RectTransform>().position;

        var ListSize = List.GetComponent<RectTransform>().sizeDelta;//リストのサイズを取得　（戻り値のため）

        ListTransform.x = (ListSize.x/2)+(ListSize.x*num);//マジックナンバーはUIの初期座標
        ListTransform.y =-ListSize.y/2;

    
        List.GetComponent<RectTransform>().anchoredPosition=ListTransform;


        





    }

    //プレイヤーの初期位置設定の結果出力
    private void Output_PlayerSetting(int player, int week, int day)
    {
       
        Player[player].GetComponent<I_Player_3D>().Player_indicate();                 //プレイヤーの表示
        Player[player].GetComponent<I_Player_3D>().Player_position_setting(week, day);//プレイヤーを初期位置へ
    }






    private void Goal_Decision()//初めてゴールを出現させる
    {
        int week, day;                                            //ランダムなゴールの場所を入れる
        do
        {
            week = Random.Range(0, Week.Length);                  //week・横の列のランダム
            day = Random.Range(0, Week[0].Day.Length);            //day・縦の列のランダム
        } while (Week[week].Day[day].GetComponent<I_Mass_3D>().Day == "null");//ランダムに選んだマスが存在しているものを見つけるまで繰り返す

       
        photonView.RPC(nameof(Output_GoalSetting), RpcTarget.AllViaServer, week, day);
    }







    public void Goal_Again()                                         //ゴールの再設置(同じ月にならないように)
    {
        int week, day;                                               //ランダムなゴールの場所を入れる
        for (int w = 0; w < Week.Length; w++)
        {
            for (int d = 0; d < Week[0].Day.Length; d++)
            {                                   
                photonView.RPC(nameof(Output_GoalClear), RpcTarget.AllViaServer, w, d);//全てのゴールを消す
            }
        }
        do
        {
            week = Random.Range(0, Week.Length);                    //横の列のランダム
            day = Random.Range(0, Week[0].Day.Length);              //縦の列のランダム
        } while (Week[week].Day[day].GetComponent<I_Mass_3D>().Day == "null" && MonthCount(day, week) == true);//選んだマスに日付があるか＆同じ月じゃないものを見つけるまで繰り返す

        photonView.RPC(nameof(Output_GoalSetting), RpcTarget.AllViaServer, week, day);
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

    [PunRPC]
    private void Output_GoalSetting(int week, int day)
    {
        XGoal = day; YGoal = week;
        Week[week].Day[day].GetComponent<I_Mass_3D>().Goal_setting();
    }





    [PunRPC]//ゴールを消す結果出力
    private void Output_GoalClear(int week, int day)
    {
        Week[week].Day[day].GetComponent<I_Mass_3D>().Goal_Clear();
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
        


        if (Goal_check == true)//誰かがゴールしていたら
        {
            Goal_Again();//ゴールの再設置
            Goal_check = false;
        }

        photonView.RPC(nameof(Output_ItemLifeSpan), RpcTarget.AllViaServer, Player_Turn);
        photonView.RPC(nameof(Output_PlayerTurn), RpcTarget.AllViaServer);
     

        for (int turn = 0; turn < Player.Count; turn++)
        {
            photonView.RPC(nameof(Output_anotherTurn), RpcTarget.AllViaServer, turn);//他プレイヤーのターンのボタンテキストを変更
        }
     
        photonView.RPC(nameof(Output_Dice_ready), RpcTarget.AllViaServer);
      
    }

    [PunRPC]
    private void Output_PlayerTurn()    //プレイヤーのターンを追加して出力
    {
   
        Player_Turn++;
        if (Player.Count <= Player_Turn)
        {
            Player_Turn = 0;
        }

    }

    [PunRPC]
    private void Output_ItemLifeSpan(int NowTurn)    //プレイヤーのターンを追加して出力
    {
        var NowPlayerItemus = Player[NowTurn].GetComponent<I_Player_3D>().Hub_Items;
        int num = NowPlayerItemus.Count;

        for (int loop = num-1; loop>=0; loop--)
        {

            if (NowPlayerItemus[loop].ItemLifespan!="null")
            {
                NowPlayerItemus[loop].ItemLifespan=(int.Parse(NowPlayerItemus[loop].ItemLifespan)-1).ToString();
                if (int.Parse(NowPlayerItemus[loop].ItemLifespan)<=0) { NowPlayerItemus.Remove(NowPlayerItemus[loop]); }
            }
        }

    }
    [PunRPC]//他プレイヤーのターンの際、ボタンテキストを変える出力
    private void Output_anotherTurn(int player)
    {
       
        Player[player].GetComponent<I_Player_3D>().another_turn();
    }

    [PunRPC]
    private void Output_Dice_ready()    //プレイヤーのターンを追加して出力
    {
 
        if (Player[Player_Turn].GetComponent<PhotonView>().IsMine)
        {
            Player[Player_Turn].GetComponent<I_Player_3D>().Dice_ready();
        }
          
    }



  

    //マスクリックしたらplayerに飛ばす
    public void Player_select()
    {
        //Debug.Log("クリックしたやつマネージャーに飛んだ");
        Player[Player_Turn].GetComponent<I_Player_3D>().MoveSelect_Clicked();
    }












    //ゴールしたらの処理
    public void Goal_Add()
    {
        photonView.RPC(nameof(Output_GoalAdd), RpcTarget.All);
                             //ゴールした全体数に加える
        for (int week = 0; week < Week.Length; week++)
        {
            for (int day = 0; day < Week[0].Day.Length; day++)
            {
                photonView.RPC(nameof(Output_GoalClear), RpcTarget.AllViaServer, week, day);//全てのゴールマスを消す
            }
        }
        Goal_check = true;                      //ゴールの再設置をするようにする
        if (Goal_AddCount >= 4)                  //全体で4回ゴールしたら
        {
            Output_GameFinish();                //ゲーム終了の処理
            photonView.RPC(nameof(Output_GameFinish), RpcTarget.AllViaServer);
        }
    }

    [PunRPC]//ゴールした全体数に加えて出力
    private void Output_GoalAdd()
    {
        Goal_AddCount++;
    }

    [PunRPC]//ゲーム終了の処理を出力
    private void Output_GameFinish()
    {

        SceneManager.GetComponent<SceneManagaer>().TransitionToResult();
        Debug.Log("ゲーム終了");
    }


    public void VideoStart(string day)
    {
        photonView.RPC(nameof(Output_VideoStart), RpcTarget.AllViaServer,day);
    }


    [PunRPC] //日付のビデオを再生する出力
    public void Output_VideoStart()
    {
        Video_obj.GetComponent<VideoPlayer>().Play();   //ビデオの再生
    }
    [PunRPC]public void Output_VideoSetting()
    {
        Video_obj.SetActive(true);
    }



    public void VideoFinish()
    {
        photonView.RPC(nameof(Output_VideoFinish), RpcTarget.AllViaServer);
    }
    [PunRPC]//日付のビデオを非表示にする出力
    public void Output_VideoFinish()
    {
        Video_obj.SetActive(false);
    }


    public void HopUpAppearance()
    {
        photonView.RPC(nameof(Output_HopUp), RpcTarget.AllViaServer);
    }
    [PunRPC] //ホップアップの表示の出力
    public void Output_HopUp()
    {
        HopUp.SetActive(true);
    }

    //ホップアップの非表示
    public void HopUp_hid()
    {
        HopUp.SetActive(false);
    }

    //以下今村=============================================================================================================================

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)//カスタムプロパティが変更されたときの挙動
    {
       
        if (propertiesThatChanged ==  hashtable["Gamestart"]&& (bool)propertiesThatChanged["Gamestart"]==true)//ｐ番目のプレイヤーの準備が完了しているなら
        {
       
        }

    }






    //ここまで=============================================================================================================================
    public void Player_Same()
    {
        int[] Alignment = { 4, 4, 4, 4 };
        int Count = 0;
        bool differNum_1 = true;
        bool differNum_2 = true;
        int[] Player_XPosition = new int[4];
        int[] Player_YPosition = new int[4];
        //01,02,03,12,13,23
        for (int PlayerNumber = 0; PlayerNumber < Player.Count; PlayerNumber++)
        {
            Player_XPosition[PlayerNumber] = Player[PlayerNumber].GetComponent<I_Player_3D>().XPlayer_position;
            Player_YPosition[PlayerNumber] = Player[PlayerNumber].GetComponent<I_Player_3D>().YPlayer_position;
        }
        for (int PlayerNumber = 0; PlayerNumber < Player.Count - 1; PlayerNumber++)// 0, 1, 2
        {
            if (PlayerNumber + 1 < Player.Count)//01,12,23
            {
                if (Player_XPosition[PlayerNumber] == Player_XPosition[PlayerNumber + 1] && Player_YPosition[PlayerNumber] == Player_YPosition[PlayerNumber + 1])
                {
                    for (int PCount = 0; PCount < Alignment.Length; PCount++)
                    {
                        if (Alignment[PCount] == PlayerNumber)
                        {
                            differNum_1 = false;
                        }
                        if (Alignment[PCount] == PlayerNumber + 1)
                        {
                            differNum_2 = false;
                        }
                    }
                    if (differNum_1)
                    {
                        Alignment[Count] = PlayerNumber;
                        Count++;
                    }
                    if (differNum_2)
                    {
                        Alignment[Count] = PlayerNumber + 1;
                        Count++;
                    }
                    differNum_1 = true;
                    differNum_2 = true;
                }
            }
            if (PlayerNumber + 2 < Player.Count)//02,13
            {
                if (Player_XPosition[PlayerNumber] == Player_XPosition[PlayerNumber + 2] && Player_YPosition[PlayerNumber] == Player_YPosition[PlayerNumber + 2])
                {
                    for (int PCount = 0; PCount < Alignment.Length; PCount++)
                    {
                        if (Alignment[PCount] == PlayerNumber)
                        {
                            differNum_1 = false;
                        }
                        if (Alignment[PCount] == PlayerNumber + 2)
                        {
                            differNum_2 = false;
                        }
                    }
                    if (differNum_1)
                    {
                        Alignment[Count] = PlayerNumber;
                        Count++;
                    }
                    if (differNum_2)
                    {
                        Alignment[Count] = PlayerNumber + 2;
                        Count++;
                    }
                    differNum_1 = true;
                    differNum_2 = true;
                }
            }
            if (PlayerNumber + 3 < Player.Count)//03
            {
                if (Player_XPosition[PlayerNumber] == Player_XPosition[PlayerNumber + 3] && Player_YPosition[PlayerNumber] == Player_YPosition[PlayerNumber + 3])
                {
                    for (int PCount = 0; PCount < Alignment.Length; PCount++)
                    {
                        if (Alignment[PCount] == PlayerNumber)
                        {
                            differNum_1 = false;
                        }
                        if (Alignment[PCount] == PlayerNumber + 3)
                        {
                            differNum_2 = false;
                        }
                    }
                    if (differNum_1)
                    {
                        Alignment[Count] = PlayerNumber;
                        Count++;
                    }
                    if (differNum_2)
                    {
                        Alignment[Count] = PlayerNumber + 3;
                        Count++;
                    }
                    differNum_1 = true;
                    differNum_2 = true;
                }
            }
        }
        //Debug.Log("0:" + Alignment[0] + "   1:" + Alignment[1] + "   2:" + Alignment[2] + "   3:" + Alignment[3]);
        Player_Alignment(Alignment, Player_XPosition, Player_YPosition);
    }
    private void Player_Alignment(int[] Alignment, int[] Player_XPosition, int[] Player_YPosition)
    {
        bool Alignment_Num = true;
        if (Alignment[0] != 4)//同じマスのものはいるか
        {
            bool[] num = { false, false, false, false };
            for (int Player = 0; Player < Alignment.Length; Player++)
            {
                if (Alignment[Player] != 4)
                {
                    num[Player] = true;
                }
            }
            for (int Player = 0; Player < Alignment.Length; Player++)
            {
                if (num[Player])
                {
                    this.Player[Player].GetComponent<Transform>().position = Week[Player_YPosition[Player]].Day[Player_XPosition[Player]].GetComponent<Transform>().position;
                }
            }
            if (Alignment[2] != 4)//3つ以上いるか
            {
                if (Player_XPosition[Alignment[1]] == Player_XPosition[Alignment[2]] && Player_YPosition[Alignment[1]] == Player_YPosition[Alignment[2]])//3つが同じマスになのか
                {
                    //昇順
                    int tmp;
                    for (int i = 0; i < Alignment.Length; ++i)
                    {
                        for (int j = i + 1; j < Alignment.Length; ++j)
                        {
                            if (Alignment[i] > Alignment[j])
                            {
                                tmp = Alignment[i];
                                Alignment[i] = Alignment[j];
                                Alignment[j] = tmp;
                            }
                        }
                    }
                    Alignment_Num = false;
                    //3つが同じマス
                    //Debug.Log("みっつ");
                    Setting_Aligement(3, Alignment, Player_XPosition, Player_YPosition);
                }
                else//2つの別の組か
                {
                    Alignment_Num = false;
                    //Debug.Log("二組");
                    Setting_Aligement(0, Alignment, Player_XPosition, Player_YPosition);
                    Setting_Aligement(2, Alignment, Player_XPosition, Player_YPosition);
                }
            }
            if (Alignment_Num)
            {
                //1組のみ
                //Debug.Log("一組");
                Setting_Aligement(0, Alignment, Player_XPosition, Player_YPosition);
            }
        }
    }

    private void Setting_Aligement(int Same, int[] Alignment, int[] Player_XPosition, int[] Player_YPosition)
    {
        //Debug.Log("aaaaa" + Alignment[a] + ":" + Alignment[a]);
        Vector3 SameMass = Week[Player_YPosition[Alignment[0]]].Day[Player_XPosition[Alignment[0]]].GetComponent<Transform>().position;
        if (Same == 0)
        {
            Output_PlayerAlignment(SameMass, Alignment[0], -0.6f, 0);
            Output_PlayerAlignment(SameMass, Alignment[1], 0.6f, 0);
        }
        if (Same == 2)
        {
            SameMass = Week[Player_YPosition[Alignment[Same]]].Day[Player_XPosition[Alignment[Same]]].GetComponent<Transform>().position;
            Output_PlayerAlignment(SameMass, Alignment[2], -0.6f, 0);
            Output_PlayerAlignment(SameMass, Alignment[3], 0.6f, 0);
        }
        if (Same == 3)
        {
            if (Alignment[3] == 4)
            {
                Output_PlayerAlignment(SameMass, Alignment[0], -0.6f, 0.6f);
                Output_PlayerAlignment(SameMass, Alignment[1], 0.6f, 0.6f);
                Output_PlayerAlignment(SameMass, Alignment[2], 0, -0.6f);
            }
            else
            {
                Output_PlayerAlignment(SameMass, Alignment[0], -0.6f, 0.6f);
                Output_PlayerAlignment(SameMass, Alignment[1], 0.6f, 0.6f);
                Output_PlayerAlignment(SameMass, Alignment[2], -0.6f, -0.6f);
                Output_PlayerAlignment(SameMass, Alignment[3], 0.6f, -0.6f);
            }
        }
    }
    
    private void Output_PlayerAlignment(Vector3 SameMass, int Player, float Xslide, float Zslide)
    {
        photonView.RPC(nameof(RPCPlayerAlignment), RpcTarget.AllViaServer, SameMass, Player, Xslide, Zslide);
    }
    [PunRPC]
    private void RPCPlayerAlignment(Vector3 SameMass, int Player, float Xslide, float Zslide)
    {
        this.Player[Player].GetComponent<Transform>().position = new Vector3(SameMass.x + Xslide, SameMass.y, SameMass.z + Zslide);
       
    }
}


