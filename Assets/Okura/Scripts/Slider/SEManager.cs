using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    private AudioSource audiosource;
    musiclist Music;

    //�V�[���J�ڂ��Ă����v
    public static SEManager Instance
    {
        get; private set;
    }

    void Awake()
    {
        Instance = this;
    }

    //������
    private void Start()
    {
        audiosource = this.GetComponent<AudioSource>();
        Music = this.GetComponent<musiclist>();
        audiosource.volume = PlayerPrefs.GetFloat("SEValue", 1.0f);
        Debug.Log(PlayerPrefs.GetFloat("SEValue", 999.9f));
    }


    //SE�炷�p
    public void DecisionSE()
    {
        audiosource.PlayOneShot(Music.DecisionSE);
    }

    public void BackSE()
    {
        audiosource.PlayOneShot(Music.BackSE);
    }

    public void GameStartSE()
    {
        audiosource.PlayOneShot(Music.GameStartSE);
    }

    public void DiceSE()
    {
        audiosource.PlayOneShot(Music.DiceSE);
    }

    public void WalkSE()
    {
        audiosource.PlayOneShot(Music.WalkSE);
    }

    public void WarpSE()
    {
        audiosource.PlayOneShot(Music.WarpSE);
    }

    public void OpenSE()
    {
        audiosource.PlayOneShot(Music.OpenSE);
    }

    public void GetSE()
    {
        audiosource.PlayOneShot(Music.GetSE);
    }

    public void GoalSE()
    {
        audiosource.PlayOneShot(Music.GoalSE);
    }

    public void GameEndSE()
    {
        audiosource.PlayOneShot(Music.GameEndSE);
    }

    public void resultSE()
    {
        audiosource.PlayOneShot(Music.resultSE);
    }

    //�X���C�_�[�ŉ��ʒ���
    public void SESlider(float Value)
    {
        audiosource.volume = Db2Pa(Value);
        PlayerPrefs.SetFloat("SEValue", Db2Pa(Value));
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetFloat("SEValue", 999.9f));
    }

    //�f�V�x�����特���ɕϊ�
    private float Db2Pa(float db)
    {
        db = Mathf.Clamp(db, -80f, 20f);
        return Mathf.Pow(10f, db / 20f);
    }
}
