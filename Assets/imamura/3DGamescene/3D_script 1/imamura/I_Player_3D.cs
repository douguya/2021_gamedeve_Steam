using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Video;


public class I_Player_3D : MonoBehaviourPunCallbacks
{
    public int PlayerNumber;                      //�v���C���[�ԍ�
    

    public int XPlayer_position;                  //�v���C���[�̌��݂̉��̈ʒu
    public int YPlayer_position;                  //�v���C���[�̌��݂̏c�̈ʒu

    int Xcenter, Ycenter;                         //�I���ł���}�X�̒��S�}�X

    public List<Anniversary_Item> Hub_Items = new List<Anniversary_Item>();

    public int Move_Point = 0;                    //�v���C���[�̈ړ��ł������ 
    private int select_Point = 0;                 //�}�X��I���ł��鐔
    private bool[] Player_warpMove = new bool[11];//�v���C���[�̈ړ����@

    public int Goalcount = 0;

    private int[] XPlayer_Loot = new int[11];     //�I�������}�X���L������
    private int[] YPlayer_Loot = new int[11];


    public GameObject GameManager;                //GameManager�I�u�W�F�N�g�̎擾
    private I_game_manager Manager;                 //I_game_manager���擾

    public GameObject DiceButton;                           //�_�C�X���~�߂�ׂ̃I�u�W�F�N�g�擾
    public GameObject ButtonText;                           //�_�C�X�̃e�L�X�g�I�u�W�F�N�g�擾
    private bool DiceStrat = true;                          //�{�^�����_�C�X�̊J�n���X�g�b�v��

    // �ȉ�MannequinPlayer��̈��p=====================================================================
    public Anniversary_Item_Master ItemMaster;
    public GameObject ItemBlock;//�A�C�e�����X�g��UGI

    // =====================================================================

    private void Awake()
    {
        GameManager= GameObject.FindWithTag("GameController");
        DiceButton=GameObject.FindWithTag("Dice");
        ButtonText=DiceButton.transform.GetChild(0).gameObject;
        
            
        
    }
    void Start()
    {
        name=""+GetComponent<PhotonView>().CreatorActorNr;
    }


    void Update()
    {

    }

    //�v���C���[�̕\��
    public void Player_indicate()
    {
        gameObject.SetActive(true);
    }



    //�v���C���[�̏����ʒu�ݒ�
    public void Player_position_setting(int Y_position, int X_position)
    {
        Manager = GameManager.GetComponent<I_game_manager>();
        XPlayer_position = X_position;//�v���C���[�̌��݂̏c�E���ʒu
        YPlayer_position = Y_position;
        transform.position = Manager.Week[Y_position].Day[X_position].GetComponent<I_Mass_3D>().transform.position;//�v���C���[�̈ړ�
        //Debug.Log("�v���C���[�̏����ʒu "+Y_position + " : "+ X_position);
    }



    //�_�C�X���񂷏���
    public void Dice_ready()
    {
        DiceButton.GetComponent<Button>().interactable = true;
        ButtonText.GetComponent<Text>().text = "�_�C�X����";
    }

    //�_�C�X���~�߂Ēl���󂯎��
    private void Dice_Stop()
    {
        Move_Point = Manager.Output_DiceStop();
        MoveSelect();
    }

    public void DicePush()
    {
        if (PlayerNumber == Manager.Player_Turn)
        {
            if (DiceStrat)
            {
                //�����Ń_�C�X���񂷏���
                Manager.Output_DiceStart();
                ButtonText.GetComponent<Text>().text = "�_�C�X���~�߂�";
                DiceStrat = false;
            }
            else
            {
                Dice_Stop();//�_�C�X���~�߂Ēl���󂯎��
                DiceButton.GetComponent<Button>().interactable = false;
                ButtonText.GetComponent<Text>().text = "�ړ���I��";
                DiceStrat = true;
            }
        }
    }

    public void another_turn()
    {
        ButtonText.GetComponent<Text>().text = "���v���C���[�̃^�[��";
    }





    //�I���ł���}�X�̕\���̏����ݒ�
    public void MoveSelect()
    {
        Xcenter = XPlayer_position;                 //�I���̒��S�ƂȂ�}�X��ݒ�
        Ycenter = YPlayer_position;
        YPlayer_Loot[0] = Ycenter;                  //�v���C���[�̌��݂̃}�X���L������
        XPlayer_Loot[0] = Xcenter;
        photonView.RPC(nameof(Output_decisionSetting), RpcTarget.All, Ycenter, Xcenter);//���݂̃}�X���ړ����肵���}�X�ɂ���
        select_Point = Move_Point;                  //�I���ł��鐔�Ƀ_�C�X�̖ڂ�����
        MoveSelect_Display();                       //�I���ł���}�X�̕\��
    }

