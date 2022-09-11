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
        Music = this.GetComponent<musiclist>();
        audiosource.volume = PlayerPrefs.GetFloat("SEValue", 1.0f);
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
        switch (SEname) {
            case "TitleBGM": audiosource.clip = Music.TitleBGM; break;
            case "LobbyBGM": audiosource.clip = Music.LobbyBGM; break;
            case "IngameBGM": audiosource.clip = Music.IngameBGM; break;
            case "ResultBGM": audiosource.clip = Music.ResultBGM; break;

            case "DecisionSE": audiosource.clip = Music.DecisionSE; break;
            case "MoveOn": audiosource.clip = Music.MoveOn; break;
            case "BackSE": audiosource.clip = Music.BackSE; break;
            case "SceneSwitching": audiosource.clip = Music.SceneSwitching; break;
            case "LogSE": audiosource.clip = Music.LogSE; break;

            case "GameStartSE": audiosource.clip = Music.GameStartSE; break;
            case "DiceSE": audiosource.clip = Music.DiceSE; break;
            case "WalkSE": audiosource.clip = Music.WalkSE; break;
            case "WarpSE": audiosource.clip = Music.WarpSE; break;
            case "OpenSE": audiosource.clip = Music.OpenSE; break;
            case "GetSE": audiosource.clip = Music.GetSE; break;
            case "GoalSE": audiosource.clip = Music.GoalSE; break;
            case "GameEndSE": audiosource.clip = Music.GameEndSE; break;
            case "TextSE": audiosource.clip = Music.TextSE; break;

            case "resultSE": audiosource.clip = Music.resultSE; break;

            case "HandClapJingle": audiosource.clip = Music.HandClapJingle; break;
            case "ResultJingle": audiosource.clip = Music.ResultJingle; break;
            case "FinalResultJingle": audiosource.clip = Music.FinalResultJingle; break;
        }

        audiosource.Play();
    }
}