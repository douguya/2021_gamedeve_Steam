using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class I_Mass_3D : MonoBehaviour
{

    public bool Open;//マスが空いてるかどうか
    public bool Goal;//マスがゴールかどうか
    public bool warp;//マスがワープ出来るマスか
    public bool decision;//マスは移動決定されてるか
    public bool On_Click;
    public string Day = "null";//マスは何日か


    public GameObject goal_flag;//ゴールの丸表示用
    public GameObject warp_point;//移動できるマスの表示用
    public GameObject hideCover;//マスの表示用
    public GameObject select;//移動できるマスの表示用
    public GameObject decision_Mass;//決定したマスの表示用
    public GameObject DayText;//マスの日付用

    void Start()
    {

    }

    void Update()
    {

    }

    public void hideCover_setting()
    {
        hideCover.SetActive(true);
        Open = false;
    }

    public void hideCover_Clear()
    {
        hideCover.SetActive(false);
        Open = true;
    }

    public void warp_setting()
    {
        warp = true;
        warp_point.SetActive(true);
    }

    public void Goal_setting()
    {
        Goal = true;
        goal_flag.SetActive(true);
    }

    public void Goal_Clear()
    {
        Goal = false;
        goal_flag.SetActive(false);
    }

    public void select_display()
    {
        select.SetActive(true);
    }

    public void select_hidden()
    {
        select.SetActive(false);
    }

    public void decision_display()
    {
        decision_Mass.SetActive(true);
        decision = true;
    }

    public void decision_hidden()
    {
        decision_Mass.SetActive(false);
        decision = false;
    }

    public void OnClick()
    {
        if (select.activeSelf == true)
        {
            select_hidden();
            decision_display();
            On_Click = true;
            GameObject.Find("I_game_manager").GetComponent<I_game_manager>().Player_select();
        }

    }
    public void DayText_setting(int day)
    {
        DayText.GetComponent<TextMesh>().text = "" + day;
    }
}

