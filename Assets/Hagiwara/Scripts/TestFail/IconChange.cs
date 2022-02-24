using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconChange : MonoBehaviour
{
    public Sprite yuurei;//幽霊の画像
    public Sprite Tsyatu;//Tシャツの画像
    public Sprite bikini;//ビキニの画像
    public Sprite Default;//通常画像

    private Image m_Image;// Image コンポーネントを格納する変数

    void Start()
    {
        m_Image = GetComponent<Image>();
    }

    public Sprite Change(string day)
    {
        switch (Daykouka.DayEffectictDictionary[day][0, 2])
        {
            case "幽霊":
                return yuurei;// 幽霊の画像に切り替える

            case "Tシャツ":
                return Tsyatu;// Tシャツの画像に切り替える

            case "ビキニ":
                return bikini;// ビキニの画像に切り替える
                
            default:
                return Default;// 通常の画像に切り替える
        }

    }
}
