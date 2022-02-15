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

    public Text ReadyText;
    int ReadyPlayerNum = 0;

    private static Hashtable hashtable = new Hashtable();

    void Start()
    {
       // var properties = new ExitGames.Client.Photon.Hashtable();
        hashtable["ReadyPlayerNum"] = ReadyPlayerNum;
       // PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }

    // Update is called once per frame
    void Update()
    {
      //  ReadyText.text =(string) PhotonNetwork.CurrentRoom.CustomProperties["ReadyPlayerNum"];
    }



    public void RedayChange()
    {

        if (Ready == false)
        {
            ReadyText.text = "èÄîıÇ…ñﬂÇÈ";
            Ready = true;
        }
        else if (Ready == true)
        {
            ReadyText.text = "èÄîıÇäÆóπÇ∑ÇÈ";
            Ready = false;
        }

   

    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {




    }


}

