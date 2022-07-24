using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;
public class sugorokuManager : MonoBehaviourPunCallbacks
{
    public int[] month = new int[4];                //�ݒu���錎���󂯎��

    private int XGoal, YGoal;                       //�S�[���̍��W

    public GameObject[] Player = new GameObject[4]; //�v���C���[�I�u�W�F�N�g�擾
    public Width[] height = new Width[10];                             //Mass�̏c��̃I�u�W�F�N�g�̎擾�E��ԉ��œ񎟌��z��ɂ��Ă���
    private int Playerturn = 0;                     //�v���C���[�̎�ԊǗ�

    private int Playcount = 0;                      //�v���C���[�̎Q���l��
    public int play = 0;                           //�N�̔Ԃ�
    public Hashtable hashRoom;
    public GameObject GameStartButton;
    public GameObject Dcomment;
    public GameObject SceneManager;
    public GameObject RadyButton;
    private bool gamestart = false;
    public int playersnum;
    public int Goalcount=0;


    private void Awake()
    {
  
    }


    void Start()
    {
        Dcomment = GameObject.Find("DayComment");


    }


    void Update()
    {
        
        if (gamestart)
        {
            //   photonView.RPC(nameof(SugorokuTUrntoRPC), RpcTarget.All);

        }

    }

    private void MonthSetting()//�}�X�Ɍ��Ɠ��t������
    {
        int Xmonth = 0;//�ݒu����}�b�v��X�̔z������炷
        int Ymonth = 0;//�ݒu����}�b�v��Y�̔z������炷
        
        for (int block = 0; block < this.month.Length; block++)//�w�肷�錎���ǂ̃u���b�N�ɂ��邩����
        {
            switch (block)//���ꂼ��̃u���b�N�Ɏw�肵�����t������悤�ɂ���
            {
                case 0:
                    Xmonth = 0;     Ymonth = 0;
                    break;
                case 1:
                    Xmonth = height[0].width.Length / 2;     Ymonth = 0;
                    break;
                case 2:
                    Xmonth = 0;     Ymonth = height.Length / 2;
                    break;
                case 3:
                    Xmonth = height[0].width.Length / 2;     Ymonth = height.Length / 2;
                    break;
            }
            for (int month = 0; month < 12; month++)//month�ɉ����������
            {
                if (this.month[block] == month + 1)//�w�肵���������������ʂ���
                {
                    DaySetting(month, Ymonth, Xmonth);//�}�X�ɓ��t������
                }
            }
        }
        
    }

    private void DaySetting(int month,int Ymonth, int Xmonth)
    {
        int nullday = 0;//�󔒂̓��t
        int countday = 0;//�������t
        int Maxday = 0;//���̌��̍ő���t

        switch (month + 1)
        {
            case 1:
                nullday = 6; Maxday = 31;
                break;
            case 2:
                nullday = 2; Maxday = 28;
                break;
            case 3:
                nullday = 2; Maxday = 31;
                break;
            case 4:
                nullday = 5; Maxday = 30;
                break;
            case 5:
                nullday = 0; Maxday = 31;
                break;
            case 6:
                nullday = 3; Maxday = 30;
                break;
            case 7:
                nullday = 5; Maxday = 31;
                break;
            case 8:
                nullday = 1; Maxday = 31;
                break;
            case 9:
                nullday = 4; Maxday = 30;
                break;
            case 10:
                nullday = 6; Maxday = 31;
                break;
            case 11:
                nullday = 2; Maxday = 30;
                break;
            case 12:
                nullday = 4; Maxday = 31;
                break;
        }
        for (int n = 0; n < 7 - nullday; n++)//���T�ڂɓ��t������
        {
            countday++;
            height[Ymonth].width[Xmonth + nullday + n].GetComponent<Mass>().Day = month + 1 + "/" + countday;
        }

        for (int h = 1; h < height.Length / 2; h++)//���T�ȍ~�̓��t������
        {
            for (int w = 0; w < height[0].width.Length / 2; w++)
            {
                if (countday < Maxday)
                {
                    countday++;
                    height[Ymonth + h].width[Xmonth + w].GetComponent<Mass>().Day = month + 1 + "/" + countday;
                }
            }
        }
    }




