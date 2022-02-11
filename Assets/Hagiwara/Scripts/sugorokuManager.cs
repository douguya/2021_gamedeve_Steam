using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sugorokuManager : MonoBehaviour
{
    private GameObject[,] mass = new GameObject[14,10];//�}�X�̊i�[
    private int XGoal,YGoal;//�S�[���̍��W
    public GameObject obj;//�v���n�u��Mass���擾
    public GameObject June;
    public GameObject July;
    public GameObject August;
    public GameObject September;
    public GameObject[] Player = new GameObject[4];

    void Start()
    {
        float x = -2.25f;
        float y = 1.2f;
        int count = 0;
        for(int i =0;i < 5; i++)//6���̐���
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
        Player[1].transform.position = transform.InverseTransformPoint(mass[13,0].transform.position); PlayerMass(1, 13, 1);
        Player[2].transform.position = transform.InverseTransformPoint(mass[0, 9].transform.position); PlayerMass(2, 0, 9);
        Player[3].transform.position = transform.InverseTransformPoint(mass[12,9].transform.position); PlayerMass(3, 12, 9);

        GoalDecision();//�S�[���̑I��
       
    }

    
    void Update() 
    {
        
        int Playerturn = -1;
        int Move = 0;
        do
        {
            Playerturn++;
            if(3 < Playerturn)
            {
                Playerturn = 0;
            }
            Move = 5;//�_�C�X����
            MoveSelect(Playerturn, Move);//�_�C�X�̃}�X���ړ��o����Ƃ����ݒ肷��
            //�ړ�����

        } while (Player[Playerturn].GetComponent<PlayerStatus>().GetGaol()==4);
        
    }

    private void GoalDecision()//���߂ăS�[�����o��������
    {
        int vertical, beside;
        do {
            vertical = Random.Range(0, 14);
            beside = Random.Range(0, 10);
        } while (mass[vertical, beside].GetComponent<Mass>().invalid == true );//�����_���ɑI�񂾃}�X������������Ȃ����̂�T��
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

    private bool MonthCount(int x,int y)//�S�[���Ɠ����������f����
    {
        bool Jach = false;
        int BeforeMonth = 0,NextMonth =0;

        if (XGoal < 7 && YGoal < 5) { BeforeMonth = 1; }
        if (7 <= XGoal&& YGoal < 5) { BeforeMonth = 2; }
        if (XGoal < 7 && 5 < YGoal) { BeforeMonth = 3; }
        if (7 <= XGoal&& 5 < YGoal) { BeforeMonth = 4; }

        if (x < 7 && y < 5) { NextMonth = 1; }
        if (7 <= x && y < 5) { NextMonth = 2; }
        if (x < 7 && 5 < y) { NextMonth = 3; }
        if (7 <= x && 5 < y) { NextMonth = 4; }

        if(BeforeMonth == NextMonth)
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

    private void PlayerMass(int P,int x,int y)//�v���C���[�̃}�X���W���L��������
    {
        Player[P].GetComponent<PlayerStatus>().SetPlayerMass(x, y);
    }


    private void MoveSelect(int Pnum, int dice)//�v���C���[�̈ړ�
    {
        int x, y;
        int[] way = new int[4];//0:�� 1:�� 2:�� 3:�E
        bool[] walk = new bool[4];//onClick���ꂽ���ǂ������ׂ�
        x = Player[Pnum].GetComponent<PlayerStatus>().PlayerX();//�v���C���[�̃}�X���W���擾
        y = Player[Pnum].GetComponent<PlayerStatus>().PlayerY();
        do
        {
            way[0] = y - 1; way[1] = y + 1; way[2] = x - 1; way[3] = x + 1;//�}�X�̑O�㍶�E���m�F

            if (0 <= way[0] && way[0] <= 13)
            {
                if (mass[x, way[0]].GetComponent<Mass>().Loot == false)//���[�g����ς݂łȂ��}�X��I���ł���悤��
                {
                    mass[x, way[0]].GetComponent<Mass>().Selecton();
                    walk[0] = mass[x, way[0]].GetComponent<Mass>().walk;
                }
            }
            if (0 <= way[1] && way[1] <= 13)
            {
                if (mass[x, way[1]].GetComponent<Mass>().Loot == false)
                {
                    mass[x, way[1]].GetComponent<Mass>().Selecton();
                    walk[1] = mass[x, way[0]].GetComponent<Mass>().walk;
                }
            }
            if (0 <= way[2] && way[2] <= 9)
            {
                if (mass[way[2], y].GetComponent<Mass>().Loot == false)
                {
                    mass[way[2], y].GetComponent<Mass>().Selecton();
                    walk[2] = mass[x, way[0]].GetComponent<Mass>().walk;
                }
            }
            if (0 <= way[3] && way[3] <= 9)
            {
                if (mass[way[3], y].GetComponent<Mass>().Loot == false)
                {
                    mass[way[3], y].GetComponent<Mass>().Selecton();
                    walk[3] = mass[x, way[0]].GetComponent<Mass>().walk;
                }
            }

            //onclick���ꂽ��I���}�X�����ă_�C�X��1���炷
            if(walk[0] == true)
            {
                dice--;
                y = way[0];
                clearSelect();
            }
            if (walk[1] == true)
            {
                dice--;
                y = way[1];
                clearSelect();
            }
            if (walk[2] == true)
            {
                dice--;
                x = way[2];
                clearSelect();
            }
            if (walk[3] == true)
            {
                dice--;
                x = way[3];
                clearSelect();
            }

        } while (dice <= 0);
    }

    private void clearSelect()
    {
        for (int i = 0; i < 14; i++)
        {
            for (int l = 0; l < 10; l++)
            {
                mass[i, l].GetComponent<Mass>().Selectoff();
            }
        }
    }





}
