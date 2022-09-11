using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class NetWorkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public string[] Room = new string[5];

    public GameObject SceneManagerObj;//シーンマネージャーのオブジェクト
                                      // public GameObject I_game_manager;//ゲームマネージャーのオブジェクト;
    public I_game_manager I_game_Manager_Script;//ゲームマネージャーのオブジェクトのスクリプト;

    //  public GameObject ReadyButton;//ゲームマネージャーのオブジェクト;
    public ReadyButton ReadyButton_Script;//ゲームマネージャーのオブジェクトのスクリプト;

    public InputField InputField;     //名前入力欄
    public Text PlayerName;           //入力されたプレイヤーの名前
    public Text[] RoomText;           //ルームの名前とテキスト
    public GameObject[] RoomBotton;   //ルームボタンのオブジェクト

    public GameObject ItemListBox;//アイテムリストの親
    public GameObject LoadImage;//ロード画面のもどき

    public GameObject ItemList_Moment;//アイテムリストの親

    [SerializeField]
    public int PlayerIdVew;
    public string PlayerNameVew;

    public Hashtable hashtable = new Hashtable();//カスタムプロパティのリスト
    public bool[] CanJoinRoom = new bool[5] { true, true, true, true, true };

    private byte MaxRoomPeople = 4;//一つのルームの最大人数
    string GameVersion = "Ver1.0";

    public GameObject[] myArray;
    public Material[] MaterialsList = new Material[4];
    public Material[] CursorColor_List = new Material[4];


    public 

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        InputField = GetComponent<InputField>();
        LoadImage.SetActive(true);
        // I_game_Manager_Script=I_game_manager.GetComponent<I_game_manager>();
        // ReadyButton_Script=ReadyButton.GetComponent<ReadyButton>();

    }
    private void Update()
    {

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




    public override void OnJoinedRoom()//部屋に入れた時の処理
    {
        StartCoroutine(OnJoinedRoom_Coroutine());
    }

    public IEnumerator OnJoinedRoom_Coroutine()///部屋に入れた時の処理　コルーチン
    {

        MaterialChange();
     
        var position = new Vector3(0.28f, -3.37f, -0.73f);
        GameObject Player = PhotonNetwork.Instantiate("Player3D", position, Quaternion.identity);//プレイヤーの生成

        GameObject ItemList_UGI = PhotonNetwork.Instantiate("ItemBlock_List", position, Quaternion.identity);//プレイヤーのアイテムリストの作成
        ReadyButton_Script.JoinedRoom_Jointed();
        GameObject PlayerCarsol = PhotonNetwork.Instantiate("Cursor", position, Quaternion.identity);//プレイヤーの生成


        
        IconChange();

        Player.GetComponent<I_Player_3D>().DiceButton.GetComponent<Button>().onClick.AddListener(Player.GetComponent<I_Player_3D>().DicePush);
 

        hashtable["PlayerSetUP"]=true;
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);//変更したカスタムプロパティの更新

        yield return new WaitForSeconds(0.4f);
        string Log = I_game_Manager_Script.PlayerColouradd(PhotonNetwork.NickName)+"が入室しました。";
        I_game_Manager_Script.Log_connection(Log);

        Playerlist_Update();
        
        LoadImage.SetActive(false);
        
        GameObject.Find("I_game_manager").GetComponent<Guide>().rady_BottonStart();
        GameObject.Find("I_game_manager").GetComponent<Guide>().option_BottonStart();

        yield break;

    }

    

    public void MaterialChange()//プレイヤーのマテリアルを変更する
    {
        
        foreach (var Materials in MaterialsList)//マテリアルの内容を順番に格納
        {
            bool juje = false;
            foreach (var PList in PhotonNetwork.PlayerList)//プレイヤーリストの内容を順番に格納
            {
                if (PList.CustomProperties.ContainsValue(System.Array.IndexOf(MaterialsList, Materials)))//そのマテリアルをプロパティに持つプレイヤーがいた場合
                {
                    juje=true;
                }
            }

            if (juje == false)
            {
              
                hashtable["PlayerNumMaterial"]=System. Array.IndexOf(MaterialsList, Materials);
                PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);//変更したカスタムプロパティの更新
                break;
            }


           
        }

    }

    public void IconChange()//プレイヤーのアイコンのプロパティを変更する
    {



        for (int loop=0;loop<4;loop++) {
            bool juje = false;
            foreach (var PList in PhotonNetwork.PlayerList)//プレイヤーリストの内容を順番に格納
            {
                if (PList.CustomProperties.ContainsValue(PList.CustomProperties["iconNum"]))//そのマテリアルをプロパティに持つプレイヤーがいた場合
                {
                    if ((int)PList.CustomProperties["iconNum"]==loop) {
                        juje=true;

                    }
                }
            }

            if (juje == false)
            {

                hashtable["iconNum"]=loop;
                PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);//変更したカスタムプロパティの更新

                break;
            }



        }

    }







    public void ItemList_UGI_Transform(GameObject ItemList)
    {
        ItemList_Moment=ItemList;
        photonView.RPC(nameof(ItemList_UGI_Transform_RPC), RpcTarget.AllViaServer);
    }

    [PunRPC]
    public void ItemList_UGI_Transform_RPC()
    {
        ItemList_Moment.transform.parent=ItemListBox.transform;
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



    public void Leave_the_room()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player player)//プレイヤーが抜けたときの処理
    {
        StartCoroutine(OnPlayerLeftRoom_Coroutine());
        string Log = "プレイヤー："+ player.NickName+"が退出しました。";
        I_game_Manager_Script.Log_Mine(Log);
    }

    public IEnumerator OnPlayerLeftRoom_Coroutine()//プレイヤーが抜けたときの処理 コルーチン
    {
        ReadyButton_Script.PlayerLeftRoom_Jointed();
        yield return new WaitForSeconds(0.4f);
        Playerlist_Update();//プレイヤーのオブジェクト格納用/初期位置への移動も含む
       

        yield break;
    }




    public override void OnPlayerEnteredRoom(Player newPlayer)//自身がルームに入ったとき
    {
        StartCoroutine(OnPlayerEnteredRoom_Coroutine());

    }

    public IEnumerator OnPlayerEnteredRoom_Coroutine()//他者がルームに入ったときコルーチン
    {



        yield return new WaitForSeconds(0.6f);
     
        yield break;
    }




    public void Playerlist_Update()//プレイヤーのオブジェクト格納用/初期位置への移動も含む
    {
  
        GameObject[] Players_spot = GameObject.FindGameObjectsWithTag("Player");//プレイヤーオブジェクトの一時保存場所　タグで軒並みとる
        GameObject[] ItemListSpot = GameObject.FindGameObjectsWithTag("ItemList_UGI");//プレイヤーオブジェクトの一時保存場所　タグで軒並みとる

     

        I_game_Manager_Script.Player.Clear();


        int loop = 0;//アイテムリストの初期値
        foreach (var PList in PhotonNetwork.PlayerList)//プレイヤーリストの内容を順番に格納
        {
            
            foreach (GameObject obj in Players_spot)//プレイヤーリストの中身と、一時保存したプレイヤーオブジェクトを突き合わせる
            {
                if (PList.ActorNumber==obj.GetComponent<PhotonView>().CreatorActorNr) //リストのプレイヤーのIDとオブジェクトの作成者のADを比較
                {
                    
                    I_game_Manager_Script.Player.Add(obj);//この処理で、プレイヤーリストの順番どおりにプレイヤーオブジェクトを保存できる　順番を変えられるようにしたいなら変更の余地あり
                    I_game_Manager_Script.Player_setting(loop);//プレイ矢―を所定の位置に移動
                    obj.GetComponent<I_Player_3D>().PlayerNumber=loop;

                   
                }
            }

            foreach (GameObject obj in ItemListSpot)//プレイヤーリストの中身と、一時保存したプレイヤーオブジェクトを突き合わせる
            {
                obj.transform.parent=ItemListBox.transform;
                string name= obj.GetComponent<PhotonView>().Owner.NickName;
          
                obj.GetComponent<ItemBlock_List_Script>().Namedisplay(name);
                if (PList.ActorNumber==obj.GetComponent<PhotonView>().CreatorActorNr) //リストのプレイヤーのIDとオブジェクトの作成者のADを比較
                {
            

                    foreach (GameObject Player in Players_spot)//対応するプレイヤーにプレイヤーリストを突っ込む
                    {
                        if (Player.GetComponent<PhotonView>().CreatorActorNr==obj.GetComponent<PhotonView>().CreatorActorNr) //リストのプレイヤーのIDとオブジェクトの作成者のADを比較
                        {
                            Player.GetComponent<I_Player_3D>().ItemBlock=obj;//ブロックリストを格納
                 
                            obj.GetComponent<ItemBlock_List_Script>().Player=Player;
                            I_game_Manager_Script.ItemList_setting(obj, loop);//プレイ矢―を所定の位置に移動

                           

                        }
                    }

                }
              
            }
            loop++;
            I_game_Manager_Script.joining_Player = PhotonNetwork.PlayerList.Length;
            if (I_game_Manager_Script.Player.Count!=loop)
            {
                Debug.LogError("問題発生。部屋を入りなおして下さい");
            }
        }

        GameObject[] Cursors = GameObject.FindGameObjectsWithTag("cursor");
       
        foreach (GameObject Cursor in Cursors)//対応するプレイヤーにプレイヤーリストを突っ込む
        {
            Cursor.transform.parent=I_game_Manager_Script.cursors.transform;

            Cursor.GetComponent<Mouse_Cursor>().RotateChange();
        }


    }



    public string Playername(GameObject Player)//プレイヤ―の名前をとる
    {
        string name="";
        foreach (var PList in PhotonNetwork.PlayerList)//プレイヤーリストの内容を順番に格納
        {
            if (PList.ActorNumber== Player.GetComponent<PhotonView>().CreatorActorNr) //リストのプレイヤーのIDとオブジェクトの作成者のADを比較
            {
                name=PList.NickName;
            }
        }
        return name;
    }


    public override void OnPlayerPropertiesUpdate(Player player, Hashtable propertiesThatChanged)
    {
       
        foreach (var prop in propertiesThatChanged)
        {
           
            if ((string)prop.Key=="PlayerNumMaterial")//変更されたプレイヤープロパティがマテリアルに関するものだった場合
            {

                Playerlist_Material_Update();

            }
            if ((string)prop.Key=="iconNum")//変更されたプレイヤープロパティがマテリアルに関するものだった場合
            {

               Icon_Update();

            }
            if ((string) prop.Key=="PlayerSetUP")
            {
                Playerlist_Update();//プレイヤーのオブジェクト格納用/初期位置への移動も含む
                Playerlist_Material_Update();
                Icon_Update();
            }

        }
    }

    public void Playerlist_Material_Update()
    {
       
        GameObject[] Players_spot = GameObject.FindGameObjectsWithTag("Player");//プレイヤーオブジェクトの一時保存場所　タグで軒並みとる
        GameObject[] Cursors = GameObject.FindGameObjectsWithTag("cursor");
        foreach (var PList in PhotonNetwork.PlayerList)//プレイヤーリストの内容を順番に格納
        {
           

            foreach (GameObject obj in Players_spot)//プレイヤーリストの中身と、一時保存したプレイヤーオブジェクトを突き合わせる
            {
              
                if (PList.ActorNumber==obj.GetComponent<PhotonView>().CreatorActorNr) //リストのプレイヤーのIDとオブジェクトの作成者のADを比較
                {

                    Transform children = obj.GetComponentInChildren<Transform>();

                    foreach (Transform ob in children)
                    {
          
                        ob.GetComponent<Renderer>().material=MaterialsList[(int)PList.CustomProperties["PlayerNumMaterial"]];
                    }

                   
                }
            }


            foreach (GameObject obj in Cursors)//プレイヤーリストの中身と、一時保存したプレイヤーオブジェクトを突き合わせる
            {

                if (PList.ActorNumber==obj.GetComponent<PhotonView>().CreatorActorNr) //リストのプレイヤーのIDとオブジェクトの作成者のADを比較
                {

                  
                        obj.GetComponent<Image>().material=CursorColor_List[(int)PList.CustomProperties["PlayerNumMaterial"]];
                    


                }
            }











        }
    }
    public void Icon_Update()
    {
        GameObject[] ItemListSpot = GameObject.FindGameObjectsWithTag("ItemList_UGI");//プレイヤーオブジェクトの一時保存場所　タグで軒並みとる
      
        foreach (var PList in PhotonNetwork.PlayerList)//プレイヤーリストの内容を順番に格納
        {


            foreach (GameObject obj in ItemListSpot)//プレイヤーリストの中身と、一時保存したプレイヤーオブジェクトを突き合わせる
            {

                if (PList.ActorNumber==obj.GetComponent<PhotonView>().CreatorActorNr) //リストのプレイヤーのIDとオブジェクトの作成者のADを比較
                {

                    var Image = obj.GetComponent<ItemBlock_List_Script>().IcobImage.GetComponent<Image>();
                    int num = (int)PList.CustomProperties["iconNum"];
                    Image.sprite= I_game_Manager_Script.IconSprits.Icons[num];//マテリアルと同じ手順でアイテムリストの画像を変更
                    Debug.Log("AAAAAAAAAAAA"+num);
                }
            }
        }
    }
    public void Voidlobby()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveLobby();
        OnLeftLobby();
    }

    public override  void OnLeftLobby()
    {
      
        SceneManagerObj.GetComponent<SceneManagaer>().TransitionToMain();
    }




    public void VoidRoom()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.LeaveRoom();
        
    }


    public override void OnLeftRoom()
    {
        Cursor.visible = true;
        SceneManagerObj.GetComponent<SceneManagaer>().TransitionToMain();
    }
}
