using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    bool isActive = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>() == null) return;
        if (isActive == true) return;
        isActive = true;

        OozeBossMain boss = FindObjectOfType<OozeBossMain>();
        boss.Activate();
        Destroy(gameObject);
    }

}
