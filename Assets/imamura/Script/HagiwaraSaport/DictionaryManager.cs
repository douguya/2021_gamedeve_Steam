using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DictionaryManager : MonoBehaviour
{
    // Start is called before the first frame update




    // Dictionary<string, int[]> dic3 = ,new  Dictionary<string, int[]>();
    public static Dictionary<string, int[]> ItemDictionary = new Dictionary<string, int[]>
    {
        //{"アイテム名",new  int [2] {ポイント,アイテム分類}}
        //アイテム分類
        //0 ゴールアイテム
        //1 食料品
        //2 その他(分類待ち)
        {"ゴール",                new int [2]{15,0 }},
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
                     1つ目の効果   { 記念日, 効果の種類,手に入るアイテムや変更するBGMの名前 },
                     2つ目の効果   {"クレジットの日","アイテム" ,"クレジットカード"}
       }},
    　 
      効果の種類について
      　手に入るアイテムや変化するBGMが示せるもの
      　　アイテム　      アイテムが手に入るだけのもの
    　 　 アイコン　      アイコンが変わる
    　 　 BGM　　　　     BGMが変わる
        　背景            背景が変わる
        　アイテム変換　  アイテムが置き換わる
         配置             スモッグ
         消滅             ノストラダムス
      　その他　　分類不可　記念日ごとの変数作成が望まれるもの

      */

        {"6/1",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/2",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/3",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/4",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/5",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/6",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/7",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/8",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/9",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/10",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/11",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/12",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/13",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/14",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/15",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/16",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/17",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/18",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/19",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/20",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/21",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/22",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/23",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/24",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/25",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/26",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/27",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/29",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"6/30",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},

        { "7/1",new string[,]{
                                {"鉄スクラップの日", "アイテム変換", "スクラップ" },
                                //{"クレジットの日","アイテム" ,"クレジットカード"}
        }},
        {"7/2",new string[,]{
                                { "タコの日", "アイテム", "タコの足" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/3",new string[,]{
                                { "みたらし団子の日", "アイテム", "タコの足" },//ダイス変化
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/4",new string[,]{
                                { "独立記念日", "アイテム", "記念硬貨" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/5",new string[,]{
                                { "ビキニスタイルの日", "アイコン", "ビキニ" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/6",new string[,]{
                                { "メロンの日", "アイテム", "メロン" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/7",new string[,]{
                                { "七夕", "アイテム", "短冊" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/8",new string[,]{
                                { "チキン南蛮の日", "アイテム", "チキン南蛮" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/9",new string[,]{
                                { "ジェットコースターの日", "アイテム", "ジェットコースター" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/10",new string[,]{
                                { "国土建設記念日", "アイテム", "日本列島" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/11",new string[,]{
                                { "真珠記念日", "アイテム", "真珠" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/12",new string[,]{
                                { "洋食器の日", "アイテム変換", "カトラリーセット" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/13",new string[,]{
                                { "お父さんの日", "アイテム", "お小遣い" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/14",new string[,]{
                                { "パリ祭り", "アイテム", "八尺球" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/15",new string[,]{
                                { "イチゴの日", "アイテム", "イチゴ" },//特殊アイテム
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/16",new string[,]{
                                { "トロの日", "アイテム", "大トロ" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/17",new string[,]{
                                { "東京の日", "背景", "東京タワーのストラップ" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/18",new string[,]{
                                { "光化学スモッグ", "配置", "スモッグ" },//スモッグ
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/19",new string[,]{
                                { "", "", "" },//何の日か見つけられていない
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/20",new string[,]{
                                { "Tシャツの日", "アイコン", "Tシャツ" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/21",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/22",new string[,]{
                                { "", "", "" },                     //ショートケーキか下駄
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/23",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/24",new string[,]{
                                { "", "", ""},
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/25",new string[,]{
                                { "かき氷の日", "アイテム", "かき氷" },//特殊アイテム
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/26",new string[,]{
                                { "幽霊の日", "アイコン", "幽霊" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/27",new string[,]{
                                { "ノストラダムスの大予言", "消滅", "ノストラダムス" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/28",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/29",new string[,]{
                                { "アマチュア無線の日", "アイテム", "無線機" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/30",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/1",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/2",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/3",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/4",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/5",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/6",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/7",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/8",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/9",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/10",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/11",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/12",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/13",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/14",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/15",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/16",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/17",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/18",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/19",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/20",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/21",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/22",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/23",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/24",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/25",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/26",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/27",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/28",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/29",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/30",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"8/31",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/1",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/2",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/3",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/4",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/5",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/6",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/7",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/8",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/9",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/10",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/11",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/12",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/13",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/14",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/15",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/16",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/17",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/18",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/19",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/20",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/21",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/22",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/23",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/24",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/25",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/26",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/27",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/28",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/29",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"9/30",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
    };



    public void DectionariyInfoA(string Item)
    {

        Debug.Log(ItemDictionary[Item][0]);
      //  return ItemDictionary["Item"];

    }




    /* 書き方がダメだった奴　正しくは、 {"大トロ",new  int [2]{5,1 } },とする
        Dictionary<string, int[]> ItemDictionary = new  Dictionary<string, int[]>
        {

            {"大トロ", int{5,1 } },
            /*

        };
    */




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
