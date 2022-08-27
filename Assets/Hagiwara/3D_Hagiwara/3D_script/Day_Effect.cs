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

    private bool PlayerTurn_change = true;

    void Start()
    {
        game_Manager = GameObject.Find("game_manager").GetComponent<game_manager>();
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
        DayImage.sprite = Day_Square_Master.Day_Squares[DayNumber].HopUp;
    }

    public VideoClip Output_VideoClip(string Day)
    {
        DaySquare_Search(Day);
        return Day_Square_Master.Day_Squares[DayNumber].Staging;
    }

    //Day_Square_Master�������̓��t�������̂�T��
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
        PlayerTurn_change = true;
        DaySquare_Search(Day);
        Effect_Move();
        //Effect_BGM();
        //Effect_Dice();
        //Effect_Instance();
        if (PlayerTurn_change)
        {
            game_Manager.PlayerTurn_change();
        }
    }



    private void Effect_Move()
    {
        string daySquare_Move = Day_Square_Master.Day_Squares[DayNumber].Move;
        if (daySquare_Move != "Noon")
        {
            if (daySquare_Move != "none")
            {
                PlayerTurn_change = false;
                char[] Char_Move = daySquare_Move.ToCharArray(); //Move�̓��e��char�^�ɕϊ�
                if (daySquare_Move.StartsWith("���[�v"))
                {
                    if (daySquare_Move.Substring(3, 2) == "�I��")
                    {
                        Debug.Log("�w�肵���v���C���[�Ƀ��[�v");
                        //�I�������v���C���[�̌��ɔ��
                        for (int Player = 0; Player < game_Manager.joining_Player; Player++)
                        {
                            if (game_Manager.Player_Turn != Player)
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
                        Debug.Log("�w��}�X���[�v");
                        //�w��}�X�ւ̃��[�v
                        gameObject.GetComponent<Player_3D>().Player_WarpMove("���[�v", daySquare_Move.Remove(0, 3));
                    }

                }
                if (daySquare_Move.StartsWith("�W��"))
                {
                    Debug.Log("�W��");
                    for (int Player = 0; Player < game_Manager.joining_Player; Player++)
                    {
                        if (game_Manager.Player_Turn != Player)
                        {
                            game_Manager.Player[Player].GetComponent<Player_3D>().Player_WarpMove("���[�v", daySquare_Move.Remove(0, 2));
                            game_Manager.Player[Player].GetComponent<Player_3D>().Turn_change = true;
                        }
                    }
                    game_Manager.PlayerTurn_change();
                }
                if (daySquare_Move.StartsWith("�I��"))
                {
                    Debug.Log("�I��");
                    //��������}�X����I�����ă��[�v
                    if (daySquare_Move.Substring(1, 5) == "���[�v�}�X")
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
                    if (daySquare_Move.Substring(2, 2) == "����")
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
                }
                if (daySquare_Move.StartsWith("����"))
                {
                    Debug.Log("����");
                    //�I�������v���C���[�ƃ}�X����������
                    Origin_XMass = gameObject.GetComponent<Player_3D>().XPlayer_position;
                    Origin_YMass = gameObject.GetComponent<Player_3D>().YPlayer_position;
                    for (int Player = 0; Player < game_Manager.joining_Player; Player++)
                    {
                        if (game_Manager.Player_Turn != Player)
                        {
                            int XMass = game_Manager.Player[Player].GetComponent<Player_3D>().XPlayer_position;
                            int YMass = game_Manager.Player[Player].GetComponent<Player_3D>().YPlayer_position;
                            gameObject.GetComponent<Player_3D>().Effect = true;
                            gameObject.GetComponent<Player_3D>().Exchange = true;
                            game_Manager.Week[YMass].Day[XMass].GetComponent<Mass_3D>().select_display();
                        }
                    }
                }
                if (daySquare_Move.StartsWith("��") || daySquare_Move.StartsWith("��") || daySquare_Move.StartsWith("�E") || daySquare_Move.StartsWith("��"))
                {
                    Debug.Log("�㉺���E");
                    //�㉺���E�A���}�X�̈ړ�
                    //Debug.Log("���t���ʂł̃X���C�h�ړ�" + Char_Move[0] + ":" + Toint(Char_Move[1]));
                    gameObject.GetComponent<Player_3D>().Player_wayMove(daySquare_Move.Substring(0, 1), Toint(Char_Move[1]));
                }
            }
        }
    }

    public void Exchange_Position()//��������
    {
        for (int Player = 0; Player < game_Manager.joining_Player; Player++)
        {
            if (game_Manager.Player[Player].GetComponent<Player_3D>().XPlayer_position == gameObject.GetComponent<Player_3D>().XPlayer_position && game_Manager.Player[Player].GetComponent<Player_3D>().YPlayer_position == gameObject.GetComponent<Player_3D>().YPlayer_position)
            {
                if (game_Manager.Player_Turn != Player)
                {
                    string day = game_Manager.Week[Origin_YMass].Day[Origin_XMass].GetComponent<Mass_3D>().Day;
                    game_Manager.Player[Player].GetComponent<Player_3D>().Player_WarpMove("���[�v", day);
                    Debug.Log(day);
                    game_Manager.Player[Player].GetComponent<Player_3D>().Turn_change = true;
                }
            }
        }
    }







    private void Effect_BGM()
    {

        if (Day_Square_Master.Day_Squares[DayNumber].BGM != "Noon")
        {
            if (Day_Square_Master.Day_Squares[DayNumber].BGM != "none")
            {
                PlayerTurn_change = false;
            }

        }
    }

    private void Effect_Dice()
    {
        char[] Char_NextDice = Day_Square_Master.Day_Squares[DayNumber].NextDice.ToCharArray();
        if (Day_Square_Master.Day_Squares[DayNumber].NextDice != "Noon")
        {
            if (Day_Square_Master.Day_Squares[DayNumber].NextDice != "none")
            {
                PlayerTurn_change = false;
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
                PlayerTurn_change = false;
            }
        }
    }

    private int Toint(char self)
    {
        return self - '0';
    }
}
