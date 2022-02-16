using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Threading.Tasks;


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












    public void ItemInfoGet(string Item)
    {
        Debug.Log(HabItem[0]);

        //  Debug.Log(Item+ItemDectionari.ItemDictionary[Item]);

       // Play.ItemDectionari.DectionariyInfo(Item);
        Debug.Log(DictionaryManager.ItemDictionary[Item][0]);


    }










    public async void  SetPlayernumShorten()
    {

        await Task.Delay(50);


        if (photonView.IsOwnerActive == false)
        {
          
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

                dropdown.options.Add(new Dropdown.OptionData { text = ""+PlayerNameVew });
                dropdown.RefreshShownValue();


                if (photonView.IsMine) {
                    Debug.Log("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
                    Botton = GameObject.Find("traffic_lights").GetComponent<Button>();
                    Botton.onClick.AddListener(() => Itemobtain("信号機"));
                }
            }
            loop++;


        }
    }

    public override  void OnPlayerLeftRoom(Player otherPlayer)
    {
        for (int loop=1;loop<5;loop++) {
            GameObject.Find("Dropdown:Player" + loop).GetComponent<Dropdown>().ClearOptions();
            GameObject.Find("Dropdown:Player" + loop).GetComponent<Dropdown>().RefreshShownValue();

        }
       
        SetPlayernumShorten();
    }


    public void Itemobtain(string Item)
    {
        
        photonView.RPC(nameof(ItemobtainToRPC), RpcTarget.All, Item);
    }



    [PunRPC]
    public void ItemobtainToRPC(string Item)
    {
        HabItem.Add(Item);
        dropdown.options.Add(new Dropdown.OptionData { text = Item + DictionaryManager.ItemDictionary[Item][0] + "P" });
        dropdown.RefreshShownValue();
    }



}


