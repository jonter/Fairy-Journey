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

    int jumpCount = 0;
    int maxJumpCount = 1;

    bool isJumpReloaded = true;  

    // Start is called before the first frame update
    void Start()
    {
        transform.position = SaveSystem.data.playerPos;
        transform.rotation = SaveSystem.data.playerRot;
        if (SaveSystem.data.doubleJump == true) maxJumpCount = 2;

        bodyCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        legsCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        gravity = rb.gravityScale;
    }

    public void UnlockDoubleJump()
    {
        SaveSystem.data.doubleJump = true;
        maxJumpCount = 2;
        Hint.instance.ShowHint("Теперь тебе доступен двойной прыжок", 0.5f, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove() == true)
        {
            SideMove();
            FlipChar();
            PlayerJump();
            Climb();
        }
        SaveSystem.data.playerPos = transform.position;
        SaveSystem.data.playerRot = transform.rotation;
        
        SetAnim();
    }

    bool CanMove()
    {
        if (state == PlayerState.DISABLED) return false;
        if (state == PlayerState.DEAD) return false;
        if (state == PlayerState.ATTACK) return false;

        return true;
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
        if (isGrounded == true) jumpCount = 0;
        
        if(jumpCount < maxJumpCount && isJumpReloaded == true)
        {
            if (jumpCount == 0 && isGrounded == false) return;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(MakeJump());
            }
        }

        if(isGrounded == false)  state = PlayerState.FALL;

    }

    IEnumerator MakeJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        isJumpReloaded = false;
        yield return new WaitForSeconds(0.15f);
        isJumpReloaded = true;
        jumpCount++;
    }



}
