using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{

    [SerializeField]
    Text textUI;
    [SerializeField]
    Transform canvas;
    [SerializeField]
    Button Itemadd;
    [SerializeField]
    Button display;
    

    List<string> HaveItem = new List<string>(/*PlayerStatasOkura.HabItem*/);

    public void AddItem(string Item)
    {
        HaveItem.Add(Item);
    }

    public void DisplayItems()
    {
        int count = 0;
        foreach (string i in HaveItem)
        {
            Text Copytext = Instantiate(textUI, new Vector3(0.0f, (count * -30) + 80.0f, 0.0f), Quaternion.identity);
            count++;
            Copytext.transform.SetParent(canvas, false);
            Copytext.text = i + "   " +DictionaryManager.ItemDictionary[i][0] + "P";
        }
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
