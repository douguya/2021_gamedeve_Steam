using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ItemBlock_List_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public List<GameObject> Block_List = new List<GameObject>();//アイテムブロックのリスト
    public GameObject BlockSumple;//ブロックのサンプル
    public GameObject AllBord;//スクロールの全体領域よう
    public GameObject BlockBox;//ブロックの入れ物　親子関係の操作で使用
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
        GameObject Block = Instantiate(BlockSumple, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);//ブロックの生成
        Block_List.Add(Block);//ブロックをリストに突っ込む
       
        Block.transform.parent=BlockBox.transform;//アイテムリストの親子関係の調整
        Block.GetComponent<ItemBlock>().ItemNumber=ItemNum;
        Block.GetComponent<ItemBlock>().Detail_Switch();

        MaskSize();
        BordSize();//ボードのサイズ調整
       
        BlockUpdate();//ブロックの場所を調整
    }

    public void LostItem(int ItemNum)
    {
        Destroy(Block_List[ItemNum]);
        Block_List.Remove(Block_List[ItemNum]);//ブロックをリストに突っ込む
                
            
        

        MaskSize();
        BordSize();//ボードのサイズ調整

        BlockUpdate();//ブロックの場所を調整
    }

    public void BordSize()//ボードのサイズ調整
    {

        int BlockPuantity;
        if (Block_List.Count<=3) 
        {
            
            BlockPuantity = 3;

            ScrollBar.GetComponent<ScrollRect>().vertical=false;

        }
        else
        {
          
            BlockPuantity = Block_List.Count;//ブロックの数
            ScrollBar.GetComponent<ScrollRect>().vertical=true;

        }


        var BordSize_x = BlockSumple.GetComponent<RectTransform>().sizeDelta.x;//ボードのサイズを取得　（戻り値のため）
        var BordSize_y = BlockSumple.GetComponent<RectTransform>().sizeDelta.y*BlockPuantity;//ボードのサイズを取得　（戻り値のため）

        AllBord.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top,0, BordSize_y);

    }

    public void MaskSize()//ボードのサイズ調整
    {
        
        int BlockPuantity;
        if (Block_List.Count>3)
        {
        
            BlockPuantity = 3;

        }
        else
        {
            
            BlockPuantity = Block_List.Count;//ブロックの数
        }


        var MaskSize_x = BlockSumple.GetComponent<RectTransform>().sizeDelta.x;//ボードのサイズを取得　（戻り値のため）
        var MaskSize_y = BlockSumple.GetComponent<RectTransform>().sizeDelta.y*BlockPuantity;//ボードのサイズを取得　（戻り値のため）
        


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




    public void BlockUpdate2()//ブロック更新時
    {

        int loop = 0;
        foreach (var Hub_Item in Player.GetComponent<MannequinPlayer>().Hub_Items)//プレイヤーのアイテムリスト
        {
           
            foreach (var ItemMastercElement in Player.GetComponent<MannequinPlayer>().ItemMaster.Anniversary_Items)//元本のアイテムリスト
            {
                var Itemunum = ItemMastercElement.ItemName;//元本のアイテムの名前
                var name= Hub_Item.ItemName;//アイテムリストの要素の名前
            
               
                if (Itemunum==name)
                {
                    var num = Player.GetComponent<MannequinPlayer>().ItemMaster.Anniversary_Items.IndexOf(ItemMastercElement);
                   
                    if (num!=Block_List[loop].GetComponent<ItemBlock>().ItemNumber)//ブロックリストの中身と入れようとしてるアイテムが違う場合
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
        BordSize();//ボードのサイズ調整
        BlockUpdate();//ブロックの場所を調整
    }
    public void BlockUpdate()//ブロックの場所を調整
    {
        int loop = 0;
        foreach (var Block in Block_List)
        {
            var BlockTransform = Block.GetComponent<RectTransform>().position;//ブロックの場所を取得

            BlockTransform.x=0;
            BlockTransform.y=FarstBlock_Transform-(BlockSumple.GetComponent<RectTransform>().sizeDelta.y*loop);//ブロックの場所を一つずつ詰めていく
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
