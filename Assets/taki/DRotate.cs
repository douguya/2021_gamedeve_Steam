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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime));
        //回転させてる

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
        if (xSpeed == 0) {
            DiceStop();
            Debug.Log(max);

            if (DiceNum == 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(30f, 0, 0), 1.0f * Time.deltaTime);
            }
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
