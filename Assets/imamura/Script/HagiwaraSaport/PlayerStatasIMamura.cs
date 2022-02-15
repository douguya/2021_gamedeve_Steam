using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerStatasIMamura : MonoBehaviourPunCallbacks
{
 
    public List<string> HabItem ;//持っているアイテム
    private int Goalcount = 0; //ゴールした数
    private int PX, PY;//プレイヤーのマス座標
    public GameObject Play;

    
            



    [SerializeField]
    private Dropdown dropdown;
    public int PlayerIdVew;
    public string PlayerNameVew;
    public int HowPlayer;
    public Button Botton;
    
    private void Awake()
    {
       
    }


    void Start()
    {
       
        PlayerIdVew = photonView.OwnerActorNr;
        PlayerNameVew = photonView.Owner.NickName;
        SetPlayernumShorten();
        if (photonView.IsRoomView==true)
        {
            Debug.Log("あああああああああああああああ");
        }

    }


    void Update()
    {
      


    }


    public void Itemobtain(string Item)
    {
        HabItem.Add(Item);
        dropdown.options.Add(new Dropdown.OptionData { text = Item + DictionaryManager.ItemDictionary[Item][0] + "P" });
        dropdown.RefreshShownValue();
    }

    
    public void ItemInfoGet(string Item)
    {
        Debug.Log(HabItem[0]);

        //  Debug.Log(Item+ItemDectionari.ItemDictionary[Item]);

       // Play.ItemDectionari.DectionariyInfo(Item);
        Debug.Log(DictionaryManager.ItemDictionary[Item][0]);


    }










    public  void SetPlayernumShorten()
    {
        if (photonView.IsOwnerActive == false)
        {
            Debug.Log("ASASA");
          //  Destroy(this.gameObject);
        }

        int loop = 1;
        foreach (var p in PhotonNetwork.PlayerList)
        {
            if (photonView.CreatorActorNr == p.ActorNumber)
            {
                dropdown.ClearOptions();
                dropdown =GameObject.Find("Dropdown:Player"+loop).GetComponent<Dropdown>();
                dropdown.ClearOptions();
                dropdown.options.Add(new Dropdown.OptionData { text = PlayerNameVew });
                dropdown.RefreshShownValue();


                if (photonView.IsMine) {
                    Botton = GameObject.Find("PurehabTest_Button (2)").GetComponent<Button>();
                    Botton.onClick.AddListener(() => Itemobtain("信号機"));
                }
            }
            loop++;


        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
       SetPlayernumShorten();
    }





}


