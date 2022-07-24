using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newRotate : MonoBehaviour
{
    public float xSpeed, ySpeed, zSpeed;�@//�e���̉�]���x
    public bool rotate; //��]���~�߂�{�^��

    public int DiceNum; //�o����������̖�

    private float xKeep, yKeep, zKeep; //��]���x�̕ۑ��p
    private float xShow, yShow; //��������̖ڂ�������Ƃ��̊p�x

    //�p�~�@public GameObject[] Dice = new GameObject[6];�@//�_�C�X�̊e�ʂɒ���t���Ă����
    //�p�~�@public GameObject max; //��ԏ�̖ʂ̋�

    public List<int> InDiceNum = new List<int> {1, 2, 3, 4, 5, 6}; //�w�肳�ꂽ�����������납��o��

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
        //��]�����Ă�

        //true�̏ꍇ��ɉ�]����
        if (rotate == true)
        {
            xSpeed = xKeep;
            ySpeed = yKeep;
            zSpeed = zKeep;
        }

        //�f�o�b�O�p�ŉE�L�[
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotate = true;
        }

        //�f�o�b�O�p�ō��L�[
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newDiceStop();
        }
    }

    //�񂷂Ƃ��̌Ăяo���p�֐�
    public void RotateStart()
    {
        rotate = true;
    }

    //�X�g�b�v����Ƃ��̌Ăяo���p�֐�
    public void newDiceStop()
    {
        //�o���ڂ̐��������_���Ő���
        for(;;)
        {
            DiceNum = Random.Range(1, 7);
            if (InDiceNum.Contains(DiceNum) == true)
            {
                break;//������������Ă���Βʂ�
            }
        }

        //����������~
        rotate = false;
        xSpeed = 0;
        ySpeed = 0;
        zSpeed = 0;

        //�o���ڂ���������Z�b�g
        switch (DiceNum)
        {
            case 1:
                xShow = -90; yShow = 0;
                break;
            case 2:
                xShow = 0; yShow = 90;
                break;
            case 3:
                xShow = 180; yShow = 0;
                break;
            case 4:
                xShow = 0; yShow = 0;
                break;
            case 5:
                xShow = 0; yShow = -90;
                break;
            case 6:
                xShow = 90; yShow = 0;
                break;
        }
        //��������̖ڂ�������
        transform.rotation = Quaternion.Euler(xShow,yShow,0);
    }

    public void OddDice() //��_�C�X�ɂȂ�
    {
        InDiceNum.Clear();
        InDiceNum.Add(1);
        InDiceNum.Add(3);
        InDiceNum.Add(5);
    }

    public void EvenDice()�@//�����_�C�X�ɂȂ�
    {
        InDiceNum.Clear();
        InDiceNum.Add(2);
        InDiceNum.Add(4);
        InDiceNum.Add(6);
    }
}
