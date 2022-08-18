using System;
using UnityEngine;



[Serializable]

[CreateAssetMenu(fileName = "Anniversary_Item", menuName = "Anniversary_Item")]
public class Anniversary_Item : ScriptableObject
{




    [Tooltip("アイテムの名前")]
    public string ItemName;
    [Tooltip("アイテムのポイント")]
    public int ItemPoint;
    [Tooltip("記念日の名前")]
    public string Anniversary;
    [Tooltip("記念日の日付")]
    public string Day;
    [Tooltip("アイテムの画像")]
    public Sprite ItemSprite;
    [Tooltip("アイテムの寿命　ない場合はnull")]
    public string ItemLifespan;
     

    [Tooltip("アイテムの説明　使用効果がある場合はそれを含む")]
    [Multiline(7)]
    public string Commentary;

    [Tooltip("アイテム仕様効果の有無 ある場合はtrue")]
    public bool ItemEffect;


    
    [Tooltip("その瞬間の移動")]
    public string Move;
    [Tooltip("変更するBGM")]
    public string BGM;
    [Tooltip("出現するオブジェクト")]
    public string Instance;
    [Tooltip("パーティクルによる二次演出")]
    public string particle;
    [Tooltip("ダイスへの影響")]
    public string DiceInfluence;
    [Tooltip("アイテムを失う")]
    public string ItemLost;
    [Tooltip("条件付きのポイント付与")]
    public string ConditionalPoint;


}

