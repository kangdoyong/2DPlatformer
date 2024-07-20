using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0.5f; // ���̵� ȿ�� ���� �ð�
    public float fadeDelay = 0; // ���̵� ȿ�� ���� �ð�

    private float timerElapsed = 0; // ���̵� ȿ�� Ÿ�̸�
    private float timerDelayElaspsed = 0; // ���̵� ���� ȿ�� Ÿ�̸�
    private SpriteRenderer spriteRenderer; // ���̵� ȿ���� ������ SpriteRenderer
    private GameObject objToRemove; // ���̵� ȿ�� ���� �� �ı��� ������Ʈ
    Color startColor; // ���̵� ȿ�� ���� �� ���� �÷���

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
            // ���̵� ȿ�� Ÿ�̸�
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
