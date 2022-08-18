using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Camera_Mouse : MonoBehaviour
{
    private float Mousewheel;//�}�E�X�z�C�[���̒l
    public int Zoom_Speed;//�Y�[���̃X�s�[�h
    private Vector2 mouse_set;//�}�E�X�̍��W
    private float Adjust_Variable= 0.009f;//���_�C���p�̒l�@Zoom_Speed��50��p�@�p���C
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Wheel_Zoom();//�}�E�X�z�C�[���ɂ��Y�[��
    }
    private void Wheel_Zoom()
    {
        Mousewheel =Input.GetAxis("Mouse ScrollWheel");//�}�E�X�z�C�[���l�̕ۑ�
        mouse_set= new Vector2(Input.mousePosition.x-Screen.width/2, Input.mousePosition.y-Screen.height/2);//��ʂ̒��S�����_�Ƃ����}�E�X�̃X�N���[�����W�̎擾

        if (Mousewheel!=0)//�}�E�X�z�C�[�����s��ꂽ�ꍇ
        {
            Vector3 Zoom_Adjust = new Vector3(mouse_set.x*Adjust_Variable, 0, mouse_set.y*Adjust_Variable);//�}�E�X�z�C�[���ɂ���ʂ̌��_�C��
            if (Mousewheel>0)//������z�C�[��
            {
                transform.position+= (transform.forward*Mousewheel*Zoom_Speed)+ Zoom_Adjust;
            }
            if (Mousewheel<0)//�������z�C�[��
            {
                transform.position+= (transform.forward*Mousewheel*Zoom_Speed)- Zoom_Adjust;
            }
        }
    }

    public float Map(float value, float R_min, float R_max, float V_min, float V_max)
    {

        return V_min+ (V_max-V_min)/((R_max-R_min)/(value-R_min));//value��V_min����V_Max�͈̔͂���R_min����R_max�͈̔͂ɂ���

    }





}
