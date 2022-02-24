using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayComment : MonoBehaviour
{
    public GameObject daycomment;
    public GameObject ItemImage;
    public GameObject dayTaitl;
    public GameObject comment;
    public GameObject Close;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DayCommenton(string day)
    {
        daycomment.SetActive(true);
        ItemImage.SetActive(true);
        dayTaitl.SetActive(true);
        comment.SetActive(true);
        Close.SetActive(true);
        dayTaitl.GetComponent<Text>().text = DictionaryManager.DayEffectictDictionary[day][0];
        comment.GetComponent<Text>().text = DictionaryManager.DayEffectictDictionary[day][0];
    }

    public void DayCommentoff()
    {
        daycomment.SetActive(false);
        ItemImage.SetActive(false);
        dayTaitl.SetActive(false);
        comment.SetActive(false);
        Close.SetActive(false);
    }

}
