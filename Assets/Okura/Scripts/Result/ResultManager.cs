using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    //----------------------------------変数----------------------------------
    [SerializeField]
    Text textUI;           //コピー元のテキスト
    [SerializeField]
    Transform ScoreBackGround;      //テキストボックスの親にするオブジェクト
    [SerializeField]
    Button Itemadd;
    [SerializeField]
    Button display;
    [SerializeField]
    float interval = -30.0f;        //生成されたテキストボックスの間隔
    [SerializeField]
    GameObject players;
    [SerializeField]
    List<string>[] OriginalItem = new List<string>[1];//実装するときの要素数は4にしてね

    Dictionary<string, int> Item = new Dictionary<string, int> { };//並べ替え用の空のdictionary
    //----------------------------------関数----------------------------------
    private void Awake()        //下準備
    {
        /*int playersnum = 1;//プレイヤーの数を参照してね

        //ここからしおりを複製するための下準備
        GameObject PlayerBackGround = Resources.Load<GameObject>("PlayerItems");//コピー元
        float[] PBGinitpos = new float[2] {30.0f, 0.0f};//複製する際の初期位置xyご自由に変えてください
        float PBGinterval = 30.0f;//複製する際のx座標の間隔*/

        players = GameObject.Find("Player1");
        OriginalItem[0] = players.GetComponent<PlayerStatasOkura>().HabItem;
        /*for (int i = 0; i < playersnum; i++)
        {
            //しおりの複製
            GameObject CopyedPBG = 
                Instantiate(PlayerBackGround,new Vector3(PBGinitpos[0] + (PBGinterval * i),PBGinitpos[1],0.0f), Quaternion.identity);
            CopyedPBG.name = "PlayerItems" + i;
            
            //並び替え元のプレイヤーの持ち物を複製
            players = GameObject.Find("Player" + i);
            OriginalItem[i] = players.GetComponent<PlayerStatasOkura>().HabItem;
        }*/
    }

    public void AddItem(string Item)
    {
        OriginalItem[0].Add(Item);
    }

    //アイテムの表示
    public void DisplayItems()
    {
        float[] initpos = new float[2] { textUI.transform.localPosition.x, textUI.transform.localPosition.y };//テキストの初期位置
        int count = 0;
        DuplicateItem();        //テストのためここに入れました。実際のゲームシーンではアイテムを追加する必要がないため、Awakeにいれてください

        foreach (string i in Item.Keys)
        {
            //最初は獲得したものを左揃えで表示
            Text Copytext = Instantiate(textUI, new Vector3(initpos[0], initpos[1]+ (count * interval), 0.0f), Quaternion.identity);
            Copytext.transform.SetParent(ScoreBackGround, false);
            Copytext.text = i;

            //次に獲得したもののポイントを右揃えで表示
            Text point = Instantiate(textUI, new Vector3(initpos[0], initpos[1], 0.0f), Quaternion.identity);
            point.transform.SetParent(Copytext.transform, false);
            point.alignment = TextAnchor.MiddleRight;
            point.text = Item[i] + "P";

            count++;
        }
    }

    //重複アイテムをまとめてすっきりさせる
    void DuplicateItem()
    {
        for (int num = 0; num < OriginalItem.Length; num++)//プレイヤーの数と同じ回数行う
        {
            for (int i = 0; i < OriginalItem[num].Count; i++)//num番目のプレイヤーの持ち物の数だけ行う
            {
                if (Item.ContainsKey(OriginalItem[num][i]))//アイテムの中に重複する者があれば
                {
                    Item[OriginalItem[num][i]] += DictionaryManager.ItemDictionary[OriginalItem[0][i]][0];//ポイントを増やす
                }
                else
                {
                    Item.Add(OriginalItem[0][i], DictionaryManager.ItemDictionary[OriginalItem[0][i]][0]);//なければ新しい項目を作る
                }
            }
        }
    }
}
