using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE, 
    WALK,
    FALL,
    CLIMB,
    ATTACK,
    DISABLED,
    DEAD
}

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 6;

    [SerializeField] float jumpSpeed = 8;
    CircleCollider2D legsCollider;
    CapsuleCollider2D bodyCollider;
    [SerializeField] float climbSpeed = 5;
    float gravity;
    Animator anim;

    public PlayerState state = PlayerState.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        bodyCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        legsCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        SideMove();
        FlipChar();
        PlayerJump();
        Climb();
        SetAnim();
    }


    void Climb()
    {
        rb.gravityScale = gravity;
        LayerMask ladderLayer = LayerMask.GetMask("Ladders");
        bool onLadder = bodyCollider.IsTouchingLayers(ladderLayer);
        if (onLadder == false) return;
        rb.gravityScale = 0;
        float inputY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, inputY * climbSpeed);

        LayerMask groundLayer = LayerMask.GetMask("Ground");
        bool isGrounded = legsCollider.IsTouchingLayers(groundLayer);
        if(isGrounded == false) state = PlayerState.CLIMB;
    }

    void SetAnim()
    {
        anim.enabled = true;
        if (state == PlayerState.IDLE) anim.SetInteger("state", 0);
        else if (state == PlayerState.WALK) anim.SetInteger("state", 1);
        else if (state == PlayerState.FALL) anim.SetInteger("state", 2);
        else if (state == PlayerState.CLIMB) StartCoroutine(SetClimbAnim());

    }

    IEnumerator SetClimbAnim()
    {
        anim.SetInteger("state", 3);
        yield return null; 
        if(rb.velocity.magnitude < 0.5)
        {
           anim.enabled = false;
        }
    }

    void SideMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

        float velX = Mathf.Abs(rb.velocity.x);
        if (velX > 0.5f) state = PlayerState.WALK;
        else state = PlayerState.IDLE;
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
        else
        {
            state = PlayerState.FALL;
        }

    }



}
