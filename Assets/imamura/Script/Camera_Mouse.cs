using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Camera_Mouse : MonoBehaviour
{
    private float Mousewheel;//マウスホイールの値
    public int Zoom_Speed;//ズームのスピード
    private Vector2 mouse_set;//マウスの座標
    private float Adjust_Variable= 0.009f;//原点修正用の値　Zoom_Speed＝50専用　用改修
    private Vector3 OriginPoint;//カメラの初期位置


    private Vector3 velocity = Vector3.zero;
    public bool Camera_Move_initials= false;//初期位置用
    public bool Camera_Move_highlight = false;

    private Vector3 Max_Position=new Vector3(84f, 82f, 56f);//カメラの限界位置
    private Vector3 Mini_Position = new Vector3(-69f, 6f, -69);//カメラの限界位置

    public Vector3 Position_highlight = new Vector3(0f, 0f, 0f);//ハイライト時のカメラの目的地
    public Vector3 Rotate_highlight = new Vector3(0f, 0f, 0f);//ハイライト時のカメラの目的角度
    public bool Permission_Zoom = true;
    float yVelocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        OriginPoint=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Wheel_Zoom();//マウスホイールによるズーム]

        if (Camera_Move_initials)//カメラを初期位置に動かす
        {
            transform.position = Vector3.SmoothDamp(transform.position, OriginPoint, ref velocity, 0.4f);
        }
        if (Vector3.Distance(transform.position, OriginPoint)<1)
        {
            Camera_Move_initials=false;
        }

        if (Camera_Move_highlight)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Position_highlight, ref velocity, 0.4f);
            var rote = transform.eulerAngles;


            rote.x =   Mathf.SmoothDampAngle(rote.x, Rotate_highlight.x, ref yVelocity, 0.4f);


            transform.eulerAngles=rote;
            // Damp angle from current y-angle towards target y-angle
        

        }
        if (Vector3.Distance(transform.position, Position_highlight)<1)
        {

            Camera_Move_highlight=false;
          

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
        Debug.Log(position);
        bool juje = false; 
        if ( position.y< Max_Position.y&&position.y> Mini_Position.y)
        {
            
             juje=true;
            
        }



        return juje;

    }
    private Vector3 Zoonconvert(Vector3 position,Vector3 FalsePosition)
    {
        Debug.Log(position);
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
        Camera_Move_initials=true;
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

}
