using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    //----------------------------------�ϐ�----------------------------------
    [SerializeField]
    Text textUI;           //�R�s�[���̃e�L�X�g
    [SerializeField]
    Transform ScoreBackGround;      //�e�L�X�g�{�b�N�X�̐e�ɂ���I�u�W�F�N�g
    [SerializeField]
    Button Itemadd;
    [SerializeField]
    Button display;
    [SerializeField]
    float interval = -30.0f;        //�������ꂽ�e�L�X�g�{�b�N�X�̊Ԋu
    [SerializeField]
    GameObject players;
    [SerializeField]
    List<string>[] OriginalItem = new List<string>[1];//��������Ƃ��̗v�f����4�ɂ��Ă�

    Dictionary<string, int> Item = new Dictionary<string, int> { };//���בւ��p�̋��dictionary
    //----------------------------------�֐�----------------------------------
    private void Awake()        //������
    {
        /*int playersnum = 1;//�v���C���[�̐����Q�Ƃ��Ă�

        //�������炵����𕡐����邽�߂̉�����
        GameObject PlayerBackGround = Resources.Load<GameObject>("PlayerItems");//�R�s�[��
        float[] PBGinitpos = new float[2] {30.0f, 0.0f};//��������ۂ̏����ʒuxy�����R�ɕς��Ă�������
        float PBGinterval = 30.0f;//��������ۂ�x���W�̊Ԋu*/

        players = GameObject.Find("Player1");
        OriginalItem[0] = players.GetComponent<PlayerStatasOkura>().HabItem;
        /*for (int i = 0; i < playersnum; i++)
        {
            //������̕���
            GameObject CopyedPBG = 
                Instantiate(PlayerBackGround,new Vector3(PBGinitpos[0] + (PBGinterval * i),PBGinitpos[1],0.0f), Quaternion.identity);
            CopyedPBG.name = "PlayerItems" + i;
            
            //���ёւ����̃v���C���[�̎������𕡐�
            players = GameObject.Find("Player" + i);
            OriginalItem[i] = players.GetComponent<PlayerStatasOkura>().HabItem;
        }*/
    }

    public void AddItem(string Item)
    {
        OriginalItem[0].Add(Item);
    }

    //�A�C�e���̕\��
    public void DisplayItems()
    {
        float[] initpos = new float[2] { textUI.transform.localPosition.x, textUI.transform.localPosition.y };//�e�L�X�g�̏����ʒu
        int count = 0;
        DuplicateItem();        //�e�X�g�̂��߂����ɓ���܂����B���ۂ̃Q�[���V�[���ł̓A�C�e����ǉ�����K�v���Ȃ����߁AAwake�ɂ���Ă�������

        foreach (string i in Item.Keys)
        {
            //�ŏ��͊l���������̂��������ŕ\��
            Text Copytext = Instantiate(textUI, new Vector3(initpos[0], initpos[1]+ (count * interval), 0.0f), Quaternion.identity);
            Copytext.transform.SetParent(ScoreBackGround, false);
            Copytext.text = i;

            //���Ɋl���������̂̃|�C���g���E�����ŕ\��
            Text point = Instantiate(textUI, new Vector3(initpos[0], initpos[1], 0.0f), Quaternion.identity);
            point.transform.SetParent(Copytext.transform, false);
            point.alignment = TextAnchor.MiddleRight;
            point.text = Item[i] + "P";

            count++;
        }
    }

    //�d���A�C�e�����܂Ƃ߂Ă������肳����
    void DuplicateItem()
    {
        for (int num = 0; num < OriginalItem.Length; num++)//�v���C���[�̐��Ɠ����񐔍s��
        {
            for (int i = 0; i < OriginalItem[num].Count; i++)//num�Ԗڂ̃v���C���[�̎������̐������s��
            {
                if (Item.ContainsKey(OriginalItem[num][i]))//�A�C�e���̒��ɏd������҂������
                {
                    Item[OriginalItem[num][i]] += DictionaryManager.ItemDictionary[OriginalItem[0][i]][0];//�|�C���g�𑝂₷
                }
                else
                {
                    Item.Add(OriginalItem[0][i], DictionaryManager.ItemDictionary[OriginalItem[0][i]][0]);//�Ȃ���ΐV�������ڂ����
                }
            }
        }
    }
}
