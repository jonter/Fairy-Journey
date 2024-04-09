using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    Animator anim;
    BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    public void Activate()
    {
        anim.SetTrigger("fire");
        StartCoroutine(ActivateCollider()); 
    }

    IEnumerator ActivateCollider()
    {
        col.enabled = true;
        yield return new WaitForSeconds(5f / 60f);
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable obj = collision.GetComponent<IDamagable>();
        if (obj != null)
        {
            obj.GetDamage(2);
        }

    }

}
