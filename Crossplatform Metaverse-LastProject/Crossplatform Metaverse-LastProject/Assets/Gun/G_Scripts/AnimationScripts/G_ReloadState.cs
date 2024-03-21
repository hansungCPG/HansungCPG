using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_ReloadState : StateMachineBehaviour
{
    //재장전 애니메이션에서 사용되는 스크립트
    public float reloadTime = 0.9f;   //애니메이션 진행도의 퍼센트를 의미함. 1f는 애니메이션이 100% 진행되었을 때
    bool hasReloaded = false;
    public G_PCCharacter body;
    public G_MobileCharacter mobileBody;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.GetComponentInParent<Transform>().
            GetComponentInParent<Transform>().
            GetComponentInParent<G_PCCharacter>();
        mobileBody = animator.GetComponentInParent<Transform>().
            GetComponentInParent<Transform>().
            GetComponentInParent<G_MobileCharacter>();
        hasReloaded = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hasReloaded) return;

        //재장전 시간만큼 애니메이션이 충분히 진행되었다면 리로드
        if (stateInfo.normalizedTime >= reloadTime)
        {
            if(body)
                body.DoReload();
            if (mobileBody)
                mobileBody.DoReload();
            hasReloaded = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasReloaded = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
