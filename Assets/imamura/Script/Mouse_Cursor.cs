using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Mouse_Cursor : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    Vector3 MousePosition;
    public float a=0;
    public float b = 0;
    void Start()
    {
       
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
}
