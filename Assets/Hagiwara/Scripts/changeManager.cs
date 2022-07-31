using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeManager : MonoBehaviour
{
    public int[] month = new int[4];                //設置する月を受け取る
    public changes_Width[] changes_height = new changes_Width[10]; //sugorokuManagreのheight[]width[]の代わり

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public class changes_Width//weekの子・横列のオブジェクトの取得
    {
        public GameObject[] changes_width;




    }

    //追加
    private void MonthSetting()//マスに月と日付を入れる
    {
        int Xmonth = 0;//設置するマップ分Xの配列をずらす
        int Ymonth = 0;//設置するマップ分Yの配列をずらす

        for (int block = 0; block < this.month.Length; block++)//指定する月がどのブロックにいるか判別
        {
            switch (block)//それぞれのブロックに指定した日付を入れるようにする
            {
                case 0:
                    Xmonth = 0; Ymonth = 0;
                    break;
                case 1:
                    Xmonth = changes_height[0].changes_width.Length / 2; Ymonth = 0;
                    break;
                case 2:
                    Xmonth = 0; Ymonth = changes_height.Length / 2;
                    break;
                case 3:
                    Xmonth = changes_height[0].changes_width.Length / 2; Ymonth = changes_height.Length / 2;
                    break;
            }
            for (int month = 0; month < 12; month++)//monthに何月か入れる
            {
                if (this.month[block] == month + 1)//指定した月が何月か判別する
                {
                    DaySetting(month, Ymonth, Xmonth);//マスに日付を入れる
                }
            }
        }

    }

    //追加
    private void DaySetting(int month, int Ymonth, int Xmonth)//マスに日付を入れる
    {
        int nullday = 0;//空白の日付
        int countday = 0;//入れる日付
        int Maxday = 0;//その月の最大日付

        switch (month + 1)
        {
            case 1:
                nullday = 6; Maxday = 31;
                break;
            case 2:
                nullday = 2; Maxday = 28;
                break;
            case 3:
                nullday = 2; Maxday = 31;
                break;
            case 4:
                nullday = 5; Maxday = 30;
                break;
            case 5:
                nullday = 0; Maxday = 31;
                break;
            case 6:
                nullday = 3; Maxday = 30;
                break;
            case 7:
                nullday = 5; Maxday = 31;
                break;
            case 8:
                nullday = 1; Maxday = 31;
                break;
            case 9:
                nullday = 4; Maxday = 30;
                break;
            case 10:
                nullday = 6; Maxday = 31;
                break;
            case 11:
                nullday = 2; Maxday = 30;
                break;
            case 12:
                nullday = 4; Maxday = 31;
                break;
        }
        for (int n = 0; n < 7 - nullday; n++)//第一週目に日付を入れる
        {
            countday++;
            changes_height[Ymonth].changes_width[Xmonth + nullday + n].GetComponent<Mass>().Day = month + 1 + "/" + countday;
        }

        for (int h = 1; h < changes_height.Length / 2; h++)//第二週以降の日付を入れる
        {
            for (int w = 0; w < changes_height[0].changes_width.Length / 2; w++)
            {
                if (countday < Maxday)
                {
                    countday++;
                    changes_height[Ymonth + h].changes_width[Xmonth + w].GetComponent<Mass>().Day = month + 1 + "/" + countday;
                }
            }
        }
    }
}
