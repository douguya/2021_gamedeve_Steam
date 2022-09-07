
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using Photon.Pun;

public class Text_Log : MonoBehaviourPunCallbacks
{
    public GameObject TextObj;//�e�L�X�g�̃I�u�W�F�N�g
    public GameObject TextBoard;//�e�L�X�g�̃I�u�W�F�N�g
    public GameObject nowText;//�e�L�X�g�̃I�u�W�F�N�g
    public GameObject TextMask;//�e�L�X�g�̃I�u�W�F�N�g
    public GameObject PullImage;
    public InputField InputField;     //���O���͗�
    public Text TestText;

    public float Scloll;//�X�N���[���̈ړ���
    public float Scloll_Coefficient;//�X�N���[���̌W��(�t�H���g�T�C�Y�̑���ɔ�Ⴕ�đ傫���Ȃ�@�v�Z������Ȃ��������ߓ���)
    private int SclollCount = 0;//�X�N���[���񐔂̃J�E���g
    private bool Text_View = false;
    private float InitialY_Value;//
    private float FastBorad;
    // Start is called before the first frame update
    void Start()
    {
        var Text = TextObj.GetComponent<Text>();
        Scloll=Text.fontSize*Text.lineSpacing* Scloll_Coefficient;//�X�N���[���ʂ̎Z�o
        InitialY_Value=TextObj.GetComponent<RectTransform>().anchoredPosition.y;//�����ʒu���o��
        FastBorad= nowText.GetComponent<RectTransform>().anchoredPosition.y;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnScroll()
    {
        var BlockTransform = TextObj.GetComponent<RectTransform>().anchoredPosition;//�u���b�N�̏ꏊ���擾
        var Mousewheel = Input.GetAxis("Mouse ScrollWheel");//�}�E�X�z�C�[���l�̕ۑ�
        var TextDami = TextObj.GetComponent<Text>().text;
        var num = TextDami.Count(f => f == '\n')+1;//���s�����R�[�h�̐�����s�����Z�o


        var Large_SclollCount = Screen.height/Scloll;



        if (Mousewheel!=0)//�}�E�X�z�C�[�����s��ꂽ�ꍇ
        {



            if (Mousewheel>0)//������z�C�[��
            {
                if (Text_View==false)
                {//�e�L�X�g�{�[�h����\��(�W��)�̎�
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
        PullImage.GetComponent<RectTransform>().Rotate(0, 0, 180f);
        Text_View =!Text_View;
        TextBoard.SetActive(Text_View);
        TextMask.GetComponent<Mask>().enabled=!Text_View;
        //===========�ʒu�������l�֖߂�
        var BlockTransform = TextObj.GetComponent<RectTransform>().anchoredPosition;//�u���b�N�̏ꏊ���擾
        BlockTransform.y=InitialY_Value;
        TextObj.GetComponent<RectTransform>().anchoredPosition= BlockTransform;
        SclollCount=0;
        //===========================
    }

    public void textadd(string LogText)//���{��50����
    {

        Text texts = TextObj.GetComponent<Text>();//���O�̃e�L�X�g���Q�ƂŎ擾
        NowTextBord(LogText);

        texts.text=texts.text+"\n"+LogText;//���O�̃e�L�X�g���e�ɒǉ�
    }



    public void NowTextBord(string LogText)//���{��50����
    {
        TestText.text=LogText;
        var BordSize = nowText.GetComponent<RectTransform>().sizeDelta;

        float SizeCount = Mathf.CeilToInt(TestText.preferredWidth/BordSize.x);

        BordSize.y = Scloll*SizeCount;

        nowText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BordSize.y);

        var nowPosi = nowText.GetComponent<RectTransform>().anchoredPosition;
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"+nowPosi);
        nowPosi.y= FastBorad+(9.73f*(SizeCount-1));
        nowText.GetComponent<RectTransform>().anchoredPosition= nowPosi;

    }








    public void Direct_Log_InputField()//�S�̂Ƀ��O�𑗂�
    {
        if (Input.GetKey(KeyCode.Return))
        {



            var name = PlayerColouradd(PhotonNetwork.NickName);
            string Chat = name+":" +InputField.GetComponent<InputField>().text;

            if (Chat!="")
            {

                Debug.Log(name);

                //���̓t�H�[���̃e�L�X�g����ɂ���
                // textadd(Chat);
                photonView.RPC(nameof(Direct_Log_RPC__InputField), RpcTarget.AllViaServer, Chat);
                InputField.text = "";
            }
        }

    }
    public string PlayerColouradd(string Chat)
    {
        var ColourNum = PhotonNetwork.LocalPlayer.CustomProperties["PlayerNumMaterial"];
        var Text = Chat;
        switch (ColourNum)
        {

            case 0:
                Text="<color=red>"+Text+"</color>";

                break;
            case 1:
                Text="<color=blue>"+Text+"</color>";

                break;
            case 2:
                Text="<color=orange>"+Text+"</color>";

                break;
            case 3:
                Text="<color=lime>"+Text+"</color>";

                break;

        }
        return Text;
    }






    [PunRPC]
    public void Direct_Log_RPC__InputField(string LogText)//�S�̂Ƀ��O�𑗂�
    {
        textadd(LogText);
    }
}