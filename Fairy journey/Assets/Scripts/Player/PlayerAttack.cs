using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    PlayerController controller;

    FireAttack fire;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        anim  = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        fire = GetComponentInChildren<FireAttack>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.state == PlayerState.IDLE || 
            controller.state == PlayerState.WALK)
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                StartCoroutine(AttackCoroutine());
            }

        }
        
    }

    IEnumerator AttackCoroutine()
    {
        controller.state = PlayerState.ATTACK;
        rb.velocity = new Vector2();

        int r = Random.Range(0, 2);
        if (r == 0) anim.SetInteger("state", 4);
        else anim.SetInteger("state", 5);
        yield return new WaitForSeconds(10f / 60f);
        fire.Activate();
        yield return new WaitForSeconds(10f / 60f);
        if (controller.state == PlayerState.DEAD) yield break;
        controller.state = PlayerState.IDLE;
    }
}
