using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLineAdjust : MonoBehaviour
{
    [SerializeField]
    public Text ItemNameTextBox;
    [SerializeField]
    public float Rectheight;
    public int LineNum;

    public void LineAdjust()
    {
        ItemNameTextBox = this.GetComponent<Text>();
        var TextBoxRect = ItemNameTextBox.GetComponent<RectTransform>();

        float TotalLength = ItemNameTextBox.fontSize * ItemNameTextBox.lineSpacing;
        float LineQuantity = Mathf.CeilToInt(ItemNameTextBox.preferredWidth / TextBoxRect.sizeDelta.x);
        Rectheight = TotalLength * LineQuantity;

        TextBoxRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, TotalLength * LineQuantity);
        Debug.Log(ItemNameTextBox.text);
        Debug.Log(LineQuantity);

        LineNum= (int)LineQuantity;
    }
}
