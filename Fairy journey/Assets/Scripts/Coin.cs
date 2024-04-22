using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] LayerMask colLayer;
    [SerializeField] int addCoins = 1;

    bool canPickup = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        canPickup = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickup == false) return;
        Collider2D col = 
            Physics2D.OverlapCircle(transform.position, 0.3f, colLayer);
        if(col != null)
        {
            CoinsManager.instance.AddCoins(addCoins);
            Destroy(gameObject);
        }

    }
}
