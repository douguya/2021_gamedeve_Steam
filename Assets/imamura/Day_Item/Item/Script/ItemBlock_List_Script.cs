using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBlock_List_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Block_List = new List<GameObject>();//�A�C�e���u���b�N�̃��X�g
    public GameObject BlockSumple;//�u���b�N�̃T���v��
    public GameObject AllBord;//�X�N���[���̑S�̗̈�悤
    public GameObject BlockBox;//�u���b�N�̓��ꕨ�@�e�q�֌W�̑���Ŏg�p
    public GameObject Player;
    public int FarstBlock_Transform;


    public void Start()
    {
        BlockUpdate2();
    }

    public void AddItem(int ItemNum)
    {
        GameObject Block = Instantiate(BlockSumple, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);//�u���b�N�̐���
        Block_List.Add(Block);//�u���b�N�����X�g�ɓ˂�����
        Block.transform.parent=BlockBox.transform;//�A�C�e�����X�g�̐e�q�֌W�̒���
        Block.GetComponent<ItemBlock>().ItemNumber=ItemNum;
        Block.GetComponent<ItemBlock>().Detail_Switch();
    }

    public void LostItem(int ItemNum)
    {
        foreach (var Block in Block_List)
        {
            if (Block.GetComponent<ItemBlock>().ItemNumber==ItemNum) 
            {
                Block_List.Remove(Block);//�u���b�N�����X�g�ɓ˂�����
            }
        }
        
        BordSize();//�{�[�h�̃T�C�Y����
        BlockUpdate();//�u���b�N�̏ꏊ�𒲐�
    }

    public void BordSize()//�{�[�h�̃T�C�Y����
    {
        var BlockPuantity = Block_List.Count;//�u���b�N�̐�
        var BordSize_x = BlockSumple.GetComponent<RectTransform>().sizeDelta.x;//�{�[�h�̃T�C�Y���擾�@�i�߂�l�̂��߁j
        var BordSize_y = BlockSumple.GetComponent<RectTransform>().sizeDelta.y*Block_List.Count;//�{�[�h�̃T�C�Y���擾�@�i�߂�l�̂��߁j
        var BordSize = new Vector2(BordSize_x , BordSize_y); ;

        AllBord.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BordSize_y);

      
    }

    public void BlockUpdate()//�u���b�N�̏ꏊ�𒲐�
    {
        int loop = 0;
        foreach (var Block in Block_List)
        {
            var BlockTransform = Block.GetComponent<RectTransform>().position;//�u���b�N�̏ꏊ���擾

            BlockTransform.y=FarstBlock_Transform-(BlockSumple.GetComponent<RectTransform>().sizeDelta.y*loop);//�u���b�N�̏ꏊ������l�߂Ă���

            Block.GetComponent<RectTransform>().position=BlockTransform;
            loop++;




        }
    }



    public void BlockUpdate2()//�u���b�N�X�V��
    {
        Block_List.Clear();
        foreach (var Hub_Item in Player.GetComponent<MannequinPlayer>().Hub_Items)//�v���C���[�̃A�C�e�����X�g
        {
           
            foreach (var ItemMastercElement in Player.GetComponent<MannequinPlayer>().ItemMaster.Anniversary_Items)//���{�̃A�C�e�����X�g
            {
                var Itemunum = ItemMastercElement.ItemName;//�u���b�N�̃A�C�e���̔ԍ�
                var name= Hub_Item.ItemName;//�A�C�e�����X�g�̗v�f�̌��{�ł̖��O
                Debug.Log(Itemunum);

               
                if (Itemunum==name)//�u���b�N�̂�ƈႤ�A�C�e���Ȃ�
                {
                    var num = Player.GetComponent<MannequinPlayer>().ItemMaster.Anniversary_Items.IndexOf(ItemMastercElement);
                    AddItem(num);
                }
            
            }
           
            

        }
        BordSize();//�{�[�h�̃T�C�Y����
        BlockUpdate();//�u���b�N�̏ꏊ�𒲐�
    }


}
