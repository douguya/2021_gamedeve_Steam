using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Item : MonoBehaviour
{
    public Text text;
    public Anniversary_Item_Master Item;
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text=(Item.Anniversary_Items[num].ItemName+"\n"+
                   Item.Anniversary_Items[num].Anniversary+"\n"+
                   Item.Anniversary_Items[num].Day+"\n"+
                   Item.Anniversary_Items[num].ItemLifespan+"\n"+
                   Item.Anniversary_Items[num].Commentary+"\n"+
       
                   Item.Anniversary_Items[num].ItemEffect);
    }
}
