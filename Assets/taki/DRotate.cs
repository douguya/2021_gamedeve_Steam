using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRotate : MonoBehaviour
{
    public float xSpeed, ySpeed, zSpeed;�@//�e���̉�]���x
    public bool rotate; //��]���~�߂�{�^��

    public GameObject[] Dice = new GameObject[6];�@//�_�C�X�̊e�ʂɒ���t���Ă����
    public GameObject max; //��ԏ�̖ʂ̋�
    public int DiceNum; //�o����������̖�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime));
        //��]�����Ă�

        //��]�����̑��x�Ȃ�0�ɂ���
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

        //�~�߂�{�^����������Ă������]���x�𗎂Ƃ��Ă���
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

        //0�ɂȂ����琔���𔻒�
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
            //�e�ʂɒ���t�����󔠂̍���(y)���ׂĈ�ԍ������̂�Ԃ�
            if(max.transform.position.y < Dice[i].transform.position.y)
            {
                max = Dice[i];
                DiceNum = i + 1;
            }
        }
    }
}
