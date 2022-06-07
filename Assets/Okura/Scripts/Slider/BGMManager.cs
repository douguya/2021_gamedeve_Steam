using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource audiosource;
    musiclist Music;

    //�V�[���J�ڂ��Ă����v
    public static BGMManager Instance
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

    //������
    private void Start()
    {
        audiosource = this.GetComponent<AudioSource>();
        Music = this.GetComponent<musiclist>();
        audiosource.volume = PlayerPrefs.GetFloat("BGMValue", 1.0f) * 0.5f;
        Debug.Log(PlayerPrefs.GetFloat("BGMValue", 999.9f));
    }


    //BGM�炷�p
    public void TitleBGM()
    {
        audiosource.PlayOneShot(Music.TitleBGM);
    }

    public void LobbyBGM()
    {
        audiosource.PlayOneShot(Music.LobbyBGM);
    }

    public void IngameBGM()
    {
        audiosource.PlayOneShot(Music.IngameBGM);
    }

    public void ResultSE()
    {
        audiosource.PlayOneShot(Music.ResultBGM);
    }


    //�X���C�_�[�ŉ��ʒ���
    public void BGMSlider(float Value)
    {
        audiosource.volume = Value;
        PlayerPrefs.SetFloat("BGMValue", Value*0.1f);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetFloat("BGMValue", 999.9f));
    }
}
