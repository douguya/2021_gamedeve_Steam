using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : MonoBehaviour
{
    public bool Open;//マスが空いてるかどうか
    public bool Goal;//マスがゴールかどうか
    public bool invalid;//そのマスが有効かどうか
    public bool Loot;//マスが移動マスとして選択されているかどうか
    public bool walk;//onClickされたかどうか調べる

    public GameObject GoalFlag;//ゴールの丸表示用
    public GameObject hako;//マスの表示用
    public GameObject select;//移動できるマスの表示用
    public GameObject decision;//移動できるマスの表示用

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

    public void Selecton()//移動できるマスの表示用
    {
        select.SetActive(true);
    }

    public void Decisionon()//移動できるマスの決定表示用
    {
        decision.SetActive(true);
    }

    public void Selectoff()//移動できるマスの非表示用
    {
        select.SetActive(false);
    }

    public void Decisionoff()//移動できるマスの決定非表示用
    {
        decision.SetActive(false);
    }

    public void onClick()
    {
        if(select.activeSelf == true)
        {
            Selectoff();
            Decisionon();
            Loot = true;
            walk = true;
        }
        
    }

}
