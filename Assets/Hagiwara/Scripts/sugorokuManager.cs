using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sugorokuManager : MonoBehaviour
{
    private int XGoal, YGoal;//�S�[���̍��W
    
    public GameObject dice;//�_�C�X���擾
    public GameObject[] Player = new GameObject[4];//�v���C���[�I�u�W�F�N�g�擾
    public days[] week;//Mass�̏c��̃I�u�W�F�N�g�̎擾�E��ԉ��œ񎟌��z��ɂ��Ă���
    private int Playerturn = 0;//�ǂ̃v���C���[�Ԃ�
    private int step = 0;//�v���C���[�̃^�[���菇
    private bool stop; //�v���C���[�̃^�[���菇�̃X�g�b�p�[
    public float speed = 0.5f;//�v���C���[�ړ����x
    private float currentTime = 0f;

    private int xplay;//�v���C���[�̃}�X���W���擾
    private int yplay;
    private int Switchnum = 0;//switch�\���̐؂�ւ�

    private int[] way = new int[4];//�}�X�̏㉺���E�̃}�X���W 0:�� 1:�� 2:�� 3:�E
    private int[] XLoot = new int [10];//�ړ�����}�X������(�Ƃ肠�����ő�10�}�X�ړ��\)
    private int[] YLoot = new int[10];
    
    int Move = 0;//�_�C�X�̏o��
    private int diceconter;//�_�C�X�̎c�萔
    
    void Start()
    {
        PlayerMass(0, 0, 1);//�v���C���[0���}�X(0,1)�Ɉړ�������
        PlayerMass(1, 13, 0);//�v���C���[1���}�X(0,1)�Ɉړ�������
        PlayerMass(2, 0, 9);//�v���C���[2���}�X(0,1)�Ɉړ�������
        PlayerMass(3, 12, 9);//�v���C���[3���}�X(0,1)�Ɉړ�������

        GoalDecision();//�S�[���̑I��

    }


    void Update()
    {

        
        
        switch (step) {
            case 0://�_�C�X����
                Move = 6;//���̓�s�̓e�X�g�p
                step = 1;
                if (stop == true)
                {
                    /*
                    Move = dice.GetComponent<imamuraDice>().StopDice();
                    Debug.Log(Move);
                    step = 1;
                    stop = false;
                    */
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
                if (currentTime > speed)
                {
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
        int Week, Day;//�����_���ȃS�[���̏ꏊ������
        do {
            Week = Random.Range(0, week.Length);//week�E���̗�̃����_��
            Day = Random.Range(0, week[0].day.Length);//day�E�c�̗�̃����_��
        } while (week[Week].day[Day].GetComponent<Mass>().invalid == true);//�����_���ɑI�񂾃}�X�����݂��Ă�����̂�������܂ŌJ��Ԃ�
        week[Week].day[Day].GetComponent<Mass>().GoalOn();//�S�[���̐ݒu
        XGoal = Day; YGoal = Week;//�S�[���z��ԍ����L��

    }

    private void GoalAgain()//�S�[���̍Đݒu(�������ɂȂ�Ȃ��悤��)
    {
        int Week, Day;//�����_���ȃS�[���̏ꏊ������
        do{
            Week = Random.Range(0, week.Length);//week�E���̗�̃����_��
            Day = Random.Range(0, week[0].day.Length);//day�E�c�̗�̃����_��
        } while (week[Week].day[Day].GetComponent<Mass>().invalid == true && MonthCount(Day, Week) == true);//�I�񂾃}�X�����݂��Ă�����́�����������Ȃ����̂�������܂ŌJ��Ԃ�
        week[Week].day[Day].GetComponent<Mass>().GoalOn();//�S�[���̐ݒu
        XGoal = Day; YGoal = Week;//�S�[���z��ԍ����L��
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
        if (x < week[0].day.Length/2 && y < week.Length/2) { Month = 1; }//����̌��ɂ��邩�ǂ���
        if (week[0].day.Length/2 <= x && y < week.Length/2) { Month = 2; }//����̌��ɂ��邩�ǂ���
        if (x < week[0].day.Length/2 && week.Length/2 < y) { Month = 3; }//����̌��ɂ��邩�ǂ���
        if (week[0].day.Length/2 <= x && week.Length/2 < y) { Month = 4; }//����̌��ɂ��邩�ǂ���
        return Month;
         
    }


    private void GoalClear()//�S�Ẵ}�X�̃S�[��������
    {
        for (int i = 0; i < week.Length; i++)
        {
            for (int l = 0; l < week[0].day.Length; l++)
            {
                week[i].day[l].GetComponent<Mass>().GoalOff();//�S�[���������Ă���
            }
        }
    }

    

    private void PlayerMass(int P, int x, int y)//�v���C���[���}�X���W�ړ�������(���t���[�v�Ɏg����)
    {
        Player[P].transform.position = transform.InverseTransformPoint(week[y].day[x].transform.position);  //�w�肵���}�X�̏�Ƀv���C���[���ړ�����
        Player[P].GetComponent<PlayerStatus>().SetPlayerMass(x, y);//�v���C���[���ǂ̃}�X�ɂ��邩�L������
    }


    public void DiceBotton()//�_�C�X���~�߂�
    {
        stop = true;
    }

    private void MoveSelect(int Pnum, int dice)//�v���C���[�̈ړ��̑I��
    {
        
        switch (Switchnum) {
            case 0://�ړ��̂��߂̏����ݒ�
                xplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerX();//�I���̒��S�}�X������(�ŏ��Ȃ̂Ńv���C���[�̂���}�X������)
                yplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerY();
                diceconter = dice;//�ړ��o����}�X�̐�������
                XLoot[diceconter] = xplay;//�����̃}�X�����ԂɋL������
                YLoot[diceconter] = yplay;
                week[yplay].day[xplay].GetComponent<Mass>().Decisionon();//�v���C���[�̑���������}�X�ɕς���
                Switchnum = 1;
                break;

            case 1://�ړ��o����}�X��\������
                way[0] = yplay - 1; way[1] = yplay + 1; way[2] = xplay - 1; way[3] = xplay + 1;//�I���̒��S�}�X�̎l���̍��W������ 0:�� 1:�� 2:�� 3:�E
                for (int i = 0;i < 2; i++)
                {
                    if (0 <= way[i] && way[i] < week.Length && week[way[i]].day[xplay].GetComponent<Mass>().invalid == false && (XLoot[diceconter+1], YLoot[diceconter+1]) != (xplay,way[i]))//�I�𒆐S�}�X�̏㉺�Ƀ}�X�͑��݂��Ĉ�O�ɑI�����Ă��Ȃ��}�X��
                    {
                        week[way[i]].day[xplay].GetComponent<Mass>().Selecton();//�}�X��I���o����Ƃ���image��\��������
                    }
                }
                for (int i = 2; i < 4; i++)
                {
                    if (0 <= way[i] && way[i] < week[0].day.Length && week[yplay].day[way[i]].GetComponent<Mass>().invalid == false && (XLoot[diceconter + 1], YLoot[diceconter + 1]) != (way[i],yplay))//�I�𒆐S�}�X�̍��E�Ƀ}�X�͑��݂��Ĉ�O�ɑI�����Ă��Ȃ��}�X��
                    {
                        week[yplay].day[way[i]].GetComponent<Mass>().Selecton();//�}�X��I���o����Ƃ���image��\��������
                    }
                }
                if ((xplay,yplay) == (0,1) || (xplay, yplay) == (13, 0) || (xplay, yplay) == (0, 9) || (xplay, yplay) == (12, 9)) {//�I�𒆐S�}�X�����[�v�}�X�ɂ��鎞�ɔ���
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
                Warpdecision(0, 1);//�E�ハ�[�v���I�����ꂽ���ɔ���
                Warpdecision(13, 0);//���ハ�[�v���I�����ꂽ���ɔ���
                Warpdecision(0, 9);//�E�����[�v���I�����ꂽ���ɔ���
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

    

    private void Warpdecision(int x,int y)//���[�v���I��������
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
    
    private void MovePlayer(int Pnum)//�v���C���[�̈ړ�
    {
        int oneLoot = 0;//���̃}�X���ړ��̍ۈ�񂵂��ʂ�Ȃ��Ȃ�true
        switch (Switchnum)
        {
            case 0:
                xplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerX();//�v���C���[�̃}�X���W
                yplay = Player[Pnum].GetComponent<PlayerStatus>().PlayerY();
                diceconter = Move;
                Switchnum = 1;
                break;

            case 1:
                for(int i=0;i< Move+1; i++)//�ړ����Ԃ̃}�X��������x�����}�X��ʂ�Ȃ��Ȃ�oneLoot��Move-1�ɂȂ�
                {
                    if ((xplay,yplay) != (XLoot[i],YLoot[i]))
                    {
                        oneLoot++;
                    }
                }
                if(Move == oneLoot)//�ړ��}�X�������}�X��ʂ�Ȃ��Ȃ猈��}�X��������
                {
                    week[yplay].day[xplay].GetComponent<Mass>().Decisionoff();//�����̌���}�X����
                }
                else
                {
                    XLoot[diceconter] = -1;//���łɒʂ����Ƃ��낪�������Ȃ��悤�ɂ���
                    YLoot[diceconter] = -1;
                }
                diceconter--;//�ړ�����}�X�ڐ�������炷
                PlayerMass(Pnum, XLoot[diceconter], YLoot[diceconter]);//�v���C���[��Loot�ɋL�����������ԂɈړ�������
                
                if (xplay == XLoot[diceconter] && yplay > YLoot[diceconter]) { Debug.Log("��"+diceconter); }//��Ɉړ��̎��ɔ���(�A�j���[�V�����p�H)
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

}
[System.Serializable]
public class days//week�̎q�E����̃I�u�W�F�N�g�̎擾
{
    public GameObject[] day;
}