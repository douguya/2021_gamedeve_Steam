using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class ReadyButton : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [SerializeField]
    bool Ready = false;�@�@�@�@//�������
    GameObject ReadyBotton;�@�@//���������{�^���ւ̃A�N�Z�X
    public GameObject GameStart;//�Q�[���X�^�[�g�{�^���̎���
    public Text ReadyText;�@�@�@//���������{�^���̃e�L�X�g
    public string Ready_Txt;�@�@//���������{�^���̃e�L�X�g����p������@���₷������p
    public int ReadyPlayerNum = 0;//�������������v���C���[�̐l��
    public Hashtable hashtable = new Hashtable();//�J�X�^���v���p�e�B�̃��X�g

    void Start()
    {
        // var properties = new ExitGames.Client.Photon.Hashtable();

        //  PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void RedayChange()//������Ԃ̕ϑJ�p
    {
        if (Ready == false)//�������������Ă��Ȃ��Ƃ��ɋN��������
        {
            hashtable["ReadyPlayerNum"] = true;//��������������:�J�X�^���v���p�e�B
            Ready = true;//��������������
        }
        else if (Ready == true)//�������������Ă���Ƃ��ɋN��������
        {
            hashtable["ReadyPlayerNum"] = false;//�������Ȃ����F�J�X�^���v���p�e�B
            Ready = false;//������������
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);//�ύX�����J�X�^���v���p�e�B�̍X�V
    }



    //�J�X�^���v���p�B���X�V���ꂽ�ۂ̃R�[���o�b�N
     public override void OnPlayerPropertiesUpdate(Player player, Hashtable propertiesThatChanged)
    {

        int loop = 0;
        foreach (var p in PhotonNetwork.PlayerList)//�v���C���[�S���̃J�X�^���v���p�e�B�F������Ԃ̏W�v
        {
            if ((bool)p.CustomProperties["ReadyPlayerNum"] == true)//���Ԗڂ̃v���C���[�̏������������Ă���Ȃ�
            {
                loop++;//�l�����J�E���g
            }
        }
      //�e�L�X�g�@=���������l��/�v���C���[�S�̂̐l��
        Ready_Txt = loop + "/ " + PhotonNetwork.PlayerList.Length;//���������e�L�X�g�F���₷�����邽�߂ɂ����ł܂Ƃ߂�

        if (Ready == false)//���v���C���[�̏������ł��Ă��Ȃ�
        {
            ReadyText.text = "��������������" + Ready_Txt;//����������҂e�L�X�g�֕ύX
        }
        else if (Ready == true) //���v���C���[�̏������ł��Ă���
        {
            ReadyText.text = "�����ɖ߂�" + Ready_Txt;//�ēx�����ɖ߂�e�L�X�g�֕ύX
        }

        if (PhotonNetwork.PlayerList.Length == loop) //���������l���� �v���C���[�S�̂̐l���������Ƃ�
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)//���̂����Ńv���C���[���}�X�^�[�N���C�A���g�ł���ꍇ
            {
                GameStart.SetActive(true);//�Q�[���X�^�[�g�{�^���̏o��
            }
        }
        else
        {
            GameStart.SetActive(false);//�Q�[���X�^�[�g�{�^���̏���
        }

    }





    public override void OnPlayerLeftRoom(Player otherPlayer)//���̃v���C���[���ޏo�����ꍇ
    {

        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);//�㉺�ǂ���������Ȃ��@���Ԃ��������玎��
        OnRoomPropertiesUpdate(hashtable);//�J�X�^���v���p�e�B���X�V�i���������󋵂̔��f�j
    }
    public override void OnJoinedRoom()//���g�����[���ɓ������Ƃ�
    {
        hashtable["ReadyPlayerNum"] = false;//�J�X�^���v���p�e�B�̃Z�b�e�B���O�@����Ȃ̂�false
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);//�X�V
        OnRoomPropertiesUpdate(hashtable);////�J�X�^���v���p�e�B���X�V�i���������󋵂̔��f�j
    }

    public void GameStartn()//�Q�[���X�^�[�g�{�^������GamestartToRPC���N������p
    {
        photonView.RPC(nameof(GamestartToRPC), RpcTarget.All);
    }

    [PunRPC]
    public void GamestartToRPC()//�Q�[���X�^�[�g
    {
        ReadyText.text = "�Q�[����";

    }




}




