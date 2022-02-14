using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryManager : MonoBehaviour
{
    // Dictionary<string, int[]> dic3 = ,new  Dictionary<string, int[]>();
    public static Dictionary<string, int[]> ItemDictionary = new Dictionary<string, int[]>
    {
        //{"アイテム名",new  int [2] {ポイント,アイテム分類}}
        //アイテム分類
        //0 ゴールアイテム
        //1 食料品
        //2 その他(分類待ち)
        {"ゴール",                new int [2]{5,0 }},
        {"大トロ",                new int [2]{5,1 }},
        {"牛タン",                new int [2]{5,1 }},
        {"プリン",                new int [2]{2,1 }},
        {"ハンバーガー",          new int [2]{3,1 }},
        {"記念硬貨",              new int [2]{7,2 }},
        {"真珠",                  new int [2]{4,2 }},
        {"メロン",                new int [2]{4,1 }},
        {"ガトーショコラ",        new int [2]{3,1 }},
        {"八尺球",                new int [2]{3,2 }},
        {"無線機",                new int [2]{5,2 }},
        {"ビキニ",                new int [2]{4,2 }},
        {"トランジスタ",          new int [2]{5,2 }},
        {"石炭",                  new int [2]{3,2 }},
        {"短冊",                  new int [2]{7,2 }},
        {"下駄",                  new int [2]{2,2 }},
        {"梅干し",                new int [2]{1,1 }},
        {"生肉",                  new int [2]{3,1 }},
        {"ジンベイザメの人形",    new int [2]{6,2 }},
        {"タコの足",              new int [2]{1,1 }},
        {"マンゴー",              new int [2]{2,1 }},
        {"ウクレレ",              new int [2]{3,2 }},
        {"ハイエイトチョコ",      new int [2]{2,1 }},
        {"ハッピー",              new int [2]{5,2 }},
        {"和菓子を入手",          new int [2]{3,1 }},
        {"チキン南蛮",            new int [2]{2,1 }},
        {"駅弁",                  new int [2]{2,1 }},
        {"国連のパンフレット",    new int [2]{5,2 }},
        {"チキンラーメン",        new int [2]{2,1 }},
        {"クルミパン",            new int [2]{2,1 }},
        {"さつま揚げ",            new int [2]{2,1 }},
        {"ジェットコースター",    new int [2]{5,2 }},
        {"帽子",                  new int [2]{3,2 }},
        {"信号機",                new int [2]{3,2 }},
        {"レインボーブリッジ",    new int [2]{1,2 }},
        {"傘",                    new int [2]{3,2 }},
        {"寿司",                  new int [2]{5,1 }},
        {"はっぴ",                new int [2]{5,2 }},
        {"山",                    new int [2]{9,2 }},
        {"パンツ",                new int [2]{1,2 }},
        {"ジェラート",            new int [2]{3,1 }},
        {"勾玉",                  new int [2]{6,2 }},
        {"オリンピックの記念品",  new int [2]{5,2 }},
        {"平和な心",              new int [2]{8,2 }},
        {"怪談話集",              new int [2]{4,2 }},
        {"金シャチ",              new int [2]{4,2 }},
        {"ベートーヴェンのズラ",  new int [2]{2,2 }},
        {"手羽先",                new int [2]{3,1 }},
        {"加須の手打ちうどん",    new int [2]{4,1 }},
        {"おもちゃ花火",          new int [2]{4,2 }},
        {"玉音放送",              new int [2]{8,2 }},
        {"ポテチ",                new int [2]{3,2 }},
        {"ロールケーキ",          new int [2]{3,1 }},
        {"ペパーミント",          new int [2]{1,1 }},
        {"パフェ",                new int [2]{3,1 }},
        {"パパ",                  new int [2]{1,2 }},
        {"パイナップル",          new int [2]{3,1 }},
        {"お小遣い",              new int [2]{3,2 }},
        {"記念ボール",            new int [2]{7,2 }},
        {"えびフライ",            new int [2]{3,1 }},
        {"キウイ",                new int [2]{3,1 }},
        {"紅葉",                  new int [2]{5,2 }},
        {"地蔵",                  new int [2]{5,2 }},
        {"日本列島",              new int [2]{8,2 }},
        {"高いベッド",            new int [2]{8,2 }},
        {"畳",                    new int [2]{2,2 }},
        {"パソコン",              new int [2]{3,2 }},
        {"落ち葉",                new int [2]{1,2 }},
        {"佃煮",                  new int [2]{2,1 }},
        {"東京タワーのストラップ",new int [2]{4,2 }},
        {"野菜",                  new int [2]{3,1 }},
        {"イチゴ",                new int [2]{1,1 }},
        {"宅配ピザ",              new int [2]{5,1 }},
        {"カブトムシ",            new int [2]{3,2 }},
        {"ヘラクレスオオカブト",  new int [2]{10,2}},
        {"暑中見舞いの品",        new int [2]{3,1 }},
        {"かき氷",                new int [2]{3,1 }},
        {"資格",                  new int [2]{9,2 }},
        {"スクラップ",            new int [2]{1,2 }},
        {"カトラリーセット",      new int [2]{5,2 }},
        {"クレジットカード",      new int [2]{7,2 }},

    };



    public static Dictionary<string, string[,]> DayEffectictDictionary = new Dictionary<string, string[,]>
    {
      /*
       * 
      {日付,new string[,]{
                     1つ目   { 記念日, 効果の種類,手に入るアイテムや変更するBGMの名前 },
                     2つ目   {"クレジットの日","アイテム" ,"クレジットカード"}
       }},
    　 
      効果の種類について
      　手に入るアイテムや変化するBGMが示せるもの
      　　アイテム　アイテムが手に入るだけのもの
    　 　 アイコン　アイコンが変わる
    　 　 BGM　　　　BGMが変わる

      　その他　　分類不可　記念日ごとの変数作成が望まれるもの

      */
        { "7/1",new string[,]{
                                {"クルミパンの日", "アイテム", "スクラップ" },
                                {"クレジットの日","アイテム" ,"クレジットカード"}
        }},
        {"7/2",new string[,]{
                                { "タコの日", "アイテム", "タコの足" },
                                {"うどんの日","アイテム" ,"生めん"}
        }},





    };



}
