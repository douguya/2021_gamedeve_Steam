using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    private AudioSource audiosource;
    musiclist Music;
    [SerializeField]
    string SEname;

    //シーン遷移しても大丈夫
    public static SEManager Instance
    {
        get; private set;
    }

    void Awake()
    {
        audiosource = this.GetComponent<AudioSource>();
        Instance = this;
    }

    //下準備
    private void Start()
    {
        Music = this.GetComponent<musiclist>();
        audiosource.volume = PlayerPrefs.GetFloat("SEValue", 1.0f);
        Debug.Log(PlayerPrefs.GetFloat("SEValue", 999.9f));
    }


    //スライダーで音量調節
    public void VolumeControl(float Value)
    {
        audiosource.volume = Db2Pa(Value);
        PlayerPrefs.SetFloat("SEValue", Db2Pa(Value));
        PlayerPrefs.Save();
    }

    //デシベルから音圧に変換
    private float Db2Pa(float db)
    {
        db = Mathf.Clamp(db, -80f, 20f);
        return Mathf.Pow(10f, db / 20f);
    }

    //SE鳴らす用
    public void SEsetandplay(string SEname)
    {
        audiosource.clip = (AudioClip)Resources.Load(SEname);
        audiosource.Play();
    }
}
