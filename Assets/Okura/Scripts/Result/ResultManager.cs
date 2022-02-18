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
    Transform canvas;      //�e�L�X�g�{�b�N�X�̐e�ɂ���I�u�W�F�N�g
                           //���ꂪ���邱�ƂŃR�s�[���ꂽ�e�L�X�g�����������ʒu�����߂���
    [SerializeField]
    Button Itemadd;
    [SerializeField]
    Button display;
    [SerializeField]
    float interval = -30.0f;        //�������ꂽ�e�L�X�g�{�b�N�X�̊Ԋu
    [SerializeField]
    GameObject players;
    [SerializeField]
    List<string> OriginalItem;
    [SerializeField]
    Dictionary<string, int> Item = new Dictionary<string, int> { };
    //----------------------------------�֐�----------------------------------
    private void Awake()
    {
        players = GameObject.Find("Player1");
        OriginalItem = players.GetComponent<PlayerStatasOkura>().HabItem;
    }

    public void AddItem(string Item)
    {
        OriginalItem.Add(Item);
    }

    //�A�C�e���̕\��
    public void DisplayItems()
    {
        float[] initpos = new float[2] { textUI.transform.localPosition.x, textUI.transform.localPosition.y };//�e�L�X�g�̏����ʒu
        int count = 0;
        DuplicateItem();

        foreach (string i in Item.Keys)
        {
            //�ŏ��͊l���������̂��������ŕ\��
            Text Copytext = Instantiate(textUI, new Vector3(initpos[0], initpos[1]+ (count * interval), 0.0f), Quaternion.identity);
            Copytext.transform.SetParent(canvas, false);
            Copytext.text = i;

            //���Ɋl���������̂̃|�C���g���E�����ŕ\��
            Text point = Instantiate(textUI, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            point.transform.SetParent(Copytext.transform, false);
            point.alignment = TextAnchor.MiddleRight;
            point.text = Item[i] + "P";


            count++;
        }
    }

    //�d���A�C�e�����܂Ƃ߂Ă������肳����
    void DuplicateItem()
    {
        for (int i = 0; i < OriginalItem.Count; i++)
        {
                if (Item.ContainsKey(OriginalItem[i]))
                {
                    Item[OriginalItem[i]] += DictionaryManager.ItemDictionary[OriginalItem[i]][0];
                }
                else
                {
                    Item.Add(OriginalItem[i], DictionaryManager.ItemDictionary[OriginalItem[i]][0]);
            }
        }
    }
}
