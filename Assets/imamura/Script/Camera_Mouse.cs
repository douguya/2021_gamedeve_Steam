using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Camera_Mouse : MonoBehaviourPunCallbacks
{
    public I_game_manager I_game_manager;
    private float Mousewheel;//マウスホイールの値
    public int Zoom_Speed;//ズームのスピード
    private Vector2 mouse_set;//マウスの座標
    private float Adjust_Variable= 0.009f;//原点修正用の値　Zoom_Speed＝50専用　用改修
    private Vector3 OriginPoint;//カメラの初期位置
    public Vector3 OriginRect;//カメラの初期位置 


    private Vector3 velocity = Vector3.zero;

    public bool Camera_Move_initials2 = false;//初期位置用
    public bool Camera_Move_highlight = false;
    public bool Camera_Move_Player = false;
    public bool Camera_Move_Goal = false;

    private Vector3 Max_Position=new Vector3(84f, 82f, 56f);//カメラの限界位置
    private Vector3 Mini_Position = new Vector3(-69f, 6f, -69);//カメラの限界位置

    public Vector3 Position_highlight = new Vector3(0f, 0f, 0f);//ハイライト時のカメラの目的地
    public Vector3 Rotate_highlight = new Vector3(0f, 0f, 0f);//ハイライト時のカメラの目的角度


    public Vector3 Position_Player = new Vector3(0f, 0f, 0f);//ぷれいやー時のカメラの目的地
    public Vector3 Rotate_Player = new Vector3(0f, 0f, 0f);//ぷれいやー時のカメラの目的角度

    public Vector3 Position_Goal = new Vector3(0f, 0f, 0f);//ゴール時のカメラの目的地
    public Vector3 Rotate_Goal = new Vector3(0f, 0f, 0f);//ゴール時のカメラの目的角度

    public Vector3 Position_Goal_Play = new Vector3(0f, 0f, 0f);//ゴール時のカメラの目的地
    public Vector3 Rotate_Goal_Play = new Vector3(0f, 0f, 0f);//ゴール時のカメラの目的角度

    public bool Permission_Zoom = true;
    
    float xVelocity = 0.0f;
    float yVelocity = 0.0f;
    float zVelocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        OriginPoint=transform.position;
        OriginRect = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (I_game_manager.HowMyTurn) {
            Wheel_Zoom();//マウスホイールによるズーム]
        }
      

        if (Camera_Move_highlight)//カメラを指定の位置に動かす
        {
            transform.position = Vector3.SmoothDamp(transform.position, Position_highlight, ref velocity, 0.4f);
            var rote = transform.eulerAngles;


            rote.x =   Mathf.SmoothDampAngle(rote.x, Rotate_highlight.x, ref xVelocity, 0.4f);
            rote.y =   Mathf.SmoothDampAngle(rote.y, Rotate_highlight.y, ref yVelocity, 0.4f);
            rote.z =   Mathf.SmoothDampAngle(rote.z, Rotate_highlight.z, ref zVelocity, 0.4f);


            transform.eulerAngles=rote;
            // Damp angle from current y-angle towards target y-angle
        

        }
        if (Vector3.Distance(transform.position, Position_highlight)<1&&Camera_Move_highlight==true)
        {


            Camera_Move_highlight=false;

           // transform.eulerAngles= OriginRect;
        }

        if (Camera_Move_initials2)//カメラを初期位置に動かす(角度含めて)
        {
            transform.position = Vector3.SmoothDamp(transform.position, OriginPoint, ref velocity, 0.4f);

            var rote = transform.eulerAngles;


            rote.x = Mathf.SmoothDampAngle(rote.x, OriginRect.x, ref xVelocity, 0.4f);
            rote.y = Mathf.SmoothDampAngle(rote.y, OriginRect.y, ref yVelocity, 0.4f);
            rote.z = Mathf.SmoothDampAngle(rote.z, OriginRect.z, ref zVelocity, 0.4f);


            transform.eulerAngles = rote;
        }
        if (Vector3.Distance(transform.position, OriginPoint) <1&&Camera_Move_initials2)
        {　　
            Camera_Move_initials2 = false;
            InstanceEnd();

            transform.eulerAngles= OriginRect;

        }



        if (Camera_Move_Goal)//カメラをゴール位置に動かす(角度含めて)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Position_Goal, ref velocity, 0.4f);

            var rote = transform.eulerAngles;


            rote.x = Mathf.SmoothDampAngle(rote.x, Rotate_Goal.x, ref xVelocity, 0.4f);
            rote.y = Mathf.SmoothDampAngle(rote.y, Rotate_Goal.y, ref yVelocity, 0.4f);
            rote.z = Mathf.SmoothDampAngle(rote.z, Rotate_Goal.z, ref zVelocity, 0.4f);


            transform.eulerAngles = rote;
        }
        if (Vector3.Distance(transform.position, Position_Goal) <1&&Camera_Move_Goal)
        {
            Camera_Move_Goal = false;

            CameraPlayer(Position_Goal_Play, Rotate_Goal_Play);

        }


        if (Camera_Move_Player)//カメラをプレイアーに動かす(角度含めて)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Position_Player, ref velocity, 0.4f);

            var rote = transform.eulerAngles;


            rote.x = Mathf.SmoothDampAngle(rote.x, Rotate_Player.x, ref xVelocity, 0.4f);
            rote.y = Mathf.SmoothDampAngle(rote.y, Rotate_Player.y, ref yVelocity, 0.4f);
            rote.z = Mathf.SmoothDampAngle(rote.z, Rotate_Player.z, ref zVelocity, 0.4f);


            transform.eulerAngles = rote;
        }
        if (Vector3.Distance(transform.position, Position_Player) <1&&Camera_Move_Player)
        {
            Camera_Move_Player = false;

            CameraReset();

        }


    }

    public void InstanceEnd()
    {
        GameObject[] Players_spot = GameObject.FindGameObjectsWithTag("Player");//プレイヤーオブジェクトの一時保存場所　タグで軒並みとる

        foreach (GameObject obj in Players_spot)//プレイヤーリストの中身と、一時保存したプレイヤーオブジェクトを突き合わせる
        {
            if (obj.GetComponent<PhotonView>().IsMine)
            {
                if (obj.GetComponent<I_Day_Effect>().InsTance_ON==true)
                {
     
                    obj.GetComponent<I_Day_Effect>().Instance_end=true;
                    obj.GetComponent<I_Day_Effect>().InsTance_ON=false;
                }
            }
        }
        
    }


    private void Wheel_Zoom()
    {
        if (Permission_Zoom)
        {
            Mousewheel =Input.GetAxis("Mouse ScrollWheel");//マウスホイール値の保存
            mouse_set= new Vector2(Input.mousePosition.x-Screen.width/2, Input.mousePosition.y-Screen.height/2);//画面の中心を原点としたマウスのスクリーン座標の取得

            
                if (Mousewheel!=0)//マウスホイールが行われた場合
                {
                    Vector3 Zoom_Adjust = new Vector3(mouse_set.x*Adjust_Variable, 0, mouse_set.y*Adjust_Variable);//マウスホイールによる画面の原点修正
                    if (Mousewheel>0)//上向きホイール ズームイン
                    {
                       var position= transform.position+ (transform.forward*Mousewheel*Zoom_Speed)+ Zoom_Adjust;

                       if (Zoonjudge2(position)) 
                       {
                        transform.position= Zoonconvert( position,transform.position);
                       }

                    }
                    if (Mousewheel<0)//下向きホイール　ズームアウト
                    {
                      
                       var position = transform.position+ (transform.forward*Mousewheel*Zoom_Speed)- Zoom_Adjust; ;

                       if (Zoonjudge2(position))
                       {
                        transform.position= Zoonconvert(position, transform.position);
                    }

                }
                


            }

        }
    }


    private bool Zoonjudge2(Vector3 position)
    {
        
        bool juje = false; 
        if ( position.y< Max_Position.y&&position.y> Mini_Position.y)
        {
            
             juje=true;
            
        }



        return juje;

    }
    private Vector3 Zoonconvert(Vector3 position,Vector3 FalsePosition)
    {
        
        Vector3 NewPosition = position;
        if (position.x> Max_Position.x|| position.x< Mini_Position.x)
        {
            NewPosition.x=FalsePosition.x;
        }
        if (position.z> Max_Position.z||position.z<Mini_Position.z)
        {
            NewPosition.z=FalsePosition.z;
        }



        return NewPosition;

    }




    private bool Zoonjudge(Vector3 position)
    {
        Debug.Log(position);
        bool juje=false;
       if(position.x< Max_Position.x&& position.y< Max_Position.y&&position.z< Max_Position.z)
       {
            if (position.x> Mini_Position.x&& position.y> Mini_Position.y&&position.z>Mini_Position.z)
            {
                juje=true;
            }
       }









        return juje;

    }










   


    public void CameraReset()
    {
        Camera_Move_initials2=true;
    }

    public void Camera_highlight()
    {

        Camera_Move_highlight =true;
    }

    public void Camera_highlight_imi(Vector3 Position,Vector3 Rotate)
    {
        
        Rotate_highlight =Rotate;
        Position_highlight =Position;
        Camera_Move_highlight =true;
    }






    public float Map(float value, float R_min, float R_max, float V_min, float V_max)
    {
        /*
        float Rrenge = (R_max-R_min);
        float convartR = (value-R_min);
        float Rratio = Rrenge/ convartR;

        float Vrenge = (V_max-V_min);
        float VDelta = (Vrenge/Rratio);


        */

        return V_min+ (V_max-V_min)/((R_max-R_min)/(value-R_min));//valueをV_minからV_Maxの範囲からR_minからR_maxの範囲にする

    }


    public void CameraOwnership()
    {
        photonView.RequestOwnership();
    }


    public void CameraPlayer(Vector3 Position, Vector3 Rotate)
    {
        Position_Player =Position;
        Rotate_Player =Rotate;
        Camera_Move_Player =true;
    }
    public void CameraGoal(Vector3 Position, Vector3 Rotate, Vector3 PositionP, Vector3 RotateP)
    {
        Position_Goal    =Position;
        Rotate_Goal      =Rotate;

        Position_Goal_Play=PositionP;
        Rotate_Goal_Play =RotateP;
       Camera_Move_Goal =true;
    }

}
