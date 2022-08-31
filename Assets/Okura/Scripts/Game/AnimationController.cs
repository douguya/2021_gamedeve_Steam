using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    Animator Anim;
    [SerializeField]
    AnimationCurve AnimCurve;

    Vector3 StartPos;
    void Start()
    {
        Anim = this.GetComponent<Animator>();
        StartPos = this.transform.position;
        AnimCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    }

    public IEnumerator StartAnimation(float MaxValue) {
        var Position = this.transform.position;
        float time = 0;

        while(this.transform.position.y < MaxValue) {
            time += 0.01f;
            Debug.Log(this.transform.position.x);

            Position.x = AnimCurve.Evaluate((Mathf.Ceil(time * 100) % 2) * 0.1f) + StartPos.x;
            Position.y = (MaxValue + (-StartPos.y)) * AnimCurve.Evaluate(time) + StartPos.y;
            this.transform.position = Position;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
