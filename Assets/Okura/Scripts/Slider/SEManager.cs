using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    private AudioSource audiosource;
    musiclist Music;
    [SerializeField]
    string SEname;

    //�V�[���J�ڂ��Ă����v
    public static SEManager Instance
    {
        get; private set;
    }

    void Awake()
    {
        audiosource = this.GetComponent<AudioSource>();
        Instance = this;
    }

    //������
    private void Start()
    {
        Music = this.GetComponent<musiclist>();
        audiosource.volume = PlayerPrefs.GetFloat("SEValue", 1.0f);
        Debug.Log(PlayerPrefs.GetFloat("SEValue", 999.9f));
    }


    //�X���C�_�[�ŉ��ʒ���
    public void VolumeControl(float Value)
    {
        audiosource.volume = Db2Pa(Value);
        PlayerPrefs.SetFloat("SEValue", Db2Pa(Value));
        PlayerPrefs.Save();
    }

    //�f�V�x�����特���ɕϊ�
    private float Db2Pa(float db)
    {
        db = Mathf.Clamp(db, -80f, 20f);
        return Mathf.Pow(10f, db / 20f);
    }

    //SE�炷�p
    public void SEsetandplay(string SEname)
    {
        switch (SEname) {
            case "TitleBGM": audiosource.clip = Music.TitleBGM; break;
            case "LobbyBGM": audiosource.clip = Music.LobbyBGM; break;
            case "IngameBGM": audiosource.clip = Music.IngameBGM; break;
            case "ResultBGM": audiosource.clip = Music.ResultBGM; break;
            case "DecisionSE": audiosource.clip = Music.DecisionSE; break;
            case "BackSE": audiosource.clip = Music.BackSE; break;
            case "GameStartSE": audiosource.clip = Music.GameStartSE; break;
            case "DiceSE": audiosource.clip = Music.DiceSE; break;
            case "WalkSE": audiosource.clip = Music.WalkSE; break;
            case "WarpSE": audiosource.clip = Music.WarpSE; break;
            case "OpenSE": audiosource.clip = Music.OpenSE; break;
            case "GetSE": audiosource.clip = Music.GetSE; break;
            case "GoalSE": audiosource.clip = Music.GoalSE; break;
            case "GameEndSE": audiosource.clip = Music.GameEndSE; break;
            case "resultSE": audiosource.clip = Music.resultSE; break;
            case "HandClapJingle": audiosource.clip = Music.HandClapJingle; break;
            case "ResultJingle": audiosource.clip = Music.resultSE; break;
            case "FinalResultJingle": audiosource.clip = Music.FinalResultJingle; break;
        }

        audiosource.Play();
    }
}