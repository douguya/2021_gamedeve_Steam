using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemDoropDown : MonoBehaviour
{
    // Start is called before the first frame update]]

    [SerializeField]
    private Dropdown dropdown;

    void Start()


    {
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void addedItem(string Item)
    {
        
        dropdown.options.Add(new Dropdown.OptionData { text = Item+ DectionariManager.ItemDictionary[Item][0]+"P" });
        dropdown.RefreshShownValue();
    }








}
