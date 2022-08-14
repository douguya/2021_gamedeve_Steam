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
   // public GameObject I_game_manager;//ゲームマネージャーのオブジェクト;
    public I_game_manager I_game_Manager_Script;//ゲームマネージャーのオブジェクトのスクリプト;

    //  public GameObject ReadyButton;//ゲームマネージャーのオブジェクト;
    public ReadyButton ReadyButton_Script;//ゲームマネージャーのオブジェクトのスクリプト;

    public InputField InputField;     //名前入力欄
    public Text PlayerName;           //入力されたプレイヤーの名前
    public Text[] RoomText;           //ルームの名前とテキスト
    public GameObject[] RoomBotton;   //ルームボタンのオブジェクト

    public GameObject LoadImage;//ロード画面のもどき
    

    [SerializeField]
    public int PlayerIdVew;
    public string PlayerNameVew;
  

    public bool[] CanJoinRoom = new bool[5] { true, true, true, true, true };

    private byte MaxRoomPeople = 4;//一つのルームの最大人数
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


    public override void OnConnectedToMaster()//マスターサーバに接続された時に呼ばれる
    {
        PhotonNetwork.JoinLobby();//ロビーに入る
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        Debug.Log("ロビーへ参加しました");
        LoadImage.SetActive(false);

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

    public override void OnPlayerLeftRoom(Player player)//プレイヤーが抜けたときの処理
    {
        Playerlist_Update();//プレイヤーのオブジェクト格納用/初期位置への移動も含む
        ReadyButton_Script.PlayerLeftRoom_Jointed();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)//自身がルームに入ったとき
    {
        Playerlist_Update();//プレイヤーのオブジェクト格納用/初期位置への移動も含む
        ReadyButton_Script.JoinedRoom_Jointed();

    }


    public void Playerlist_Update()//プレイヤーのオブジェクト格納用/初期位置への移動も含む
    {

        Players_spot = GameObject.FindGameObjectsWithTag("Player");//プレイヤーオブジェクトの一時保存場所　タグで軒並みとる
   

        int loop = 0;//アイテムリストの初期値
        foreach (var PList in PhotonNetwork.PlayerList)//プレイヤーリストの内容を順番に格納
        {
            foreach (GameObject obj in Players_spot)//プレイヤーリストの中身と、一時保存したプレイヤーオブジェクトを突き合わせる
            {

                if (PList.ActorNumber==obj.GetComponent<PhotonView>().CreatorActorNr) //リストのプレイヤーのIDとオブジェクトの作成者のADを比較
                { I_game_Manager_Script.Player.Add(obj);}//この処理で、プレイヤーリストの順番どおりにプレイヤーオブジェクトを保存できる　順番を変えられるようにしたいなら変更の余地あり
                I_game_Manager_Script.Player_setting(loop);//プレイ矢―を所定の位置に移動
            }

            loop++;
        }
        I_game_Manager_Script.joining_Player = PhotonNetwork.PlayerList.Length;
        if (I_game_Manager_Script.Player.Count!=loop)
        {
            Debug.LogError("問題発生。部屋を入りなおして下さい");
        }
    }




}