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
    public InputField InputField;     //���O���͗�
    public Text PlayerName;           //���͂��ꂽ�v���C���[�̖��O
    public Text[] RoomText;           //���[���̖��O�ƃe�L�X�g
    public GameObject[] RoomBotton;   //���[���{�^���̃I�u�W�F�N�g
    public PlayerStatasIMamura PlayerStatasIMamura;

    [SerializeField]
    public int PlayerIdVew;
    public string PlayerNameVew;
    public GameObject parent;

    public bool[] CanJoinRoom = new bool[5] { true, true, true, true, true };

    private byte MaxRoomPeople = 4;//��̃��[���̍ő�l��
    string GameVersion = "Ver1.0";





    void Start()


    {
        PhotonNetwork.ConnectUsingSettings();
        InputField = GetComponent<InputField>();

    }


    public override void OnConnectedToMaster()//�}�X�^�[�T�[�o�ɐڑ����ꂽ���ɌĂ΂��
    {
        PhotonNetwork.JoinLobby();//���r�[�ɓ���
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        Debug.Log("���r�[�֎Q�����܂���");
    
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
        var position = new Vector3(-7.69f, -3.66f);
        GameObject blockTile = PhotonNetwork.Instantiate("playerAA", position, Quaternion.identity);
        position = new Vector3(-303.5f, -71f);
        yield return new WaitForSeconds(0.4f);
        yield break;
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






   

}