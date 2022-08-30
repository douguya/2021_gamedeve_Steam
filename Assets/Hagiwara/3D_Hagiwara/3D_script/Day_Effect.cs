using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Day_Effect : MonoBehaviour
{
    private game_manager game_Manager;
    private Image DayImage;
    public Day_Square_Master Day_Square_Master;

    private int DayNumber;
    private int Origin_XMass;
    private int Origin_YMass;

    private bool DiceChange = false;
    private bool[] DiceNumber = new bool[6];


    void Start()
    {
        for (int Dice = 0; Dice < DiceNumber.Length; Dice++)
        {
            DiceNumber[Dice] = false;
        }
        game_Manager = GameObject.Find("game_manager").GetComponent<game_manager>();
        DayImage = GameObject.Find("game_manager").GetComponent<game_manager>().HopUp.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    //ホップアップの中に記念日名、記念日説明、記念日画像を出力
    public void Output_HopUp_Setting(string Day)
    {
        DaySquare_Search(Day);
        DayImage.sprite = Day_Square_Master.Day_Squares[DayNumber].HopUp;
    }

    public VideoClip Output_VideoClip(string Day)
    {
        DaySquare_Search(Day);
        return Day_Square_Master.Day_Squares[DayNumber].Staging;
    }

    //Day_Square_Masterから特定の日付を持つものを探す
    private void DaySquare_Search(string Day)
    {
        for (int num = 0; num < Day_Square_Master.Day_Squares.Count; num++)
        {
            if (Day_Square_Master.Day_Squares[num].Day == Day)
            {
                DayNumber = num;
            }
        }

    }

    public void Day_EffectReaction(string Day)
    {
        DaySquare_Search(Day);
        Effect_Move();
        //Effect_BGM();
        Effect_Dice();
        Effect_NextMove();
        //Effect_Instance();
    }



    private void Effect_Move()
    {
        string daySquare_Move = Day_Square_Master.Day_Squares[DayNumber].Move;
        if (daySquare_Move != "Noon")
        {
            if (daySquare_Move != "none")
            {
                int turn = game_Manager.Player_Turn - 1;
                if (turn < 0)
                {
                    turn = game_Manager.joining_Player - 1;
                }
                char[] Char_Move = daySquare_Move.ToCharArray(); //Moveの内容をchar型に変換
                if (daySquare_Move.StartsWith("ワープ"))
                {
                    if (daySquare_Move.Remove(0, 3) == "選択プレイヤー")
                    {
                        Debug.Log("指定したプレイヤーにワープ");
                        //選択したプレイヤーの元に飛ぶ
                        Output_TurnChange(turn);
                        for (int Player = 0; Player < game_Manager.joining_Player; Player++)
                        {
                            if (turn != Player)
                            {
                                int XMass = game_Manager.Player[Player].GetComponent<Player_3D>().XPlayer_position;
                                int YMass = game_Manager.Player[Player].GetComponent<Player_3D>().YPlayer_position;
                                gameObject.GetComponent<Player_3D>().Effect = true;
                                game_Manager.Week[YMass].Day[XMass].GetComponent<Mass_3D>().select_display();
                            }
                        }

                    }
                    else
                    {
                        Debug.Log("指定マスワープ");
                        //指定マスへのワープ
                        Output_TurnChange(turn);
                        gameObject.GetComponent<Player_3D>().Player_WarpMove("ワープ", daySquare_Move.Remove(0, 3));
                    }

                }
                if (daySquare_Move.StartsWith("集合"))
                {
                    Debug.Log("集合");
                    for (int Player = 0; Player < game_Manager.joining_Player; Player++)
                    {
                        if (turn != Player)
                        {
                            Output_TurnChange(Player);
                            game_Manager.Player[Player].GetComponent<Player_3D>().Player_WarpMove("ワープ", daySquare_Move.Remove(0, 2));
                        }
                    }
                }
                if (daySquare_Move.StartsWith("選択"))
                {
                    Debug.Log("選択");
                    //複数あるマスから選択してワープ
                    Output_TurnChange(turn);
                    if (daySquare_Move.Remove(0, 2) == "ワープマス")
                    {
                        for (int week = 0; week < game_Manager.Week.Length; week++)
                        {
                            for (int day = 0; day < game_Manager.Week[0].Day.Length; day++)
                            {
                                if (game_Manager.Week[week].Day[day].GetComponent<Mass_3D>().warp == true)
                                {
                                    gameObject.GetComponent<Player_3D>().Effect = true;
                                    game_Manager.Week[week].Day[day].GetComponent<Mass_3D>().select_display();
                                }
                            }
                        }
                    }
                    if (daySquare_Move.Substring(2, 2) == "毎月")
                    {
                        for (int week = 0; week < game_Manager.Week.Length; week++)
                        {
                            for (int day = 0; day < game_Manager.Week[0].Day.Length; day++)
                            {
                                if (game_Manager.Week[week].Day[day].GetComponent<Mass_3D>().Day != "null")
                                {
                                    string[] Day_part = game_Manager.Week[week].Day[day].GetComponent<Mass_3D>().Day.Split('/');
                                    if (Day_part[1] == daySquare_Move.Remove(0, 4))
                                    {
                                        gameObject.GetComponent<Player_3D>().Effect = true;
                                        game_Manager.Week[week].Day[day].GetComponent<Mass_3D>().select_display();
                                    }
                                }
                            }
                        }
                    }
                    if (daySquare_Move.Remove(0, 2) == "全マス")
                    {
                        for (int week = 0; week < game_Manager.Week.Length; week++)
                        {
                            for (int day = 0; day < game_Manager.Week[0].Day.Length; day++)
                            {
                                gameObject.GetComponent<Player_3D>().Effect = true;
                                game_Manager.Week[week].Day[day].GetComponent<Mass_3D>().select_display();
                            }
                        }
                    }
                }
                if (daySquare_Move.StartsWith("交換"))
                {
                    Debug.Log("交換");
                    //選択したプレイヤーとマスを交換する
                    Output_TurnChange(turn);
                    Origin_XMass = gameObject.GetComponent<Player_3D>().XPlayer_position;
                    Origin_YMass = gameObject.GetComponent<Player_3D>().YPlayer_position;
                    for (int Player = 0; Player < game_Manager.joining_Player; Player++)
                    {
                        if (turn != Player)
                        {
                            int XMass = game_Manager.Player[Player].GetComponent<Player_3D>().XPlayer_position;
                            int YMass = game_Manager.Player[Player].GetComponent<Player_3D>().YPlayer_position;
                            gameObject.GetComponent<Player_3D>().Effect = true;
                            gameObject.GetComponent<Player_3D>().Exchange = true;
                            game_Manager.Week[YMass].Day[XMass].GetComponent<Mass_3D>().select_display();
                        }
                    }
                }
                if (daySquare_Move.StartsWith("上") || daySquare_Move.StartsWith("下") || daySquare_Move.StartsWith("右") || daySquare_Move.StartsWith("左"))
                {
                    Debug.Log("上下左右");
                    //上下左右、何マスの移動
                    Output_TurnChange(turn);
                    //Debug.Log("日付効果でのスライド移動" + Char_Move[0] + ":" + Toint(Char_Move[1]));
                    gameObject.GetComponent<Player_3D>().Player_wayMove(daySquare_Move.Substring(0, 1), Toint(Char_Move[1]));
                }
            }
        }
    }

    public void Exchange_Position()//交換処理
    {
        int turn = game_Manager.Player_Turn - 1;
        if (turn < 0)
        {
            turn = game_Manager.joining_Player - 1;
        }
        for (int Player = 0; Player < game_Manager.joining_Player; Player++)
        {
            if (game_Manager.Player[Player].GetComponent<Player_3D>().XPlayer_position == gameObject.GetComponent<Player_3D>().XPlayer_position && game_Manager.Player[Player].GetComponent<Player_3D>().YPlayer_position == gameObject.GetComponent<Player_3D>().YPlayer_position)
            {
                if (turn != Player)
                {
                    string day = game_Manager.Week[Origin_YMass].Day[Origin_XMass].GetComponent<Mass_3D>().Day;
                    Output_TurnChange(Player);
                    game_Manager.Player[Player].GetComponent<Player_3D>().Player_WarpMove("ワープ", day);
                    Debug.Log(day);
                }
            }
        }
    }

    public void Output_TurnChange(int Player)
    {
        game_Manager.Player[Player].GetComponent<Player_3D>().Turn_change = true;
    }







    private void Effect_BGM()
    {

        if (Day_Square_Master.Day_Squares[DayNumber].BGM != "Noon")
        {
            if (Day_Square_Master.Day_Squares[DayNumber].BGM != "none")
            {
            }

        }
    }




    private void Effect_Dice()
    {
        string daySquare_NextDice = Day_Square_Master.Day_Squares[DayNumber].NextDice;
        char[] Char_NextDice = daySquare_NextDice.ToCharArray();
        if (daySquare_NextDice != "Noon")
        {
            if (daySquare_NextDice != "none")
            {
                //ダイスの出目に増減
                if (daySquare_NextDice.StartsWith("+"))
                {
                    gameObject.GetComponent<Player_3D>().DiceAdd += Toint(Char_NextDice[1]);

                }
                if (daySquare_NextDice.StartsWith("*"))
                {
                    gameObject.GetComponent<Player_3D>().DiceMultiply += Toint(Char_NextDice[1]);

                }
                //ダイスの出目に増減(全員)
                if (daySquare_NextDice.StartsWith("全員"))
                {
                    if (daySquare_NextDice.Substring(2, 1) == "+")
                    {
                        for (int Player = 0; Player < game_Manager.joining_Player; Player++)
                        {
                            Output_DiceAdd(Player, Char_NextDice[1]);
                        }
                    }
                    if (daySquare_NextDice.Substring(2, 1) == "*")
                    {
                        for (int Player = 0; Player < game_Manager.joining_Player; Player++)
                        {
                            Output_DiceMultiply(Player, Char_NextDice[1]);
                        }
                    }
                }
                //ダイスの出目の変化
                if (daySquare_NextDice.StartsWith("出目"))
                {
                    DiceChange = true;
                    if (daySquare_NextDice.Contains("1"))
                    {
                        DiceNumber[0] = true;
                    }
                    if (daySquare_NextDice.Contains("2"))
                    {
                        DiceNumber[1] = true;
                    }
                    if (daySquare_NextDice.Contains("3"))
                    {
                        DiceNumber[2] = true;
                    }
                    if (daySquare_NextDice.Contains("4"))
                    {
                        DiceNumber[3] = true;
                    }
                    if (daySquare_NextDice.Contains("5"))
                    {
                        DiceNumber[4] = true;
                    }
                    if (daySquare_NextDice.Contains("6"))
                    {
                        DiceNumber[5] = true;
                    }
                }
            }
        }
    }

    public void DiceSetting()
    {
        if (DiceChange)
        {
            game_Manager.Dice.GetComponent<newRotate>().InDiceNum.Clear();
            for (int Dice = 0; Dice < DiceNumber.Length; Dice++)
            {
                if (DiceNumber[Dice] == true)
                {
                    game_Manager.Dice.GetComponent<newRotate>().InDiceNum.Add(Dice + 1);
                }
                DiceNumber[Dice] = false;
            }
            DiceChange = false;
        }
        else
        {
            game_Manager.Dice.GetComponent<newRotate>().resetDice();
        }
    }

    private void Output_DiceAdd(int Player, char add)
    {
        game_Manager.Player[Player].GetComponent<Player_3D>().DiceAdd += Toint(add);
    }

    private void Output_DiceMultiply(int Player, char Multiply)
    {
        game_Manager.Player[Player].GetComponent<Player_3D>().DiceAdd += Toint(Multiply);
    }













    private void Effect_NextMove()
    {
        string daySquare_NextMove = Day_Square_Master.Day_Squares[DayNumber].NextMove;
        char[] Char_NextMove = daySquare_NextMove.ToCharArray();
        if (daySquare_NextMove != "Noon")
        {
            if (daySquare_NextMove != "none")
            {
                if (daySquare_NextMove.StartsWith("2回"))
                {
                    gameObject.GetComponent<Player_3D>().OneMore_Dice = true;
                }

                if (daySquare_NextMove.StartsWith("ダイス"))
                {
                    Debug.Log("何マスかまで進んでいい");
                    if (daySquare_NextMove.Substring(3, 1) == "+")
                    {
                        gameObject.GetComponent<Player_3D>().MoveAdd_point += Toint(Char_NextMove[4]);
                    }
                }

                if (daySquare_NextMove.StartsWith("選択"))
                {
                    gameObject.GetComponent<Player_3D>().selectwark = true;
                    gameObject.GetComponent<Player_3D>().MoveAdd_point += Toint(Char_NextMove[2]);
                }

            }
        }
    }



    private void Effect_Instance()
    {
        char[] Char_Instance = Day_Square_Master.Day_Squares[DayNumber].Instance.ToCharArray();
        if (Day_Square_Master.Day_Squares[DayNumber].Instance != "Noon")
        {
            if (Day_Square_Master.Day_Squares[DayNumber].Instance != "none")
            {

            }
        }
    }

    private int Toint(char self)
    {
        return self - '0';
    }
}
