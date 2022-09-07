using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICountact : MonoBehaviour
{
    public Camera_Mouse Canera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UIEnter()
    {
        
        Canera.Permission_Zoom=false;
    }
    public void UIExit()
    {

        Canera.Permission_Zoom=true;
    }

}
