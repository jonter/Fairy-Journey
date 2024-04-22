using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{
    TMP_Text coinsText;
    int coins = 0;

    public static CoinsManager instance;

    [SerializeField] GameObject coinPrefab;

    public void SpawnCoin(Vector3 pos)
    {
        GameObject copy = Instantiate(coinPrefab, pos, Quaternion.identity);
        Vector2 vel = new Vector2(Random.Range(-3f, 3f), Random.Range(6f, 9f));
        copy.GetComponent<Rigidbody2D>().velocity = vel;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        coinsText = GetComponentInChildren<TMP_Text>();
        coinsText.text = "" + coins;
    }

    public void AddCoins(int add)
    {
        coins += add;
        coinsText.text = "" + coins;
    }

    public bool SpendCoins(int price)
    {
        if (price > coins) return false;
        coins -= price;
        coinsText.text = "" + coins;
        return true;
    }

    
}
