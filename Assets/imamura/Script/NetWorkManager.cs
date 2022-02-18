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
    public PlayerStatasIMamura playerStatasIMamura;

    [SerializeField]
    public int PlayerIdVew;
    public string PlayerNameVew;
    public GameObject parent;
   
    public bool[] CanJoinRoom = new bool[5] {true,true,true,true,true};
    string GameVersion = "Ver1.0";
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        inputField = GetComponent<InputField>();
      
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();//ロビーに入る
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        Debug.Log("ロビーへ参加しました");
    }
    void Update()
    {

    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)//ルームリスト更新時
    {
        int forL=0;
        foreach (var info in roomList)//ルームリストの取得
        {  //部屋のテキスト   部屋の名前　　部屋のプレイヤーの数　　部屋の最大人数
            RoomText[forL].text=info.Name +"  "+info.PlayerCount+"/"+info.MaxPlayers;
            forL ++;
            
        }
    }
    public async void JoineLoom(int RoomNum)//部屋に入る処理
    {
            SceneManagerOj.GetComponent<SceneManagaer>().TransitionToGame();//ゲームシーンへ遷移
            await Task.Delay(400);//ディレイ　タイミング用
            var roomOptions = new RoomOptions();//ルームオプションの設定
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.JoinOrCreateRoom("ルーム" + RoomNum, roomOptions, TypedLobby.Default);
          
    }
    public override void OnJoinedRoom()//部屋に入る
    {
      
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(-7.69f, -3.66f);
        PhotonNetwork.Instantiate("PurehabTest_Player", position, Quaternion.identity);
        position = new Vector3(-303.5f, -71f);

    }
    public override void OnJoinRoomFailed(short returnCode, string message)//部屋に入れなかったとき
    {
        if (SceneManager.GetActiveScene().name == SceneManagaer.Gamesend)//ゲームシーンに入ってしまった場合
        {
            SceneManager.LoadScene(SceneManagaer.Lobysend);//ロビーシーンに返す
            PhotonNetwork.JoinLobby();//ロビーに返す
        }
    }



    public void FinishInputName()//名前が入力されたとき
    {
        PhotonNetwork.NickName = PlayerName.text;//プレイヤーの名前を変更する
        PlayerNameVew = PlayerName.text;//プレイヤーの名前をインスペクターから見えるようにする
    }


  

}