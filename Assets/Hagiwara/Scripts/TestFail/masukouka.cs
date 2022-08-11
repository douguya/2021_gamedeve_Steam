using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masukouka : MonoBehaviour
{
    public GameObject BuckGround;
    public GameObject Icon;
    public GameObject[] Player = new GameObject[4];
    public float speed = 0.5f;                      //プレイヤー移動速度
    private float currentTime = 0f;
    private int Count = 0;
    private int SwitchCount = 0;
    private bool Go = false;
    private int[] Xplay = new int[4];
    private int[] Yplay = new int[4];
    public Sprite yuurei;//幽霊の画像
    public Sprite Tsyatu;//Tシャツの画像
    public Sprite bikini;//ビキニの画像
    public Sprite Default;//通常画像

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Effects(string day)
    {
        switch (Daykouka.DayEffectictDictionary[day][0, 1])
        {
            case "アイテム":
                GetItem(Daykouka.DayEffectictDictionary[day][0, 2]);
                break;

            case "アイコン":
                Icon.GetComponent<BuckGroundChange>().Change(day);
                break;

            case "BGM":
                //BGM切り替え
                break;

            case "背景":
                BuckGround.GetComponent<BuckGroundChange>().Change(day);//東京の日のみなのでこうする
                GetItem(Daykouka.DayEffectictDictionary[day][0, 2]);
                break;

            case "背景とBGM":
                BuckGround.GetComponent<BuckGroundChange>().Change(day);
                //BGM切り替え
                break;

            case "消滅":
                for (int i = 5; i < 10; i++)
                {
                    for (int l = 7; l < 14; l++)
                    {
                        GetComponent<PlayerStatus>().week[i].width[l].GetComponent<Mass>().invalid = true;
                        if (GetComponent<PlayerStatus>().week[i].width[l].GetComponent<Mass>().Goal == true)
                        {
                            GetComponent<PlayerStatus>().week[i].width[l].GetComponent<Mass>().Goal = false;
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (6 < Player[i].GetComponent<PlayerStatus>().PlayerX() && 4 < Player[i].GetComponent<PlayerStatus>().PlayerY())
                    {
                        int Week, Day;
                        do
                        {
                            Week = Random.Range(0, 10);
                            Day = Random.Range(0, 14);
                        } while (GetComponent<PlayerStatus>().week[Week].width[Day].GetComponent<Mass>().invalid == true);
                        Player[i].GetComponent<PlayerStatus>().PlayerMass(Day,Week);
                    }
                }
                GoalDecision();
                break;

            case "暑中見舞い":
                for (int i = 0; i < 4; i++)
                {
                    if (6 < Player[i].GetComponent<PlayerStatus>().PlayerX() && 4 < Player[i].GetComponent<PlayerStatus>().PlayerY())
                    {
                        Player[i].GetComponent<PlayerStatus>().Itemobtain("暑中見舞い");
                    }
                }
                
                break;

            case "野菜":
                if (GetComponent<PlayerStatus>().week[8].width[3].GetComponent<Mass>().Open == true)
                {
                    GetComponent<PlayerStatus>().Itemobtain("緑黄色野菜+");
                }
                else
                {
                    GetComponent<PlayerStatus>().Itemobtain("緑黄色野菜");
                }
                break;

            case "ケーキ":
                GetComponent<PlayerStatus>().Itemobtain("ショートケーキ");
                if (GetComponent<PlayerStatus>().week[7].width[11].GetComponent<Mass>().Open == true)
                {
                    GetComponent<PlayerStatus>().PlayerMass(11,7);
                }
                break;

            case "イチゴ":
                int have = 0;
                int list = GetComponent<PlayerStatus>().HabItem.Count;
                for (int i = 0; i < list; i++)
                {
                    if (GetComponent<PlayerStatus>().HabItem[i] == "ゴール")
                    {
                        have = 1;
                    }
                }
                if (have < 1)
                {
                    GetComponent<PlayerStatus>().Itemobtain("イチゴ+");
                }
                else
                {
                    GetComponent<PlayerStatus>().Itemobtain("イチゴ");
                }
                break;

            case "波":
                int[,] Loot = new int[,] { { 1, 8 }, { 1, 9 }, { 1, 10 } };
                currentTime += Time.deltaTime;      //プレイヤーの移動が一歩ずつ進むように
                if (currentTime > speed)
                {
                    GetComponent<PlayerStatus>().PlayerMass(Loot[Count,1], Loot[Count,0]); //一歩進める
                    Count++;
                    currentTime = 0f;
                }
                if (Count > 2)
                {
                    Count = 0;
                }
                break;

            case "裏切り":
                int[] Xplay = new int[4];
                int[] Yplay = new int[4];

                switch (SwitchCount)
                {
                    case 0:
                        for (int i = 0; i < 4; i++)
                        {
                            Xplay[i] = Player[i].GetComponent<PlayerStatus>().PlayerX();
                            Yplay[i] = Player[i].GetComponent<PlayerStatus>().PlayerY();
                            GetComponent<PlayerStatus>().week[Yplay[i]].width[Xplay[i]].GetComponent<Mass>().Selecton();

                        }
                        SwitchCount = 1;
                        break;
                    case 1:

                        Move();

                        for (int i = 0; i < GetComponent<PlayerStatus>().week.Length; i++)
                        {
                            for (int l = 0; l < GetComponent<PlayerStatus>().week[0].width.Length; l++)
                            {
                                for (int P = 0; P < 4; P++)
                                {
                                    if (Yplay[P] == i && Xplay[P] == l && GetComponent<PlayerStatus>().week[i].width[l].GetComponent<Mass>().walk == true)
                                    {
                                        Player[P].GetComponent<PlayerStatus>().PlayerMass(GetComponent<PlayerStatus>().PlayerX(), GetComponent<PlayerStatus>().PlayerY());
                                        GetComponent<PlayerStatus>().PlayerMass(l, i);
                                        Go = true;
                                    }
                                }
                            }
                        }
                        if (Go)
                        {
                            Go = false;
                            clearSelect();
                            SwitchCount = 0;
                        }

                        break;
                }
                break;

            case "恋人":
                switch (SwitchCount)
                {
                    case 0:
                        for (int i = 0; i < 4; i++)
                        {
                            GetComponent<PlayerStatus>().week[Player[i].GetComponent<PlayerStatus>().PlayerY()].width[Player[i].GetComponent<PlayerStatus>().PlayerX()].GetComponent<Mass>().Selecton();
                        }
                        SwitchCount = 1;
                        break;
                    case 1:
                        Move();
                        break;
                }
                break;

            case "バス":

                switch (SwitchCount)
                {
                    case 0:
                        GetComponent<PlayerStatus>().week[7].width[9].GetComponent<Mass>().Selecton();
                        GetComponent<PlayerStatus>().week[9].width[9].GetComponent<Mass>().Selecton();
                        GetComponent<PlayerStatus>().week[8].width[8].GetComponent<Mass>().Selecton();
                        GetComponent<PlayerStatus>().week[8].width[10].GetComponent<Mass>().Selecton();
                        SwitchCount = 1;
                        break;
                    case 1:
                        Move();
                        break;
                }
                break;

            case "UFO":

                switch (SwitchCount)
                {
                    case 0:
                        GetComponent<PlayerStatus>().week[3].width[5].GetComponent<Mass>().Selecton();
                        GetComponent<PlayerStatus>().week[4].width[7].GetComponent<Mass>().Selecton();
                        GetComponent<PlayerStatus>().week[8].width[3].GetComponent<Mass>().Selecton();
                        GetComponent<PlayerStatus>().week[8].width[13].GetComponent<Mass>().Selecton();
                        SwitchCount = 1;
                        break;
                    case 1:
                        Move();
                        break;
                }
                break;

            case "世界観光":

                switch (SwitchCount)
                {
                    case 0:
                        GetComponent<PlayerStatus>().week[1].width[0].GetComponent<Mass>().Selecton();
                        GetComponent<PlayerStatus>().week[0].width[13].GetComponent<Mass>().Selecton();
                        GetComponent<PlayerStatus>().week[9].width[0].GetComponent<Mass>().Selecton();
                        GetComponent<PlayerStatus>().week[9].width[12].GetComponent<Mass>().Selecton();
                        SwitchCount = 1;
                        break;
                    case 1:
                        Move();
                        break;
                }
                break;

            case "時":

                switch (SwitchCount)
                {
                    case 0:
                        for (int i = 0; i < GetComponent<PlayerStatus>().week.Length; i++)
                        {
                            for (int l = 0; l < GetComponent<PlayerStatus>().week[0].width.Length; l++)
                            {
                                if (GetComponent<PlayerStatus>().week[i].width[l].GetComponent<Mass>().invalid == false)
                                {
                                    GetComponent<PlayerStatus>().week[i].width[l].GetComponent<Mass>().Selecton();
                                }
                            }
                        }
                        SwitchCount = 1;
                        break;
                    case 1:
                        Move();
                        break;
                }
                break;

            case "パリメトロ":
                GetComponent<PlayerStatus>().PlayerMass(5, 7);
                break;

            case "演説":
                for (int i = 0; i < 4; i++)
                {
                    Player[i].GetComponent<PlayerStatus>().PlayerMass(1, 4);
                }
                break;

            case "":
                break;

            default:
                break;
        }



    }

    private void step()
    {
        //GetComponent<PlayerStatus>().stopon();
    }

    private void GetItem(string Iname)
    {
        GetComponent<PlayerStatus>().Itemobtain(Iname);
    }

    private void GoalDecision()
    {
        int Week, Day;
        do
        {
            Week = Random.Range(0, 10);
            Day = Random.Range(0, 14);
        } while (GetComponent<PlayerStatus>().week[Week].width[Day].GetComponent<Mass>().invalid == true);
        GetComponent<PlayerStatus>().week[Week].width[Day].GetComponent<Mass>().GoalOn();

    }

    private void Move()
    {
        for (int i = 0; i < GetComponent<PlayerStatus>().week.Length; i++)
        {
            for (int l = 0; l < GetComponent<PlayerStatus>().week[0].width.Length; l++)
            {
                if (GetComponent<PlayerStatus>().week[i].width[l].GetComponent<Mass>().walk == true)
                {
                    GetComponent<PlayerStatus>().PlayerMass(l, i);
                    Go = true;
                }
            }
        }
        if (Go)
        {
            Go = false;
            clearSelect();
            SwitchCount = 0;
        }
    }

    private void clearSelect()//選択できるマスの全消去
    {
        for (int i = 0; i < GetComponent<PlayerStatus>().week.Length; i++)
        {
            for (int l = 0; l < GetComponent<PlayerStatus>().week[0].width.Length; l++)
            {
                GetComponent<PlayerStatus>().week[i].width[l].GetComponent<Mass>().Selectoff();//マスを選択出来るというimageを消す
                GetComponent<PlayerStatus>().week[i].width[l].GetComponent<Mass>().walk = false;//クリックされたという判定を消す
            }
        }
    }
}
