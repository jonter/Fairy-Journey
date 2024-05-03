using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OozeBossWalk : StateMachineBehaviour
{
    float walkSpeed = 4;
    PlayerHealth player;
    Rigidbody2D rb;

    float attackDistance = 2.5f;
    Animator anim;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerHealth>();
        rb = animator.GetComponent<Rigidbody2D>();
        anim = animator;
        FlipBoss();
        rb.velocity = anim.transform.right * walkSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float xDistance = anim.transform.position.x - player.transform.position.x;
        if(xDistance < 0) xDistance = - xDistance;

        if(xDistance < attackDistance)
        {
            anim.SetTrigger("attack");
        }

    }

    void FlipBoss()
    {
        if (player.transform.position.x > anim.transform.position.x)
        {
            anim.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            anim.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = new Vector2();
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
