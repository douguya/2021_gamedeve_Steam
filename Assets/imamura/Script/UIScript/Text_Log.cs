
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
    public GameObject TextMask;//テキストのオブジェクト
    public GameObject PullImage;
    public InputField InputField;     //名前入力欄
    public Text TestText;

    public float Scloll;//スクロールの移動量
    public float Scloll_Coefficient;//スクロールの係数(フォントサイズの増大に比例して大きくなる　計算しきれなかったため入力)
    private int SclollCount = 0;//スクロール回数のカウント
    private bool Text_View = false;
    private float InitialY_Value;//
    private float FastBorad;
    // Start is called before the first frame update
    void Start()
    {
        var Text = TextObj.GetComponent<Text>();
        Scloll=Text.fontSize*Text.lineSpacing* Scloll_Coefficient;//スクロール量の算出
        InitialY_Value=TextObj.GetComponent<RectTransform>().anchoredPosition.y;//初期位置を出す
        FastBorad= nowText.GetComponent<RectTransform>().anchoredPosition.y;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnScroll()
    {
        var BlockTransform = TextObj.GetComponent<RectTransform>().anchoredPosition;//ブロックの場所を取得
        var Mousewheel = Input.GetAxis("Mouse ScrollWheel");//マウスホイール値の保存
        var TextDami = TextObj.GetComponent<Text>().text;
        var num = TextDami.Count(f => f == '\n')+1;//改行文字コードの数から行数を算出


        var Large_SclollCount = Screen.height/Scloll;



        if (Mousewheel!=0)//マウスホイールが行われた場合
        {



            if (Mousewheel>0)//上向きホイール
            {
                if (Text_View==false)
                {//テキストボードが非表示(標準)の時
                    if (SclollCount< num+-1)
                    {
                        SclollCount++;
                        BlockTransform.y-=(Scloll);
                        TextObj.GetComponent<RectTransform>().anchoredPosition=BlockTransform;
                    }
                }
                else
                {
                    if (SclollCount< num- Large_SclollCount)
                    {
                        SclollCount++;
                        BlockTransform.y-=(Scloll);
                        TextObj.GetComponent<RectTransform>().anchoredPosition=BlockTransform;
                    }
                }


            }
            if (Mousewheel<0)//下向きホイール
            {

                if (SclollCount>0)
                {
                    SclollCount--;
                    BlockTransform.y+=(Scloll);
                    TextObj.GetComponent<RectTransform>().anchoredPosition=BlockTransform;
                }


            }

            Debug.Log(" SclollCount"+SclollCount);
        }



    }
    public void TextBoard_View()
    {
        PullImage.GetComponent<RectTransform>().Rotate(0, 0, 180f);
        Text_View =!Text_View;
        TextBoard.SetActive(Text_View);
        TextMask.GetComponent<Mask>().enabled=!Text_View;
        //===========位置を初期値へ戻す
        var BlockTransform = TextObj.GetComponent<RectTransform>().anchoredPosition;//ブロックの場所を取得
        BlockTransform.y=InitialY_Value;
        TextObj.GetComponent<RectTransform>().anchoredPosition= BlockTransform;
        SclollCount=0;
        //===========================
    }

    public void textadd(string LogText)//日本語50文字
    {

        Text texts = TextObj.GetComponent<Text>();//ログのテキストを参照で取得
        NowTextBord(LogText);

        texts.text=texts.text+"\n"+LogText;//ログのテキスト内容に追加
    }



    public void NowTextBord(string LogText)//日本語50文字
    {
        TestText.text=LogText;
        var BordSize = nowText.GetComponent<RectTransform>().sizeDelta;

        float SizeCount = Mathf.CeilToInt(TestText.preferredWidth/BordSize.x);

        BordSize.y = Scloll*SizeCount;

        nowText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BordSize.y);

        var nowPosi = nowText.GetComponent<RectTransform>().anchoredPosition;
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"+nowPosi);
        nowPosi.y= FastBorad+(9.73f*(SizeCount-1));
        nowText.GetComponent<RectTransform>().anchoredPosition= nowPosi;

    }








    public void Direct_Log_InputField()//全体にログを送る
    {
        if (Input.GetKey(KeyCode.Return))
        {



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
}