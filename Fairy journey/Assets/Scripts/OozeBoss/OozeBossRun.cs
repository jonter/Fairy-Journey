using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class OozeBossRun : StateMachineBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float checkWallDist = 0.9f;
    [SerializeField] float rushDuration = 8;
    [SerializeField] float runSpeed = 11;
     
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        rb.velocity = animator.transform.right * runSpeed;
        animator.GetComponent<OozeBossMain>().StartCoroutine(StopRush(animator));
    }

    IEnumerator StopRush(Animator anim)
    {
        yield return new WaitForSeconds(rushDuration);
        anim.SetTrigger("idle");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool isHit = CheckWall();
        if(isHit == true)
        {
            rb.transform.Rotate(0, 180, 0);
            rb.velocity = animator.transform.right * runSpeed;
        }
    }

    bool CheckWall()
    {
        Vector2 origin = rb.transform.position;
        Vector2 dir = rb.transform.right;
        LayerMask layer = LayerMask.GetMask("Ground", "Hazards");
        bool isHit = Physics2D.Raycast(origin, dir, checkWallDist, layer);
        return isHit;
    }

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
