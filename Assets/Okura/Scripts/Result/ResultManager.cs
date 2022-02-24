using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    //----------------------------------変数----------------------------------
    [SerializeField]
    int playersnum;
    [SerializeField]
    Text textUI;           //コピー元のテキスト
    [SerializeField]
    Text[] total;
    [SerializeField]
    Transform Canvas;
    [SerializeField]
    Transform[] ScoreBackGround;      //テキストボックスの親にするオブジェクト(以下SBG)
    [SerializeField]
    Button Itemadd;
    [SerializeField]
    Button display;
    [SerializeField]
    float interval = -30.0f;        //生成されたテキストボックスの間隔
    [SerializeField]
    GameObject players;
    [SerializeField]
    List<string>[] OriginalItem;//実装するときの要素数は4にしてね

    Dictionary<string, int> Item0 = new Dictionary<string, int> { };//並べ替え用の空のdictionary
    Dictionary<string, int> Item1 = new Dictionary<string, int> { };
    Dictionary<string, int> Item2 = new Dictionary<string, int> { };
    Dictionary<string, int> Item3 = new Dictionary<string, int> { };

    Dictionary<int, Dictionary<string, int>> Items;
    //----------------------------------関数----------------------------------
    private void Awake()        //下準備
    {
        //ワールド変数を代入
        playersnum = 4;
        total = new Text[playersnum];
        Canvas = GameObject.Find("Canvas").transform;
        ScoreBackGround = new Transform[playersnum];
        OriginalItem = new List<string>[playersnum];
        Items = new Dictionary<int, Dictionary<string, int>> {{ 0, Item0 },{ 1, Item1 },{ 2, Item2 },{ 3, Item3 }};


        //ここからプレハブを生成するための下準備
        GameObject[] PlayerBackGround = new GameObject[playersnum];
        float[] PBGinitpos = new float[2] {-235.0f, -16.9f};//複製する際の初期位置xyご自由に変えてください
        float PBGinterval = 160.0f;//複製する際のx座標の間隔*/

        for (int i = 0; i < playersnum; i++)
        {
            //プレハブとプレイヤーの情報をロード
            PlayerBackGround[i] = Resources.Load<GameObject>("PlayerItems" + i);
            players = GameObject.Find("Player" + i);

            //プレハブを生成する
            GameObject CopyedPBG = Instantiate(PlayerBackGround[i],new Vector3(PBGinitpos[0] + (PBGinterval * i),PBGinitpos[1],0.0f), Quaternion.identity);
            CopyedPBG.name = "PlayerItems" + i;
            CopyedPBG.transform.SetParent(Canvas, false);
            //名前の設定
            Text Playername = GameObject.Find("Playername" + i).GetComponent<Text>();
            Playername.text = players.GetComponent<PlayerStatasOkura>().Name;
            //表示時に使うSBGを設定
            ScoreBackGround[i] = GameObject.Find("Content" + i).transform;
            total[i] = GameObject.Find("Total" + i).GetComponent<Text>();

            //並び替え元のプレイヤーの持ち物を複製
            OriginalItem[i] = players.GetComponent<PlayerStatasOkura>().HabItem;
        }
    }

    //アイテムの表示
    public void DisplayItems()
    {
        textUI = GameObject.Find("Items").GetComponent<Text>();
        float[] initpos = new float[2] { textUI.transform.localPosition.x, textUI.transform.localPosition.y };//テキストの初期位置
        int count = 0;
        DuplicateItem();        //テストのためここに入れました。実際のゲームシーンではアイテムを追加する必要がないため、Awakeにいれてください

        foreach(Transform i in ScoreBackGround)
        {
            int totalpoint = 0;
            int dupcount = 0;
            foreach (string j in Items[count].Keys)
            {
                //最初は獲得したものを左揃えで表示
                Text Copytext = Instantiate(textUI, new Vector3(initpos[0], initpos[1] + (dupcount * interval), 0.0f), Quaternion.identity);
                Copytext.transform.SetParent(i, false);
                Copytext.text = j;

                //次に獲得したもののポイントを右揃えで表示
                Text point = Instantiate(textUI, new Vector3(initpos[0], initpos[1], 0.0f), Quaternion.identity);
                point.transform.SetParent(Copytext.transform, false);
                point.alignment = TextAnchor.MiddleRight;
                point.text = Items[count][j] + "P";

                totalpoint += Items[count][j];
                dupcount++;
            }

            total[count].text = totalpoint.ToString() + "P";
            count++;
        }
    }

    //重複アイテムをまとめてすっきりさせる
    void DuplicateItem()
    {
        for (int num = 0; num < OriginalItem.Length; num++)//プレイヤーの数と同じ回数行う
        {
            Debug.Log(num + "人目");
            for (int i = 0; i < OriginalItem[num].Count; i++)//num番目のプレイヤーの持ち物の数だけ行う
            {
                Debug.Log(i + "回目");
                Debug.Log(OriginalItem[num][i]);
                if (Items[num].ContainsKey(OriginalItem[num][i]))//アイテムの中に重複する者があれば
                {
                    Debug.Log("A");
                    Items[num][OriginalItem[num][i]] += DictionaryManager.ItemDictionary[OriginalItem[num][i]][0];//ポイントを増やす
                }
                else
                {
                    Debug.Log("B");
                    Items[num].Add(OriginalItem[num][i], DictionaryManager.ItemDictionary[OriginalItem[num][i]][0]);//なければ新しい項目を作る
                }
            }
        }
    }
}
