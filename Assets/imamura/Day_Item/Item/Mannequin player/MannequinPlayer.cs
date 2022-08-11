 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MannequinPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public Anniversary_Item_Master ItemMaster;
    public List<Anniversary_Item> Hub_Items = new List<Anniversary_Item>(); 
    public List<int> Itemu_Life = new List<int>();
    void Start()
    {

    } 

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ItemAdd(int ItemNum)//ItemNum���}�X�^�[�o�^���̔ԍ�
    {
        Hub_Items.Add(ScriptableObject.Instantiate(ItemMaster.Anniversary_Items[ItemNum]));//�}�X�^�[�ɂ���ItemNum�̃A�C�e���𐶐����AHub�ɒǉ�
    }

    public void ItemLost(int HubItemNum)//HubItemNum�������A�C�e���o�^���̔ԍ�
    {
        Hub_Items.RemoveAt(HubItemNum);//��������HubItemNum�Ԗڂ̃A�C�e��������
       
    }

    public void NextMyTurn()
    {
        Debug.Log("AAA"+Hub_Items.Count);
        int num = Hub_Items.Count;
       
        for (int loop=num-1;loop>=0;loop--)
        {
            
            if (Hub_Items[loop].ItemLifespan!="null") 
            {
                Hub_Items[loop].ItemLifespan=(int.Parse(Hub_Items[loop].ItemLifespan)-1).ToString();
                if (int.Parse(Hub_Items[loop].ItemLifespan)<=0){ Hub_Items.Remove(Hub_Items[loop]); }
            }
        }

    }

    public void Doun()
    {
        Hub_Items[0].ItemLifespan=(int.Parse(Hub_Items[0].ItemLifespan)-1).ToString();
    }

 

}
