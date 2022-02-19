using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sugorokuManager : MonoBehaviour
{
    private int XGoal, YGoal;                       //�S�[���̍��W
    
    public GameObject[] Player = new GameObject[4]; //�v���C���[�I�u�W�F�N�g�擾
    public Width[] height=new Width[10];                             //Mass�̏c��̃I�u�W�F�N�g�̎擾�E��ԉ��œ񎟌��z��ɂ��Ă���
    private int Playerturn = 0;                     //�v���C���[�̎�ԊǗ�
    
    private int Playcount = 0;                      //�v���C���[�̎Q���l��
    private int play = 0;                           //�N�̔Ԃ�


    private bool gamestart = false;

    void Start()
    {
    
        


        GoalDecision();//�S�[���̑I��

    }


    void Update()
    {

        if (gamestart)
        {
            switch (Playerturn)
            {
                case 0:
                    Player[play].GetComponent<PlayerStatus>().step = 1;             //�v���C���[���R���g���[���o����悤�ɂ���
                    Playerturn = 1;
                    break;

                case 1:
                    if (Player[play].GetComponent<PlayerStatus>().Goalup == true)   //�������̎�ԂɃS�[�����Ă�����
                    {
                        Player[play].GetComponent<PlayerStatus>().Goalup = false;   //�S�[���錾������
                        GoalAgain();                                                //�S�[���̍Đݒu
                    }
                    if (Player[play].GetComponent<PlayerStatus>().GetGaol() == 4)   //�S�[�����������S�Ȃ�
                    {
                        Playerturn = 3;                                             //�Q�[���I��
                    }
                    if (Player[play].GetComponent<PlayerStatus>().nextturn == true) //�v���C���[���^�[�����I�����Ă�����
                    {
                        Player[play].GetComponent<PlayerStatus>().nextturn = false;
                        Playerturn = 2;
                        play++;                                                    //���̃v���C���[�̔Ԃɂ���
                    }
                    break;

                case 2:
                    Playerturn = 0;
                    if (play >= Playcount)//�v���C���[�Q���l���𒴂�����
                    {
                        play = 0;     //�v���C���[0�̎�ԂɂȂ�
                    }
                    break;

                case 3:
                    //�Q�[���I��
                    Debug.Log("�Q�[���I��");
                    break;
            }
        }
    }


    private void GoalClear()//�S�Ẵ}�X�̃S�[��������
    {
        for (int i = 0; i < height.Length; i++)
        {
            for (int l = 0; l < height[0].width.Length; l++)
            {
               // Debug.Log(height[i].width[l]);
                height[i].width[l].GetComponent<Mass>().GoalOff();//�S�[���������Ă���
            }
        }
    }

    private void GoalDecision()//���߂ăS�[�����o��������
    {
        int Week, Day;                                               //�����_���ȃS�[���̏ꏊ������
        GoalClear();                                                 //�S�Ẵ}�X�̃S�[��������
        do {
            Week = Random.Range(0, height.Length);                  //week�E���̗�̃����_��
            Day = Random.Range(0, height[0].width.Length);          //day�E�c�̗�̃����_��
        } while (height[Week].width[Day].GetComponent<Mass>().invalid == true);//�����_���ɑI�񂾃}�X�����݂��Ă�����̂�������܂ŌJ��Ԃ�
        height[Week].width[Day].GetComponent<Mass>().GoalOn();      //�S�[���̐ݒu
        XGoal = Day; YGoal = Week;                                  //�S�[���z��ԍ����L��

    }

    public void GoalAgain()                                         //�S�[���̍Đݒu(�������ɂȂ�Ȃ��悤��)
    {
        int Week, Day;                                              //�����_���ȃS�[���̏ꏊ������
        GoalClear();                                                //�S�Ẵ}�X�̃S�[��������
        do
        {
            Week = Random.Range(0, height.Length);                  //���̗�̃����_��
            Day = Random.Range(0, height[0].width.Length);          //�c�̗�̃����_��
        } while (height[Week].width[Day].GetComponent<Mass>().invalid == true && MonthCount(Day, Week) == true);//�I�񂾃}�X�����݂��Ă�����́�����������Ȃ����̂�������܂ŌJ��Ԃ�
        height[Week].width[Day].GetComponent<Mass>().GoalOn();      //�S�[���̐ݒu
        XGoal = Day; YGoal = Week;                                  //�S�[���z��ԍ����L��
    }

    private bool MonthCount(int x, int y)//�S�[���Ɠ����������f����
    {
        if (WhichMonth(XGoal, YGoal) == WhichMonth(x,y))//�������Ȃ�true
        {
            return true;
        }
        else//�Ⴄ���Ȃ�false
        {
            return false;
        }
    }
    private int WhichMonth(int x,int y)//x,y�������ɂ���̂����ׂ�
    {
        int Month = 0;
        if (x < height[0].width.Length/2 && y < height.Length/2) { Month = 1; }//����̌��ɂ��邩�ǂ���
        if (height[0].width.Length/2 <= x && y < height.Length/2) { Month = 2; }//����̌��ɂ��邩�ǂ���
        if (x < height[0].width.Length/2 && height.Length/2 < y) { Month = 3; }//����̌��ɂ��邩�ǂ���
        if (height[0].width.Length/2 <= x && height.Length/2 < y) { Month = 4; }//����̌��ɂ��邩�ǂ���
        return Month;
         
    }


    public void StartOfimitation()
    {
        gamestart = (gamestart == false);//���]

    }






}
[System.Serializable]
public class Width//week�̎q�E����̃I�u�W�F�N�g�̎擾
{
    public  GameObject[] width;




}