using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerStatus : MonoBehaviour
{
    private int PlayerNumber;//�v���C���[�̔ԍ�
    private string Name;//���O
    private List<string> ItemName = new List<string>();//�����Ă���A�C�e���̖��O
    private List<int> ItemPoint = new List<int>();//�����Ă���A�C�e���̃|�C���g
    private int Goalcount = 0;//�S�[��������
    private int PX,PY;//�v���C���[�̃}�X���W

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    

    public PlayerStatus(int Pnum, string n, int G)
    {
        PlayerNumber = Pnum; Name = n; Goalcount = G;
    }

    public void SetName(string n)//���O�̍Đݒ�
    {
        Name = n;
    }

    public void Goaladd()//�S�[���̐��v���X
    {
        Goalcount++;
    }

    public void Itemadd(string IName, int IPoint)//�A�C�e���̎擾
    {
        ItemName.Add(IName);
        ItemPoint.Add(IPoint);
    }

    public void SetPlayerMass(int x,int y)//�v���C���[���ǂ̃}�X�ɂ��邩�L��
    {
        PX = x;
        PY = y;
    }

    public int GetPlayerNumber()//�v���C���[�ԍ��̏o��
    {
        return PlayerNumber;
    }

    public string GetName()//���O�̏o��
    {
        return Name;
    }

    public string GetItemName(int num)//�����Ă���A�C�e���̖��O
    {
        return ItemName[num];
    }

    public int GetItemPoint(int num)//�����Ă���A�C�e���̃|�C���g
    {
        return ItemPoint[num];
    }

    public int GetGaol()//�S�[��������
    {
        return Goalcount;
    }

    public int PlayerX()//�v���C���[�̃}�X���WX���o��
    {
        return PX;
    }
    public int PlayerY()//�v���C���[�̃}�X���WY���o��
    {
        return PY;
    }
}
