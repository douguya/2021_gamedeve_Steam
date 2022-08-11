using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public Anniversary_Item_Master Item_Master;
    public int ItemNumber;
    public Image ItemImage;
    public Text ItemName;
    //public GameObject ItemDetail;
    //private bool Detail_bool = false;
    void Start()
    {
        //ItemDetail.SetActive(Detail_bool);
        ItemImage.sprite=Item_Master.Anniversary_Items[ItemNumber].ItemSprite;
        ItemName.text=Item_Master.Anniversary_Items[ItemNumber].ItemName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Detail_Switch ()
    {
        //Detail_bool=!Detail_bool;
        //ItemDetail.SetActive(Detail_bool);
        
    }


}
