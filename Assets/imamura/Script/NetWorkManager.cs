using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public GameObject SceneManagerOj;
    string GameVersion = "Ver1.0";
    void Start()
    {
       
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        Debug.Log("ÉçÉrÅ[Ç÷éQâ¡ÇµÇ‹ÇµÇΩ");
    }
    void Update()
    {
        
    }

    public void OnJoineLoom(int RoomNum)
    {
        SceneManagerOj.GetComponent<SceneManagaer>().TransitionToGame();

        Debug.Log("Ç†ÇŒÇŒÇŒÇŒÇŒÇŒ");
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Room" + RoomNum, new RoomOptions(), TypedLobby.Default);



    }


    public override void OnJoinedRoom()
    {

       
        Debug.Log(PhotonNetwork.CurrentRoom);
    }





}
