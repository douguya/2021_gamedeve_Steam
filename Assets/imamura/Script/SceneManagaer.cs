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

    public void TransitionToMain() �@�@�@//�^�C�g���ɔ��
    {


        StartCoroutine(Scene_transition(Main));

    }

    public void TransitionTolobby() �@�@�@//���r�[�ɔ��
    {
        StartCoroutine(Scene_transition(lobby));

    }



    public void TransitionToGame() �@�@�@//�Q�[���V�[���ɔ��
    {
        StartCoroutine(Scene_transition(Game));
    }

    public void TransitionToResult() �@�@�@//���U���g�ɔ��
    {

        StartCoroutine(Scene_transition(Result));
    }


    public IEnumerator Scene_transition(string Sene)
    {

        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(Sene);

        yield break;
    }
}