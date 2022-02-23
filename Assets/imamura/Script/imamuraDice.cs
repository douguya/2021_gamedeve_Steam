using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
public class imamuraDice : MonoBehaviour
{
    [SerializeField]
    Sprite[] DiceImages=new Sprite[6]; //ダイスの画像

    private Image Dice;　　　　　　　　//Imagへの干渉
    private bool DiceSpin = true;　　　 //サイコロを回す許可
    public int DiseSpeed;       //サイコロの切り替えスピード
    int loop = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Dice = this.GetComponent<Image>();//ダイスの画像コンポーネント指定
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public async void OnDiceSpin()//サイコロを回す関数
    {
        DiceSpin = true;
        loop =0;　　　　　　　//サイコロの画像指定用
        while (DiceSpin == true) //サイコロの回転許可
        {
            await Task.Delay(DiseSpeed);//サイコロの画像変更のディレイ
            Dice.sprite =DiceImages[loop];//サイコロの画像変更
            loop++;　　　　　　　　　　
            if (loop > 5)
            { loop = 0; }
         }
    }

    public int  StopDice()//ダイスを止めて値を返す
    {
        DiceSpin = false;//ダイスストップ
       
        return loop+1;// ダイスの値を返す
    }


    public void StopDiceBotton()//ボタンから無理やり起動すよう
    {
        StopDice();
        Debug.Log(StopDice());

    }
}
