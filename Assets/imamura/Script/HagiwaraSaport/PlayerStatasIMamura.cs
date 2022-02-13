using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatasIMamura : MonoBehaviour
{
    private int PlayerNumber;//�v���C���[�̔ԍ�
    private string Name;//���O
    public List<string> HabItem ;//�����Ă���A�C�e��
    private int Goalcount = 0; //�S�[��������
    private int PX, PY;//�v���C���[�̃}�X���W
    public GameObject Play;

    [SerializeField]
    private Dropdown dropdown;


    void Start()
    {

    }


    void Update()
    {

    }


    public PlayerStatasIMamura(int Pnum, string n, int G)
    {
        PlayerNumber = Pnum; Name = n; Goalcount = G;
    }
    public void Itemobtain(string Item)
    {
        HabItem.Add(Item);
        dropdown.options.Add(new Dropdown.OptionData { text = Item + DectionariManager.ItemDictionary[Item][0] + "P" });
        dropdown.RefreshShownValue();
    }

    
    public void ItemInfoGet(string Item)
    {
        Debug.Log(HabItem[0]);

        //  Debug.Log(Item+ItemDectionari.ItemDictionary[Item]);

       // Play.ItemDectionari.DectionariyInfo(Item);
        Debug.Log(DectionariManager.ItemDictionary[Item][0]);


    }
    




    public void SetName(string n)//���O�̍Đݒ�
    {
        Name = n;
    }

    public void Goaladd()//�S�[���̐��v���X
    {
        Goalcount++;
    }

    public void Itemadd(string IName)//�A�C�e���̎擾
    {
        HabItem.Add(IName);
        
    }

    public void SetPlayerMass(int x, int y)//�v���C���[���ǂ̃}�X�ɂ��邩�L��
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
        return HabItem[num];
    }

    /*
    public int GetItemPoint(int num)//�����Ă���A�C�e���̃|�C���g
    {
        return ItemPoint[num];
    }
    */
    public int GetGaol()//�S�[���������̎擾
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


