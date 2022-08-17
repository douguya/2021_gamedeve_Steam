using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBlock_List_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Block_List = new List<GameObject>();//アイテムブロックのリスト
    public GameObject BlockSumple;//ブロックのサンプル
    public GameObject AllBord;//スクロールの全体領域よう
    public GameObject BlockBox;//ブロックの入れ物　親子関係の操作で使用
    public GameObject Player;
    public int FarstBlock_Transform;


    public void Start()
    {
        BlockUpdate2();
    }

    public void AddItem(int ItemNum)
    {
        GameObject Block = Instantiate(BlockSumple, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);//ブロックの生成
        Block_List.Add(Block);//ブロックをリストに突っ込む
        Block.transform.parent=BlockBox.transform;//アイテムリストの親子関係の調整
        Block.GetComponent<ItemBlock>().ItemNumber=ItemNum;
        Block.GetComponent<ItemBlock>().Detail_Switch();
    }

    public void LostItem(int ItemNum)
    {
        foreach (var Block in Block_List)
        {
            if (Block.GetComponent<ItemBlock>().ItemNumber==ItemNum) 
            {
                Block_List.Remove(Block);//ブロックをリストに突っ込む
            }
        }
        
        BordSize();//ボードのサイズ調整
        BlockUpdate();//ブロックの場所を調整
    }

    public void BordSize()//ボードのサイズ調整
    {
        var BlockPuantity = Block_List.Count;//ブロックの数
        var BordSize_x = BlockSumple.GetComponent<RectTransform>().sizeDelta.x;//ボードのサイズを取得　（戻り値のため）
        var BordSize_y = BlockSumple.GetComponent<RectTransform>().sizeDelta.y*Block_List.Count;//ボードのサイズを取得　（戻り値のため）
        var BordSize = new Vector2(BordSize_x , BordSize_y); ;

        AllBord.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BordSize_y);

      
    }

    public void BlockUpdate()//ブロックの場所を調整
    {
        int loop = 0;
        foreach (var Block in Block_List)
        {
            var BlockTransform = Block.GetComponent<RectTransform>().position;//ブロックの場所を取得

            BlockTransform.y=FarstBlock_Transform-(BlockSumple.GetComponent<RectTransform>().sizeDelta.y*loop);//ブロックの場所を一つずつ詰めていく

            Block.GetComponent<RectTransform>().position=BlockTransform;
            loop++;




        }
    }



    public void BlockUpdate2()//ブロック更新時
    {
        Block_List.Clear();
        foreach (var Hub_Item in Player.GetComponent<MannequinPlayer>().Hub_Items)//プレイヤーのアイテムリスト
        {
           
            foreach (var ItemMastercElement in Player.GetComponent<MannequinPlayer>().ItemMaster.Anniversary_Items)//元本のアイテムリスト
            {
                var Itemunum = ItemMastercElement.ItemName;//ブロックのアイテムの番号
                var name= Hub_Item.ItemName;//アイテムリストの要素の元本での名前
                Debug.Log(Itemunum);

               
                if (Itemunum==name)//ブロックのやつと違うアイテムなら
                {
                    var num = Player.GetComponent<MannequinPlayer>().ItemMaster.Anniversary_Items.IndexOf(ItemMastercElement);
                    AddItem(num);
                }
            
            }
           
            

        }
        BordSize();//ボードのサイズ調整
        BlockUpdate();//ブロックの場所を調整
    }


}
