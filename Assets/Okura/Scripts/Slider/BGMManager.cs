using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource audiosource;
    musiclist Music;

    //シーン遷移しても大丈夫
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

    //下準備
    private void Start()
    {
        audiosource = this.GetComponent<AudioSource>();
        Music = this.GetComponent<musiclist>();
        audiosource.volume = PlayerPrefs.GetFloat("BGMValue", 0.434f);
        Debug.Log(PlayerPrefs.GetFloat("BGMValue", 999.9f));
    }


    //BGM鳴らす用
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


    //スライダーで音量調節
    public void BGMSlider(float Value)
    {
        audiosource.volume = Db2Pa(Value);
        PlayerPrefs.SetFloat("BGMValue", Db2Pa(Value));
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetFloat("BGMValue", 999.9f));
    }

    //デシベルから音圧に変換
    private float Db2Pa(float db)
    {
        db = Mathf.Clamp(db, -80f, 20f);
        return Mathf.Pow(10f, db / 20f);
    }
}
