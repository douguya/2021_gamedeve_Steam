using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneManagaer : MonoBehaviour
{
    public string Main;        //
    public string lobby;
    public static string Lobysend; //
    public string Game;
    public static string Gamesend;  
    public string Result;      //

    // Start is called before the first frame update
    void Start()
    {
        Debug.developerConsoleVisible = false;
        Gamesend = Game;
        Lobysend = lobby;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void TransitionToMain() �@�@�@//�^�C�g���ɔ��
    {
        await Task.Delay(400);
        SceneManager.LoadScene(Main);
    }

    public async void TransitionTolobby() �@�@�@//���r�[�ɔ��
    {
        await Task.Delay(400);
        SceneManager.LoadScene(lobby);
    }



    public async void TransitionToGame() �@�@�@//�Q�[���V�[���ɔ��
    {
        await Task.Delay(400);
        SceneManager.LoadScene(Game);
    }

    public async void TransitionToResult() �@�@�@//���U���g�ɔ��
    {
        await Task.Delay(400);
        SceneManager.LoadScene(Result);
    }
    public async void Quit()//�Q�[���I��
    {
        await Task.Delay(400);
        Application.Quit();
       //  UnityEditor.EditorApplication.isPlaying = false;
    }
}
