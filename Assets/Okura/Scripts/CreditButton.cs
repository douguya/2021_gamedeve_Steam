using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButton : MonoBehaviour
{
    [SerializeField]
    int pagenum;
    [SerializeField]
    Transform Canvas;

    private void Start()
    {
        Canvas = this.gameObject.transform.root;
    }

    public void OnClick(string objectName)
    {
        SEManager manager = SEManager.Instance;
        //manager.SEsetandplay();

        // オブジェクトの数だけ処理を分岐
        if ("Credit".Equals(objectName)) this.CreditClick();
        else if ("Back".Equals(objectName)) this.BackClick();
        else if ("Next".Equals(objectName)) this.NextClick();
        else throw new System.Exception("Not implemented!!");
    }

    public void CreditClick() {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void BackClick()
    {
        pagenum = int.Parse(this.transform.parent.name.Substring(4, 1));
        if (pagenum == 1)
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else 
        {
            this.transform.parent.gameObject.SetActive(false);
            Canvas.GetChild(Canvas.childCount - 2).transform.GetChild(pagenum - 2).gameObject.SetActive(true);
        }
    }

    public void NextClick() {
        pagenum = int.Parse(this.transform.parent.name.Substring(4, 1));
        
        this.transform.parent.gameObject.SetActive(false);
        Canvas.GetChild(Canvas.childCount - 2).transform.GetChild(pagenum).gameObject.SetActive(true);
    }

}