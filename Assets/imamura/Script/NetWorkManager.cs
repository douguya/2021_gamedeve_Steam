using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public GameObject SceneManagerOj;
    public InputField inputField;
    public Text PlayerName;
    public Text[] RoomText;
    public GameObject[] RoomBotton;

    [SerializeField]
    public string PlayerIdVew;
    public string PlayerNameVew;




    public bool[] CanJoinRoom = new bool[5] {true,true,true,true,true};

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
        Debug.Log("ロビーへ参加しました");
    }
    void Update()
    {

    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int forL=0;
        foreach (var info in roomList)
        {
            RoomText[forL].text=info.Name +"  "+info.PlayerCount+"/"+info.MaxPlayers;
            forL ++;
            
        }
    }
    public async void JoineLoom(int RoomNum)
    {
            SceneManagerOj.GetComponent<SceneManagaer>().TransitionToGame();
            await Task.Delay(400);
            var roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.JoinOrCreateRoom("ルーム" + RoomNum, roomOptions, TypedLobby.Default);
           
            


    }
    public override void OnJoinedRoom()
    {
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(-7.69f, -3.66f);
        PhotonNetwork.Instantiate("PurehabTest_Player", position, Quaternion.identity);
        
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (SceneManager.GetActiveScene().name == SceneManagaer.Gamesend)
        {
            SceneManager.LoadScene(SceneManagaer.Lobysend);
        }
    }



    public void FinishInputName()
    {
        PhotonNetwork.NickName = PlayerName.text;
       // Debug.Log("" + PlayerName.text);
        Debug.Log("AAAAAAAAA"+PlayerName.text);
        Debug.Log("PPOPPPPPP"+PhotonNetwork.LocalPlayer.UserId);


    }
   
}