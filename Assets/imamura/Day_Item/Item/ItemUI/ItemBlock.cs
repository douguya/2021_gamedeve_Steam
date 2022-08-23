using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class ItemBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public Anniversary_Item_Master Item_Master;
    public int ItemNumber;
    public Image ItemImage;
    public Text ItemName;
    public GameObject ItemProperty;
    public Text PropertyText;

    public bool Rist_View = false;
    public List<GameObject> OtherList;
    //public GameObject ItemDetail;
    //private bool Detail_bool = false;
    void Start()
    {
        ItemProperty.SetActive(Rist_View);

    }

    // Update is called once per frame
    void Update()
    {
    
        if (EventSystem.current.IsPointerOverGameObject()==false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ItemProperty_Hide();
            }

        }

      

    }

    public void Detail_Switch ()
    {
        var ItemInfo = Item_Master.Anniversary_Items[ItemNumber];
        ItemImage.sprite= ItemInfo.ItemSprite;
        ItemName.text= ItemInfo.ItemName;
        PropertyText.text=ItemInfo.Day+" "+ItemInfo.Anniversary+"\n"+ItemInfo.ItemPoint+"ポイント";
    }
    public void ItemProperty_View()
    {
        Rist_View=!Rist_View;
        ItemProperty.SetActive(Rist_View);
        if (Rist_View==true)
        {
            ItemProperty_Hide_other();
        }
    }
    public void ItemProperty_Hide()
    {
        Rist_View=false;
        ItemProperty.SetActive(Rist_View);


       


    }

    public void ItemProperty_Hide_other()
    {

        OtherList.Clear();

        GameObject[] Other = GameObject.FindGameObjectsWithTag("ItemProperty");//プレイヤーオブジェクトの一時保存場所　タグで軒並みとる

        foreach (GameObject obj in Other)
        {
            if (obj!=ItemProperty)
            {
                OtherList.Add(obj);
            }
        }

        
         foreach (GameObject obj in OtherList)
         {
            var ota= obj.transform.parent.gameObject;
            Debug.Log(ota);
            ota.GetComponent<ItemBlock>().ItemProperty_Hide();
           
         }


    }


    public void OnDisable()
    {
        ItemProperty_Hide();
    }
}
