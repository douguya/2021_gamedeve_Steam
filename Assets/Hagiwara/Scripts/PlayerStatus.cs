using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Player
{
    private int PlayerNumber;//�v���C���[�̔ԍ�
    private string Name;//���O
    private List<string> ItemName = new List<string>();//�����Ă���A�C�e���̖��O
    private List<int> ItemPoint = new List<int>();//�����Ă���A�C�e���̃|�C���g
    private int GoalNum = 0;//�S�[��������

    public Player(int Pnum,string n,int G)
    {
        PlayerNumber = Pnum; Name = n; GoalNum = G;
    }

    public void SetName(string n)//���O�̍Đݒ�
    {
        Name = n;
    }

    public void Goaladd()//�S�[���̐��v���X
    {
        GoalNum++;
    }

    
}

public class PlayerStatus : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
