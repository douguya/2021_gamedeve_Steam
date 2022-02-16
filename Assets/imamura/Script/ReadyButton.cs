using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;





public class ReadyButton : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [SerializeField]
    bool Ready = false;
    GameObject ReadyBotton;
    public GameObject GameStart;
    public Text ReadyText;
    public string Ready_Txt;
    public int ReadyPlayerNum = 0;
    public  Hashtable hashtable = new Hashtable();

    void Start()
    {
        // var properties = new ExitGames.Client.Photon.Hashtable();
  
        //  PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("OOOOOOOOOOOOOOOOOO");

        //  ReadyText.text =(string) PhotonNetwork.CurrentRoom.CustomProperties["ReadyPlayerNum"];
    }



    public void RedayChange()
    {
        // int ReadyPlayerNum = (int)PhotonNetwork.CurrentRoom.CustomProperties["ReadyPlayerNum"];




        if (Ready == false)
        {
            hashtable["ReadyPlayerNum"] = true;
          
            Ready = true;
        }
        else if (Ready == true)
        {
            hashtable["ReadyPlayerNum"] = false;
    
            Ready = false;
        }


       



        Debug.Log("999999999999999999999999"+PhotonNetwork.LocalPlayer);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
     
    }




    public override void OnPlayerPropertiesUpdate (Player player, Hashtable propertiesThatChanged)
    {

        int loop = 0;
        foreach (var p in PhotonNetwork.PlayerList)
        {
            
           // Debug.Log("AAAAAAAAAA" + hashtable["ReadyPlayerNum"]);
            if ((bool)p.CustomProperties["ReadyPlayerNum"] == true)
            {
               Debug.Log("BBBBBBBBBB"+ p+ propertiesThatChanged["ReadyPlayerNum"]);
                loop++; 
            }
           
        }


        Ready_Txt = loop + "/ " + PhotonNetwork.PlayerList.Length;

        if (Ready == false)
        {
            ReadyText.text = "準備を完了する" + Ready_Txt;
        }
        else if (Ready == true)
        {
            ReadyText.text = "準備に戻る" + Ready_Txt;
        }

        if (PhotonNetwork.PlayerList.Length == loop)
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                GameStart.SetActive(true);
            }
        }
        else
        {
            GameStart.SetActive(false);
        }



    }





    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        OnRoomPropertiesUpdate(hashtable);
    }
    public override void OnJoinedRoom()
    {
      
        hashtable["ReadyPlayerNum"] = false;
        Debug.Log(hashtable["ReadyPlayerNum"]);
      
         Debug.Log(hashtable["ReadyPlayerNum"]);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        OnRoomPropertiesUpdate(hashtable);
   
        Debug.Log("へうあにはいったよおおおおおおおお");


    }

    public void GameStartn()
    {

        photonView.RPC(nameof(GamestartToRPC), RpcTarget.All);


    }


    [PunRPC]
    public void GamestartToRPC()
    {
        Debug.Log("ゲームスタート＝＝＝＝＝＝＝＝＝＝＝＝＝");

        ReadyText.text = "バーカバーカ";

    }




}




