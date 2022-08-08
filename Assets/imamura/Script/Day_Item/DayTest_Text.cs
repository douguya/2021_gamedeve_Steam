using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTest_Text : MonoBehaviour
{
    // Start is called before the first frame update

    public Text text;
    public Image sp;
    public Day_Square_Master Day;
    public int ASD;
    void Start()
    {
       
    }
    
    // Update is called once per frame
    void Update()
    {
        text.text=(Day.Day_Squares[ASD].Anniversary+"\n"+
                   Day.Day_Squares[ASD].Day+"\n"+
                   Day.Day_Squares[ASD].Commentary+"\n"+
                   Day.Day_Squares[ASD].Item+"\n"+
                   Day.Day_Squares[ASD].Move+"\n"+
                   Day.Day_Squares[ASD].BGM+"\n"+
                   Day.Day_Squares[ASD].Instance+"\n"+
                   Day.Day_Squares[ASD].particle+"\n"+
                   Day.Day_Squares[ASD].NextDice+"\n"+
                   Day.Day_Squares[ASD].NextMove+"\n"+
                   Day.Day_Squares[ASD].ItemLost+"\n"+
                   Day.Day_Squares[ASD].ConditionalPoint);





        sp.sprite=(Day.Day_Squares[ASD].HopUp);
    }
}
