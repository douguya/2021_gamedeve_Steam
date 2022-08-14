using System;
using UnityEngine;
using UnityEngine.Video;


[Serializable]

[CreateAssetMenu(fileName = "Day_Square", menuName = "Day_Square")]
public class Day_Square : ScriptableObject
{





    [Tooltip("記念日の名前")]
    public string Anniversary;
    [Tooltip("記念日の日付")]
    public string Day;        
    [Tooltip("演出アニメーション")]
    public VideoClip Staging;    
    [Tooltip("ホップアップ画像")]
    public Sprite HopUp;   　 
    [Tooltip("日付の説明")][Multiline(7)]
    public string Commentary;
    
    


    [Header("")]
    [Header("日付に該当する効果がない場合はNoonと入力する")]
    [Header("日付効果、該当する物を書き込む")]
      



    [Tooltip("アイテムの名前")]
    public string ItemName;　　 　　　 
    [Tooltip("その瞬間の移動")]
    public string Move;　　　 　   
    [Tooltip("変更するBGM")]
    public string BGM;             
    [Tooltip("出現するオブジェクト")]
    public string Instance;        
    [Tooltip("パーティクルによる二次演出")]
    public string particle;        
    [Tooltip("次の自分のターンでのダイス")]
    public string NextDice;        
    [Tooltip("次の自分のターンでの移動")]
    public string NextMove;        
    [Tooltip("アイテムを失う")]
    public string ItemLost;        
    [Tooltip("条件付きのポイント付与")]
    public string ConditionalPoint;

    
}
