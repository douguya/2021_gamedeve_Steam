using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Threading.Tasks;


public class PlayerStatasIMamura : MonoBehaviourPunCallbacks
{

    public List<string> HabItem;//持っているアイテム
    private int Goalcount = 0;  //ゴールした数
    private int PX, PY;         //プレイヤーのマス座標
    public GameObject Play;     //プレイヤーのゲームオブジェクト


    [SerializeField]
    private Dropdown dropdown;　//アイテム一覧へのアクセス用
    public int PlayerIdVew;　　 //プレイヤーのID
    public string PlayerNameVew;//プレイヤーの名前
    public int HowPlayer;       //何人プレイヤーがいるかの閲覧用
    public Button Botton;　　　 //動作テスト用ボタンへのアクセス用

    void Start()
    {
        PlayerIdVew = photonView.OwnerActorNr;　　//プレイヤーのIDの同期
        PlayerNameVew = photonView.Owner.NickName;//プレイヤーの名前の同期
        SetPlayernumShorten();                    //アイテムリストUIとプレイヤーの同期[

        this.name = photonView.Owner.NickName;

       


    }

    public async void SetPlayernumShorten()//アイテムリストUIの同期
    {
        await Task.Delay(50);//一気に複数のプレイヤーとアイテムリストの同期をするための一時停止






        int loop = 1;//アイテムリストの初期値
        foreach (var PList in PhotonNetwork.PlayerList)//プレイヤーリストの内容を順番に格納
        {
            if (photonView.CreatorActorNr == PList.ActorNumber)//自分の作成者のIDがPListのIDとイコールなら
            {
                dropdown.ClearOptions();//移行前のリスト消去
                dropdown = GameObject.Find("Dropdown:Player" + loop).GetComponent<Dropdown>();//プレイヤーの順番に対応したアイテムリストUIとの同期
                dropdown.ClearOptions();//同期したアイテムリストの初期化

                dropdown.options.Add(new Dropdown.OptionData { text = "" + PlayerNameVew });//アイテムリストのラベル付け
                dropdown.RefreshShownValue();//アイテムリストUIの更新

                //テスト用ボタンのセッティング
                if (photonView.IsMine)//PListがプレイヤーのものなら
                {
                    Botton = GameObject.Find("traffic_lights").GetComponent<Button>();//テスト用ボタンへのアクセス用
                    Botton.onClick.AddListener(() => Itemobtain("信号機"));//テスト用ボタンへの関数追加
                }
            }
            loop++;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)//他のプレイヤーがいなくなった時のコールバック
    {
        //アイテムリストUIの更新のための全体初期化
        for (int loop = 1; loop < 5; loop++)
        {
            GameObject.Find("Dropdown:Player" + loop).GetComponent<Dropdown>().ClearOptions();//削除
            GameObject.Find("Dropdown:Player" + loop).GetComponent<Dropdown>().RefreshShownValue();//更新

        }

        SetPlayernumShorten();//改めてのアイテムリストUIの同期
    }


    public void Itemobtain(string Item)//アイテムを手に入れた場合の関数を呼び出す
    {
        photonView.RPC(nameof(ItemobtainToRPC), RpcTarget.All, Item);
    }

    [PunRPC]
    public void ItemobtainToRPC(string Item)//アイテムを手に入れた場合の関数
    {
        HabItem.Add(Item);//Itemのリストへの追加
        dropdown.options.Add(new Dropdown.OptionData { text = Item + DictionaryManager.ItemDictionary[Item][0] + "P" });//アイテムとそのポイントをアイテムリストUIに追加
        dropdown.RefreshShownValue();//アイテムリストUIの更新
    }

}


