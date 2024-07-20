using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBehaviour : StateMachineBehaviour
{
    public string boolName; // 컨트롤 할 Bool형 데이터 파라미터
    public bool updateOnState; // State 에서 컨트롤 할 건지에 대한 여부
    public bool updateOnStateMachine; // StateMachine 에서 컨트롤 할 건지에 대한 여부
    public bool valueOnEnter, valueOnExit; // Enter, Exit 상태일 때 처리할 값
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // UpdateOnState가 true 상태일때
        if(updateOnState)
        {
            // boolName의 이름을 가진 Bool 파라미터의 값을 valueOnEnter로 설정한다.
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // UpdateOnState가 true 상태일때
        if (updateOnState)
        {
            // boolName의 이름을 가진 Bool 파라미터의 값을 valueOnExit로 설정한다.
            animator.SetBool(boolName, valueOnExit);
        }
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        // UpdateOnStateMachine이 true 상태일때
        if (updateOnStateMachine)
        {
            // boolName의 이름을 가진 Bool 파라미터의 값을 valueOnEnter로 설정한다.
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        // UpdateOnStateMachine이 true 상태일때
        if (updateOnStateMachine)
        {
            // boolName의 이름을 가진 Bool 파라미터의 값을 valueOnExit로 설정한다.
            animator.SetBool(boolName, valueOnExit);
        }
    }
}
