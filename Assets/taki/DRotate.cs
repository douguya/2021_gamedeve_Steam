using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRotate : MonoBehaviour
{
    public float xSpeed, ySpeed, zSpeed;　//各軸の回転速度
    public bool rotate; //回転を止めるボタン

    public GameObject[] Dice = new GameObject[6];　//ダイスの各面に張り付けてある空箱
    public GameObject max; //一番上の面の空箱
    public int DiceNum; //出たさいころの目

    private float xKeep, yKeep, zKeep; //回転速度の保存用

    public float xShow, zShow; //さいころの目を見せるときの角度

    // Start is called before the first frame update
    void Start()
    {
        xKeep = xSpeed;
        yKeep = ySpeed;
        zKeep = zSpeed;
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

        //回転が負の速度なら0にする
        if (xSpeed < 0)
        {
            xSpeed = 0;
        }

        if (ySpeed < 0)
        {
            ySpeed = 0;
        }

        if (zSpeed < 0)
        {
            zSpeed = 0;
        }

        //止めるボタンが押されていたら回転速度を落としていく
        if (rotate == false)
        {
            if (xSpeed > 0)
            {
                xSpeed -= 30f * Time.deltaTime;
            }

            if (ySpeed > 0)
            {
                ySpeed -= 30f * Time.deltaTime;
            }

            if (zSpeed > 0)
            {
                zSpeed -= 30f * Time.deltaTime;
            }
        }

        //0になったら数字を判定
        if (xSpeed == 0 && ySpeed == 0 && zSpeed == 0) {
            DiceStop();
            Debug.Log(max);

            //出た目の数によって向く方向をセット
            switch (DiceNum)
            {
                case 1:
                    xShow = -30; zShow = 0;
                    break;
                case 2:
                    xShow = -30; zShow = 90;
                    break;
                case 3:
                    xShow = 60; zShow = 0;
                    break;
                case 4:
                    xShow = -120; zShow = 0;
                    break;
                case 5:
                    xShow = -30; zShow = -90;
                    break;
                case 6:
                    xShow = 150; zShow = 0;
                    break;
            }

            //出た目の面がこっちを向く
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xShow, 0, zShow), 1.0f * Time.deltaTime);

        }

    }

    public void DiceStop()
    {
        max = Dice[0];
        DiceNum = 1;
        for (int i = 1; i < 6; i++)
        {
            //各面に張り付けた空箱の高さ(y)を比べて一番高いものを返す
            if(max.transform.position.y < Dice[i].transform.position.y)
            {
                max = Dice[i];
                DiceNum = i + 1;
            }
        }


    }
}
