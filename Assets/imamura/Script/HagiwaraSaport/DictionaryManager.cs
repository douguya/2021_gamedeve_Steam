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
        {"和菓子",          new int [2]{3,1 }},
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
        {"汎用カレンダー",      new int [2]{1,2 }},
        

    };



  

    public static Dictionary<string, string[]> EffectictCategoryDictionary = new Dictionary<string, string[]>
    {
        

        {"資格チャレンジの日" ,new string[]{ "", "" } },

        {"裏切りの日" ,new string[]{ "", "裏切り" } },

        {"くるみパンの日" ,new string[]{ "アイテム", "くるみパン" } },

        {"虫の日" ,new string[]{ "", "" } },

        {"環境の日" ,new string[]{ "", "" } },

        {"ロールケーキの日" ,new string[]{ "", "ロールケーキ" } },

        {"ムダ毛なしの日" ,new string[]{ "", "" } },

        {"世界海洋デー" ,new string[]{ "アイテム", "ジンベイザメの人形" } },

        {"まがたまの日" ,new string[]{ "アイテム", "勾玉" } },

        {"時の記念日" ,new string[]{ "", "時" } },

        {"傘の日" ,new string[]{ "アイテム", "傘" } },

        {"恋人の日" ,new string[]{ "", "恋人" } },

        {"鉄人の日" ,new string[]{ "", "" } },

        {"手羽先記念日" ,new string[]{ "アイテム", "手羽先" } },

        {"暑中見舞いの日" ,new string[]{ "アイテム", "暑中見舞い" } },

        {"和菓子の日" ,new string[]{ "アイテム", "和菓子" } },

        {"薩摩の日" ,new string[]{ "アイテム", "さつま揚げ" } },

        {"国際寿司の日" ,new string[]{ "アイテム", "寿司" } },

        {"ベースボール記念日" ,new string[]{ "アイテム", "記念ボール" } },

        {"ペパーミントの日" ,new string[]{ "アイテム", "ペパーミント" } },

        {"えびフライの日" ,new string[]{ "アイテム", "えびフライ" } },

        {"DHAの日" ,new string[]{ "", "" } },

        {"オリンピックデー" ,new string[]{ "アイテム", "オリンピックの記念品" } },

        {"UFOの日" ,new string[]{ "", "UFO" } },

        {"加須市うどんの日" ,new string[]{ "アイテム", "加須の手打ちうどん" } },

        {"露天風呂記念日" ,new string[]{ "", "" } },

        {"演説の日" ,new string[]{ "", "演説" } },

        {"パフェの日" ,new string[]{ "アイテム", "パフェ" } },

        {"佃煮の日" ,new string[]{ "アイテム", "佃煮" } },

        {"トランジスタの日" ,new string[]{ "アイテム", "トランジスタ" } },

        {"鉄スクラップの日" ,new string[]{ "アイテム変換", "スクラップ" } },

        {"タコの日" ,new string[]{ "アイテム", "タコの足" } },

        {"波の日" ,new string[]{ "", "波" } },

        {"独立記念日" ,new string[]{ "アイテム", "記念硬貨" } },

        {"ビキニスタイルの日" ,new string[]{ "アイコン", "ビキニ" } },

        {"ピアノの日" ,new string[]{ "BGM", "ピアノ" } },

        {"七夕" ,new string[]{ "アイテム", "短冊" } },

        {"チキン南蛮の日" ,new string[]{ "アイテム", "チキン南蛮" } },

        {"ジェットコースターの日" ,new string[]{ "アイテム", "ジェットコースター" } },

        {"国土建設記念日" ,new string[]{ "アイテム", "日本列島" } },

        {"真珠記念日" ,new string[]{ "アイテム", "真珠" } },

        {"洋食器の日" ,new string[]{ "アイテム変換", "カトラリーセット" } },

        {"オカルト記念日" ,new string[]{ "背景とBGM", "ムー大陸" } },

        {"パリ祭り" ,new string[]{ "アイテム", "八尺球" } },

        {"マンゴーの日" ,new string[]{ "アイテム", "マンゴー" } },

        {"駅弁記念日" ,new string[]{ "アイテム", "駅弁" } },

        {"東京の日" ,new string[]{ "背景", "東京タワーのストラップ" } },

        {"光化学スモッグ" ,new string[]{ "配置", "スモッグ" } },

        {"パリメトロ一号開通" ,new string[]{ "パリメトロ", "パリメトロ" } },

        {"Tシャツの日" ,new string[]{ "アイコン", "Tシャツ" } },

        {"下駄の日" ,new string[]{ "アイテム", "下駄" } },

        {"地蔵会の日" ,new string[]{ "アイテム", "地蔵" } },

        {"かき氷の日" ,new string[]{ "アイテム", "かき氷" } },

        {"幽霊の日" ,new string[]{ "アイコン", "幽霊" } },

        {"ノストラダムスの大予言" ,new string[]{ "消滅", "ノストラダムス" } },

        {"アマチュア無線の日" ,new string[]{ "アイテム", "無線機" } },

        {"梅干しの日" ,new string[]{ "アイテム", "梅干し" } },

        {"はっぴの日" ,new string[]{ "アイテム", "はっぴ" } },

        {"おやつの日" ,new string[]{ "", "" } },

        {"みたらし団子の日" ,new string[]{ "", "" } },

        {"パチスロの日" ,new string[]{ "", "" } },

        {"タクシーの日" ,new string[]{ "", "" } },

        {"平和記念日" ,new string[]{ "アイテム", "平和な心" } },

        {"おもちゃ花火の日" ,new string[]{ "アイテム", "おもちゃ花火" } },

        {"パパの日" ,new string[]{ "アイテム", "パパ" } },

        {"長崎平和の日" ,new string[]{ "", "平和の心" } },

        {"ハットの日" ,new string[]{ "アイテム", "帽子" } },

        {"山の日" ,new string[]{ "アイテム", "山" } },

        {"配布の日" ,new string[]{ "", "" } },

        {"怪談の日" ,new string[]{ "アイテム", "怪談話集" } },

        {"水泳の日" ,new string[]{ "", "" } },

        {"終戦記念日" ,new string[]{ "アイテム", "ラジオ放送" } },

        {"トロの日" ,new string[]{ "アイテム", "大トロ" } },

        {"パイナップルの日" ,new string[]{ "アイテム", "パイナップル" } },

        {"世界人道デー" ,new string[]{ "アイテム", "国連のパンフレット" } },

        {"交通信号設置記念日" ,new string[]{ "アイテム", "信号機" } },

        {"イージーパンツの日" ,new string[]{ "アイテム", "パンツ" } },

        {"金シャチの日" ,new string[]{ "アイテム", "金シャチ" } },

        {"湖池屋ポテトチップスの日" ,new string[]{ "アイテム", "ポテチ" } },

        {"ドレッシングの日" ,new string[]{ "", "やさい" } },

        {"即席ラーメン記念日" ,new string[]{ "アイテム", "チキンラーメン" } },

        {"レインボーブリッジ開通記念日" ,new string[]{ "アイテム", "レインボーブリッジ" } },

        {"ジェラートの日" ,new string[]{ "アイテム", "ジェラート" } },

        {"ヴァイオリンの日" ,new string[]{ "アイテム", "ベートーヴェンのズラ" } },

        {"焼肉の日" ,new string[]{ "アイテム", "生肉" } },

        {"ハッピーサンシャインデー" ,new string[]{ "アイテム", "ハッピー" } },

        {"野菜の日" ,new string[]{ "野菜", "野菜" } },

        {"キウイの日" ,new string[]{ "アイテム", "キウイ" } },

        {"宝くじの日" ,new string[]{ "", "" } },

        {"ベッドの日" ,new string[]{ "アイテム", "高いベッド" } },

        {"オークションの日" ,new string[]{ "", "" } },

        {"石炭の日" ,new string[]{ "アイテム", "石炭" } },

        {"メロンの日" ,new string[]{ "アイテム", "メロン" } },

        {"クリーナーの日" ,new string[]{ "", "" } },

        {"休養の日" ,new string[]{ "", "" } },

        {"温泉の日" ,new string[]{ "", "" } },

        {"牛タンの日" ,new string[]{ "アイテム", "牛タン" } },

        {"めんの日" ,new string[]{ "", "" } },

        {"マラソンの日" ,new string[]{ "", "" } },

        {"お父さんの日" ,new string[]{ "", "" } },

        {"セプテンバーバレンタイン" ,new string[]{ "", "" } },

        {"イチゴの日" ,new string[]{ "イチゴ", "イチゴ" } },

        {"モノレール開業記念日" ,new string[]{ "", "" } },

        {"カイワレ大根の日" ,new string[]{ "", "" } },

        {"敬老の日" ,new string[]{ "", "" } },

        {"バスの日" ,new string[]{ "バス", "バス" } },

        {"ガトーショコラの日" ,new string[]{ "アイテム", "ガトーショコラ" } },

        {"ショートケーキの日" ,new string[]{ "ケーキ", "ケーキ" } },

        {"秋分の日" ,new string[]{ "アイテム", "紅葉" } },

        {"畳の日" ,new string[]{ "アイテム", "畳" } },

        {"プリンの日" ,new string[]{ "アイテム", "プリン" } },

        {"風呂の日" ,new string[]{ "", "" } },

        {"世界観光の日" ,new string[]{ "世界観光", "世界観光" } },

        {"パソコンの日" ,new string[]{ "アイテム", "パソコン" } },

        {"招き猫の日" ,new string[]{ "", "" } },

        {"宅配ピザの日" ,new string[]{ "", "" } },
        
        {"",new string[]{"アイテム","汎用カレンダー" } }


    };

    public static Dictionary<string, string[]> DayEffectictDictionary = new Dictionary<string, string[]>
    {
      /*
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

        {"6/1",new string[]{
                               "資格チャレンジの日", "", ""

        }},
        {"6/2",new string[]{
                                "裏切りの日", "", "" ,

        }},
        {"6/3",new string[]{
                               "くるみパンの日", "", ""

        }},
        {"6/4",new string[]{
                               "虫の日", "", ""

        }},
        {"6/5",new string[]{
                               "環境の日", "", ""

        }},
        {"6/6",new string[]{
                               "ロールケーキの日", "", ""

        }},
        {"6/7",new string[]{
                               "ムダ毛なしの日", "", ""

        }},
        {"6/8",new string[]{
                               "世界海洋デー", "", ""

        }},
        {"6/9",new string[]{
                               "まがたまの日", "", ""

        }},
        {"6/10",new string[]{
                               "時の記念日", "", ""

        }},
        {"6/11",new string[]{
                               "傘の日", "", ""

        }},
        {"6/12",new string[]{
                               "恋人の日", "", ""

        }},
        {"6/13",new string[]{
                               "鉄人の日", "", ""

        }},
        {"6/14",new string[]{
                               "手羽先記念日", "", ""

        }},
        {"6/15",new string[]{
                              "暑中見舞いの日", "", ""

        }},
        {"6/16",new string[]{
                               "和菓子の日", "", ""

        }},
        {"6/17",new string[]{
                               "薩摩の日", "", ""

        }},
        {"6/18",new string[]{
                               "国際寿司の日", "", ""

        }},
        {"6/19",new string[]{
                               "ベースボール記念日", "", ""

        }},
        {"6/20",new string[]{
                               "ペパーミントの日", "", ""

        }},
        {"6/21",new string[]{
                               "えびフライの日", "", ""

        }},
        {"6/22",new string[]{
                               "DHAの日", "", ""

        }},
        {"6/23",new string[]{
                               "オリンピックデー", "", ""

        }},
        {"6/24",new string[]{
                               "UFOの日", "", ""

        }},
        {"6/25",new string[]{
                               "加須市うどんの日", "", ""

        }},
        {"6/26",new string[]{
                               "露天風呂記念日", "", ""

        }},
        {"6/27",new string[]{
                               "演説の日", "", ""

        }},
        {"6/28",new string[]{
                               "パフェの日", "", "" } },
                                //{"うどんの日","アイテム" ,"生めん"}
        
        {"6/29",new string[]{
                               "佃煮の日", "", ""

        }},
        {"6/30",new string[]{
                               "トランジスタの日", "", ""

        }},

        { "7/1",new string[]{
                                "鉄スクラップの日", "", "" ,

        }},
        {"7/2",new string[]{
                                "タコの日", "", "" ,

        }},
        {"7/3",new string[]{
                               "波の日", "", "" ,//ダイス変化
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {"7/4",new string[]{
                                "独立記念日", "", "" ,

        }},
        {"7/5",new string[]{
                                "ビキニスタイルの日", "", "" ,

        }},
        {"7/6",new string[]{
                                "ピアノの日", "", "" ,

        }},
        {"7/7",new string[]{
                               "七夕", "", "" ,

        }},
        {"7/8",new string[]{
                                "チキン南蛮の日", "", "" ,

        }},
        {"7/9",new string[]{
                               "ジェットコースターの日", "", "" ,

        }},
        {"7/10",new string[]{
                                "国土建設記念日", "", "" ,

        }},
        {"7/11",new string[]{
                                "真珠記念日", "", "" ,

        }},
        {"7/12",new string[]{
                                "洋食器の日", "", "" ,

        }},
        {"7/13",new string[]{
                                "オカルト記念日", "", "" ,

        }},
        {"7/14",new string[]{
                                "パリ祭り", "", "" ,

        }},
        {"7/15",new string[]{
                                "マンゴーの日", "", "" ,//特殊アイテム
                                
        }},
        {"7/16",new string[]{
                                "駅弁記念日", "", "" ,

        }},
        {"7/17",new string[]{
                               "東京の日", "", ""

        }},
        {"7/18",new string[]{
                                "光化学スモッグ", "", "" ,//スモッグ
                                
        }},
        {"7/19",new string[]{
                               "パリメトロ一号開通", "", "" //何の日か見つけられていない
                                
        }},
        {"7/20",new string[]{
                                "Tシャツの日", "", ""

        }},
        {"7/21",new string[]{
                               "", "", ""

        }},
        {"7/22",new string[]{
                               "下駄の日", "", ""                      //ショートケーキか下駄
                                
        }},
        {"7/23",new string[]{
                               "", "", ""

        }},
        {"7/24",new string[]{
                               "地蔵会の日", "", ""

        }},
        {"7/25",new string[]{
                                "かき氷の日", "", "" ,//特殊アイテム
                                
        }},
        {"7/26",new string[]{
                               "幽霊の日", "", ""

        }},
        {"7/27",new string[]{
                               "ノストラダムスの大予言", "", ""

        }},
        {"7/28",new string[]{
                               "", "", ""

        }},
        {"7/29",new string[]{
                               "アマチュア無線の日", "", ""

        }},
        {"7/30",new string[]{
                               "梅干しの日", "", ""

        }},
        {"7/31",new string[]{
                                 "", "", ""

        }},
        {"8/1",new string[]{
                               "はっぴの日", "", ""

        }},
        {"8/2",new string[]{
                               "おやつの日", "", ""

        }},
        {"8/3",new string[]{
                               "みたらし団子の日", "", ""

        }},
        {"8/4",new string[]{
                               "パチスロの日", "", ""

        }},
        {"8/5",new string[]{
                               "タクシーの日", "", ""

        }},
        {"8/6",new string[]{
                               "平和記念日", "", ""

        }},
        {"8/7",new string[]{
                               "おもちゃ花火の日", "", ""

        }},
        {"8/8",new string[]{
                               "パパの日", "", ""

        }},
        {"8/9",new string[]{
                               "長崎平和の日", "", ""

        }},
        {"8/10",new string[]{
                               "ハットの日", "", ""

        }},
        {"8/11",new string[]{
                               "山の日", "", ""

        }},
        {"8/12",new string[]{
                               "配布の日", "", ""

        }},
        {"8/13",new string[]{
                               "怪談の日", "", ""

        }},
        {"8/14",new string[]{
                               "水泳の日", "", ""

        }},
        {"8/15",new string[]{
                               "終戦記念日", "", ""

        }},
        {"8/16",new string[]{
                               "トロの日", "", ""

        }},
        {"8/17",new string[]{
                               "パイナップルの日", "", ""

        }},
        {"8/18",new string[]{
                               "", "", ""

        }},
        {"8/19",new string[]{
                               "世界人道デー", "", ""

        }},
        {"8/20",new string[]{
                               "交通信号設置記念日", "", ""

        }},
        {"8/21",new string[]{
                               "イージーパンツの日", "", ""

        }},
        {"8/22",new string[]{
                               "金シャチの日", "", ""

        }},
        {"8/23",new string[]{
                               "湖池屋ポテトチップスの日", "", ""

        }},
        {"8/24",new string[]{
                               "ドレッシングの日", "", ""

        }},
        {"8/25",new string[]{
                               "即席ラーメン記念日", "", ""

        }},
        {"8/26",new string[]{
                               "レインボーブリッジ開通記念日", "", ""

        }},
        {"8/27",new string[]{
                               "ジェラートの日", "", ""

        }},
        {"8/28",new string[]{
                               "ヴァイオリンの日", "", ""

        }},
        {"8/29",new string[]{
                               "焼肉の日", "", ""

        }},
        {"8/30",new string[]{
                               "ハッピーサンシャインデー", "", ""

        }},
        {"8/31",new string[]{
                               "野菜の日", "", ""

        }},
        {"9/1",new string[]{
                               "キウイの日", "", ""

        }},
        {"9/2",new string[]{
                               "宝くじの日", "", ""

        }},
        {"9/3",new string[]{
                               "ベッドの日", "", ""

        }},
        {"9/4",new string[]{
                               "オークションの日", "", ""

        }},
        {"9/5",new string[]{
                               "石炭の日", "", ""

        }},
        {"9/6",new string[]{
                               "メロンの日", "", ""

        }},
        {"9/7",new string[]{
                               "クリーナーの日", "", ""

        }},
        {"9/8",new string[]{
                               "休養の日", "", ""

        }},
        {"9/9",new string[]{
                               "温泉の日", "", ""

        }},
        {"9/10",new string[]{
                               "牛タンの日", "", ""

        }},
        {"9/11",new string[]{
                               "めんの日", "", ""

        }},
        {"9/12",new string[]{
                               "マラソンの日", "", ""

        }},
        {"9/13",new string[]{
                               "お父さんの日", "", ""

        }},
        {"9/14",new string[]{
                               "セプテンバーバレンタイン", "", ""

        }},
        {"9/15",new string[]{
                               "イチゴの日", "", ""

        }},
        {"9/16",new string[]{
                               "トロの日", "", ""

        }},
        {"9/17",new string[]{
                                "モノレール開業記念日", "", ""

        }},
        {"9/18",new string[]{
                               "カイワレ大根の日", "", ""

        }},
        {"9/19",new string[]{
                               "敬老の日", "", ""

        }},
        {"9/20",new string[]{
                               "バスの日", "", ""

        }},
        {"9/21",new string[]{
                               "ガトーショコラの日", "", ""

        }},
        {"9/22",new string[]{
                               "ショートケーキの日", "", ""

        }},
        {"9/23",new string[]{
                               "秋分の日", "", ""

        }},
        {"9/24",new string[]{
                               "畳の日", "", ""

        }},
        {"9/25",new string[]{
                               "プリンの日", "", ""

        }},
        {"9/26",new string[]{
                               "風呂の日", "", ""

        }},
        {"9/27",new string[]{
                               "世界観光の日", "", ""

        }},
        {"9/28",new string[]{
                               "パソコンの日", "", ""

        }},
        {"9/29",new string[]{
                               "招き猫の日", "", ""

        }},
        {"9/30",new string[]{
                               "宅配ピザの日", "", ""

        }},
    };
    public void DectionariyInfoA(string Item)
    {

        Debug.Log(ItemDictionary[Item][0]);
        //  return ItemDictionary["Item"];

    }

    /*

        {
    "7/1",new string[,]{
                                {"鉄スクラップの日", "アイテム変換", "スクラップ" },
                                //{"クレジットの日","アイテム" ,"クレジットカード"}
        }},
        {
    "7/2",new string[,]{
                                { "タコの日", "アイテム", "タコの足" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/3",new string[,]{
                                { "みたらし団子の日", "アイテム", "タコの足" },//ダイス変化
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/4",new string[,]{
                                { "独立記念日", "アイテム", "記念硬貨" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/5",new string[,]{
                                { "ビキニスタイルの日", "アイコン", "ビキニ" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/6",new string[,]{
                                { "メロンの日", "アイテム", "メロン" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/7",new string[,]{
                                { "七夕", "アイテム", "短冊" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/8",new string[,]{
                                { "チキン南蛮の日", "アイテム", "チキン南蛮" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/9",new string[,]{
                                { "ジェットコースターの日", "アイテム", "ジェットコースター" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/10",new string[,]{
                                { "国土建設記念日", "アイテム", "日本列島" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/11",new string[,]{
                                { "真珠記念日", "アイテム", "真珠" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/12",new string[,]{
                                { "洋食器の日", "アイテム変換", "カトラリーセット" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/13",new string[,]{
                                { "お父さんの日", "アイテム", "お小遣い" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/14",new string[,]{
                                { "パリ祭り", "アイテム", "八尺球" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/15",new string[,]{
                                { "イチゴの日", "アイテム", "イチゴ" },//特殊アイテム
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/16",new string[,]{
                                { "トロの日", "アイテム", "大トロ" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/17",new string[,]{
                                { "東京の日", "背景", "東京タワーのストラップ" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/18",new string[,]{
                                { "光化学スモッグ", "配置", "スモッグ" },//スモッグ
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
 
        {
    "7/20",new string[,]{
                                { "Tシャツの日", "アイコン", "Tシャツ" },
                                //{"うどんの日","アイテム" ,"生めん"}
  
        {
    "7/25",new string[,]{
                                { "かき氷の日", "アイテム", "かき氷" },//特殊アイテム
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/26",new string[,]{
                                { "幽霊の日", "アイコン", "幽霊" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/27",new string[,]{
                                { "ノストラダムスの大予言", "消滅", "ノストラダムス" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/28",new string[,]{
                                { "", "", "" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
        {
    "7/29",new string[,]{
                                { "アマチュア無線の日", "アイテム", "無線機" },
                                //{"うどんの日","アイテム" ,"生めん"}
        }},
    
        };
    */


    /*
    {"資格チャレンジの日" ,new string[]{ "", "" } },

        {"裏切りの日" ,new string[]{ "", "裏切り" } },

        {"くるみパンの日" ,new string[]{ "アイテム", "くるみパン" } },

        {"虫の日" ,new string[]{ "", "" } },

        {"環境の日" ,new string[]{ "", "" } },

        {"ロールケーキの日" ,new string[]{ "アイテム", "ロールケーキ" } },

        {"ムダ毛なしの日" ,new string[]{ "", "" } },

        {"世界海洋デー" ,new string[]{ "アイテム", "ジンベイザメの人形" } },

        {"まがたまの日" ,new string[]{ "アイテム", "勾玉" } },

        {"時の記念日" ,new string[]{ "", "時" } },

        {"傘の日" ,new string[]{ "アイテム", "傘" } },

        {"恋人の日" ,new string[]{ "アイテム", "恋人" } },

        {"鉄人の日" ,new string[]{ "", "" } },

        {"手羽先記念日" ,new string[]{ "アイテム", "手羽先" } },

        {"暑中見舞いの日" ,new string[]{ "アイテム", "暑中見舞い" } },

        {"和菓子の日" ,new string[]{ "アイテム", "和菓子" } },

        {"薩摩の日" ,new string[]{ "アイテム", "さつま揚げ" } },

        {"国際寿司の日" ,new string[]{ "アイテム", "寿司" } },

        {"ベースボール記念日" ,new string[]{ "アイテム", "記念ボール" } },

        {"ペパーミントの日" ,new string[]{ "アイテム", "ペパーミント" } },

        {"えびフライの日" ,new string[]{ "アイテム", "えびフライ" } },

        {"DHAの日" ,new string[]{ "", "" } },

        {"オリンピックデー" ,new string[]{ "アイテム", "オリンピックの記念品" } },

        {"UFOの日" ,new string[]{ "アイテム", "UFO" } },

        {"加須市うどんの日" ,new string[]{ "アイテム", "加須の手打ちうどん" } },

        {"露天風呂記念日" ,new string[]{ "", "" } },

        {"演説の日" ,new string[]{ "", "演説" } },

        {"パフェの日" ,new string[]{ "アイテム", "パフェ" } },

        {"佃煮の日" ,new string[]{ "アイテム", "佃煮" } },

        {"トランジスタの日" ,new string[]{ "アイテム", "トランジスタ" } },

        {"鉄スクラップの日" ,new string[]{ "アイテム変換", "スクラップ" } },

        {"タコの日" ,new string[]{ "アイテム", "タコの足" } },

        {"波の日" ,new string[]{ "", "波" } },

        {"独立記念日" ,new string[]{ "アイテム", "記念硬貨" } },

        {"ビキニスタイルの日" ,new string[]{ "アイコン", "ビキニ" } },

        {"ピアノの日" ,new string[]{ "BGM", "ピアノ" } },

        {"七夕" ,new string[]{ "アイテム", "短冊" } },

        {"チキン南蛮の日" ,new string[]{ "アイテム", "チキン南蛮" } },

        {"ジェットコースターの日" ,new string[]{ "アイテム", "ジェットコースター" } },

        {"国土建設記念日" ,new string[]{ "アイテム", "日本列島" } },

        {"真珠記念日" ,new string[]{ "アイテム", "真珠" } },

        {"洋食器の日" ,new string[]{ "アイテム変換", "カトラリーセット" } },

        {"オカルト記念日" ,new string[]{ "背景とBGM", "ムー大陸" } },

        {"パリ祭り" ,new string[]{ "アイテム", "八尺球" } },

        {"マンゴーの日" ,new string[]{ "アイテム", "マンゴー" } },

        {"駅弁記念日" ,new string[]{ "アイテム", "駅弁" } },

        {"東京の日" ,new string[]{ "背景", "東京タワーのストラップ" } },

        {"光化学スモッグ" ,new string[]{ "配置", "スモッグ" } },

        {"パリメトロ一号開通" ,new string[]{ "パリメトロ", "パリメトロ" } },

        {"Tシャツの日" ,new string[]{ "アイコン", "Tシャツ" } },

        {"下駄の日" ,new string[]{ "アイテム", "下駄" } },

        {"地蔵会の日" ,new string[]{ "アイテム", "地蔵" } },

        {"かき氷の日" ,new string[]{ "アイテム", "かき氷" } },

        {"幽霊の日" ,new string[]{ "アイコン", "幽霊" } },

        {"ノストラダムスの大予言" ,new string[]{ "消滅", "ノストラダムス" } },

        {"アマチュア無線の日" ,new string[]{ "アイテム", "無線機" } },

        {"梅干しの日" ,new string[]{ "アイテム", "梅干し" } },

        {"はっぴの日" ,new string[]{ "アイテム", "はっぴ" } },

        {"おやつの日" ,new string[]{ "", "" } },

        {"みたらし団子の日" ,new string[]{ "", "" } },

        {"パチスロの日" ,new string[]{ "", "" } },

        {"タクシーの日" ,new string[]{ "", "" } },

        {"平和記念日" ,new string[]{ "アイテム", "平和な心" } },

        {"おもちゃ花火の日" ,new string[]{ "アイテム", "おもちゃ花火" } },

        {"パパの日" ,new string[]{ "アイテム", "パパ" } },

        {"長崎平和の日" ,new string[]{ "アイテム", "平和の心" } },

        {"ハットの日" ,new string[]{ "アイテム", "帽子" } },

        {"山の日" ,new string[]{ "アイテム", "山" } },

        {"配布の日" ,new string[]{ "", "" } },

        {"怪談の日" ,new string[]{ "アイテム", "怪談話集" } },

        {"水泳の日" ,new string[]{ "", "" } },

        {"終戦記念日" ,new string[]{ "アイテム", "ラジオ放送" } },

        {"トロの日" ,new string[]{ "アイテム", "大トロ" } },

        {"パイナップルの日" ,new string[]{ "アイテム", "パイナップル" } },

        {"世界人道デー" ,new string[]{ "アイテム", "国連のパンフレット" } },

        {"交通信号設置記念日" ,new string[]{ "アイテム", "信号機" } },

        {"イージーパンツの日" ,new string[]{ "アイテム", "パンツ" } },

        {"金シャチの日" ,new string[]{ "アイテム", "金シャチ" } },

        {"湖池屋ポテトチップスの日" ,new string[]{ "アイテム", "ポテチ" } },

        {"ドレッシングの日" ,new string[]{ "アイテム", "やさい" } },

        {"即席ラーメン記念日" ,new string[]{ "アイテム", "チキンラーメン" } },

        {"レインボーブリッジ開通記念日" ,new string[]{ "アイテム", "レインボーブリッジ" } },

        {"ジェラートの日" ,new string[]{ "アイテム", "ジェラート" } },

        {"ヴァイオリンの日" ,new string[]{ "アイテム", "ベートーヴェンのズラ" } },

        {"焼肉の日" ,new string[]{ "アイテム", "生肉" } },

        {"ハッピーサンシャインデー" ,new string[]{ "アイテム", "ハッピー" } },

        {"野菜の日" ,new string[]{ "野菜", "野菜" } },

        {"キウイの日" ,new string[]{ "アイテム", "キウイ" } },

        {"宝くじの日" ,new string[]{ "", "" } },

        {"ベッドの日" ,new string[]{ "アイテム", "高いベッド" } },

        {"オークションの日" ,new string[]{ "", "" } },

        {"石炭の日" ,new string[]{ "アイテム", "石炭" } },

        {"メロンの日" ,new string[]{ "アイテム", "メロン" } },

        {"クリーナーの日" ,new string[]{ "", "" } },

        {"休養の日" ,new string[]{ "", "" } },

        {"温泉の日" ,new string[]{ "", "" } },

        {"牛タンの日" ,new string[]{ "アイテム", "牛タン" } },

        {"めんの日" ,new string[]{ "", "" } },

        {"マラソンの日" ,new string[]{ "", "" } },

        {"お父さんの日" ,new string[]{ "", "" } },

        {"セプテンバーバレンタイン" ,new string[]{ "", "" } },

        {"イチゴの日" ,new string[]{ "イチゴ", "イチゴ" } },

        {"モノレール開業記念日" ,new string[]{ "", "" } },

        {"カイワレ大根の日" ,new string[]{ "", "" } },

        {"敬老の日" ,new string[]{ "", "" } },

        {"バスの日" ,new string[]{ "バス", "バス" } },

        {"ガトーショコラの日" ,new string[]{ "アイテム", "ガトーショコラ" } },

        {"ショートケーキの日" ,new string[]{ "ケーキ", "ケーキ" } },

        {"秋分の日" ,new string[]{ "アイテム", "紅葉" } },

        {"畳の日" ,new string[]{ "アイテム", "畳" } },

        {"プリンの日" ,new string[]{ "アイテム", "プリン" } },

        {"風呂の日" ,new string[]{ "", "" } },

        {"世界観光の日" ,new string[]{ "世界観光", "世界観光" } },

        {"パソコンの日" ,new string[]{ "アイテム", "パソコン" } },

        {"招き猫の日" ,new string[]{ "", "" } },

        {"宅配ピザの日" ,new string[]{ "", "" } },
        
        {"",new string[]{"アイテム","汎用カレンダー" } }

     */

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
