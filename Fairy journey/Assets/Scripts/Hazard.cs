using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.transform.GetComponent<PlayerHealth>(); 
        if(player)
        {
            Vector3 vel = new Vector3(0, 10);
            player.GetDamage(vel);
        }
        
    }
}
