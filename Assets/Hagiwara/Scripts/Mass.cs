using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : MonoBehaviour
{
    public bool Open;//�}�X���󂢂Ă邩�ǂ���
    public bool Goal;//�}�X���S�[�����ǂ���
    public bool invalid;//���̃}�X���L�����ǂ���
    public GameObject GoalFlag;//�S�[���̊ە\���p
    public GameObject hako;//�}�X�̕\���p

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

    
}
