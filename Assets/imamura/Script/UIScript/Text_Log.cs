using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


public class Text_Log : MonoBehaviour
{
    public GameObject TextObj;//�e�L�X�g�̃I�u�W�F�N�g
    public GameObject TextBoard;//�e�L�X�g�̃I�u�W�F�N�g
    public GameObject TextMask;//�e�L�X�g�̃I�u�W�F�N�g
   public float Scloll;//�X�N���[���̈ړ���
    public float Scloll_Coefficient;//�X�N���[���̌W��(�t�H���g�T�C�Y�̑���ɔ�Ⴕ�đ傫���Ȃ�@�v�Z������Ȃ��������ߓ���)
    private int SclollCount=0;//�X�N���[���񐔂̃J�E���g
    private bool Text_View = false;
    private float InitialY_Value;//
    // Start is called before the first frame update
    void Start()
    {
        var Text = TextObj.GetComponent<Text>();
        Scloll=Text.fontSize*Text.lineSpacing* Scloll_Coefficient;//�X�N���[���ʂ̎Z�o
        InitialY_Value=TextObj.GetComponent<RectTransform>().anchoredPosition.y;//�����ʒu���o��

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
        var num = TextDami.Count(f => f == '\n')+1;//���s�����R�[�h�̐�����s�����Z�o


        var Large_SclollCount = Screen.height/Scloll;



        if (Mousewheel!=0)//�}�E�X�z�C�[�����s��ꂽ�ꍇ
        {

     
          
            if (Mousewheel>0)//������z�C�[��
            {
                if (Text_View==false) {//�e�L�X�g�{�[�h����\��(�W��)�̎�
                    if (SclollCount< num+-1)
                    {
                        SclollCount++;
                        BlockTransform.y-=(Scloll);
                        TextObj.GetComponent<RectTransform>().anchoredPosition=BlockTransform;
                    }
                }
                else
                {
                    if (SclollCount< num- Large_SclollCount)
                    {
                        SclollCount++;
                        BlockTransform.y-=(Scloll);
                        TextObj.GetComponent<RectTransform>().anchoredPosition=BlockTransform;
                    }
                }


            }
            if (Mousewheel<0)//�������z�C�[��
            {

                if (SclollCount>0)
                {
                    SclollCount--;
                    BlockTransform.y+=(Scloll);
                    TextObj.GetComponent<RectTransform>().anchoredPosition=BlockTransform;
                }


            }

            Debug.Log(" SclollCount"+SclollCount);
        }

      

    }
    public void TextBoard_View()
    {
        Text_View=!Text_View;
        TextBoard.SetActive(Text_View);
        TextMask.GetComponent<Mask>().enabled=!Text_View;
        //===========�ʒu�������l�֖߂�
        var BlockTransform = TextObj.GetComponent<RectTransform>().anchoredPosition;//�u���b�N�̏ꏊ���擾
        BlockTransform.y=InitialY_Value;
        TextObj.GetComponent<RectTransform>().anchoredPosition= BlockTransform;
        SclollCount=0;
        //===========================
    }
}
