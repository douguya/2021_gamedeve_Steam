using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RoomSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject LobbyUI;//ロビーのUI
    [SerializeField]
    private GameObject enterMatchWaitRoomUI;//待機部屋のUI

    [SerializeField]
    private TextMeshProUGUI RoomNum;//部屋にいる人数の把握　未解決の

    [SerializeField]
    private TextMeshProUGUI PlayerName;

    [SerializeField]
    private TextMeshProUGUI joinedPlayer;

    [SerializeField]
    GameObject avatarName;


    [SerializeField]
    private GameObject GoButton;//マスタークライアント以外はGOボタンが押せない

    [SerializeField]
    private int PlayerNum;

    public bool enterMatchWaitRoomJudge;

    [SerializeField] int num = 0;
    [SerializeField] int num2;

    [SerializeField] GameObject Camera1;
    //[SerializeField] GameObject Camera2;
    //[SerializeField] GameObject Camera3;
    //[SerializeField] GameObject Camera4;

    GameObject player;

    string p;

    [SerializeField] int p1;
    public static int Porder;//プレイヤーの順番

    [SerializeField] List<int> Pnum;
    [SerializeField] bool Pbool;
    [SerializeField] int[] PN = { 0, 0, 0, 0 };

    [SerializeField] int RoomNumber;

    public static bool OneTime = true;

    //[SerializeField]Photon

    //  private string id;
    private void Start()
    {
        // PhotonServerSettingsに設定した内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.LocalPlayer.NickName = "Player" + Random.Range(1, 1000);//Avatarプレハブ（ネットワークオブジェクト）で作られたプレイヤーの名前を受け取り、Instantiateした際には変更を読み取る
        PhotonNetwork.IsMessageQueueRunning = true;
       // CameraMove.transCamera = Camera.main.transform;
        Camera1.SetActive(false);
        //Camera2.SetActive(false);
        //Camera3.SetActive(false);
        //Camera4.SetActive(false);
        Pnum.AddRange(PN);

    }

    // マスターサーバーへの接続が成功したら、ロビーに参加する
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        enterMatchWaitRoomJudge = false;

    }

    public void Update()
    {
        RoomNum.text = PhotonNetwork.CountOfPlayersInRooms + "/" + PhotonNetwork.CountOfPlayers;
        // Debug.Log(PhotonNetwork.CountOfRooms);
        UpdateMemberList();
        //num = photonView.OwnerActorNr % 4;
        // Pbool = Pnum.Contains(4);

    }

    public override void OnJoinedLobby()//検証用
    {
        Debug.Log("ロビーに参加");
        LobbyUI.SetActive(true);
        enterMatchWaitRoomUI.SetActive(false);

    }
    public override void OnJoinedRoom()//ローカルプレイヤーのみに反応
    {
        Debug.Log("待機ルームに参加");
        LobbyUI.SetActive(false);
        enterMatchWaitRoomUI.SetActive(true);

        //PhotonNetwork.Instantiate("NewPlayer", new Vector3(Random.Range(160, 180), 30, Random.Range(250, 270)), Quaternion.identity);
        //PhotonNetwork.Instantiate("MainCamera", new Vector3(Random.Range(160, 180), 30, Random.Range(250, 270)), Quaternion.identity);
        player = PhotonNetwork.Instantiate("NewPlayer", new Vector3(Random.Range(160, 180), 30, Random.Range(250, 270)), Quaternion.identity);

        //player.name = "Player" + Porder;

        //// Camera.SetActive(true);
        //  Instantiate(Camera1, new Vector3(Random.Range(160, 180), 30, Random.Range(250, 270)), Quaternion.identity) ;
        //プレイヤーのプレハブのタグ名を統一？　適応がされるか要確認　適応された場合はプレイヤープレハブタグがついたオブ軸とから逃げる操作を実装する
        Photon.Realtime.Player player2 = PhotonNetwork.LocalPlayer;
        // Debug.Log("PlayerNo" + player2.ActorNumber);
        p1 = player2.ActorNumber;


        // photonView.RPC(nameof(PlayerNameShare), RpcTarget.AllBuffered, player.name);


        //camera.name = PhotonNetwork.LocalPlayer.NickName+"Camera";

        ///////////////////////////////////////////////////////////絶対修正
        if (PhotonNetwork.IsMasterClient)
        {
            // Debug.Log("自身がマスタークライアントです");
            GoButton.SetActive(true);
            // player.name = "Player1";
            // camera.name = "Camera1";
        }
        else
        {
            // Debug.Log("自身がローカルプライヤーです");
            GoButton.SetActive(false);
            // player.name = "Player2";
        }
        /////////////////////////////////////////////////////////////
        // PhotonNetwork.Instantiate("Avatar", new Vector3(0, 0, 0), Quaternion.identity);//他のネットワークオブジェクトがダメだったので可視化されているこれで確認したが駄目だった
        //Debug.Log("ルームに入ってない人"+PhotonNetwork.CountOfPlayersOnMaster);　未解決
        //Debug.Log("ルームに入っている人" + PhotonNetwork.CountOfPlayersInRooms); 未解決
    }



    public void OnPhotonPlayerConnected()
    {
        // Debug.Log(player.name + " is joined.");
        UpdateMemberList();
        RoomNumber = PhotonNetwork.CountOfPlayersInRooms;
    }
    public void UpdateMemberList()
    {
        num = 0;
        joinedPlayer.text = "";

        foreach (var p in PhotonNetwork.PlayerList)
        {
            joinedPlayer.text += p.NickName + "\n";
            // Pnum.Insert(num,p.ActorNumber);
            Pnum[num] = p.ActorNumber;
            num++;
            //PhotonNetwork.Instantiate("PlayerName", new Vector3(0, 0, 0), Quaternion.identity);
        }
        for (int i = 0; i < Pnum.Count; i++)
        {
            if (p1 == Pnum[i]) { Porder = i + 1; }//
        }
        if (OneTime)
        {

            photonView.RPC(nameof(RoomID), RpcTarget.AllBuffered, player);// player.name = "Player" + Porder;
            OneTime = false;
            Debug.Log(player.name);
            Camera1.SetActive(true);
        }
        //Debug.Log("プレイヤー"+Porder);
        // Debug.Log(num);
    }
    public override void OnLeftRoom()
    {
        OneTime = true;
        Debug.Log("待機ルームから退出");
        LobbyUI.SetActive(true);
        enterMatchWaitRoomUI.SetActive(false);
    }

    // ルームの作成が成功した時に呼ばれるコールバック
    public override void OnCreatedRoom()
    {
        Debug.Log("ルーム作成に成功しました");
    }

    // ルームの作成が失敗した時に呼ばれるコールバック
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"ルーム作成に失敗しました: {message}");
    }

    [PunRPC]
    private void RoomID(GameObject player)
    {
        player.name = "Player" + Porder;

    }




    [PunRPC]
    private void PlayerNameShare(string p)
    {
        switch (p)
        {
            case "Player1": break;
            case "Player2": break;
            case "Player3": break;
            case "Player4": break;
        }
    }

}



