using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
public class NetWorkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public GameObject SceneManagerOj;
    public InputField inputField;
    public Text PlayerName;
    string GameVersion = "Ver1.0";
    void Start()
    {
       
        PhotonNetwork.ConnectUsingSettings();
        inputField = GetComponent<InputField>();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        Debug.Log("���r�[�֎Q�����܂���");
    }
    void Update()
    {
        
    }

    public async void JoineLoom(int RoomNum)
    {

        SceneManagerOj.GetComponent<SceneManagaer>().TransitionToGame();
        await Task.Delay(400);
       
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
       
      

        PhotonNetwork.JoinOrCreateRoom("Room" + RoomNum, new RoomOptions(), TypedLobby.Default);
      


    }
    public override void OnJoinedRoom()
    {
      
        
        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
    }

   

    public void FinishInputName()
    {
        PhotonNetwork.NickName= PlayerName.text;
        Debug.Log("" + PlayerName.text);
        Debug.Log(PlayerName.text);
    }



}
