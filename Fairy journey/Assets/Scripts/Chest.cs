using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IDamagable
{
    int hitCount = 0;
    bool isOpen = false;

    [SerializeField] int coinDrops = 15;

    public bool GetIsOpen() { return isOpen; }

    public void SetState(bool open)
    {
        if(open == true)
        {
            isOpen = true;
            GetComponent<Animator>().SetTrigger("open");
        }
    }

    public void GetDamage(int damage)
    {
        if (isOpen == true) return;
        hitCount++;

        if(hitCount >= 3)
        {
            StartCoroutine(OpenCoroutine());
        }
    }

    IEnumerator OpenCoroutine()
    {
        isOpen = true;
        GetComponent<Animator>().SetTrigger("open");
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < coinDrops; i++)
        {
            CoinsManager.instance.SpawnCoin(transform.position);
            yield return new WaitForSeconds(0.05f);
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}
