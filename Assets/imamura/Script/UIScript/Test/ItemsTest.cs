using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsTest : MonoBehaviour
{
    public Anniversary_Item_Master ItamData;
    // Start is called before the first frame update


    public Text Text_ItemName;
    public Text Text_ItemPoint;
    public Text Text_Anniversary;
    public Text Text_Day;

    public Text Text_classification;
    public Text Text_ItemLifespan;


    public Image ItemImage;
    public int Itemnum = 0;
    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        Text_ItemName.text=ItamData.Anniversary_Items[Itemnum].ItemName;
        Text_ItemPoint.text=""+ItamData.Anniversary_Items[Itemnum].ItemPoint;
        Text_Anniversary.text=ItamData.Anniversary_Items[Itemnum].Anniversary;
        Text_Day.text=ItamData.Anniversary_Items[Itemnum].Day;

        Text_classification.text=ItamData.Anniversary_Items[Itemnum].classification;
        Text_ItemLifespan.text=ItamData.Anniversary_Items[Itemnum].ItemLifespan;
    
        ItemImage.sprite=ItamData.Anniversary_Items[Itemnum].ItemSprite;
    }


    public void Axs()
    {
        if (Itemnum<ItamData.Anniversary_Items.Count)
        {
            Itemnum++;
        }
    }
    public void Back()
    {
        if (Itemnum>0)
        {
            Itemnum--;
        }
    }

}
