using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBehaviour : StateMachineBehaviour
{
    public string boolName; // ��Ʈ�� �� Bool�� ������ �Ķ����
    public bool updateOnState; // State ���� ��Ʈ�� �� ������ ���� ����
    public bool updateOnStateMachine; // StateMachine ���� ��Ʈ�� �� ������ ���� ����
    public bool valueOnEnter, valueOnExit; // Enter, Exit ������ �� ó���� ��
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // UpdateOnState�� true �����϶�
        if(updateOnState)
        {
            // boolName�� �̸��� ���� Bool �Ķ������ ���� valueOnEnter�� �����Ѵ�.
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // UpdateOnState�� true �����϶�
        if (updateOnState)
        {
            // boolName�� �̸��� ���� Bool �Ķ������ ���� valueOnExit�� �����Ѵ�.
            animator.SetBool(boolName, valueOnExit);
        }
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        // UpdateOnStateMachine�� true �����϶�
        if (updateOnStateMachine)
        {
            // boolName�� �̸��� ���� Bool �Ķ������ ���� valueOnEnter�� �����Ѵ�.
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        // UpdateOnStateMachine�� true �����϶�
        if (updateOnStateMachine)
        {
            // boolName�� �̸��� ���� Bool �Ķ������ ���� valueOnExit�� �����Ѵ�.
            animator.SetBool(boolName, valueOnExit);
        }
    }
}
