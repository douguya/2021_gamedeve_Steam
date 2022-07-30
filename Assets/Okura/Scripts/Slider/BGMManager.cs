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
        audiosource.volume = PlayerPrefs.GetFloat("BGMValue", 0.434f);
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
        audiosource.volume = Db2Pa(Value);
        PlayerPrefs.SetFloat("BGMValue", Db2Pa(Value));
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetFloat("BGMValue", 999.9f));
    }

    //�f�V�x�����特���ɕϊ�
    private float Db2Pa(float db)
    {
        db = Mathf.Clamp(db, -80f, 20f);
        return Mathf.Pow(10f, db / 20f);
    }
}
