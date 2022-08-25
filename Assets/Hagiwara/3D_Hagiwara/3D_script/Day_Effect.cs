using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Day_Effect : MonoBehaviour
{
    private Image DayImage;
    public Day_Square_Master[] Day_Square_Master;

    private int MonthNumber;
    private int DayNumber;
    void Start()
    {
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
        DayImage.sprite = Day_Square_Master[MonthNumber].Day_Squares[DayNumber].HopUp;
    }

    public VideoClip Output_VideoClip(string Day)
    {
        DaySquare_Search(Day);
        return Day_Square_Master[MonthNumber].Day_Squares[DayNumber].Staging;
    }

    //Day_Square_Masterから特定の日付を持つものを探す
    private void DaySquare_Search(string Day)
    {
        for (int month = 0; month < Day_Square_Master.Length; month++)
        {
            for (int num = 0; num < Day_Square_Master[month].Day_Squares.Count; num++)
            {
                if (Day_Square_Master[month].Day_Squares[num].Day == Day)
                {
                    DayNumber = num;
                    MonthNumber = month;
                }
            }
        }
    }

    public void Day_EffectReaction(string Day)
    {
        DaySquare_Search(Day);
        Effect_Move();
        Effect_BGM();
        Effect_Dice();
        Effect_Instance();
    }



    private void Effect_Move()
    {
        string daySquare_Move = Day_Square_Master[MonthNumber].Day_Squares[DayNumber].Move;
        if (daySquare_Move != "noon")
        {
            char[] Char_Move = daySquare_Move.ToCharArray(); //Moveの内容をchar型に変換
            if (daySquare_Move.StartsWith("ワープ"))
            {
                if (daySquare_Move.Substring(3, 2) == "選択")
                {
                    //選択したプレイヤーの元に飛ぶ
                }
                else
                {
                    //指定マスへのワープ
                    gameObject.GetComponent<Player_3D>().Player_WarpMove("ワープ", daySquare_Move.Substring(3, 3));
                }

            }
            if (daySquare_Move.StartsWith("交換"))
            {
                //選択したプレイヤーとマスを交換する
            }
            if (daySquare_Move.StartsWith("上") || daySquare_Move.StartsWith("下") || daySquare_Move.StartsWith("右") || daySquare_Move.StartsWith("左"))
            {
                //上下左右、何マスの移動
                //Debug.Log("日付効果でのスライド移動" + Char_Move[0] + ":" + Toint(Char_Move[1]));
                gameObject.GetComponent<Player_3D>().Player_wayMove(Char_Move[0], Toint(Char_Move[1]));
            }
        }
    }

    private void Effect_BGM()
    {

        if (Day_Square_Master[MonthNumber].Day_Squares[DayNumber].BGM != "noon")
        {

        }
    }

    private void Effect_Dice()
    {
        char[] Char_NextDice = Day_Square_Master[MonthNumber].Day_Squares[DayNumber].NextDice.ToCharArray();
        if (Day_Square_Master[MonthNumber].Day_Squares[DayNumber].NextDice != "noon")
        {

        }
    }

    private void Effect_Instance()
    {
        char[] Char_Instance = Day_Square_Master[MonthNumber].Day_Squares[DayNumber].Instance.ToCharArray();
        if (Day_Square_Master[MonthNumber].Day_Squares[DayNumber].Instance != "noon")
        {

        }
    }

    private int Toint(char self)
    {
        return self - '0';
    }
}