    [PunRPC]
    private void GoalClear()//�S�Ẵ}�X�̃S�[��������
    {
        for (int i = 0; i < height.Length; i++)
        {
            for (int l = 0; l < height[0].width.Length; l++)
            {
                // Debug.Log(height[i].width[l]);
                height[i].width[l].GetComponent<Mass>().GoalOff();//�S�[���������Ă���
            }
        }
    }



    private void GoalDecision()//���߂ăS�[�����o��������
    {
      
        int Week, Day;
        photonView.RPC(nameof(GoalClear), RpcTarget.All);//�����_���ȃS�[���̏ꏊ������
                                               //�S�Ẵ}�X�̃S�[��������
        do {
            Week = Random.Range(0, height.Length);                  //week�E���̗�̃����_��
            Day = Random.Range(0, height[0].width.Length);          //day�E�c�̗�̃����_��
       
        } while (height[Week].width[Day].GetComponent<Mass>().invalid == true);//�����_���ɑI�񂾃}�X�����݂��Ă�����̂�������܂ŌJ��Ԃ�

        Debug.Log("EWEW"+height[Week].width[Day]);
        photonView.RPC(nameof(GoalPutRPC), RpcTarget.All, Week, Day);                        //�S�[���z��ԍ����L��

    }
    
    public void GoalAgain()                                         //�S�[���̍Đݒu(�������ɂȂ�Ȃ��悤��)
    {
        int Week, Day;                                              //�����_���ȃS�[���̏ꏊ������
        photonView.RPC(nameof(GoalClear), RpcTarget.All);                                            //�S�Ẵ}�X�̃S�[��������
        do
        {
            Week = Random.Range(0, height.Length);                  //���̗�̃����_��
            Day = Random.Range(0, height[0].width.Length);          //�c�̗�̃����_��
        } while (height[Week].width[Day].GetComponent<Mass>().invalid == true && MonthCount(Day, Week) == true);//�I�񂾃}�X�����݂��Ă�����́�����������Ȃ����̂�������܂ŌJ��Ԃ�
        photonView.RPC(nameof(GoalPutRPC), RpcTarget.All, Week, Day);
      //  GoalPutRPC(Week,Day);
    }

    [PunRPC]
    public void GoalPutRPC(int we,int da ){
     
        height[we].width[da].GetComponent<Mass>().GoalOn();      //�S�[���̐ݒu
        XGoal = da; YGoal = we;                                  //�S�[���z��ԍ����L��

        }





    private bool MonthCount(int x, int y)//�S�[���Ɠ����������f����
    {
        if (WhichMonth(XGoal, YGoal) == WhichMonth(x, y))//�������Ȃ�true
        {
            return true;
        }
        else//�Ⴄ���Ȃ�false
        {
            return false;
        }
    }
    private int WhichMonth(int x, int y)//x,y�������ɂ���̂����ׂ�
    {
        int Month = 0;
        if (x < height[0].width.Length / 2 && y < height.Length / 2) { Month = 1; }//����̌��ɂ��邩�ǂ���
        if (height[0].width.Length / 2 <= x && y < height.Length / 2) { Month = 2; }//����̌��ɂ��邩�ǂ���
        if (x < height[0].width.Length / 2 && height.Length / 2 < y) { Month = 3; }//����̌��ɂ��邩�ǂ���
        if (height[0].width.Length / 2 <= x && height.Length / 2 < y) { Month = 4; }//����̌��ɂ��邩�ǂ���
        return Month;

    }






    //======================================================================================================


