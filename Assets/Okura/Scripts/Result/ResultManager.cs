using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    //----------------------------------�ϐ�----------------------------------
    [SerializeField]
    int playersnum;
    [SerializeField]
    Text textUI;           //�R�s�[���̃e�L�X�g
    [SerializeField]
    Text[] total;
    [SerializeField]
    Transform Canvas;
    [SerializeField]
    Transform[] ScoreBackGround;      //�e�L�X�g�{�b�N�X�̐e�ɂ���I�u�W�F�N�g(�ȉ�SBG)
    [SerializeField]
    GameObject[] PlayerBackGround;
    [SerializeField]
    Button Itemadd;
    [SerializeField]
    Button display;
    [SerializeField]
    float interval = -30.0f;        //�������ꂽ�e�L�X�g�{�b�N�X�̊Ԋu
    [SerializeField]
    GameObject players;
    [SerializeField]
    List<string>[] OriginalItem;//��������Ƃ��̗v�f����4�ɂ��Ă�

    Dictionary<string, int> Item0 = new Dictionary<string, int> { };//���בւ��p�̋��dictionary
    Dictionary<string, int> Item1 = new Dictionary<string, int> { };
    Dictionary<string, int> Item2 = new Dictionary<string, int> { };
    Dictionary<string, int> Item3 = new Dictionary<string, int> { };

    Dictionary<int, Dictionary<string, int>> Items;
    //----------------------------------�֐�----------------------------------
    private void Awake()        //������
    {
        //���[���h�ϐ�����
        playersnum = 4;
        total = new Text[playersnum];
        Canvas = GameObject.Find("Canvas").transform;
        ScoreBackGround = new Transform[playersnum];
        OriginalItem = new List<string>[playersnum];
        Items = new Dictionary<int, Dictionary<string, int>> {{ 0, Item0 },{ 1, Item1 },{ 2, Item2 },{ 3, Item3 }};


        //��������v���n�u�𐶐����邽�߂̉�����
        PlayerBackGround = new GameObject[playersnum];
        float[] PBGinitpos = new float[2] {-235.0f, -16.9f};//��������ۂ̏����ʒuxy�����R�ɕς��Ă�������
        float PBGinterval = 160.0f;//��������ۂ�x���W�̊Ԋu*/

        for (int i = 0; i < playersnum; i++)
        {
            //�v���n�u�ƃv���C���[�̏������[�h
            PlayerBackGround[i] = Resources.Load<GameObject>("PlayerItems" + i);
            players = GameObject.Find("Player" + i);

            //�v���n�u�𐶐�����
            GameObject CopyedPBG = Instantiate(PlayerBackGround[i],new Vector3(PBGinitpos[0] + (PBGinterval * i),PBGinitpos[1],0.0f), Quaternion.identity);
            CopyedPBG.name = "PlayerItems" + i;
            CopyedPBG.transform.SetParent(Canvas, false);
            //���O�̐ݒ�
            Text Playername = GameObject.Find("Playername" + i).GetComponent<Text>();
            Playername.text = players.GetComponent<PlayerStatasOkura>().Name;
            //�\�����Ɏg��SBG��ݒ�
            ScoreBackGround[i] = GameObject.Find("Content" + i).transform;
            total[i] = GameObject.Find("Total" + i).GetComponent<Text>();

            //���ёւ����̃v���C���[�̎������𕡐�
            OriginalItem[i] = players.GetComponent<PlayerStatasOkura>().HabItem;
        }
    }

    //�A�C�e���̕\��
    public void DisplayItems()
    {
        textUI = GameObject.Find("Items").GetComponent<Text>();
        float[] initpos = new float[2] { textUI.transform.localPosition.x, textUI.transform.localPosition.y };//�e�L�X�g�̏����ʒu
        int[] totalpoint = new int[playersnum];
        int count = 0;
        DuplicateItem();        //�e�X�g�̂��߂����ɓ���܂����B���ۂ̃Q�[���V�[���ł̓A�C�e����ǉ�����K�v���Ȃ����߁AAwake�ɂ���Ă�������

        foreach(Transform i in ScoreBackGround)
        {
            int dupcount = 0;
            foreach (string j in Items[count].Keys)
            {
                //�ŏ��͊l���������̂��������ŕ\��
                Text Copytext = Instantiate(textUI, new Vector3(initpos[0], initpos[1] + (dupcount * interval), 0.0f), Quaternion.identity);
                Copytext.transform.SetParent(i, false);
                Copytext.text = j;

                //���Ɋl���������̂̃|�C���g���E�����ŕ\��
                Text point = Instantiate(textUI, new Vector3(initpos[0], initpos[1], 0.0f), Quaternion.identity);
                point.transform.SetParent(Copytext.transform, false);
                point.alignment = TextAnchor.MiddleRight;
                point.text = Items[count][j] + "P";

                totalpoint[count] += Items[count][j];
                dupcount++;
            }

            total[count].text = totalpoint[count].ToString() + "P";
            count++;
        }

        int[] Rank = new int[4] { 0, 1, 2, 3 };
        for (int i = 0; i < playersnum; ++i)
        {
            for (int j = i + 1; j < playersnum; ++j)
            {
                if(totalpoint[i] < totalpoint[j])
                {
                    Debug.Log(totalpoint[i] + "��" + totalpoint[j] +"��菬����");
                    int tmp;
                    tmp = Rank[i];
                    Rank[i] = Rank[j];
                    Rank[j] = tmp;
                }
                else
                {
                    Debug.Log(totalpoint[i] + "��" + totalpoint[j] + "���傫��");
                }
            }
        }
        Image RImage = GameObject.Find("Rank" + Rank[0]).GetComponent<Image>();
        RImage.sprite = Resources.Load<Sprite>("1st");
    }


    //�d���A�C�e�����܂Ƃ߂Ă������肳����
    void DuplicateItem()
    {
        for (int num = 0; num < OriginalItem.Length; num++)//�v���C���[�̐��Ɠ����񐔍s��
        {
            for (int i = 0; i < OriginalItem[num].Count; i++)//num�Ԗڂ̃v���C���[�̎������̐������s��
            {
                if (Items[num].ContainsKey(OriginalItem[num][i]))//�A�C�e���̒��ɏd������҂������
                {
                    Items[num][OriginalItem[num][i]] += DictionaryManager.ItemDictionary[OriginalItem[num][i]][0];//�|�C���g�𑝂₷
                }
                else
                {
                    Items[num].Add(OriginalItem[num][i], DictionaryManager.ItemDictionary[OriginalItem[num][i]][0]);//�Ȃ���ΐV�������ڂ����
                }
            }
        }
    }
}
