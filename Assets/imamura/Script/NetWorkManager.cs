using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public GameObject SceneManagerOj;
    public InputField inputField;
    public Text PlayerName;
    public Text[] RoomText;
    public GameObject[] RoomBotton;
    public PlayerStatasIMamura playerStatasIMamura;

    [SerializeField]
    public int PlayerIdVew;
    public string PlayerNameVew;
    public GameObject parent;

    public bool[] CanJoinRoom = new bool[5] { true, true, true, true, true };
    string GameVersion = "Ver1.0";
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        inputField = GetComponent<InputField>();

    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();//���r�[�ɓ���
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        Debug.Log("���r�[�֎Q�����܂���");
    }
    void Update()
    {

    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)//���[�����X�g�X�V��
    {
        int forL = 0;
        foreach (var info in roomList)//���[�����X�g�̎擾
        {  //�����̃e�L�X�g   �����̖��O�@�@�����̃v���C���[�̐��@�@�����̍ő�l��
            RoomText[forL].text = info.Name + "  " + info.PlayerCount + "/" + info.MaxPlayers;
            forL++;

        }
    }
    public void JoineLoom(int RoomNum)//�����ɓ��鏈��
    {
        SceneManagerOj.GetComponent<SceneManagaer>().TransitionToGame();//�Q�[���V�[���֑J��
        StartCoroutine(JoineLoom_Coroutine(RoomNum));
    }




    public IEnumerator JoineLoom_Coroutine(int RoomNum)
    {

        yield return new WaitForSeconds(0.4f);
        var roomOptions = new RoomOptions();//���[���I�v�V�����̐ݒ�
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("���[��" + RoomNum, roomOptions, TypedLobby.Default);

        yield break;
    }



    public  override void OnJoinedRoom()//�����ɓ���
    {
        StartCoroutine(OnJoinedRoom_Coroutine());
    }

    public IEnumerator OnJoinedRoom_Coroutine()
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