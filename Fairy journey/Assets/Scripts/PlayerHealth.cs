using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int maxHP = 5;
    int hp;
    PlayerController controller;
    Rigidbody2D rb;

    bool immortal = false;
    HealthDisplay hpDisplay;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        hpDisplay = FindObjectOfType<HealthDisplay>();
        hpDisplay.SetupHealth(maxHP);
    }

    public void GetDamage(Vector3 vel)
    {
        if (immortal == true) return;
        if (controller.state == PlayerState.DEAD) return;

        hp--;
        rb.velocity = vel;
        hpDisplay.Display(hp);

        if(hp <= 0)
        {
            StartCoroutine(DeathCoroutine());
        }    
        else StartCoroutine(DisableCoroutine());
    }

    IEnumerator DisableCoroutine()
    {
        controller.state = PlayerState.DISABLED;
        immortal = true;
        yield return new WaitForSeconds(0.1f);
        controller.state = PlayerState.FALL;
        yield return new WaitForSeconds(0.25f);
        immortal = false;
    }

    IEnumerator DeathCoroutine()
    {
        controller.state = PlayerState.DEAD;
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetInteger("state", 10);
        GetComponent<Rigidbody2D>().velocity = new Vector2();
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        // ещё добавить задержку по времени и показать экран проигрыша
    }


    
}
