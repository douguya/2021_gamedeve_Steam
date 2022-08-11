using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckGroundChange : MonoBehaviour
{
    public Sprite mutairiku;//ムー大陸の画像
    public Sprite ToKyoTower;//東京タワーの画像

    SpriteRenderer sr;// 画像描画用のコンポーネント
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();// SpriteのSpriteRendererコンポーネントを取得
    }

    public void Change(string day)
    {
        switch (Daykouka.DayEffectictDictionary[day][0, 2])
        {
            case "ムー大陸":
                sr.sprite = mutairiku;// ムー大陸の画像に切り替える
                break;

            case "東京タワー":
                sr.sprite = ToKyoTower;// 東京タワーの画像に切り替える
                break;
        }
    }



}
