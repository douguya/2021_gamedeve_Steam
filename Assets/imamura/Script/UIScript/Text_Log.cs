using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


public class Text_Log : MonoBehaviour
{
    public GameObject TextObj;
    public int Scloll;
    public float InitialY_Value;
    // Start is called before the first frame update
    void Start()
    {
        InitialY_Value=TextObj.GetComponent<RectTransform>().anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnScroll()
    {
        var BlockTransform = TextObj.GetComponent<RectTransform>().anchoredPosition;//�u���b�N�̏ꏊ���擾
        
             

        var  Mousewheel =Input.GetAxis("Mouse ScrollWheel");//�}�E�X�z�C�[���l�̕ۑ�



        var TextDami = TextObj.GetComponent<Text>().text;
        var num = TextDami.Count(f => f == '\n')+1;

        Debug.Log("AAAAAAAAAAAAAA"+ (BlockTransform.y-=(Mousewheel*Scloll)));
        Debug.Log( "qqqqqqqqqqqqq"+(InitialY_Value-(Scloll*num*Mousewheel)));

       

        if (Mousewheel!=0)//�}�E�X�z�C�[�����s��ꂽ�ꍇ
        {
           
            if (Mousewheel>0)//������z�C�[��
            {
                if ((BlockTransform.y-=(Mousewheel*Scloll))>=InitialY_Value-(Scloll*num*0.1))
                {
                   
                }
                BlockTransform.y-=(Mousewheel*Scloll);
                TextObj.GetComponent<RectTransform>().anchoredPosition=BlockTransform;

            }
            if (Mousewheel<0)//�������z�C�[��
            {
                if ((BlockTransform.y-=(Mousewheel*Scloll))<=InitialY_Value)
                {
                 
                }
              
                BlockTransform.y-=(Mousewheel*Scloll);
                TextObj.GetComponent<RectTransform>().anchoredPosition=BlockTransform;
            }
        }
        
        

    }
}
