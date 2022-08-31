using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIMpalse : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Gain()
    {
      GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulse();
        Debug.Log("GURARARA");
    }
}
