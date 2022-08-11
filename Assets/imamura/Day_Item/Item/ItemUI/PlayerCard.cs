using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    public GameObject ItemBlocks;
    public GameObject ItemBlock;
    public GameObject AAA;

    private bool Blocks_bool = false;

    public int num;

    // Start is called before the first frame update
    void Start()
    {
        ItemBlocks.SetActive(Blocks_bool);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 sd = ItemBlock.GetComponent<RectTransform>().sizeDelta;
         sd.y*=num;
        AAA.GetComponent<RectTransform>().sizeDelta=sd;

    }

    public void ItemBlocks_Switch()
    {
        Blocks_bool=!Blocks_bool;
        ItemBlocks.SetActive(Blocks_bool);

    }
}