    public void StartOfimitation()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        photonView.RPC(nameof(hashRoom_StartUp), RpcTarget.AllViaServer);
        StartCoroutine(StartOfimitation_TransitionToResult());
       

    }


    public IEnumerator StartOfimitation_TransitionToResult() �@�@�@//���U���g�ɔ��
    {
        yield return new WaitForSeconds(0.4f);
        Start_Col_Debac();
        yield break;

    }



    public void Start_Col_Debac()
    { 
        GoalDecision();//�S�[���̑I��
        photonView.RPC(nameof(RadyClose), RpcTarget.All);
        GameStartButton.SetActive(false);
        photonView.RPC(nameof(AbleToPlayerControl), RpcTarget.All);
    }


    [PunRPC]
    public void RadyClose()
    {
        RadyButton.SetActive(false);

    }





    [PunRPC]
    public void hashRoom_StartUp()
    {
        hashRoom = new Hashtable();
        hashRoom["Turn_of_Player"] = 0;
        hashRoom["GoalCount"] =0;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashRoom);

    }




    [PunRPC]
    public  void AbleToPlayerControl()
    {

        play = (int)PhotonNetwork.CurrentRoom.CustomProperties["Turn_of_Player"]; //Turn_of_Player�̒l���擾�@*�ǐ��̂���

        if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[play]){
            Player[play].GetComponent<PlayerStatus>().TurnDice();             //�v���C���[���R���g���[���o����悤�ɂ���
           
        }
        
    }


    public void  AfterMoving()
    {
        //  Debug.Log(Player[play].GetComponent<PlayerStatus>().Goalup);
        if (Player[play].GetComponent<PlayerStatus>().Goalup == true)   //�������̎�ԂɃS�[�����Ă�����
        {

            photonView.RPC(nameof(GettingGoal), RpcTarget.All);
            Player[play].GetComponent<PlayerStatus>().Goalup = false;   //�S�[���錾������
            GoalAgain();
      
        }

        playerRounded();
    }


    [PunRPC]
    public void GettingGoal()
    {

        Goalcount++;

        if (Goalcount >= 4)   //�S�[�����������S�Ȃ�
        {
            photonView.RPC(nameof(GameFinish), RpcTarget.All);
            //�Q�[���I��
        }

    }








    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        // �X�V���ꂽ���[���̃J�X�^���v���p�e�B�̃y�A���R���\�[���ɏo�͂���
       




       


    }




        public void playerRounded()
    {
        play++;
        if (play >= PhotonNetwork.PlayerList.Length)//�v���C���[�Q���l���𒴂�����
        {

            play = 0;     //�v���C���[0�̎�ԂɂȂ�*****************
        }
      //  Debug.Log("################################"+ hashRoom);
        //Debug.Log("################################" +hashRoom["Turn_of_Player"]);
        hashRoom["Turn_of_Player"] = play;

        PhotonNetwork.CurrentRoom.SetCustomProperties(hashRoom);


        StartCoroutine(playerRounded_Coroutine());
       
        


    }





    public IEnumerator playerRounded_Coroutine()
    {

        yield return new WaitForSeconds(0.2f);
        AbleToPlayerControl_Demo();

        yield break;
    }



    public void AbleToPlayerControl_Demo()
    {
        photonView.RPC(nameof(AbleToPlayerControl), RpcTarget.All);
    }






    //  public IEnumerator AfterMoving_Coroutine()
    //  {
    //
    //  yield return new WaitForSeconds(1.2f);



    //     yield break;
    //   }




    [PunRPC]
    public void GameFinish()
    {

        SceneManager.GetComponent<SceneManagaer>().TransitionToResult();
        Debug.Log("�Q�[���I��!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }



    public void daycommentoff()
    {
        photonView.RPC(nameof(Daycommentoff), RpcTarget.All);
    }

    [PunRPC]
    public void Daycommentoff()//�������߂�
    {

        Dcomment.GetComponent<DayComment>().DayCommentoff();

    }




}


[System.Serializable]
public class Width//week�̎q�E����̃I�u�W�F�N�g�̎擾
{
    public  GameObject[] width;




}