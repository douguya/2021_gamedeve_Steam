using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnimation : MonoBehaviour
{
    I_game_manager I_game_Manager;
    // Start is called before the first frame update
    void Start()
    {
        I_game_Manager = GameObject.Find("I_game_manager").GetComponent<I_game_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoalAnimation_Finish()
    {
        Debug.Log("ゴールアニメーション");
        gameObject.GetComponent<Animator>().SetBool("GoalAnimation", false);
        Animation_After();
    }

    //ここどうしよう
    private void Animation_After()
    {
        I_game_Manager.Player[I_game_Manager.Player_Turn].GetComponent<I_Player_3D>().GoalAnimation_After();
    }
}
