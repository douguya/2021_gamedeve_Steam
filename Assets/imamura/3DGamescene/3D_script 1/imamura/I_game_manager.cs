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

    public int[] month = new int[4];                        //�ݒu���錎���󂯎��
    public string[] warp = new string[4];                   //�ݒu���郏�[�v�̈ʒu���󂯎��

    public List<GameObject> Player = new List<GameObject>();
    public int joining_Player = 0;                          //�Q������v���C���[�̐l�����擾
    public string[] Player_InitialPosition = new string[4]; //�ݒu����v���C���[�̏����ʒu���󂯎��
    public int Player_Turn = -1;                             //�v���C���[�̌��݂̎��

    public Days[] Week = new Days[10];                      //Mass�̏c��̃I�u�W�F�N�g�̎擾�E�֐�Days�œ񎟌��z��ɂ��Ă���

    private int XGoal = 0, YGoal = 0;                       //�S�[���̃}�X�̉��E�c�̌��݈ʒu
    private bool Goal_check;                                //�S�[���������ǂ���
    private int Goal_AddCount = 0;                          //�S�[���������v

    private I_Day_Animation Day_Animation;                    //���t�̍Đ�����r�f�I���󂯓n���X�N���v�g�擾
    public GameObject Video_obj;                            //�r�f�I�Đ��p�̃I�u�W�F�N�g�擾

    public GameObject HopUp;                                //�z�b�v�A�b�v�̃I�u�W�F�N�g�擾

    public GameObject Dice;                                 //�T�C�R���̃I�u�W�F�N�g�擾



    //�@�ȉ�����========================================================================================//

    private bool GameStart=false;



    //  �����܂�=========================================================================================//

    void Start()
    {
        Day_Animation = GetComponent<I_Day_Animation>();
        Month_Setting();
        Goal_Decision();
        //Goal_Again();
    }
   public void Gamestart()
    {
        PlayerTurn_change();
        Output_DiceStop();

    }

    
    void Update()
    {

    }



    [System.Serializable]
    public class Days//week�̎q�E����̃I�u�W�F�N�g�̎擾
    {
        public GameObject[] Day;

    }




    //MonthSetting���ĂԂ�month����󂯎�������̓��t��Mass��Day�ɓ����&���ꂽ���t��warp���󂯎���Ă���t�Ȃ烏�[�v��ݒu����
    private void Month_Setting()
    {
        int Xmonth = 0;//�ݒu����}�b�v��X�̔z������炷
        int Ymonth = 0;//�ݒu����}�b�v��Y�̔z������炷

        for (int block = 0; block < this.month.Length; block++)//�w�肷�錎���ǂ̃u���b�N�ɂ��邩����
        {
            switch (block)//���ꂼ��̃u���b�N�Ɏw�肵�����t������悤�ɂ���
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
            for (int month = 0; month < 12; month++)//month�ɉ����������
            {
                if (this.month[block] == month + 1)//�w�肵���������������ʂ���
                {
                    Day_Setting(month, Ymonth, Xmonth);//�}�X�ɓ��t������
                }
            }
        }

    }

    //MonthSetting���猎�Ɠ����}�X�̏ꏊ���󂯎��}�X�ɓ��t������&���ꂽ���t�����[�v�̃}�X�Ȃ烏�[�v�}�X�ɂ���
    private void Day_Setting(int month, int Ymonth, int Xmonth)
    {
        int nullday = 0;//�󔒂̓��t
        int countday = 0;//�������t
        int Maxday = 0;//���̌��̍ő���t

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
        for (int n = 0; n < 7 - nullday; n++)//���T�ڂɓ��t������
        {
            countday++;
            Output_DaySetting(month, countday, Ymonth, Xmonth + nullday + n);//���t������

            for (int i = 0; i < warp.Length; i++)//���[�v�̒ǉ�
            {
                if (warp[i] == month + 1 + "/" + countday)//���ꂽ���t�����[�v��ݒu�ʒu�������烏�[�v�̃}�X�ɂ���
                {
                    Output_WarpSetting(Ymonth, Xmonth + nullday + n);//���[�v�o����}�X�̐ݒu
                }
            }
        }

        for (int h = 1; h < Week.Length / 2; h++)//���T�ȍ~�̓��t������
        {
            for (int w = 0; w < Week[0].Day.Length / 2; w++)
            {
                if (countday < Maxday)
                {
                    countday++;
                    Output_DaySetting(month, countday, Ymonth + h, Xmonth + w);//���t������

                    for (int i = 0; i < warp.Length; i++)//���[�v�̒ǉ�
                    {
                        if (warp[i] == month + 1 + "/" + countday)//���ꂽ���t�����[�v��ݒu�ʒu�������烏�[�v�̃}�X�ɂ���
                        {
                            Output_WarpSetting(Ymonth + h, Xmonth + w);//���[�v�o����}�X�̐ݒu
                        }
                    }
                }
            }
        }
    }

    //�}�X�ɓ��t�����錋�ʏo��
    private void Output_DaySetting(int month, int countday, int week, int day)
    {
        Week[week].Day[day].GetComponent<I_Mass_3D>().Day = month + 1 + "/" + countday;//���t������
        Week[week].Day[day].GetComponent<I_Mass_3D>().hideCover_setting();             //hideCover(�����)�̕\��
    }

    //���[�v�o����}�X�̐ݒu�o��
    private void Output_WarpSetting(int week, int day)
    {
        Week[week].Day[day].GetComponent<I_Mass_3D>().warp_setting();//���[�v�}�X�ɐݒ�
    }






    //�v���C���[�̏����ʒu�ݒ�
    public void Player_setting(int playernum)
    {   


            for (int week = 0; week < Week.Length; week++)
            {
                for (int day = 0; day < Week[0].Day.Length; day++)
                {
                    if (Player_InitialPosition[playernum] == Week[week].Day[day].GetComponent<I_Mass_3D>().Day)
                    {
                        Output_PlayerSetting(playernum, week, day);//�v���C���[�̏����ʒu�ݒ�
                    }
                }
            }
        
    }

    //�v���C���[�̏����ʒu�ݒ�̌��ʏo��
    private void Output_PlayerSetting(int player, int week, int day)
    {
        Player[player].GetComponent<I_Player_3D>().Player_indicate();                 //�v���C���[�̕\��
        Player[player].GetComponent<I_Player_3D>().Player_position_setting(week, day);//�v���C���[�������ʒu��
    }






    private void Goal_Decision()//���߂ăS�[�����o��������
    {
        int week, day;                                            //�����_���ȃS�[���̏ꏊ������
        do
        {
            week = Random.Range(0, Week.Length);                  //week�E���̗�̃����_��
            day = Random.Range(0, Week[0].Day.Length);            //day�E�c�̗�̃����_��
        } while (Week[week].Day[day].GetComponent<I_Mass_3D>().Day == "null");//�����_���ɑI�񂾃}�X�����݂��Ă�����̂�������܂ŌJ��Ԃ�

        Output_GoalSetting(week, day);

    }

    public void Goal_Again()                                         //�S�[���̍Đݒu(�������ɂȂ�Ȃ��悤��)
    {
        int week, day;                                               //�����_���ȃS�[���̏ꏊ������
        for (int w = 0; w < Week.Length; w++)
        {
            for (int d = 0; d < Week[0].Day.Length; d++)
            {
                Output_GoalClear(w, d);                                   //�S�ẴS�[��������
            }
        }
        do
        {
            week = Random.Range(0, Week.Length);                    //���̗�̃����_��
            day = Random.Range(0, Week[0].Day.Length);              //�c�̗�̃����_��
        } while (Week[week].Day[day].GetComponent<I_Mass_3D>().Day == "null" && MonthCount(day, week) == true);//�I�񂾃}�X�ɓ��t�����邩������������Ȃ����̂�������܂ŌJ��Ԃ�

        Output_GoalSetting(week, day);
    }

    private bool MonthCount(int day, int week)//�S�[���Ɠ����������f����
    {
        if (Month_Block(XGoal, YGoal) == Month_Block(day, week))//�������Ȃ�true
        {
            return true;
        }
        else//�Ⴄ���Ȃ�false
        {
            return false;
        }
    }
    private int Month_Block(int day, int week)//day,week�������ɂ���̂����ׂ�
    {
        int Month = 0;
        if (day < Week[0].Day.Length / 2 && week < Week.Length / 2) { Month = 1; }//����̃u���b�N�ɂ��邩�ǂ���
        if (Week[0].Day.Length / 2 <= day && week < Week.Length / 2) { Month = 2; }//�E��̃u���b�N�ɂ��邩�ǂ���
        if (day < Week[0].Day.Length / 2 && Week.Length / 2 < week) { Month = 3; }//�����̌��ɂ��邩�ǂ���
        if (Week[0].Day.Length / 2 <= day && Week.Length / 2 < week) { Month = 4; }//�E���̌��ɂ��邩�ǂ���
        return Month;
    }

    //�S�[����ݒu���錋�ʏo��
    private void Output_GoalSetting(int week, int day)
    {
        XGoal = day; YGoal = week;
        Week[week].Day[day].GetComponent<I_Mass_3D>().Goal_setting();
    }

    //�S�[�����������ʏo��
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




    //�v���C���[�^�[����؂�ւ���
    public void PlayerTurn_change()
    {

        if (Goal_check == true)//�N�����S�[�����Ă�����
        {
            Goal_Again();//�S�[���̍Đݒu
            Goal_check = false;
        }

        Output_PlayerTurn();//�v���C���[�̃^�[����ς���
        if (joining_Player <= Player_Turn)
        {
            Player_Turn = 0;
        }

        for (int turn = 0; turn < Player.Count; turn++)
        {
            Output_anotherTurn(turn);//���v���C���[�̃^�[���̃{�^���e�L�X�g��ύX
        }

        Player[Player_Turn].GetComponent<I_Player_3D>().Dice_ready();

        Debug.Log("�v���C���[�F" + Player_Turn);
    }

    //�v���C���[�̃^�[����ǉ����ďo��
    private void Output_PlayerTurn()
    {
        Player_Turn++;
    }

    //���v���C���[�̃^�[���̍ہA�{�^���e�L�X�g��ς���o��
    private void Output_anotherTurn(int player)
    {
        Player[player].GetComponent<I_Player_3D>().another_turn();
    }


    //�}�X�N���b�N������player�ɔ�΂�
    public void Player_select()
    {
        //Debug.Log("�N���b�N������}�l�[�W���[�ɔ��");
        Player[Player_Turn].GetComponent<I_Player_3D>().MoveSelect_Clicked();
    }












    //�S�[��������̏���
    public void Goal_Add()
    {
        Output_GoalAdd();                       //�S�[�������S�̐��ɉ�����
        for (int week = 0; week < Week.Length; week++)
        {
            for (int day = 0; day < Week[0].Day.Length; day++)
            {
                Output_GoalClear(week, day);    //�S�ẴS�[���}�X������
            }
        }
        Goal_check = true;                      //�S�[���̍Đݒu������悤�ɂ���
        if (Goal_AddCount >= 4)                  //�S�̂�4��S�[��������
        {
            Output_GameFinish();                //�Q�[���I���̏���
        }
    }

    //�S�[�������S�̐��ɉ����ďo��
    private void Output_GoalAdd()
    {
        Goal_AddCount++;
    }

    //�Q�[���I���̏������o��
    private void Output_GameFinish()
    {
        Debug.Log("�Q�[���I��");
    }



    //���t�̃r�f�I���Đ�����o��
    public void Output_VideoStart(string day)
    {
        Video_obj.SetActive(true);                      //�r�f�I��\���ɂ���
        Video_obj.GetComponent<VideoPlayer>().clip = Day_Animation.play_video("1/1");
        //Video_obj.GetComponent<VideoPlayer>().clip = Day_Animation.play_video(day); //�{�������������r�f�I�������ĂȂ��̂ŏ�̂ő�p
        Video_obj.GetComponent<VideoPlayer>().Play();   //�r�f�I�̍Đ�
    }

    //���t�̃r�f�I���\���ɂ���o��
    public void Output_VideoFinish()
    {
        Video_obj.SetActive(false);
    }

    //�z�b�v�A�b�v�̕\���̏o��
    public void Output_HopUp()
    {
        HopUp.SetActive(true);
    }

    //�z�b�v�A�b�v�̔�\��
    public void HopUp_hid()
    {
        HopUp.SetActive(false);
    }

    //�ȉ�����=============================================================================================================================


    





    //�����܂�=============================================================================================================================

}

