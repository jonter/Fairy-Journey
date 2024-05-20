using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>() == null) return;

        OozeBossMain boss = FindObjectOfType<OozeBossMain>();
        boss.Activate();
        Destroy(gameObject);
    }

}
