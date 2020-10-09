using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBhvr : StateMachineBehaviour
{
    public HumanBodyBones attackLimb; // membrul cu care ataca in animatia curenta

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float normalizedTime = stateInfo.normalizedTime;
        if(normalizedTime > 0.04f && normalizedTime < 0.15f) // intervalul de timp in care HitBox este activ
            animator.GetBoneTransform(attackLimb).GetComponent<SphereCollider>().enabled = true;
        else
            animator.GetBoneTransform(attackLimb).GetComponent<SphereCollider>().enabled = false;


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

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
