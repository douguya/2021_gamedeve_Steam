using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagaer : MonoBehaviour
{
    public string Main;        //
    public string lobby;
    public static string Lobysend; //
    public string Game;
    public static string Gamesend;
    public string Result;      //

    //-----------------------------------------大蔵-----------------------------------------
    public GameObject OptionButton;
    public GameObject OptionWindow;
    //---------------------------------------ここまで-----------------------------------------

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.developerConsoleVisible = false;
        Gamesend = Game;
        Lobysend = lobby;
    }

    // Update is called once per frame
    void Update()
    {




    }

    public void TransitionToMain() 　　　//タイトルに飛ぶ
    {


        StartCoroutine(Scene_transition(Main));

    }

    public void TransitionTolobby() 　　　//ロビーに飛ぶ
    {
        StartCoroutine(Scene_transition(lobby));

    }



    public void TransitionToGame() 　　　//ゲームシーンに飛ぶ
    {
        StartCoroutine(Scene_transition(Game));
    }

    public void TransitionToResult() 　　　//リザルトに飛ぶ
    {

        StartCoroutine(Scene_transition(Result));
    }


    public IEnumerator Scene_transition(string Sene)
    {

        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(Sene);

        yield break;
    }


    //-----------------------------------------大蔵-----------------------------------------

    public void DisplayOptionWindow() 　　　//オプション画面を表示する
    {
        GameObject.Find("I_game_manager").GetComponent<Guide>().option_BottonFinish();
        OptionButton.SetActive(false);
        OptionWindow.SetActive(true);
    }

    public void UnDisplayOptionWindow() 　　　//オプション画面を非表示にする
    {
        OptionButton.SetActive(true);
        OptionWindow.SetActive(false);
    }

    //---------------------------------------ここまで-----------------------------------------

}