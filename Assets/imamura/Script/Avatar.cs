using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Avatar : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        var nameLabel = GetComponent<TextMeshPro>();
        Debug.Log(photonView.Owner.NickName);
         nameLabel.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
