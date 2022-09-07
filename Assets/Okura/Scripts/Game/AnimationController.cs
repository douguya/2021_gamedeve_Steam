using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimationController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    AnimationCurve AnimCurve;
    [SerializeField]
    Camera MainCamera;
    [SerializeField]
    public Day_Square InstanceDay;

    public float InstanceY;
    public Vector3 CameraPos;
    public Vector3 CameraRot;

    Vector3 StartPos;
    Camera_Mouse CameraControll;
    void Start()
    {
        StartPos = this.transform.position;
        AnimCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        CameraControll = MainCamera.GetComponent<Camera_Mouse>();
    }

    public IEnumerator StartAnimation(float MaxValue) {
        photonView.RequestOwnership();
        var Position = this.transform.position;
        float time = 0;

        while(this.transform.position.y < MaxValue) {
            time += 0.01f;

            Position.x = AnimCurve.Evaluate((Mathf.Ceil(time * 100) % 2) * 0.1f) + StartPos.x;
            Position.y = (MaxValue + (-StartPos.y)) * AnimCurve.Evaluate(time) + StartPos.y;
            this.transform.position = Position;

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(2.0f);
        CameraControll.Camera_Move_initials2 = true;
    }


    [PunRPC] public void connection1()
    {




    }



}
