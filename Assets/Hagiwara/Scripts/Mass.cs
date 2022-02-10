using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : MonoBehaviour
{
    public bool Open;//マスが空いてるかどうか
    public bool Goal;//マスがゴールかどうか
    public bool invalid;//そのマスが有効かどうか
    public GameObject GoalFlag;//ゴールの丸表示用
    public GameObject hako;//マスの表示用

    void Start()
    {
        //Open = false;
        //Goal = false;
        //invalid = false;
        //GoalFlag.SetActive(false);

    }

   
    void Update()
    {
        if(Goal == true)
        {
            GoalFlag.SetActive(true);
        }
        if (invalid == true)
        {
            hako.SetActive(false);
        }
    }

    public void GoalOn()
    {

    }

    
}
