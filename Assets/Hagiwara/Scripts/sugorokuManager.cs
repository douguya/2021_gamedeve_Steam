using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sugorokuManager : MonoBehaviour
{
    private GameObject[,] mass = new GameObject[14, 10];//�}�X�̊i�[
    private int XGoal, YGoal;//�S�[���̍��W
    public GameObject obj;//�v���n�u��Mass���擾
    public GameObject June;
    public GameObject July;
    public GameObject August;
    public GameObject September;
    public GameObject dice;//�_�C�X���擾
    public GameObject[] Player = new GameObject[4];//�v���C���[�I�u�W�F�N�g�擾
    private int Playerturn = 0;//�ǂ̃v���C���[�Ԃ�
    private int step = 0;//�v���C���[�̃^�[���菇
    private bool stop; //�v���C���[�̃^�[���菇�̃X�g�b�p�[
    private float span = 2f;//�v���C���[�ړ����x
    private float currentTime = 0f;

    private int xplay;//�v���C���[�̃}�X���W���擾
    private int yplay;
    private int MoveSelectnum = 0;//�ړ��I���̐؂�ւ�
    private int[] way = new int[4];//0:�� 1:�� 2:�� 3:�E
    private bool[] walk = new bool[4];//onClick���ꂽ���ǂ������ׂ�
    int Move = 0;//�_�C�X�̏o��
    private int diceconter;//�_�C�X�̎c�萔
    
    void Start()
    {
        float x = -2.25f;
        float y = 1.2f;
        int count = 0;
        for (int i = 0; i < 5; i++)//6���̐���
        {
            for (int l = 0; l < 7; l++)
            {
                mass[l, i] = Instantiate(obj, new Vector3(x, y, 0.0f), Quaternion.identity);//�}�X���w����W�ւ̐���and�i�[
                mass[l, i].transform.parent = June.transform;//6���̎q�Ƃ��Đ���
                x = x + 0.75f;
                count++;
            }
            y = y - 0.6f;
            x = -2.25f;
        }

        y = 1.2f;
        for (int i = 0; i < 5; i++)//7���̐���
        {
            for (int l = 7; l < 14; l++)
            {
                mass[l, i] = Instantiate(obj, new Vector3(x, y, 0.0f), Quaternion.identity);//�}�X���w����W�ւ̐���and�i�[
                mass[l, i].transform.parent = July.transform;//7���̎q�Ƃ��Đ���
                x = x + 0.75f;
                count++;
            }
            y = y - 0.6f;
            x = -2.25f;
        }

        y = 1.2f;
        for (int i = 5; i < 10; i++)//8���̐���
        {
            for (int l = 0; l < 7; l++)
            {
                mass[l, i] = Instantiate(obj, new Vector3(x, y, 0.0f), Quaternion.identity);//�}�X���w����W�ւ̐���and�i�[
                mass[l, i].transform.parent = August.transform;//8���̎q�Ƃ��Đ���
                x = x + 0.75f;
                count++;
            }
            y = y - 0.6f;
            x = -2.25f;
        }

        y = 1.2f;
        for (int i = 5; i < 10; i++)//9���̐���
        {
            for (int l = 7; l < 14; l++)
            {
                mass[l, i] = Instantiate(obj, new Vector3(x, y, 0.0f), Quaternion.identity);//�}�X���w����W�ւ̐���and�i�[
                mass[l, i].transform.parent = September.transform;//9���̎q�Ƃ��Đ���
                x = x + 0.75f;
                count++;
            }
            y = y - 0.6f;
            x = -2.25f;
        }
        June.transform.position = new Vector3(-2.9f, 1.6f, 0);//6���̈ړ�
        July.transform.position = new Vector3(2.9f, 1.6f, 0);//7���̈ړ�
        August.transform.position = new Vector3(-2.9f, -2.4f, 0);//8���̈ړ�
        September.transform.position = new Vector3(2.9f, -2.4f, 0);//9���̈ړ�

        invalid();//����Ȃ��}�X�̖�����

        Player[0].transform.position = transform.InverseTransformPoint(mass[0, 1].transform.position); PlayerMass(0, 0, 1);//�v���C���[�̔z�u�ƃ}�X���W�̋L��
        Player[1].transform.position = transform.InverseTransformPoint(mass[13, 0].transform.position); PlayerMass(1, 13, 1);
        Player[2].transform.position = transform.InverseTransformPoint(mass[0, 9].transform.position); PlayerMass(2, 0, 9);
        Player[3].transform.position = transform.InverseTransformPoint(mass[12, 9].transform.position); PlayerMass(3, 12, 9);

        GoalDecision();//�S�[���̑I��

    }


    void Update()
    {

        
        
        switch (step) {
            case 0://�_�C�X����
                //dice.GetComponent<imamuraDice>().OnDiceSpin();
                if(stop == true)
                {
                    Move = dice.GetComponent<imamuraDice>().StopDice();
                    Debug.Log(Move);
                    step = 1;
                    stop = false;
                }
                
                break;
            case 1://�_�C�X�̃}�X���ړ��o����Ƃ����ݒ肷��
                MoveSelect(Playerturn, Move);
                if(stop == true)
                {
                    step = 2;
                    stop = false;
                }
                break;
            case 2://�v���C���[�̈ړ�
                
                currentTime += Time.deltaTime;

                if (currentTime > span)
                {
                    //Debug.LogFormat("{0}�b�o��", span);
                    MovePlayer(Playerturn);
                    currentTime = 0f;
                }
                
                if (stop == true)
                {
                    step = 3;
                    stop = false;
                }
                break;

            case 3://�S�[�����}�X�̌���
                //�S�[�����}�X�̌���
                step = 4;
                break;

            case 4://���̐l�̔Ԃ�
                Playerturn++;
                if (3 < Playerturn)
                {
                    Playerturn = 0;
                }
                step = 0;
                break;
        }
        
        
    }

    private void GoalDecision()//���߂ăS�[�����o��������
    {
        int vertical, beside;
        do {
            vertical = Random.Range(0, 14);
            beside = Random.Range(0, 10);
        } while (mass[vertical, beside].GetComponent<Mass>().invalid == true);//�����_���ɑI�񂾃}�X������������Ȃ����̂�T��
        mass[vertical, beside].GetComponent<Mass>().Goal = true;//�S�[���̐ݒu
        XGoal = vertical; YGoal = beside;//�S�[���ʒu�̋L��

    }

    private void GoalAgain()//�S�[���̍Đݒu
    {
        int vertical, beside;
        do
        {
            vertical = Random.Range(0, 14);
            beside = Random.Range(0, 10);
        } while (mass[vertical, beside].GetComponent<Mass>().invalid == true && MonthCount(vertical, beside) == true);//�I�񂾃}�X��������������������Ȃ����̂�T��
        mass[vertical, beside].GetComponent<Mass>().Goal = true;//�S�[���̐ݒu
        XGoal = vertical; YGoal = beside;//�S�[���ʒu�̋L��
    }

    private bool MonthCount(int x, int y)//�S�[���Ɠ����������f����
    {
        bool Jach = false;
        int BeforeMonth = 0, NextMonth = 0;

        if (XGoal < 7 && YGoal < 5) { BeforeMonth = 1; }
        if (7 <= XGoal && YGoal < 5) { BeforeMonth = 2; }
        if (XGoal < 7 && 5 < YGoal) { BeforeMonth = 3; }
        if (7 <= XGoal && 5 < YGoal) { BeforeMonth = 4; }

        if (x < 7 && y < 5) { NextMonth = 1; }
        if (7 <= x && y < 5) { NextMonth = 2; }
        if (x < 7 && 5 < y) { NextMonth = 3; }
        if (7 <= x && 5 < y) { NextMonth = 4; }

        if (BeforeMonth == NextMonth)
        {
            Jach = true;
        }
        else
        {
            Jach = false;
        }
        return Jach;
    }

    private void GoalClear()//�S�[��������
    {

        for (int i = 0; i < 14; i++)
        {
            for (int l = 0; l < 10; l++)
            {
                mass[i, l].GetComponent<Mass>().Goal = false;
            }
        }
    }

    private void invalid()//����Ȃ��}�X�̖�����
    {
        mass[0, 0].GetComponent<Mass>().invalid = true;//5/29
        mass[1, 0].GetComponent<Mass>().invalid = true;//5/30
        mass[2, 0].GetComponent<Mass>().invalid = true;//5/31
        mass[5, 4].GetComponent<Mass>().invalid = true;//7/1
        mass[6, 4].GetComponent<Mass>().invalid = true;//7/2

        mass[7, 0].GetComponent<Mass>().invalid = true;//6/26
        mass[8, 0].GetComponent<Mass>().invalid = true;//6/27
        mass[9, 0].GetComponent<Mass>().invalid = true;//6/28
        mass[10, 0].GetComponent<Mass>().invalid = true;//6/29
        mass[11, 0].GetComponent<Mass>().invalid = true;//6/30

        mass[0, 5].GetComponent<Mass>().invalid = true;//7/31
        mass[4, 9].GetComponent<Mass>().invalid = true;//9/1
        mass[5, 9].GetComponent<Mass>().invalid = true;//9/2
        mass[6, 9].GetComponent<Mass>().invalid = true;//9/3

        mass[7, 5].GetComponent<Mass>().invalid = true;//8/28
        mass[8, 5].GetComponent<Mass>().invalid = true;//8/29
        mass[9, 5].GetComponent<Mass>().invalid = true;//8/30
        mass[10, 5].GetComponent<Mass>().invalid = true;//8/31
        mass[13, 9].GetComponent<Mass>().invalid = true;//10/1

    }

    private void PlayerMass(int P, int x, int y)//�v���C���[�̃}�X���W���L��������
    {
        Player[P].GetComponent<PlayerStatus>().SetPlayerMass(x, y);
    }
    public void DiceBotton()//�_�C�X���~�߂�
    {
        stop = true;
    }

    private void MoveSelect(int Pnum, int dice)//�v���C���[�̈ړ��̑I��
    {
        
        switch (MoveSelectnum) {
            case 0:
                xplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerX();
                yplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerY();
                diceconter = dice;
                mass[xplay, yplay].GetComponent<Mass>().Decisionon();
                mass[xplay, yplay].GetComponent<Mass>().Loot = true; 
                MoveSelectnum = 1;
                break;

            case 1:
                way[0] = yplay - 1; way[1] = yplay + 1; way[2] = xplay - 1; way[3] = xplay + 1;
                if (0 <= way[0] && way[0] <= 10 && mass[xplay, way[0]].GetComponent<Mass>().invalid == false && mass[xplay, way[0]].GetComponent<Mass>().Loot == false)
                {
                    mass[xplay, way[0]].GetComponent<Mass>().Selecton();
                }
                if (0 <= way[1] && way[1] <= 10 && mass[xplay, way[1]].GetComponent<Mass>().invalid == false && mass[xplay, way[1]].GetComponent<Mass>().Loot == false)
                {
                    mass[xplay, way[1]].GetComponent<Mass>().Selecton();
                }
                if (0 <= way[2] && way[2] <= 13 && mass[way[2], yplay].GetComponent<Mass>().invalid == false && mass[way[2], yplay].GetComponent<Mass>().Loot == false)
                {
                    mass[way[2], yplay].GetComponent<Mass>().Selecton();
                }
                if (0 <= way[3] && way[3] <= 13 && mass[way[3], yplay].GetComponent<Mass>().invalid == false && mass[way[3], yplay].GetComponent<Mass>().Loot == false)
                {
                    mass[way[3], yplay].GetComponent<Mass>().Selecton();
                }
                MoveSelectnum = 2;
                break;

            case 2:
                if (0 <= way[0] && way[0] <= 10 && mass[xplay, way[0]].GetComponent<Mass>().walk == true)
                {
                    diceconter--;
                    yplay = way[0];
                    clearSelect();
                }
                if (0 <= way[1] && way[1] <= 10 && mass[xplay, way[1]].GetComponent<Mass>().walk == true)
                {
                    diceconter--;
                    yplay = way[1];
                    clearSelect();
                }
                if (0 <= way[2] && way[2] <= 13 && mass[way[2], yplay].GetComponent<Mass>().walk == true)
                {
                    diceconter--;
                    xplay = way[2];
                    clearSelect();
                }
                if (0 <= way[3] && way[3] <= 13 && mass[way[3], yplay].GetComponent<Mass>().walk == true)
                {
                    diceconter--;
                    xplay = way[3];
                    clearSelect();
                }
                if(diceconter > 0)
                {
                    MoveSelectnum = 1;
                }
                else
                {
                    MoveSelectnum = 0;
                    Debug.Log("�I���I��");
                    stop = true;
                }
                
                break;
        }
    }

    private void clearSelect()//�I���ł���}�X�̑S����
    {
        for (int i = 0; i < 14; i++)
        {
            for (int l = 0; l < 10; l++)
            {
                mass[i, l].GetComponent<Mass>().Selectoff();
                mass[i, l].GetComponent<Mass>().walk = false;
            }
        }
    }

    /*
    private void MoveSelect(int Pnum)//�v���C���[�̈ړ�
    {
        
    }
    */

    private void MovePlayer(int Pnum)//�v���C���[�̈ړ�
    {
        
        xplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerX();//�v���C���[�̃}�X���W
        yplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerY();
        way[0] = yplay - 1; way[1] = yplay + 1; way[2] = xplay - 1; way[3] = xplay + 1;
        mass[xplay, yplay].GetComponent<Mass>().Loot = false;//������Loot����
        mass[xplay, yplay].GetComponent<Mass>().Decisionoff();//����}�X����
        bool stoper = false;
        if (0 <= way[0] && way[0] <= 10 && mass[xplay, way[0]].GetComponent<Mass>().Loot == true && stoper == false)//�l����Loot�����邩�T���Ĉړ�����
        {
            Move--;
            Player[Pnum].transform.position = transform.InverseTransformPoint(mass[xplay, way[0]].transform.position);
            PlayerMass(Pnum, xplay, way[0]);
            yplay = way[0];
            stoper = true;
        }
        if (0 <= way[1] && way[1] <= 10 && mass[xplay, way[1]].GetComponent<Mass>().Loot == true && stoper == false)
        {
            Move--;
            Player[Pnum].transform.position = transform.InverseTransformPoint(mass[xplay, way[1]].transform.position);
            PlayerMass(Pnum, xplay, way[1]);
            yplay = way[1];
            stoper = true;
        }
        if (0 <= way[2] && way[2] <= 13 && mass[way[2], yplay].GetComponent<Mass>().Loot == true && stoper == false)
        {
            Move--;
            Player[Pnum].transform.position = transform.InverseTransformPoint(mass[way[2], yplay].transform.position);
            PlayerMass(Pnum, way[2], yplay);
            xplay = way[2];
            stoper = true;
        }
        if (0 <= way[3] && way[3] <= 13 && mass[way[3], yplay].GetComponent<Mass>().Loot == true && stoper == false)
        {
            Move--;
            Player[Pnum].transform.position = transform.InverseTransformPoint(mass[way[3], yplay].transform.position);
            PlayerMass(Pnum, way[3], yplay);
            xplay = way[3];
            stoper = true;
        }
        //Debug.Log(Move);
        if (Move == 0)
        {
            Debug.Log("�I����Ă�");
            mass[xplay, yplay].GetComponent<Mass>().Loot = false;//������Loot����
            mass[xplay, yplay].GetComponent<Mass>().Decisionoff();//����}�X����
            stop = true;
        }
    }

}
