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

    public GameObject SceneManagerObj;//シーンマネージャーのオブジェクト
    public InputField InputField;     //名前入力欄
    public Text PlayerName;           //入力されたプレイヤーの名前
    public Text[] RoomText;           //ルームの名前とテキスト
    public GameObject[] RoomBotton;   //ルームボタンのオブジェクト
    public PlayerStatasIMamura PlayerStatasIMamura;

    [SerializeField]
    public int PlayerIdVew;
    public string PlayerNameVew;
    public GameObject parent;

    public bool[] CanJoinRoom = new bool[5] { true, true, true, true, true };

    private byte MaxRoomPeople = 4;//一つのルームの最大人数
    string GameVersion = "Ver1.0";





    void Start()


    {
        PhotonNetwork.ConnectUsingSettings();
        InputField = GetComponent<InputField>();

    }


    public override void OnConnectedToMaster()//マスターサーバに接続された時に呼ばれる
    {
        PhotonNetwork.JoinLobby();//ロビーに入る
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        Debug.Log("ロビーへ参加しました");
    
    }




    public override void OnRoomListUpdate(List<RoomInfo> roomList)//ルームリスト更新時 更新されたルームのみを受け取る
    {
      
        foreach (var info in roomList)//ルームリストの取得
        {
            int RoomNum = int.Parse(Regex.Replace(info.Name, @"[^0-9]", ""));//変更されたルームの番号を抽出

            RoomText[RoomNum-1].text = info.Name + "A " + info.PlayerCount + "/" + MaxRoomPeople;


        }
    
        
    }


    public void JoineLoom(int RoomNum)//部屋に入る処理
    {
        SceneManagerObj.GetComponent<SceneManagaer>().TransitionToGame();//ゲームシーンへ遷移
        StartCoroutine(JoineLoom_Coroutine(RoomNum));
    }

    public IEnumerator JoineLoom_Coroutine(int RoomNum)//部屋に入る処理,コルーチン
    {
        yield return new WaitForSeconds(0.4f);
        var roomOptions = new RoomOptions();//ルームオプションの設定
        roomOptions.MaxPlayers = MaxRoomPeople;
        PhotonNetwork.JoinOrCreateRoom("ルーム" + RoomNum, roomOptions, TypedLobby.Default);

        yield break;
    }




    public  override void OnJoinedRoom()//部屋に入れた時の処理
    {
        StartCoroutine(OnJoinedRoom_Coroutine());
    }

    public IEnumerator OnJoinedRoom_Coroutine()///部屋に入れた時の処理　コルーチン
    {
        var position = new Vector3(-7.69f, -3.66f);
        GameObject blockTile = PhotonNetwork.Instantiate("playerAA", position, Quaternion.identity);
        position = new Vector3(-303.5f, -71f);
        yield return new WaitForSeconds(0.4f);
        yield break;
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



    public void Leave_the_room(){
        PhotonNetwork.LeaveRoom();
    }






   

}