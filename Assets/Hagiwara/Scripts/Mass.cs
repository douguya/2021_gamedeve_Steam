using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Mass : MonoBehaviour
{
    public bool Open;//マスが空いてるかどうか
    public bool Goal;//マスがゴールかどうか
    public bool invalid;//そのマスが存在するかどうか
    public bool walk;//onClickされたかどうか調べる
    public string Day;//マスは何日か

    public GameObject GoalFlag;//ゴールの丸表示用
    public GameObject hako;//マスの表示用
    public GameObject select;//移動できるマスの表示用
    public GameObject decision;//移動できるマスの表示用

    void Start()
    {
        if (invalid == true)
        {
            hako.SetActive(false);
        }
    }

   
    void Update()
    {
        
    }

    public void GoalOn()//ゴールを表示させる
    {
        GoalFlag.SetActive(true);
        Goal = true;
    }

    public void GoalOff()//ゴールを消す
    {
        GoalFlag.SetActive(false);
        Goal = false;
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
        Debug.Log("AAA");
        if (select.activeSelf == true)
        {
           
            Selectoff();
            Decisionon();
            walk = true;
        }
        
    }

}