    //�I���ł���}�X�̕\��
    private void MoveSelect_Display()
    {
        int[] Select = new int[4];                                              //�I���̒��S�ƂȂ�}�X�̎l����ݒ肷��

        Output_SelectClear(Ycenter, Xcenter);                                   //�I���̒��S�ƂȂ�}�X�̑I���}�X(���F���)��\��

        Select[0] = Xcenter - 1; Select[1] = Xcenter + 1;                       //�I���̒��S�ƂȂ�}�X�̍��E������
        for (int way = 0; way < 2; way++)
        {
            //�I���̒��S�ƂȂ�}�X�̍��E�����݂��ړ����肳�ꂽ�}�X�łȂ���
            if (0 <= Select[way] && Select[way] < Manager.Week[0].Day.Length && Manager.Week[Ycenter].Day[Select[way]].GetComponent<I_Mass_3D>().decision == false)
            {
                Output_SelectSetting(Ycenter, Select[way]);                      //�ړ����肵���}�X��\��
            }
        }

        Select[2] = Ycenter - 1; Select[3] = Ycenter + 1;                        //�I���̒��S�ƂȂ�}�X�̏㉺������
        for (int way = 2; way < Select.Length; way++)
        {
            //�I���̒��S�ƂȂ�}�X�̏㉺�����݂��ړ����肳�ꂽ�}�X�łȂ���
            if (0 <= Select[way] && Select[way] < Manager.Week.Length && Manager.Week[Select[way]].Day[Xcenter].GetComponent<I_Mass_3D>().decision == false)
            {
                Output_SelectSetting(Select[way], Xcenter);                     //�ړ����肵���}�X��\��
            }
        }

        if (Manager.Week[Ycenter].Day[Xcenter].GetComponent<I_Mass_3D>().warp == true)//�I���̒��S�ƂȂ�}�X�����[�v�}�X�Ȃ�
        {
            for (int week = 0; week < Manager.Week.Length; week++)
            {
                for (int day = 0; day < Manager.Week[0].Day.Length; day++)
                {
                    if (Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().warp == true)
                    {
                        Output_SelectSetting(week, day);                        //�I���ł���}�X��\��
                    }
                }
            }
        }
    }

    //�I���ł���}�X����ړ����肷��
    public void MoveSelect_Clicked()
    {
        for (int week = 0; week < Manager.Week.Length; week++)
        {
            for (int day = 0; day < Manager.Week[0].Day.Length; day++)
            {

                Output_SelectClear(week, day);                                           //�S�Ă̑I���}�X(���F���)��\��
                if (Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().On_Click)        //�}�X���N���b�N���ꂽ���̂�
                {
                    //Debug.Log("���肵���}�X�I");
                    select_Point--;                                                      //�v���C���[�̈ړ��ł��������1���炷
                    Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().On_Click = false;//�N���b�N���ꂽ����������
                    YPlayer_Loot[Move_Point - select_Point] = week;                      //�ړ����肵���}�X���L������
                    XPlayer_Loot[Move_Point - select_Point] = day;
                    //���S�}�X�����[�v�}�X�ł������烏�[�v�}�X�Ɉړ�������
                    if (Manager.Week[Ycenter].Day[Xcenter].GetComponent<I_Mass_3D>().warp == true && Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().warp == true)
                    {
                        Player_warpMove[Move_Point - select_Point] = true;              //���[�v�̃��[�V����������悤�ɂ���
                        Debug.Log("���[�V����");
                    }
                    //Debug.Log("�s���:"+ (Move_Point - select_Point));
                    Ycenter = week; Xcenter = day;                                      //�I���̒��S�}�X���N���b�N���ꂽ�}�X�Ɉڂ�
                    if (Move_Point - select_Point - 2 >= 0)
                    {
                        Manager.Week[YPlayer_Loot[Move_Point - select_Point - 2]].Day[XPlayer_Loot[Move_Point - select_Point - 2]].GetComponent<I_Mass_3D>().decision = false;
                    }
                }
            }
        }

        if (select_Point > 0)         //�܂��ړ��ł������������Ȃ�
        {
            MoveSelect_Display();     //�I���ł���}�X�̕\��
        }
        else
        {
            Debug.Log("�s���I��");
            StartCoroutine(PlayerMove_Coroutine(Move_Point, true));//�v���C���[�̈ړ��J�n
        }
    }

