using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0.5f; // 페이드 효과 지속 시간
    public float fadeDelay = 0; // 페이드 효과 지연 시간

    private float timerElapsed = 0; // 페이드 효과 타이머
    private float timerDelayElaspsed = 0; // 페이드 지연 효과 타이머
    private SpriteRenderer spriteRenderer; // 페이드 효과를 적용할 SpriteRenderer
    private GameObject objToRemove; // 페이드 효과 적용 후 파괴할 오브젝트
    Color startColor; // 페이드 효과 적용 전 시작 컬러값

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timerElapsed = 0;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(fadeDelay > timerDelayElaspsed)
        {
            timerDelayElaspsed += Time.deltaTime;
        }
        else
        {
            // 페이드 효과 타이머
            timerElapsed += Time.deltaTime;

            // fadeTime : 1
            // timerElapsed : 0
            // newAlpha : 1
            // fadeTime : 1
            // timerElapsed : 0.5
            // newAlpha : 0.5

            float newAlpha = startColor.a * (1 - (timerElapsed / fadeTime));

            spriteRenderer.color =
                new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            if(timerElapsed > fadeTime)
            {
                Destroy(objToRemove);
            }
        }
    }
}
