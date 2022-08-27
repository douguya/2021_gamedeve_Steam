using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


public class Text_Log : MonoBehaviour
{
    public GameObject TextObj;//テキストのオブジェクト
    public GameObject TextBoard;//テキストのオブジェクト
    public GameObject TextMask;//テキストのオブジェクト
   public float Scloll;//スクロールの移動量
    public float Scloll_Coefficient;//スクロールの係数(フォントサイズの増大に比例して大きくなる　計算しきれなかったため入力)
    private int SclollCount=0;//スクロール回数のカウント
    private bool Text_View = false;
    private float InitialY_Value;//
    // Start is called before the first frame update
    void Start()
    {
        var Text = TextObj.GetComponent<Text>();
        Scloll=Text.fontSize*Text.lineSpacing* Scloll_Coefficient;//スクロール量の算出
        InitialY_Value=TextObj.GetComponent<RectTransform>().anchoredPosition.y;//初期位置を出す

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnScroll()
    {
        var BlockTransform = TextObj.GetComponent<RectTransform>().anchoredPosition;//ブロックの場所を取得
        var  Mousewheel =Input.GetAxis("Mouse ScrollWheel");//マウスホイール値の保存
        var TextDami = TextObj.GetComponent<Text>().text;
        var num = TextDami.Count(f => f == '\n')+1;//改行文字コードの数から行数を算出


        var Large_SclollCount = Screen.height/Scloll;



        if (Mousewheel!=0)//マウスホイールが行われた場合
        {

     
          
            if (Mousewheel>0)//上向きホイール
            {
                if (Text_View==false) {//テキストボードが非表示(標準)の時
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
        Text_View=!Text_View;
        TextBoard.SetActive(Text_View);
        TextMask.GetComponent<Mask>().enabled=!Text_View;
        //===========位置を初期値へ戻す
        var BlockTransform = TextObj.GetComponent<RectTransform>().anchoredPosition;//ブロックの場所を取得
        BlockTransform.y=InitialY_Value;
        TextObj.GetComponent<RectTransform>().anchoredPosition= BlockTransform;
        SclollCount=0;
        //===========================
    }
}
