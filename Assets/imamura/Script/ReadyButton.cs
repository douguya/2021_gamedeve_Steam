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
    bool Ready = false;　　　　//準備状態
    GameObject ReadyBotton;　　//準備完了ボタンへのアクセス
    public GameObject GameStart;//ゲームスタートボタンの実装
    public Text ReadyText;　　　//準備完了ボタンのテキスト
    public string Ready_Txt;　　//準備完了ボタンのテキスト代入用文字列　見やすくする用
    public int ReadyPlayerNum = 0;//準備完了したプレイヤーの人数
    public Hashtable hashtable = new Hashtable();//カスタムプロパティのリスト

    void Start()
    {
        // var properties = new ExitGames.Client.Photon.Hashtable();

        //  PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void RedayChange()//準備状態の変遷用
    {
        GameObject.Find("I_game_manager").GetComponent<Guide>().rady_BottonFinish();
        if (Ready == false)//準備を完了していないときに起動したら
        {
            hashtable["ReadyPlayerNum"] = true;//準備を完了する:カスタムプロパティ
            Ready = true;//準備を完了する
        }
        else if (Ready == true)//準備を完了しているときに起動したら
        {
            hashtable["ReadyPlayerNum"] = false;//準備しなおす：カスタムプロパティ
            Ready = false;//準備をし直す
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);//変更したカスタムプロパティの更新
    }



    //カスタムプロパィが更新された際のコールバック
     public override void OnPlayerPropertiesUpdate(Player player, Hashtable propertiesThatChanged)
    {

        foreach (var prop in propertiesThatChanged)
        {

            if ((string)prop.Key=="ReadyPlayerNum")//変更されたプレイヤープロパティがマテリアルに関するものだった場合
            {

    
              
                    int loop = 0;

                    foreach (var p in PhotonNetwork.PlayerList)//プレイヤー全員のカスタムプロパティ：準備状態の集計
                    {
                        if ((bool)p.CustomProperties["ReadyPlayerNum"] == true)//ｐ番目のプレイヤーの準備が完了しているなら
                        {
                            loop++;//人数をカウント
                        }
                    }

                    //テキスト　=準備完了人数/プレイヤー全体の人数
                    Ready_Txt = loop + "/ " + PhotonNetwork.PlayerList.Length;//準備完了テキスト：見やすくするためにここでまとめる

                    if (Ready == false)//当プレイヤーの準備ができていない
                    {
                        ReadyText.text = "準備を完了する" + Ready_Txt;//準備完了を待つテキストへ変更
                    }
                    else if (Ready == true) //当プレイヤーの準備ができている
                    {
                        ReadyText.text = "準備に戻る" + Ready_Txt;//再度準備に戻るテキストへ変更
                    }

                    if (PhotonNetwork.PlayerList.Length == loop) //準備完了人数と プレイヤー全体の人数が同じとき
                    {
                        if (PhotonNetwork.LocalPlayer.IsMasterClient)//そのうえでプレイヤーがマスタークライアントである場合
                    {
                          if (PhotonNetwork.CurrentRoom.IsOpen == true)//ゲーム中でないなら
                          {
                            GameStart.SetActive(true);//ゲームスタートボタンの出現
                          }
                        }
                    }
                    else
                    {
                        GameStart.SetActive(false);//ゲームスタートボタンの消失
                    }

                

            }

          

        }
     
       
        

    }


    
    public void PlayerLeftRoom_Jointed()//他のプレイヤーが退出した場合
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);//上下どっちかいらない　時間があったら試す
        OnRoomPropertiesUpdate(hashtable);//カスタムプロパティを更新（準備完了状況の反映）
    }

    public void JoinedRoom_Jointed()//自身がルームに入ったとき
    {
        hashtable["ReadyPlayerNum"] = false;//カスタムプロパティのセッティング　初手なのでfalse
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);//更新
       // OnRoomPropertiesUpdate(hashtable);////カスタムプロパティを更新（準備完了状況の反映）
    }


}




