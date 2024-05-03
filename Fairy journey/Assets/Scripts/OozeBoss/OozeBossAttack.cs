using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OozeBossAttack : StateMachineBehaviour
{
    [SerializeField] GameObject orbPrefab;
    PlayerHealth player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = FindObjectOfType<PlayerHealth>();
        animator.GetComponent<OozeBossMain>().StartCoroutine(SpawnOrbsCoroutine(animator));
    }

    IEnumerator SpawnOrbsCoroutine(Animator anim)
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 3; i++)
        {
            float orbSpeedX = Random.Range(6f, 9f);
            if (anim.transform.right.x < 0) orbSpeedX = -orbSpeedX;
            float orbSpeedY = Random.Range(4f, 7f);
            Vector3 spawnPos = anim.transform.position + anim.transform.right;
            Vector2 vel = new Vector2(orbSpeedX, orbSpeedY);   
            GameObject orb = Instantiate(orbPrefab, spawnPos, Quaternion.identity);
            orb.GetComponent<Rigidbody2D>().velocity = vel;
        }
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("idle");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
