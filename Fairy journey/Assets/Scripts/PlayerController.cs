using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 6;

    [SerializeField] float jumpSpeed = 8;
    CircleCollider2D legsCollider;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        legsCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SideMove();
        FlipChar();
        PlayerJump();
        
    }

    void SideMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

        float velX = Mathf.Abs(rb.velocity.x);
        if (velX > 0.5f) anim.SetBool("walk", true);
        else anim.SetBool("walk", false);
    }

    void FlipChar()
    {
        if(rb.velocity.x > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (rb.velocity.x < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    void PlayerJump()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        bool isGrounded = legsCollider.IsTouchingLayers(groundLayer);
        if(isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }

    }



}
