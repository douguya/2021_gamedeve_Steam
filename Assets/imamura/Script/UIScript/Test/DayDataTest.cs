using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using UnityEngine;
using UnityEngine.Video;
 
public class DayDataTest : MonoBehaviour
{

    public Day_Square_Master Days;
    public Text Text_Anniversary;
    public Text Text_Day;
    public Text Text_Staging;
    public Text Text_HopUp;

    public Text Text_Anniversary_Item;
    public Text Text_Move;
    public Text Text_BGM;
    public Text Text_Instance;
    public Text Text_Icon;
    public Text Text_particle;
    public Text Text_NextDice;
    public Text Text_NextMove;
    public Text Text_ItemLost;
    public Text Text_ConditionalPoint;
    public Text Text_OtherEffects;

    public Image DayImage;

    public GameObject Video;
    public int Daynum=0;
    // Start is called before the first frame update
    void Start()
    {

      

    }

    // Update is called once per frame
    void Update()
    {
        Text_Anniversary.text=Days.Day_Squares[Daynum].Anniversary;
        Text_Day.text=Days.Day_Squares[Daynum].Day;
        Text_Staging.text=Days.Day_Squares[Daynum].Staging.name;
        Text_HopUp.text=Days.Day_Squares[Daynum].HopUp.name;

        Text_Anniversary_Item.text=Days.Day_Squares[Daynum].Anniversary_Item.name;
        Text_Move.text=Days.Day_Squares[Daynum].Move;
        Text_BGM.text=Days.Day_Squares[Daynum].BGM;
        Text_Instance.text=Days.Day_Squares[Daynum].Instance;
        // Text_Icon.text=Days.Day_Squares[Daynum].Icon.name;
        Text_particle.text=Days.Day_Squares[Daynum].particle;
        Text_NextDice.text=Days.Day_Squares[Daynum].NextDice;
        Text_NextMove.text=Days.Day_Squares[Daynum].NextMove;
        Text_ItemLost.text=Days.Day_Squares[Daynum].ItemLost;
        Text_ConditionalPoint.text=Days.Day_Squares[Daynum].ConditionalPoint;
        Text_OtherEffects.text=Days.Day_Squares[Daynum].OtherEffects;

     

    }


    public void Axs()
    {
        if (Daynum<Days.Day_Squares.Count)
        {
            Daynum++;
            Video.GetComponent<VideoPlayer>().clip=Days.Day_Squares[Daynum].Staging;
            DayImage.sprite=Days.Day_Squares[Daynum].HopUp;
            Video.GetComponent<VideoPlayer>().Play();
        }
    }
    public void Back()
    {
        if (Daynum>0)
        {
            Daynum--;
            Video.GetComponent<VideoPlayer>().clip=Days.Day_Squares[Daynum].Staging;
            DayImage.sprite=Days.Day_Squares[Daynum].HopUp;
            Video.GetComponent<VideoPlayer>().Play();
        }
    }

}
