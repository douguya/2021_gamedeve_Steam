using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ResultManager : MonoBehaviourPunCallbacks
{
    //----------------------------------変数----------------------------------
    [SerializeField]
    int playersnum;         //プレイヤーの人数
    [SerializeField]
    Text textItems;           //コピー元のテキスト
    [SerializeField]
    Text textPoints;           //コピー元のテキスト
    [SerializeField]
    Text[] total;           //トータルスコア
    [SerializeField]
    Transform Canvas;       //Canvas
    [SerializeField]
    Transform[] ScoreBackGround;      //TextUIのコピーの親にするオブジェクト(以下SBG)
    [SerializeField]
    GameObject[] PlayerBackGround;    //プレイヤーの情報を出す場所(以下PBG)
    [SerializeField]
    Button display;
    [SerializeField]
    GameObject[] ScrollBar;
    [SerializeField]
    float interval = -30.0f;        //PBGで生成されるテキストボックスの間隔
    [SerializeField]
    GameObject[] players;             //I_Player_3Dを持ってるオブジェクト
    [SerializeField]
    List<Anniversary_Item>[] OriginalItem;    //プレイヤーが持っている処理前のアイテム

    Dictionary<string, int> Item0 = new Dictionary<string, int> { };//並べ替え用の空のListを人数分
    Dictionary<string, int> Item1 = new Dictionary<string, int> { };
    Dictionary<string, int> Item2 = new Dictionary<string, int> { };
    Dictionary<string, int> Item3 = new Dictionary<string, int> { };

    List<Dictionary<string, int>> Items;//上のdictionaryを多次元化したもの。player1のアイテムは1と入力する
    //----------------------------------関数----------------------------------
    private void Awake()
    {



        //ワールド変数を代入
        playersnum = PhotonNetwork.PlayerList.Length;
        total = new Text[playersnum];
        Canvas = GameObject.Find("Canvas").transform;
        ScoreBackGround = new Transform[playersnum];
        OriginalItem = new List<Anniversary_Item>[playersnum];
        Items = new List<Dictionary<string, int>> {{ Item0 },{ Item1 },{ Item2 },{ Item3 }};
        players = GameObject.FindGameObjectsWithTag("Player");
        ScrollBar = new GameObject[playersnum];


        //ここからプレハブを生成するための下準備
        PlayerBackGround = new GameObject[playersnum];
        float[] PBGinitpos = new float[2] {-235.0f, -16.9f};//複製する際の初期位置xy
        float PBGinterval = 160.0f;//複製する際のx座標の間隔

        for (int i = 0; i < playersnum; i++)
        {
            //プレハブとプレイヤーの情報をロード
            PlayerBackGround[i] = Resources.Load<GameObject>("PlayerItems" + i);

            //プレハブを生成する
            GameObject CopyedPBG = Instantiate(PlayerBackGround[i],new Vector3(PBGinitpos[0] + (PBGinterval * i),PBGinitpos[1],0.0f), Quaternion.identity);
            CopyedPBG.name = "PlayerItems" + i;//名前を変更
            CopyedPBG.transform.SetParent(Canvas, false);//canvasの子に設定して表示

            //プレイヤーの名前を参照し設定
            Text Playername = GameObject.Find("Playername" + i).GetComponent<Text>();
            Playername.text = ReferencePlayername(players[i]);

            //表示時に使うSBGとトータルスコアを出すテキストボックスを参照し設定
            ScoreBackGround[i] = GameObject.Find("Content" + i).transform;
            total[i] = GameObject.Find("Total" + i).GetComponent<Text>();

            //並び替え前のプレイヤーの持ち物を参照
            OriginalItem[i] = players[i].GetComponent<I_Player_3D>().Hub_Items;

            //スクロールバーの大きさ調整で必要な参照
            ScrollBar[i] = GameObject.Find("Scroll View" + i);
        }

        DisplayItems();
    }


    //名前の参照
    public string ReferencePlayername(GameObject Player)
    {
        string name = "";
        foreach (var PList in PhotonNetwork.PlayerList)//プレイヤーリストの内容を順番に格納
        {
            if (PList.ActorNumber == Player.GetComponent<PhotonView>().CreatorActorNr) //リストのプレイヤーのIDとオブジェクトの作成者のADを比較
            {
                name = PList.NickName;
            }
        }
        return name;
    }


    //アイテムの表示
    public void DisplayItems()
    {
        textItems = GameObject.Find("Items").GetComponent<Text>();//コピー元を参照
        textPoints = GameObject.Find("Points").GetComponent<Text>();//コピー元を参照
        float[] initpos = new float[4] { textItems.transform.localPosition.x, textItems.transform.localPosition.y ,
                                         textPoints.transform.localPosition.x, textPoints.transform.localPosition.y };//テキストの初期位置,[0][1]はアイテム,[2][3]はポイント
        int[] totalpoint = new int[playersnum];//トータルスコア格納用
        int count = 0;//ループ回数
        

        DuplicateItem();

        foreach(Transform i in ScoreBackGround)
        {
            int dupcount = 0;//アイテムの表示回数
            float TotalBordSize = 0.0f;
            Text LastCopy = null;
            TextLineAdjust LastCopyLineAdjust = null;

            foreach (string j in Items[count].Keys)
            {
                //最初は獲得したものを左揃えで表示
                Text Copytext = Instantiate(textItems, new Vector2(initpos[0], initpos[1]), Quaternion.identity);
                Copytext.transform.SetParent(i, false);
                Copytext.text = j;
                var CopyLineAdjust = Copytext.GetComponent<TextLineAdjust>();
                CopyLineAdjust.LineAdjust();

                var linenum = CopyLineAdjust.LineNum;
                if (dupcount >= 1) {

                    var CopyTextPosition = Copytext.GetComponent<RectTransform>().anchoredPosition;

                    

                    var dump = (linenum - 1)* Copytext.fontSize;
                    CopyTextPosition.y = (LastCopy.GetComponent<RectTransform>().anchoredPosition.y + -(LastCopyLineAdjust.Rectheight / 2 )- dump) + interval;
                    Copytext.GetComponent<RectTransform>().anchoredPosition = CopyTextPosition;
                    TotalBordSize = -((CopyTextPosition.y + -(CopyLineAdjust.Rectheight / 2) - dump) + interval);
                    Debug.Log(LastCopy.GetComponent<RectTransform>().anchoredPosition.y - Copytext.GetComponent<RectTransform>().anchoredPosition.y);
                }

                LastCopy = Copytext;
                LastCopyLineAdjust = LastCopy.GetComponent<TextLineAdjust>();

                //次に獲得したもののポイントを右揃えで表示
                Text point = Instantiate(textPoints, new Vector2(initpos[2], initpos[3]), Quaternion.identity);
                    
                point.transform.SetParent(Copytext.transform, false);
                point.alignment = TextAnchor.MiddleRight;
                point.text = Items[count][j] + "P";
                var pointRect = point.GetComponent<RectTransform>();

                var PointHight= Copytext.GetComponent<RectTransform>().sizeDelta.y;
                pointRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, PointHight);

                var point_spot = pointRect.anchoredPosition;
                point_spot.y= -linenum*(CopyLineAdjust.ItemNameTextBox.fontSize)/2;
                pointRect.anchoredPosition=point_spot;

                totalpoint[count] += Items[count][j];
                    dupcount++;
            }

            BordSizeAdjust(Items[count], i, ScrollBar[count], TotalBordSize);
            total[count].text = totalpoint[count].ToString() + "P";//トータルスコアの表示
            count++;
        }


        //順位表示,totalpointのうつしと順位
        int[] tp = new int[playersnum];
        int[] Rank = new int[playersnum];

        //値を代入
        for (int i = 0; i < playersnum; ++i)
        {
            tp[i] = totalpoint[i];
            Rank[i] = i;
        }

        //降順に
        for (int i = 0; i < playersnum; ++i)
        {
            for (int j = 0; j < playersnum; ++j)
            {
                if(tp[i] > tp[j])
                {
                    int tmp;
                    tmp = Rank[i];
                    Rank[i] = Rank[j];
                    Rank[j] = tmp;

                    tmp = tp[i];
                    tp[i] = tp[j];
                    tp[j] = tmp;
                }
            }
        }

        //一位(Rank[0])のみ王冠を表示
        Image RImage = GameObject.Find("Rank" + Rank[0]).GetComponent<Image>();
        RImage.sprite = Resources.Load<Sprite>("1st");
    }


    //重複アイテムをまとめてすっきりさせる
    void DuplicateItem()
    {
        for (int num = 0; num < OriginalItem.Length; num++)//プレイヤーの数と同じ回数行う
        {
            for (int i = 0; i < OriginalItem[num].Count; i++)//num番目のプレイヤーの持ち物の数だけ行う
            {
                if (Items[num].ContainsKey(OriginalItem[num][i].ItemName))//アイテムの中に重複する者があれば
                {
                    Items[num][OriginalItem[num][i].ItemName] += OriginalItem[num][i].ItemPoint;//ポイントを増やす
                }
                else
                {
                    Items[num].Add(OriginalItem[num][i].ItemName, OriginalItem[num][i].ItemPoint);//なければ新しい項目を作る
                }
            }
        }
    }



    //ボードのサイズ調整
    public void BordSizeAdjust(Dictionary<string, int> PlayerItems, Transform SBG, GameObject Bar, float TotalBordY)
    {
        int BlockQuantity;

        if (PlayerItems.Keys.Count <= 9)
        {

            BlockQuantity = 9;
            Bar.GetComponent<ScrollRect>().vertical = false;

        }
        else
        {

            BlockQuantity = PlayerItems.Keys.Count;//アイテムの数
            Bar.GetComponent<ScrollRect>().vertical = true;

        }


        var BordSize_y = TotalBordY;//ボードのサイズを取得　（戻り値のため）

        SBG.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, BordSize_y);
    }
}