    //�v���C���[�̈ړ�
    IEnumerator PlayerMove_Coroutine(int MovePoint, bool Effect)
    {
        for (int Move = 1; Move < MovePoint + 1; Move++)//�v���C���[�̌��蕪�����ړ�
        {
            if (Player_warpMove[Move] == true)      //���[�v���邩
            {
                photonView.RPC(nameof(Output_AnimationWarpUp), RpcTarget.AllViaServer);  //���[�v�̃A�j���[�V����
                yield return new WaitForSeconds(1);     //1�b�҂�
                photonView.RPC(nameof(Output_AnimationStop), RpcTarget.AllViaServer);     //�r�f�I�̍Đ�
                photonView.RPC(nameof(Output_PlayerMove), RpcTarget.AllViaServer, YPlayer_Loot[Move], XPlayer_Loot[Move]);//���[�v�̃A�j���[�V�����ƈړ�
                yield return new WaitForSeconds(0.1f);     //0.1�b�҂�
            }
            else
            {
                if (YPlayer_Loot[Move] < YPlayer_Loot[Move - 1] && YPlayer_Loot[Move - 1] != 5)
                {
                   photonView.RPC(nameof(Output_AnimationUp), RpcTarget.AllViaServer); //��ړ��̃A�j���[�V����
                }
                else if (YPlayer_Loot[Move - 1] == 5)
                {
                    photonView.RPC(nameof(Output_AnimationUpMonth), RpcTarget.AllViaServer); //��ړ��Ō����ׂ��A�j���[�V����
                }
                if (YPlayer_Loot[Move] > YPlayer_Loot[Move - 1] && YPlayer_Loot[Move - 1] != 4)
                {
                   photonView.RPC(nameof(Output_AnimationDown), RpcTarget.AllViaServer); //���ړ��̃A�j���[�V����
                }
                else if (YPlayer_Loot[Move - 1] == 4)
                {
                   photonView.RPC(nameof(Output_AnimationDownMonth), RpcTarget.AllViaServer);//���ړ��Ō����ׂ��A�j���[�V����
                }
                if (XPlayer_Loot[Move] > XPlayer_Loot[Move - 1] && XPlayer_Loot[Move - 1] != 6)
                {
                   photonView.RPC(nameof(Output_AnimationRight), RpcTarget.AllViaServer);//�E�ړ��̃A�j���[�V����
                }
                else if (XPlayer_Loot[Move - 1] == 6)
                {
                   photonView.RPC(nameof(Output_AnimationRightMonth), RpcTarget.AllViaServer);//�E�ړ��Ō����ׂ��A�j���[�V����
                }
                if (XPlayer_Loot[Move] < XPlayer_Loot[Move - 1] && XPlayer_Loot[Move - 1] != 7)
                {
                   photonView.RPC(nameof(Output_AnimationLeft), RpcTarget.AllViaServer);//�E�ړ��Ō����ׂ��A�j���[�V��//���ړ��̃A�j���[�V����
                }
                else if (XPlayer_Loot[Move - 1] == 7)
                {
                   photonView.RPC(nameof(Output_AnimationLeftMonth), RpcTarget.AllViaServer);//���ړ��Ō����ׂ��A�j���[�V����
                }

            }
            XPlayer_position = XPlayer_Loot[Move];  //�v���C���[�̌��݂̏c�E���ʒu��ݒ�
            YPlayer_position = YPlayer_Loot[Move];
            yield return new WaitForSeconds(1);     //1�b�҂�

           photonView.RPC(nameof(Output_AnimationStop), RpcTarget.AllViaServer);  //�S�ẴA�j���[�V�������~�߂�
           photonView.RPC(nameof(Output_PlayerMove), RpcTarget.AllViaServer, YPlayer_Loot[Move], XPlayer_Loot[Move]);                //���W�ړ�
            yield return new WaitForSeconds(0.3f);     //0.1�b�҂�
        }
        for (int week = 0; week < Manager.Week.Length; week++)
        {
            for (int day = 0; day < Manager.Week[0].Day.Length; day++)
            {
                photonView.RPC(nameof(Output_decisionClear), RpcTarget.AllViaServer, week, day);
        
            }
        }
        if (Effect == true)
        {
            StopDay_Effect(); //�~�܂����}�X�̏���
        }
    }



