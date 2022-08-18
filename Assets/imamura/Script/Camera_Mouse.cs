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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Wheel_Zoom();//マウスホイールによるズーム
    }
    private void Wheel_Zoom()
    {
        Mousewheel =Input.GetAxis("Mouse ScrollWheel");//マウスホイール値の保存
        mouse_set= new Vector2(Input.mousePosition.x-Screen.width/2, Input.mousePosition.y-Screen.height/2);//画面の中心を原点としたマウスのスクリーン座標の取得

        if (Mousewheel!=0)//マウスホイールが行われた場合
        {
            Vector3 Zoom_Adjust = new Vector3(mouse_set.x*Adjust_Variable, 0, mouse_set.y*Adjust_Variable);//マウスホイールによる画面の原点修正
            if (Mousewheel>0)//上向きホイール
            {
                transform.position+= (transform.forward*Mousewheel*Zoom_Speed)+ Zoom_Adjust;
            }
            if (Mousewheel<0)//下向きホイール
            {
                transform.position+= (transform.forward*Mousewheel*Zoom_Speed)- Zoom_Adjust;
            }
        }
    }

    public float Map(float value, float R_min, float R_max, float V_min, float V_max)
    {

        return V_min+ (V_max-V_min)/((R_max-R_min)/(value-R_min));//valueをV_minからV_Maxの範囲からR_minからR_maxの範囲にする

    }





}
