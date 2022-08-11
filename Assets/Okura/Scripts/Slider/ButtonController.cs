using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void OnClick(string objectName)
    {
        SEManager sEManager = SEManager.Instance;
        sEManager.SEsetandplay("Decition1");

        // オブジェクトの数だけ処理を分岐
        if ("Scene1".Equals(objectName)) this.Button1Click();
        else if ("Scene2".Equals(objectName)) this.Button2Click();
        else if ("Scene3".Equals(objectName)) this.Button3Click();
        else throw new System.Exception("Not implemented!!");
    }

    void Button1Click()
    {
        SceneManager.LoadScene("TestScene");
    }

    void Button2Click()
    {
        SceneManager.LoadScene("TestScene2");
    }

    void Button3Click()
    {
        SceneManager.LoadScene("TestScene3");
    }
}
