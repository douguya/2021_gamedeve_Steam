using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : MonoBehaviour
{
    public bool Open;//�}�X���󂢂Ă邩�ǂ���
    public bool Goal;//�}�X���S�[�����ǂ���
    public bool invalid;//���̃}�X���L�����ǂ���
    public bool Loot;//�}�X���ړ��}�X�Ƃ��đI������Ă��邩�ǂ���
    public bool walk;//onClick���ꂽ���ǂ������ׂ�

    public GameObject GoalFlag;//�S�[���̊ە\���p
    public GameObject hako;//�}�X�̕\���p
    public GameObject select;//�ړ��ł���}�X�̕\���p
    public GameObject decision;//�ړ��ł���}�X�̕\���p

    void Start()
    {
        //Open = false;
        //Goal = false;
        //invalid = false;
        //GoalFlag.SetActive(false);

    }

   
    void Update()
    {
        if(Goal == true)
        {
            GoalFlag.SetActive(true);
        }
        if (invalid == true)
        {
            hako.SetActive(false);
        }
    }

    public void GoalOn()
    {

    }

    public void Selecton()//�ړ��ł���}�X�̕\���p
    {
        select.SetActive(true);
    }

    public void Decisionon()//�ړ��ł���}�X�̌���\���p
    {
        decision.SetActive(true);
    }

    public void Selectoff()//�ړ��ł���}�X�̔�\���p
    {
        select.SetActive(false);
    }

    public void Decisionoff()//�ړ��ł���}�X�̌����\���p
    {
        decision.SetActive(false);
    }

    public void onClick()
    {
        if(select.activeSelf == true)
        {
            Selectoff();
            Decisionon();
            Loot = true;
            walk = true;
        }
        
    }

}
