using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class Mouse_Cursor : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    Vector3 MousePosition;
    public float a=0;
    public float b = 0;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        var rote = GetComponent<RectTransform>().rotation;

        var nextrote = 28.689f- rote.z;


        GetComponent<RectTransform>().Rotate(0, 0, nextrote);
       

        if (SceneManager.GetActiveScene().name=="T1")
        {
            Destroy(this.gameObject);
        }

        if (SceneManager.GetActiveScene().name=="Result")
        {
            var maz = GameObject.FindWithTag("MouseMazer");
            GameObject[] Cursors = GameObject.FindGameObjectsWithTag("cursor");

            foreach (GameObject Cursor in Cursors)//対応するプレイヤーにプレイヤーリストを突っ込む
            {
                Cursor.transform.parent=maz.transform;

                Cursor.GetComponent<Mouse_Cursor>().RotateChange();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine) {
            MousePosition= Input.mousePosition;

            MousePosition.x+=a;
            MousePosition.y+=b;

            this.transform.position =  MousePosition;

            MouseInScreen(MousePosition);
        }
        DontDestroyOnLoad(this.gameObject);

        if (SceneManager.GetActiveScene().name=="T1")
        {
            Destroy(this.gameObject);
        }

    }


    public void MouseInScreen(Vector3 MousePosition)
    {

        if (MousePosition.y<Screen.height&&MousePosition.y>0&&MousePosition.x<Screen.width&&MousePosition.x>0)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

    }


    public void RotateChange()
    {
    }
}
