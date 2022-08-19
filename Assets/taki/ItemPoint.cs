using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        アイテムリスト（現在存在している分）
        8月
        はっぴ５
        平和な心？３
        おもちゃ花火４
        パパ？１
        平和の心？８
        帽子３
        山９
        怪談話４
        玉音放送８
        パイン３
        国連パンフレット５
        信号機３
        パンツ１
        金シャチ４
        ウクレレ？
        ドレッシング２
        チキンラーメン２
        レインボーブリッジ１
        ジェラート３
        ベートーヴェンずら２
        生肉３
        ハッピー５

        */


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ポイント追加用イベント
    void ConditionalPoint(string x,int y)
    {
        int num = int.Parse(x);
        y += num;
    }


}
