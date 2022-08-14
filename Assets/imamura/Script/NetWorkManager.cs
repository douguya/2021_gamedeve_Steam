using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public string[] Room=new string[5];

    public GameObject SceneManagerObj;//�V�[���}�l�[�W���[�̃I�u�W�F�N�g
   // public GameObject I_game_manager;//�Q�[���}�l�[�W���[�̃I�u�W�F�N�g;
    public I_game_manager I_game_Manager_Script;//�Q�[���}�l�[�W���[�̃I�u�W�F�N�g�̃X�N���v�g;

    //  public GameObject ReadyButton;//�Q�[���}�l�[�W���[�̃I�u�W�F�N�g;
    public ReadyButton ReadyButton_Script;//�Q�[���}�l�[�W���[�̃I�u�W�F�N�g�̃X�N���v�g;

    public InputField InputField;     //���O���͗�
    public Text PlayerName;           //���͂��ꂽ�v���C���[�̖��O
    public Text[] RoomText;           //���[���̖��O�ƃe�L�X�g
    public GameObject[] RoomBotton;   //���[���{�^���̃I�u�W�F�N�g

    public GameObject LoadImage;//���[�h��ʂ̂��ǂ�
    

    [SerializeField]
    public int PlayerIdVew;
    public string PlayerNameVew;
  

    public bool[] CanJoinRoom = new bool[5] { true, true, true, true, true };

    private byte MaxRoomPeople = 4;//��̃��[���̍ő�l��
    string GameVersion = "Ver1.0";

    private GameObject[] Players_spot;



    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        InputField = GetComponent<InputField>();
        LoadImage.SetActive(true);
       // I_game_Manager_Script=I_game_manager.GetComponent<I_game_manager>();
       // ReadyButton_Script=ReadyButton.GetComponent<ReadyButton>();

    }


    public override void OnConnectedToMaster()//�}�X�^�[�T�[�o�ɐڑ����ꂽ���ɌĂ΂��
    {
        PhotonNetwork.JoinLobby();//���r�[�ɓ���
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        Debug.Log("���r�[�֎Q�����܂���");
        LoadImage.SetActive(false);

    }




    public override void OnRoomListUpdate(List<RoomInfo> roomList)//���[�����X�g�X�V�� �X�V���ꂽ���[���݂̂��󂯎��
    {
      
        foreach (var info in roomList)//���[�����X�g�̎擾
        {
            int RoomNum = int.Parse(Regex.Replace(info.Name, @"[^0-9]", ""));//�ύX���ꂽ���[���̔ԍ��𒊏o

            RoomText[RoomNum-1].text = info.Name + "A " + info.PlayerCount + "/" + MaxRoomPeople;


        }
    
        
    }


    public void JoineLoom(int RoomNum)//�����ɓ��鏈��
    {
        SceneManagerObj.GetComponent<SceneManagaer>().TransitionToGame();//�Q�[���V�[���֑J��
        StartCoroutine(JoineLoom_Coroutine(RoomNum));
    }

    public IEnumerator JoineLoom_Coroutine(int RoomNum)//�����ɓ��鏈��,�R���[�`��
    {
        yield return new WaitForSeconds(0.4f);
        var roomOptions = new RoomOptions();//���[���I�v�V�����̐ݒ�
        roomOptions.MaxPlayers = MaxRoomPeople;
        PhotonNetwork.JoinOrCreateRoom("���[��" + RoomNum, roomOptions, TypedLobby.Default);

        yield break;
    }




    public  override void OnJoinedRoom()//�����ɓ��ꂽ���̏���
    {
        StartCoroutine(OnJoinedRoom_Coroutine());
    }

    public IEnumerator OnJoinedRoom_Coroutine()///�����ɓ��ꂽ���̏����@�R���[�`��
    {

        var position = new Vector3(0.28f, -3.37f, -0.73f);
        GameObject blockTile = PhotonNetwork.Instantiate("Player3D", position, Quaternion.identity);
        position = new Vector3(-303.5f, -71f);
        Playerlist_Update();
        Debug.Log(blockTile);
        
        yield return new WaitForSeconds(0.4f);
        LoadImage.SetActive(false);
        yield break;
        
    }

    [PunRPC]
    public void PlayerAppearance(GameObject Player)
    {
        Player.SetActive(true);
    }







    public override void OnJoinRoomFailed(short returnCode, string message)//�����ɓ���Ȃ������Ƃ�
    {
        if (SceneManager.GetActiveScene().name == SceneManagaer.Gamesend)//�Q�[���V�[���ɓ����Ă��܂����ꍇ
        {
            SceneManager.LoadScene(SceneManagaer.Lobysend);//���r�[�V�[���ɕԂ�
            PhotonNetwork.JoinLobby();//���r�[�ɕԂ�
        }
    }



    public void FinishInputName()//���O�����͂��ꂽ�Ƃ�
    {
        PhotonNetwork.NickName = PlayerName.text;//�v���C���[�̖��O��ύX����
        PlayerNameVew = PlayerName.text;//�v���C���[�̖��O���C���X�y�N�^�[���猩����悤�ɂ���
    }



    public void Leave_the_room(){
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player player)//�v���C���[���������Ƃ��̏���
    {
        Playerlist_Update();//�v���C���[�̃I�u�W�F�N�g�i�[�p/�����ʒu�ւ̈ړ����܂�
        ReadyButton_Script.PlayerLeftRoom_Jointed();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)//���g�����[���ɓ������Ƃ�
    {
        Playerlist_Update();//�v���C���[�̃I�u�W�F�N�g�i�[�p/�����ʒu�ւ̈ړ����܂�
        ReadyButton_Script.JoinedRoom_Jointed();

    }


    public void Playerlist_Update()//�v���C���[�̃I�u�W�F�N�g�i�[�p/�����ʒu�ւ̈ړ����܂�
    {

        Players_spot = GameObject.FindGameObjectsWithTag("Player");//�v���C���[�I�u�W�F�N�g�̈ꎞ�ۑ��ꏊ�@�^�O�Ō����݂Ƃ�
   

        int loop = 0;//�A�C�e�����X�g�̏����l
        foreach (var PList in PhotonNetwork.PlayerList)//�v���C���[���X�g�̓��e�����ԂɊi�[
        {
            foreach (GameObject obj in Players_spot)//�v���C���[���X�g�̒��g�ƁA�ꎞ�ۑ������v���C���[�I�u�W�F�N�g��˂����킹��
            {

                if (PList.ActorNumber==obj.GetComponent<PhotonView>().CreatorActorNr) //���X�g�̃v���C���[��ID�ƃI�u�W�F�N�g�̍쐬�҂�AD���r
                { I_game_Manager_Script.Player.Add(obj);}//���̏����ŁA�v���C���[���X�g�̏��Ԃǂ���Ƀv���C���[�I�u�W�F�N�g��ۑ��ł���@���Ԃ�ς�����悤�ɂ������Ȃ�ύX�̗]�n����
                I_game_Manager_Script.Player_setting(loop);//�v���C��\������̈ʒu�Ɉړ�
            }

            loop++;
        }
        I_game_Manager_Script.joining_Player = PhotonNetwork.PlayerList.Length;
        if (I_game_Manager_Script.Player.Count!=loop)
        {
            Debug.LogError("��蔭���B���������Ȃ����ĉ�����");
        }
    }




}