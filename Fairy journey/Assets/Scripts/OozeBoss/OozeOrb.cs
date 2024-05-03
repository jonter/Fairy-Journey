using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OozeOrb : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player)
        {
            Vector3 vel = new Vector3(-5, 7);
            if (player.transform.position.x > transform.position.x)
                vel = new Vector3(5, 7);

            player.GetDamage(vel);
            Destroy(gameObject);
        }
        
    }


}