    [PunRPC]  //�ړ��̍ۂ̍��W�ړ����o��
    private void Output_PlayerMove(int YPlayer_Loot_Move, int XPlayer_Loot_Move)
    {
        transform.position = Manager.Week[YPlayer_Loot_Move].Day[XPlayer_Loot_Move].GetComponent<I_Mass_3D>().transform.position;//�v���C���[�̈ړ�
    }




    [PunRPC]//��ړ��̃A�j���[�V�������o��
    private void Output_AnimationUp()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_up", true);
    }
    [PunRPC] //��ړ��Ō����ׂ��A�j���[�V�������o��
    private void Output_AnimationUpMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_upMonth", true);
    }
    [PunRPC]//���ړ��̃A�j���[�V�������o��
    private void Output_AnimationDown()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_down", true);
    }
    [PunRPC]//���ړ��Ō����ׂ��A�j���[�V�������o��
    private void Output_AnimationDownMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_downMonth", true);
    }
    [PunRPC]//�E�ړ��̃A�j���[�V�������o��
    private void Output_AnimationRight()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_right", true);
    }
    [PunRPC]//�E�ړ��Ō����ׂ��A�j���[�V�������o��
    private void Output_AnimationRightMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_rightMonth", true);
    }
    [PunRPC] //���ړ��̃A�j���[�V�������o��
    private void Output_AnimationLeft()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_left", true);
    }
    [PunRPC] //���ړ��Ō����ׂ��A�j���[�V�������o��
    private void Output_AnimationLeftMonth()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_leftMonth", true);
    }
    [PunRPC] //���[�v�̃A�j���[�V�������o��
    private void Output_AnimationWarpUp()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_warpup", true);
    }




    [PunRPC] //�ړ��A�j���[�V�������~�߂���o��
    private void Output_AnimationStop()
    {
        gameObject.GetComponent<Animator>().SetBool("Move_up", false);
        gameObject.GetComponent<Animator>().SetBool("Move_down", false);
        gameObject.GetComponent<Animator>().SetBool("Move_right", false);
        gameObject.GetComponent<Animator>().SetBool("Move_left", false);
        gameObject.GetComponent<Animator>().SetBool("Move_upMonth", false);
        gameObject.GetComponent<Animator>().SetBool("Move_downMonth", false);
        gameObject.GetComponent<Animator>().SetBool("Move_rightMonth", false);
        gameObject.GetComponent<Animator>().SetBool("Move_leftMonth", false);
        gameObject.GetComponent<Animator>().SetBool("Move_warpup", false);
    }



    //�ړ������ɕ������i��(����(�㉺���E), ����)
    public void Player_wayMove(string way, int step)
    {
        YPlayer_Loot[0] = YPlayer_position;                  //�v���C���[�̌��݂̃}�X���L������
        XPlayer_Loot[0] = XPlayer_position;
        Debug.Log(0 + " : " + YPlayer_Loot[0] + ":" + XPlayer_Loot[0]);
        for (int Move = 1; Move < step + 1; Move++)
        {
            switch (way)
            {
                case "��":
                    YPlayer_Loot[Move] = YPlayer_Loot[Move - 1] - 1;
                    XPlayer_Loot[Move] = XPlayer_Loot[Move - 1];
                    break;

                case "��":
                    YPlayer_Loot[Move] = YPlayer_Loot[Move - 1] + 1;
                    XPlayer_Loot[Move] = XPlayer_Loot[Move - 1];
                    break;

                case "�E":
                    YPlayer_Loot[Move] = YPlayer_Loot[Move - 1];
                    XPlayer_Loot[Move] = XPlayer_Loot[Move - 1] + 1;
                    break;

                case "��":
                    YPlayer_Loot[Move] = YPlayer_Loot[Move - 1];
                    XPlayer_Loot[Move] = XPlayer_Loot[Move - 1] - 1;
                    break;
            }
            Debug.Log(Move + " : " + YPlayer_Loot[Move] + ":" + XPlayer_Loot[Move]);
            if (YPlayer_Loot[Move] < 0 || Manager.Week.Length < YPlayer_Loot[Move])
            {
                YPlayer_Loot[Move] = YPlayer_Loot[Move - 1];
            }
            if (XPlayer_Loot[Move] < 0 || Manager.Week[0].Day.Length < XPlayer_Loot[Move])
            {
                XPlayer_Loot[Move] = XPlayer_Loot[Move - 1];
            }
            Debug.Log(Move + " : " + YPlayer_Loot[Move] + ":" + XPlayer_Loot[Move]);
        }
        StartCoroutine(PlayerMove_Coroutine(step, false));//�v���C���[�̈ړ��J�n
    }





    [PunRPC]  //�I���ł���}�X��\�����ďo��(���L����Ƒ��v���C���[���N���b�N�o����\��������׋��L���Ȃ��悤�ɗ���)
    private void Output_SelectSetting(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().select_display();
    }

    [PunRPC] //�I���ł���}�X���\���ɂ��ďo��
    private void Output_SelectClear(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().select_hidden();
    }

    [PunRPC] //�ړ����肵���}�X��\�����ďo��
    private void Output_decisionSetting(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().decision_display();
    }

    [PunRPC]//�ړ����肵���}�X���\���ɂ��ďo��
    private void Output_decisionClear(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().decision_hidden();
    }





    //�~�܂����}�X�̏���
    private void StopDay_Effect()
    {
        if (Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Goal == true)
        {
            Player_Goal();//�S�[�������Ƃ��̏���
        }
        else
        {
            if (Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Open == false)//�܂��J���ĂȂ��}�X�Ȃ�
            {
               
                photonView.RPC(nameof(Output_hideCoverClear), RpcTarget.All, YPlayer_position, XPlayer_position); //�}�X���J�����\���ɂ���
                Player_DayEffect();//���t�̌���
            }
            else
            {
                Manager.PlayerTurn_change();         //�^�[����ς���
            }
        }
    }





    //�S�[���������̏���
    public void Player_Goal()
    {
        photonView.RPC(nameof(Output_GoalCount), RpcTarget.All); //�S�[���������Z
        Manager.Goal_Add();//�Q�[���S�̂̃S�[�����ɉ��Z
        Player_DayEffect();//���t�̌���
    }

    //�S�[���������̃S�[�������o��
     [PunRPC]private void Output_GoalCount()
    {
        Goalcount++;
    }

    //���t�̌��ʔ���
    public void Player_DayEffect()
    {
        string day = Manager.Week[YPlayer_position].Day[XPlayer_position].GetComponent<I_Mass_3D>().Day;//����������t���擾
        StartCoroutine(Day_Animation(day));     //�r�f�I�̍Đ��ƃz�b�v�A�b�v�̕\��
                                                //�����ɓ��t�̌��ʓ����

    }

    //�J�����}�X���\���ɂ��ďo��
    [PunRPC] private void Output_hideCoverClear(int week, int day)
    {
        Manager.Week[week].Day[day].GetComponent<I_Mass_3D>().hideCover_Clear();//�}�X���J�����\���ɂ���
    }

    //�r�f�I�̍Đ��ƃz�b�v�A�b�v�̕\��
  

    //�r�f�I�̍Đ��ƃz�b�v�A�b�v�̕\��
    IEnumerator Day_Animation(string day)
    {
        Manager.Output_VideoSetting();
        Manager.Output_HopUp();
        gameObject.GetComponent<Day_Effect>().Output_HopUp_Setting(day);
        Manager.Video_obj.GetComponent<VideoPlayer>().clip = gameObject.GetComponent<Day_Effect>().Output_VideoClip(day);
        Manager.Output_VideoStart();     //�r�f�I�̍Đ� Day������ƃG���[��f���̂ł���������
        yield return new WaitForSeconds(8);     //8�b�҂�
        Manager.Output_VideoFinish();     //�r�f�I�̔�\��
        Manager.PlayerTurn_change();         //�^�[����ς���
    }




    // �ȉ�MannequinPlayer��̈��p=====================================================================
    [PunRPC] public void ItemAdd(int ItemNum)//ItemNum���}�X�^�[�o�^���̔ԍ�
    {
        Hub_Items.Add(ScriptableObject.Instantiate(ItemMaster.Anniversary_Items[ItemNum]));//�}�X�^�[�ɂ���ItemNum�̃A�C�e���𐶐����AHub�ɒǉ�
        ItemBlock.GetComponent<ItemBlock_List_Script>().AddItem(ItemNum);
    }

    [PunRPC] public void ItemLost(int HubItemNum)//HubItemNum�������A�C�e���o�^���̔ԍ�
    {

        Hub_Items.RemoveAt(HubItemNum);//��������HubItemNum�Ԗڂ̃A�C�e��������
        ItemBlock.GetComponent<ItemBlock_List_Script>().LostItem(HubItemNum);


    }
    // =====================================================================

















}


