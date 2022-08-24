using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class I_Day_Animation : MonoBehaviour
{

    private VideoClip Day_video; //再生するビデオを格納
    //public video[] video_folder = new video[31]; //日付で再生するビデオを格納
    public videos[] Month = new videos[12];

    [System.Serializable]
    public class videos//weekの子・横列のオブジェクトの取得
    {
        public VideoClip[] Day = new VideoClip[31];
    }

    //日付を受けとり対応するビデオを渡す
    public VideoClip play_video(string Day)
    {
        video_select(Day); //対応するビデオを探す
        return Day_video; //ビデオを渡す
    }

    //日付から対応するビデオを探す
    private void video_select(string Day)
    {
        char[] Char_day = Day.ToCharArray(); //日付をchar型に変換
        int video_month = Toint(Char_day[0]); //日付の月を取得
        int video_day = Toint(Char_day[2]); //日付の日数を取得
        Day_video = Month[video_month - 1].Day[video_day - 1]; //日付に対応するビデオを格納する
    }

    //char型をint型に変換する
    private int Toint(char self)
    {
        return self - '0';
    }
}


