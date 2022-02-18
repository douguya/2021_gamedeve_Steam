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
    Transform canvas;      //テキストボックスの親にするオブジェクト
                           //これがあることでコピーされたテキストが生成される位置を決められる
    [SerializeField]
    Button Itemadd;
    [SerializeField]
    Button display;
    [SerializeField]
    float interval = -30.0f;        //生成されたテキストボックスの間隔
    [SerializeField]
    GameObject players;
    [SerializeField]
    List<string> OriginalItem;
    [SerializeField]
    Dictionary<string, int> Item = new Dictionary<string, int> { };
    //----------------------------------関数----------------------------------
    private void Awake()
    {
        players = GameObject.Find("Player1");
        OriginalItem = players.GetComponent<PlayerStatasOkura>().HabItem;
    }

    public void AddItem(string Item)
    {
        OriginalItem.Add(Item);
    }

    //アイテムの表示
    public void DisplayItems()
    {
        float[] initpos = new float[2] { textUI.transform.localPosition.x, textUI.transform.localPosition.y };//テキストの初期位置
        int count = 0;
        DuplicateItem();

        foreach (string i in Item.Keys)
        {
            //最初は獲得したものを左揃えで表示
            Text Copytext = Instantiate(textUI, new Vector3(initpos[0], initpos[1]+ (count * interval), 0.0f), Quaternion.identity);
            Copytext.transform.SetParent(canvas, false);
            Copytext.text = i;

            //次に獲得したもののポイントを右揃えで表示
            Text point = Instantiate(textUI, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            point.transform.SetParent(Copytext.transform, false);
            point.alignment = TextAnchor.MiddleRight;
            point.text = Item[i] + "P";


            count++;
        }
    }

    //重複アイテムをまとめてすっきりさせる
    void DuplicateItem()
    {
        for (int i = 0; i < OriginalItem.Count; i++)
        {
                if (Item.ContainsKey(OriginalItem[i]))
                {
                    Item[OriginalItem[i]] += DictionaryManager.ItemDictionary[OriginalItem[i]][0];
                }
                else
                {
                    Item.Add(OriginalItem[i], DictionaryManager.ItemDictionary[OriginalItem[i]][0]);
            }
        }
    }
}
