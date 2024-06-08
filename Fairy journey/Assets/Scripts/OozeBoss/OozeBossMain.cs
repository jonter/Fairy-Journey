using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OozeBossMain : MonoBehaviour, IDamagable
{
    Animator anim;
    [SerializeField] GameObject displayPrefab;
    BossHealthDisplay healthDisplay;

    [SerializeField] int startHP = 50;
    int hp;

    bool isAlive = true;

    [SerializeField] int coinsDrop = 30;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveSystem.data.oozeBossKilled == true) Destroy(gameObject);
        hp = startHP;
        anim = GetComponent<Animator>();
    }

    public void Activate()
    {
        anim.enabled = true;
        Hint.instance.ShowHint("Битва с боссом начинается", 0.5f, 3);
        StartCoroutine(SpawnHealthDisplay());
    }

    IEnumerator SpawnHealthDisplay()
    {
        yield return new WaitForSeconds(2f);
        Canvas c = FindObjectOfType<Canvas>();
        GameObject clone = Instantiate(displayPrefab, c.transform);
        healthDisplay = clone.GetComponent<BossHealthDisplay>();    
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player)
        {
            Vector3 dir = new Vector3(7, 15);
            if (player.transform.position.x < transform.position.x)
                dir = new Vector3(-7, 15);

            player.GetDamage(dir);
        }

    }

    public void GetDamage(int damage)
    {
        if (isAlive == false) return;
        hp -= damage;
        healthDisplay.SetHealth( (float)hp / startHP);

        if(hp <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(DeathCoroutine());
        }
        
    }

    IEnumerator DeathCoroutine()
    {
        isAlive = false;
        anim.SetTrigger("death");
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        healthDisplay.DisableDisplay();
        Destroy(healthDisplay.gameObject, 3f);
        Destroy(gameObject, 50);
        StartCoroutine(SpawnCoinsAfterDeath());
        SaveSystem.data.oozeBossKilled = true;
        FindObjectOfType<PlayerController>().UnlockDoubleJump();
    }

    IEnumerator SpawnCoinsAfterDeath()
    {
        for (int i = 0; i < coinsDrop; i++)
        {
            CoinsManager.instance.SpawnCoin(transform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
