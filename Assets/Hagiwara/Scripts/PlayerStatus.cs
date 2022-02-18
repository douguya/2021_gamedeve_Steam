using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatus : MonoBehaviour
{
    private int PlayerNumber;//�v���C���[�̔ԍ�
    public string Name;//���O
    public List<string> HabItem;//�����Ă���A�C�e��
    private int Goalcount = 0;//�S�[��������
    private int PX,PY;//�v���C���[�̃}�X���W
    public GameObject Play;
    
    [SerializeField]
    private Dropdown dropdown;

    public GameObject dice;                         //�_�C�X���擾
    private bool dicestart = true;                  //�_�C�X����

    public int initialX, initialY;
    public days[] week;                             //Mass�̏c��̃I�u�W�F�N�g�̎擾�E��ԉ��œ񎟌��z��ɂ��Ă���
    public int step = 0;                            //�v���C���[�̃^�[���菇
    private bool stop;                              //�v���C���[�̃^�[���菇�̃X�g�b�p�[
    public float speed = 0.5f;                      //�v���C���[�ړ����x
    private float currentTime = 0f;
    public bool nextturn;                           //���̃v���C���[�̔Ԃɂ���
    public bool Goalup;                             //�����̃^�[���ɃS�[�������Ƃ����錾

    private int xplay;                              //�I�������}�X���W���擾
    private int yplay;
    private int Switchnum = 0;                      //switch�\���̐؂�ւ�

    private int[] way;                //�}�X�̏㉺���E�̃}�X���W 0:�� 1:�� 2:�� 3:�E
    private int[] XLoot;              //�ړ�����}�X������(�Ƃ肠�����ő�10�}�X�ړ��\)
    private int[] YLoot;

    private int Move = 0;                           //�_�C�X�̏o��
    private int diceconter;


    void Start()
    {
        dicestart = true;                        //������
        way = new int[4];
        XLoot = new int[10];
        YLoot = new int[10];
        PlayerMass(initialX, initialY);         //�v���C���[�������ʒu�ɂ�
    }

    
    void Update()
    {
        switch (step)
        {
            case 0:
                //�����Ȃ����
                stop = false;
                break;

            case 1://�_�C�X����
                if (dicestart) {                                    //��񂵂��������Ȃ�
                    dice.GetComponent<imamuraDice>().OnDiceSpin();  //�_�C�X����
                    dicestart = false;
                }
                
                if (stop == true)                                   //�X�g�b�v�������ꂽ��
                {
                    Move = dice.GetComponent<imamuraDice>().StopDice();//�_�C�X���~�߂�
                    Debug.Log(Move);
                    step = 2;
                    stop = false;
                    dicestart = true;
                }
                break;

            case 2://�_�C�X�̃}�X���ړ��o����Ƃ����ݒ肷��
                MoveSelect(Move);                   //�}�X�̑I��
                if (stop == true)                   //�I�����I��������
                {
                    step = 3;
                    stop = false;
                }
                break;

            case 3://�v���C���[�̈ړ�

                currentTime += Time.deltaTime;      //�v���C���[�̈ړ���������i�ނ悤��
                if (currentTime > speed)
                {
                    MovePlayer();                   //����i�߂�
                    currentTime = 0f;
                }

                if (stop == true)                   //�ړ����I��������
                {
                    step = 4;
                    stop = false;
                }
                break;

            case 4://�S�[�����}�X�̌���
                if (week[yplay].day[xplay].GetComponent<Mass>().Goal == true)   //�����S�[���}�X�Ɏ~�܂�����
                {
                    Goaladd();                                                  //�S�[������1�グ��
                    Itemobtain("�S�[��");                                       //�h���b�v�_�E���ɃS�[����ǉ�
                    Goalup = true;                                              //�S�[���������ۂ̐錾
                    stop = true;
                }
                if (week[yplay].day[xplay].GetComponent<Mass>().Open == false)  //�~�܂����}�X���󂢂Ă��Ȃ�������
                {
                    GetComponent<MassEffect>().Effects( week[yplay].day[xplay].GetComponent<Mass>().Day);//�}�X�̌��ʂ̔���
                    week[yplay].day[xplay].GetComponent<Mass>().Open = true;    //�}�X���J������Ԃɂ���
                }
                
                if (stop == true)                      //�}�X�̏������I��������
                {
                    step = 5;
                    stop = false;
                }
                break;

            case 5://���̐l�̔Ԃ�
                nextturn = true;        //�v���C���[�̃^�[�����I������
                step = 0;
                break;
        }
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

    public void Itemobtain(string Item)//�������A�C�e���̖��O�ŌĂԂƃh���b�v�_�E���ɓ���
    {
        HabItem.Add(Item);
        dropdown.options.Add(new Dropdown.OptionData { text = Item + DictionaryManager.ItemDictionary[Item][0] + "P" });
        dropdown.RefreshShownValue();
    }

    public void ItemInfoGet(string Item)
    {
        Debug.Log(HabItem[0]);

        //  Debug.Log(Item+ItemDectionari.ItemDictionary[Item]);

        // Play.ItemDectionari.DectionariyInfo(Item);
        Debug.Log(DictionaryManager.ItemDictionary[Item][0]);


    }

    public void Itemadd(string IName)//�A�C�e���̎擾
    {
        HabItem.Add(IName);

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
        return HabItem[num];
    }
    /*
    public int GetItemPoint(int num)//�����Ă���A�C�e���̃|�C���g
    {
        return ItemPoint[num];
    }
    */
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
    

    private void MoveSelect(int dice)//�v���C���[�̈ړ��̑I��
    {

        switch (Switchnum)
        {
            case 0://�ړ��̂��߂̏����ݒ�
                xplay = PlayerX();//�I���̒��S�}�X������(�ŏ��Ȃ̂Ńv���C���[�̂���}�X������)
                yplay = PlayerY();
                diceconter = dice;//�ړ��o����}�X�̐�������
                XLoot[diceconter] = xplay;//�����̃}�X�����ԂɋL������
                YLoot[diceconter] = yplay;
                week[yplay].day[xplay].GetComponent<Mass>().Decisionon();//�v���C���[�̑���������}�X�ɕς���
                Switchnum = 1;
                break;

            case 1://�ړ��o����}�X��\������
                way[0] = yplay - 1; way[1] = yplay + 1; way[2] = xplay - 1; way[3] = xplay + 1;//�I���̒��S�}�X�̎l���̍��W������ 0:�� 1:�� 2:�� 3:�E
                for (int i = 0; i < 2; i++)
                {
                    if (0 <= way[i] && way[i] < week.Length && week[way[i]].day[xplay].GetComponent<Mass>().invalid == false && (XLoot[diceconter + 1], YLoot[diceconter + 1]) != (xplay, way[i]))//�I�𒆐S�}�X�̏㉺�Ƀ}�X�͑��݂��Ĉ�O�ɑI�����Ă��Ȃ��}�X��
                    {
                        week[way[i]].day[xplay].GetComponent<Mass>().Selecton();//�}�X��I���o����Ƃ���image��\��������
                    }
                }
                for (int i = 2; i < 4; i++)
                {
                    if (0 <= way[i] && way[i] < week[0].day.Length && week[yplay].day[way[i]].GetComponent<Mass>().invalid == false && (XLoot[diceconter + 1], YLoot[diceconter + 1]) != (way[i], yplay))//�I�𒆐S�}�X�̍��E�Ƀ}�X�͑��݂��Ĉ�O�ɑI�����Ă��Ȃ��}�X��
                    {
                        week[yplay].day[way[i]].GetComponent<Mass>().Selecton();//�}�X��I���o����Ƃ���image��\��������
                    }
                }
                if ((xplay, yplay) == (0, 1) || (xplay, yplay) == (13, 0) || (xplay, yplay) == (0, 9) || (xplay, yplay) == (12, 9))
                {//�I�𒆐S�}�X�����[�v�}�X�ɂ��鎞�ɔ���
                    week[1].day[0].GetComponent<Mass>().Selecton();
                    week[0].day[13].GetComponent<Mass>().Selecton();
                    week[9].day[0].GetComponent<Mass>().Selecton();
                    week[9].day[12].GetComponent<Mass>().Selecton();
                }
                week[yplay].day[xplay].GetComponent<Mass>().Selectoff();
                Switchnum = 2;
                break;

            case 2://�I���o����}�X���N���b�N���ꂽ���̔���

                for (int i = 0; i < 2; i++)
                {
                    if (0 <= way[i] && way[i] < week.Length && week[way[i]].day[xplay].GetComponent<Mass>().walk == true)//�I�𒆐S�}�X�̏㉺�Ƀ}�X�͑��݂��ăN���b�N���ꂽ��
                    {
                        diceconter--;//�ړ��o����}�X��������炷
                        yplay = way[i];//�I�𒆐S�}�X���N���b�N�����}�X�Ɉڂ�
                        XLoot[diceconter] = xplay;//�ړ����肵���}�X�����ԂɋL������
                        YLoot[diceconter] = yplay;
                        clearSelect();//�I���ł���}�X�̑S����
                    }
                }

                for (int i = 2; i < 4; i++)
                {
                    if (0 <= way[i] && way[i] < week[0].day.Length && week[yplay].day[way[i]].GetComponent<Mass>().walk == true)//�I�𒆐S�}�X�̍��E�Ƀ}�X�͑��݂��ăN���b�N���ꂽ��
                    {
                        diceconter--;//�ړ��o����}�X��������炷
                        xplay = way[i];//�I�𒆐S�}�X���N���b�N�����}�X�Ɉڂ�
                        XLoot[diceconter] = xplay;//�ړ����肵���}�X�����ԂɋL������
                        YLoot[diceconter] = yplay;
                        clearSelect();//�I���ł���}�X�̑S����
                    }
                }
                Warpdecision(0, 1); //�E�ハ�[�v���I�����ꂽ���ɔ���
                Warpdecision(13, 0);//���ハ�[�v���I�����ꂽ���ɔ���
                Warpdecision(0, 9); //�E�����[�v���I�����ꂽ���ɔ���
                Warpdecision(12, 9);//�������[�v���I�����ꂽ���ɔ���

                if (diceconter > 0)
                {
                    Switchnum = 1;
                }
                else
                {
                    Switchnum = 0;
                    Debug.Log("�I���I��");
                    stop = true;
                }

                break;
        }
    }

    private void Warpdecision(int x, int y)//���[�v���I��������
    {
        if (week[y].day[x].GetComponent<Mass>().walk == true)
        {
            diceconter--;//�ړ��o����}�X��������炷
            xplay = x;//�I�𒆐S�}�X���N���b�N�����}�X�Ɉڂ�
            yplay = y;
            XLoot[diceconter] = xplay;//�ړ����肵���}�X�����ԂɋL������
            YLoot[diceconter] = yplay;
            clearSelect();//�I���ł���}�X�̑S����
        }
    }

    private void clearSelect()//�I���ł���}�X�̑S����
    {
        for (int i = 0; i < week.Length; i++)
        {
            for (int l = 0; l < week[0].day.Length; l++)
            {
                week[i].day[l].GetComponent<Mass>().Selectoff();//�}�X��I���o����Ƃ���image������
                week[i].day[l].GetComponent<Mass>().walk = false;//�N���b�N���ꂽ�Ƃ������������
            }
        }
    }

    private void MovePlayer()//�v���C���[�̈ړ�
    {
        int oneLoot = 0;//���̃}�X���ړ��̍ۈ�񂵂��ʂ�Ȃ��Ȃ�true
        switch (Switchnum)
        {
            case 0:
                xplay = PlayerX();//�v���C���[�̃}�X���W
                yplay = PlayerY();
                diceconter = Move;
                Switchnum = 1;
                break;

            case 1:
                for (int i = 0; i < Move + 1; i++)//�ړ����Ԃ̃}�X��������x�����}�X��ʂ�Ȃ��Ȃ�oneLoot��Move-1�ɂȂ�
                {
                    if ((xplay, yplay) != (XLoot[i], YLoot[i]))
                    {
                        oneLoot++;
                    }
                }
                if (Move == oneLoot)//�ړ��}�X�������}�X��ʂ�Ȃ��Ȃ猈��}�X��������
                {
                    week[yplay].day[xplay].GetComponent<Mass>().Decisionoff();//�����̌���}�X����
                }
                else
                {
                    XLoot[diceconter] = -1;//���łɒʂ����Ƃ��낪�������Ȃ��悤�ɂ���
                    YLoot[diceconter] = -1;
                }
                diceconter--;//�ړ�����}�X�ڐ�������炷
                PlayerMass(XLoot[diceconter], YLoot[diceconter]);//�v���C���[��Loot�ɋL�����������ԂɈړ�������

                if (xplay == XLoot[diceconter] && yplay > YLoot[diceconter]) { Debug.Log("��" + diceconter); }//��Ɉړ��̎��ɔ���(�A�j���[�V�����p�H)
                if (xplay == XLoot[diceconter] && yplay < YLoot[diceconter]) { Debug.Log("��" + diceconter); }
                if (xplay > XLoot[diceconter] && yplay == YLoot[diceconter]) { Debug.Log("��" + diceconter); }
                if (xplay < XLoot[diceconter] && yplay == YLoot[diceconter]) { Debug.Log("�E" + diceconter); }

                xplay = XLoot[diceconter];//�v���C���[�̂���}�X���L��
                yplay = YLoot[diceconter];

                if (diceconter == 0)
                {
                    Debug.Log("�I����Ă�");
                    week[yplay].day[xplay].GetComponent<Mass>().Decisionoff();//�����̌���}�X����
                    Switchnum = 0;
                    stop = true;
                }
                break;
        }

    }

    private void PlayerMass(int x, int y)//�v���C���[���}�X���W�ړ�������(���t���[�v�Ɏg����)
    {
        transform.position = week[y].day[x].transform.position;//�w�肵���}�X�̏�Ƀv���C���[���ړ�����
        SetPlayerMass(x, y);//�v���C���[���ǂ̃}�X�ɂ��邩�L������
    }


    public void stopon()//�_�C�X���~�߂�
    {
        stop = true;
    }

}


[System.Serializable]
public class days//week�̎q�E����̃I�u�W�F�N�g�̎擾
{
    public GameObject[] day;
}