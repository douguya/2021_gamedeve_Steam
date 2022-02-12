using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourseController : MonoBehaviour
{
    public bool DontDestroyEnabled = true;

    public static AudioSourseController Instance
    {
        get; private set;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
