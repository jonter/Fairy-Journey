using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;

public enum BeholderState
{
    IDLE,
    WALK,
    RUSH,
    DISABLED,
    DEAD
}

public class BeholderEnemy : MonoBehaviour
{
    AIPath path;
    Rigidbody2D rb;
    PlayerHealth player;
    Animator anim;

    BeholderState state = BeholderState.IDLE;

    [SerializeField] float rageRadius = 7;
    [SerializeField] float attackRadius = 4;

    [SerializeField] float rushSpeed = 6;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rageRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        path = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerHealth>();
        StartCoroutine(SearchPlayer());
    }

    IEnumerator SearchPlayer()
    {
        yield return new WaitForSeconds(1);
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < rageRadius)
        {
            state = BeholderState.WALK;
        }
        else
        {
            StartCoroutine(SearchPlayer());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (state != BeholderState.WALK) return;

        path.destination = player.transform.position;
        FlipEnemy();
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < attackRadius) StartCoroutine(RushCoroutine());
    }

    IEnumerator RushCoroutine()
    {
        PrepareForRush();
        yield return new WaitForSeconds(0.5f);
        MakeRush();
        
        StartCoroutine(CheckForCollision());

    }

    IEnumerator CheckForCollision()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            LayerMask layers = LayerMask.GetMask("Ground", "Hazards", "Player");
            Collider2D col = Physics2D.OverlapCircle(transform.position, 0.45f, layers);
            if (col)
            {
                HandleCollision(col);
                yield break;
            }
            yield return null;
        }
        StartCoroutine(DisableCoroutine(0.2f));
    }

    void HandleCollision(Collider2D col)
    {
        PlayerHealth p = col.GetComponent<PlayerHealth>();
        if (p)
        {
            Vector3 vel = new Vector3(10, 2, 0);
            if(p.transform.position.x < transform.position.x)
                vel = new Vector3(-10, 2, 0);
            p.GetDamage(vel);
            StartCoroutine( DisableCoroutine(0.5f));
        }
        else
        {
            StartCoroutine(DisableCoroutine(1.5f));
            FlyAway();
        }
    }

    void FlyAway()
    {
        path.canMove = true;
        LayerMask layer = LayerMask.GetMask("Ground", "Hazards");
        RaycastHit2D hitBottom = 
            Physics2D.Raycast(transform.position, -transform.up, 0.5f, layer);
        RaycastHit2D hitUp =
           Physics2D.Raycast(transform.position, transform.up, 0.5f, layer);
        RaycastHit2D hitRight =
           Physics2D.Raycast(transform.position, transform.right, 0.5f, layer);
        RaycastHit2D hitLeft =
           Physics2D.Raycast(transform.position, -transform.right, 0.5f, layer);

        Vector3 dest = transform.position;
        if (hitBottom.transform) dest += transform.up * 0.4f;
        else if (hitUp.transform) dest -= transform.up * 0.4f;
        else if (hitRight.transform) dest -= transform.right * 0.4f;
        else if (hitLeft.transform) dest += transform.right * 0.4f ;
        else dest += transform.up * 0.4f;

        path.destination = dest;
    }
    

    IEnumerator DisableCoroutine(float duration)
    {
        state = BeholderState.DISABLED;
        rb.velocity = new Vector2(0, 0);
        anim.SetBool("rush", false);
        yield return new WaitForSeconds(duration);
        path.canMove = true;
        state = BeholderState.WALK;
    }



    void MakeRush()
    {
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        if (dir.x > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
        else transform.rotation = Quaternion.Euler(0, 180, 0);
        rb.velocity = dir * rushSpeed;
        anim.SetBool("rush", true);
    }

    void PrepareForRush()
    {
        state = BeholderState.RUSH;
        path.canMove = false;
    }




    void FlipEnemy()
    {
        if(path.desiredVelocity.x > 0.1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (path.desiredVelocity.x < -0.1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }




    
}
