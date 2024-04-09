using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OozeEnemy : MonoBehaviour, IDamagable
{
    Rigidbody2D rb;
    [SerializeField] float speed = 2;

    [SerializeField] int hp = 3;
    bool isAlive = true;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false) return;
        bool isWall = CheckWall();
        bool isBreak = CheckBreak();
        
        if(isWall == true  || isBreak == false)
        {
            transform.Rotate(0, 180, 0);
            rb.velocity = -transform.right * speed;
        }

    }

    bool CheckWall()
    {
        Vector2 origin = transform.position;
        Vector2 dir = -transform.right;
        float distance = 0.8f;
        LayerMask layer = LayerMask.GetMask("Ground", "Hazards");
        bool isHit = Physics2D.Raycast(origin, dir, distance, layer);
        return isHit;
    }

    bool CheckBreak()
    {
        Vector2 origin = transform.position;
        Vector2 dir = -transform.right - transform.up;
        float distance = 0.8f;
        LayerMask layer = LayerMask.GetMask("Ground");
        bool isHit = Physics2D.Raycast(origin, dir, distance, layer);
        return isHit;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.transform.GetComponent<PlayerHealth>();
        if(player)
        {
            Vector3 vel = new Vector3(-5, 7);
            if (player.transform.position.x > transform.position.x)
                vel = new Vector3(5, 7);

            player.GetDamage(vel);
        }

    }

    public void GetDamage(int damage)
    {
        if (isAlive == false) return;
        hp -= damage;

        if(hp <= 0)
        {
            Death();
        }

    }

    void Death()
    {
        isAlive = false;
        rb.velocity = new Vector2();
        anim.SetTrigger("death");
        GetComponent<Collider2D>().enabled = false;
        // выпадение монет с монстра
        Destroy(gameObject, 20);
    }



}
