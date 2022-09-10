
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using Photon.Pun;

public class Text_Log : MonoBehaviourPunCallbacks
{
    public GameObject TextObj;//テキストのオブジェクト
    public GameObject TextBoard;//テキストのオブジェクト
    public GameObject nowText;//テキストのオブジェクト
    public I_game_manager Game_Manager;
   
    public InputField InputField;     //名前入力欄
    public Text TestText;

    public float Scloll;//スクロールの移動量
    public float Scloll_Coefficient;//スクロールの係数(フォントサイズの増大に比例して大きくなる　計算しきれなかったため入力)
    private int SclollCount = 0;//スクロール回数のカウント
    private bool Text_View = false;
    private Vector2 Initial_Value;//
    private float FastBorad;

    

    public int textVar = 0;

    public int Large_Sclol = 0;
    public int FallLineCount = 0;

    public string Password;
    // Start is called before the first frame update
    void Start()
    {
        var Text = TextObj.GetComponent<Text>();
        Scloll=Text.fontSize*Text.lineSpacing* Scloll_Coefficient;//スクロール量の算出
        Initial_Value=TextObj.GetComponent<RectTransform>().anchoredPosition;//初期位置を出す
        FastBorad= nowText.GetComponent<RectTransform>().anchoredPosition.y;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnScroll()
    {
        var BlockTransform = TextObj.GetComponent<RectTransform>().anchoredPosition;//ブロックの場所を取得
        var nowPosi = nowText.GetComponent<RectTransform>().anchoredPosition;
        var Mousewheel = Input.GetAxis("Mouse ScrollWheel");//マウスホイール値の保存
        var TextDami = TextObj.GetComponent<Text>().text;
        var num = TextDami.Count(f => f == '\n')+1+FallLineCount;//改行文字コードの数から行数を算出
        textVar=num;
      

        var TextBordSize = TextBoard.GetComponent<RectTransform>().sizeDelta;
        int Large_SclollCount =33;

        Large_Sclol=Large_SclollCount;

        if (Mousewheel!=0)//マウスホイールが行われた場合
        {



            if (Mousewheel>0)//上向きホイール
            {
               
                    if (SclollCount< textVar-Large_SclollCount)
                    {
                        SclollCount++;
                        BlockTransform.y-=(Scloll);
                        nowPosi.y-=Scloll;
                        TextObj.GetComponent<RectTransform>().anchoredPosition=BlockTransform;

                        nowText.GetComponent<RectTransform>().anchoredPosition= nowPosi;
                    }
                


            }
            if (Mousewheel<0)//下向きホイール
            {

                if (SclollCount>0)
                {
                    SclollCount--;
                    BlockTransform.y+=(Scloll);
                    TextObj.GetComponent<RectTransform>().anchoredPosition=BlockTransform;
                    nowPosi.y+=Scloll;
                    nowText.GetComponent<RectTransform>().anchoredPosition= nowPosi;
                }


            }

            Debug.Log(" SclollCount"+SclollCount);
        }




    }
   

    public void textadd(string LogText)//日本語50文字
    {
        SclollCount=0;

        TextObj.GetComponent<RectTransform>().anchoredPosition= Initial_Value;

        Text texts = TextObj.GetComponent<Text>();//ログのテキストを参照で取得
        NowTextBord(LogText);

        texts.text=texts.text+"\n"+LogText;//ログのテキスト内容に追加
    }






    public void NowTextBord(string LogText)//日本語50文字
    {
        TestText.text=LogText;
        var BordSize = nowText.GetComponent<RectTransform>().sizeDelta;

       int SizeCount = Mathf.CeilToInt(TestText.preferredWidth/BordSize.x);
        if (SizeCount>1)
        {
            FallLineCount+=(SizeCount-1);
        }

        BordSize.y = Scloll*SizeCount;

        nowText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BordSize.y);

        var nowPosi = nowText.GetComponent<RectTransform>().anchoredPosition;

        nowPosi.y= FastBorad+(9.73f*(SizeCount-1));
        nowText.GetComponent<RectTransform>().anchoredPosition= nowPosi;

    }








    public void Direct_Log_InputField()//チャットに入力
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (InputField.GetComponent<InputField>().text==Password)
            {
                Game_Manager.ConnectGameFinish();
            }


            var name = PlayerColouradd(PhotonNetwork.NickName);
            string Chat = name+":" +InputField.GetComponent<InputField>().text;

            if (Chat!="")
            {

                Debug.Log(name);

                //入力フォームのテキストを空にする
                // textadd(Chat);
                photonView.RPC(nameof(Direct_Log_RPC__InputField), RpcTarget.AllViaServer, Chat);
                InputField.text = "";
            }
        }

    }
    public string PlayerColouradd(string Chat)
    {
        var ColourNum = PhotonNetwork.LocalPlayer.CustomProperties["PlayerNumMaterial"];
        var Text = Chat;
        switch (ColourNum)
        {

            case 0:
                Text="<color=red>"+Text+"</color>";

                break;
            case 1:
                Text="<color=blue>"+Text+"</color>";

                break;
            case 2:
                Text="<color=orange>"+Text+"</color>";

                break;
            case 3:
                Text="<color=lime>"+Text+"</color>";

                break;

        }
        return Text;
    }






    [PunRPC]
    public void Direct_Log_RPC__InputField(string LogText)//全体にログを送る
    {

        textadd(LogText);
    }
    public string SistemrColouradd()
    {

        var system = "<color=purple>"+"システム:"+"</color>";


        return system;
    }


}