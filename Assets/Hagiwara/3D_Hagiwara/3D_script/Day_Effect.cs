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



    //�z�b�v�A�b�v�̒��ɋL�O�����A�L�O�������A�L�O���摜���o��
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

    //Day_Square_Master�������̓��t�������̂�T��
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
            char[] Char_Move = daySquare_Move.ToCharArray(); //Move�̓��e��char�^�ɕϊ�
            if (daySquare_Move.StartsWith("���[�v"))
            {
                if (daySquare_Move.Substring(3, 2) == "�I��")
                {
                    //�I�������v���C���[�̌��ɔ��
                }
                else
                {
                    //�w��}�X�ւ̃��[�v
                    gameObject.GetComponent<Player_3D>().Player_WarpMove("���[�v", daySquare_Move.Substring(3, 3));
                }

            }
            if (daySquare_Move.StartsWith("����"))
            {
                //�I�������v���C���[�ƃ}�X����������
            }
            if (daySquare_Move.StartsWith("��") || daySquare_Move.StartsWith("��") || daySquare_Move.StartsWith("�E") || daySquare_Move.StartsWith("��"))
            {
                //�㉺���E�A���}�X�̈ړ�
                //Debug.Log("���t���ʂł̃X���C�h�ړ�" + Char_Move[0] + ":" + Toint(Char_Move[1]));
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
