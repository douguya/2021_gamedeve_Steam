using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newRotate : MonoBehaviour
{
    public float xSpeed, ySpeed, zSpeed;　//各軸の回転速度
    public bool rotate; //回転を止めるボタン

    public int DiceNum; //出たさいころの目

    private float xKeep, yKeep, zKeep; //回転速度の保存用
    private float xShow, zShow; //さいころの目を見せるときの角度

    //廃止　public GameObject[] Dice = new GameObject[6];　//ダイスの各面に張り付けてある空箱
    //廃止　public GameObject max; //一番上の面の空箱

    public List<int> InDiceNum = new List<int> { 1, 2, 3, 4, 5, 6 }; //指定された数がさいころから出る

    // Start is called before the first frame update
    void Start()
    {
        xKeep = xSpeed;
        yKeep = ySpeed;
        zKeep = zSpeed;
        newDiceStop();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime));
        //回転させてる

        //trueの場合常に回転する
        if (rotate == true)
        {
            xSpeed = xKeep;
            ySpeed = yKeep;
            zSpeed = zKeep;
        }


    }

    //回すときの呼び出し用関数
    public void RotateStart()
    {
        rotate = true;
    }

    //ストップするときの呼び出し用関数
    public void newDiceStop()
    {
        //出た目の数をランダムで生成
        for (; ; )
        {
            DiceNum = Random.Range(1, 7);
            if (InDiceNum.Contains(DiceNum) == true)
            {
                break;//数字が許可されていれば通す
            }
        }

        //さいころを停止
        rotate = false;
        xSpeed = 0;
        ySpeed = 0;
        zSpeed = 0;

        //出た目から方向をセット
        switch (DiceNum)
        {
            case 1:
                xShow = 0; zShow = 0;
                break;
            case 2:
                xShow = 0; zShow = 90;
                break;
            case 3:
                xShow = -90; zShow = 0;
                break;
            case 4:
                xShow = 90; zShow = 0;
                break;
            case 5:
                xShow = 0; zShow = -90;
                break;
            case 6:
                xShow = 180; zShow = 0;
                break;
        }
        //さいころの目を見せる
        transform.rotation = Quaternion.Euler(xShow, 0, zShow);
    }

    public void OddDice() //奇数ダイスになる
    {
        InDiceNum.Clear();
        InDiceNum.Add(1);
        InDiceNum.Add(3);
        InDiceNum.Add(5);
    }

    public void EvenDice()　//偶数ダイスになる
    {
        InDiceNum.Clear();
        InDiceNum.Add(2);
        InDiceNum.Add(4);
        InDiceNum.Add(6);
    }

    public void resetDice()
    {
        InDiceNum.Clear();
        InDiceNum.Add(1);
        InDiceNum.Add(2);
        InDiceNum.Add(3);
        InDiceNum.Add(4);
        InDiceNum.Add(5);
        InDiceNum.Add(6);
    }
}
