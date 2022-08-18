using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ItemBlock_List_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public List<GameObject> Block_List = new List<GameObject>();//�A�C�e���u���b�N�̃��X�g
    public GameObject BlockSumple;//�u���b�N�̃T���v��
    public GameObject AllBord;//�X�N���[���̑S�̗̈�悤
    public GameObject BlockBox;//�u���b�N�̓��ꕨ�@�e�q�֌W�̑���Ŏg�p
    public GameObject Mask;
    public GameObject Scroll;
    public GameObject ScrollBar;
    public Text PlayerName;
    public float FarstBlock_Transform;
    public bool Rist_View=false;

    public void Start()
    {
        BlockUpdate();
        MaskSize();
        BordSize();
        Scroll.SetActive(Rist_View);
       
       
    }
    private void Update()
    {
       
    }

    public void AddItem(int ItemNum)
    {
        GameObject Block = Instantiate(BlockSumple, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);//�u���b�N�̐���
        Block_List.Add(Block);//�u���b�N�����X�g�ɓ˂�����
       
        Block.transform.parent=BlockBox.transform;//�A�C�e�����X�g�̐e�q�֌W�̒���
        Block.GetComponent<ItemBlock>().ItemNumber=ItemNum;
        Block.GetComponent<ItemBlock>().Detail_Switch();

        MaskSize();
        BordSize();//�{�[�h�̃T�C�Y����
       
        BlockUpdate();//�u���b�N�̏ꏊ�𒲐�
    }

    public void LostItem(int ItemNum)
    {
        Destroy(Block_List[ItemNum]);
        Block_List.Remove(Block_List[ItemNum]);//�u���b�N�����X�g�ɓ˂�����
                
            
        

        MaskSize();
        BordSize();//�{�[�h�̃T�C�Y����

        BlockUpdate();//�u���b�N�̏ꏊ�𒲐�
    }

    public void BordSize()//�{�[�h�̃T�C�Y����
    {

        int BlockPuantity;
        if (Block_List.Count<=3) 
        {
            
            BlockPuantity = 3;

            ScrollBar.GetComponent<ScrollRect>().vertical=false;

        }
        else
        {
          
            BlockPuantity = Block_List.Count;//�u���b�N�̐�
            ScrollBar.GetComponent<ScrollRect>().vertical=true;

        }


        var BordSize_x = BlockSumple.GetComponent<RectTransform>().sizeDelta.x;//�{�[�h�̃T�C�Y���擾�@�i�߂�l�̂��߁j
        var BordSize_y = BlockSumple.GetComponent<RectTransform>().sizeDelta.y*BlockPuantity;//�{�[�h�̃T�C�Y���擾�@�i�߂�l�̂��߁j

        AllBord.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top,0, BordSize_y);

    }

    public void MaskSize()//�{�[�h�̃T�C�Y����
    {
        
        int BlockPuantity;
        if (Block_List.Count>3)
        {
        
            BlockPuantity = 3;

        }
        else
        {
            
            BlockPuantity = Block_List.Count;//�u���b�N�̐�
        }


        var MaskSize_x = BlockSumple.GetComponent<RectTransform>().sizeDelta.x;//�{�[�h�̃T�C�Y���擾�@�i�߂�l�̂��߁j
        var MaskSize_y = BlockSumple.GetComponent<RectTransform>().sizeDelta.y*BlockPuantity;//�{�[�h�̃T�C�Y���擾�@�i�߂�l�̂��߁j
        


        Mask.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, -2, MaskSize_y+3.5f);
     
        var MasTransform = Mask.GetComponent<RectTransform>().position;
         Debug.Log(MasTransform);
        MasTransform.y = MasTransform.y+1.50f;
        if (Block_List.Count==3) {
            Mask.GetComponent<RectTransform>().position=MasTransform;
           
        }
        else
        {

        }
    }




    public void BlockUpdate2()//�u���b�N�X�V��
    {

        int loop = 0;
        foreach (var Hub_Item in Player.GetComponent<MannequinPlayer>().Hub_Items)//�v���C���[�̃A�C�e�����X�g
        {
           
            foreach (var ItemMastercElement in Player.GetComponent<MannequinPlayer>().ItemMaster.Anniversary_Items)//���{�̃A�C�e�����X�g
            {
                var Itemunum = ItemMastercElement.ItemName;//���{�̃A�C�e���̖��O
                var name= Hub_Item.ItemName;//�A�C�e�����X�g�̗v�f�̖��O
            
               
                if (Itemunum==name)
                {
                    var num = Player.GetComponent<MannequinPlayer>().ItemMaster.Anniversary_Items.IndexOf(ItemMastercElement);
                   
                    if (num!=Block_List[loop].GetComponent<ItemBlock>().ItemNumber)//�u���b�N���X�g�̒��g�Ɠ���悤�Ƃ��Ă�A�C�e�����Ⴄ�ꍇ
                    {
                        if (Block_List.Count<num+1)
                        {
                            Block_List[loop].GetComponent<ItemBlock>().ItemNumber=num;
                            Block_List[loop].GetComponent<ItemBlock>().Detail_Switch();
                        }
                        else
                        {
                            AddItem(num);
                            break;
                        }
                    }
                    


                }
            
            }

            loop++;

        }
        BordSize();//�{�[�h�̃T�C�Y����
        BlockUpdate();//�u���b�N�̏ꏊ�𒲐�
    }
    public void BlockUpdate()//�u���b�N�̏ꏊ�𒲐�
    {
        int loop = 0;
        foreach (var Block in Block_List)
        {
            var BlockTransform = Block.GetComponent<RectTransform>().position;//�u���b�N�̏ꏊ���擾

            BlockTransform.x=0;
            BlockTransform.y=FarstBlock_Transform-(BlockSumple.GetComponent<RectTransform>().sizeDelta.y*loop);//�u���b�N�̏ꏊ������l�߂Ă���
            if (Block_List.Count==3)
            {
               // BlockTransform.y+=5;
            }
            Block.GetComponent<RectTransform>().anchoredPosition=BlockTransform;
            loop++;




        }
    }

    public void ItemRist_View()
    {
        Rist_View=!Rist_View;
        Scroll.SetActive(Rist_View);
    }

    public void Namedisplay(string name)
    {
        PlayerName.text=name;

    }
}
