using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour, IDamagable
{
    [SerializeField] Sprite[] sprites;
    SpriteRenderer spr;

    public void GetDamage(int damage)
    {
        int r = Random.Range(0, sprites.Length);
        spr.sprite = sprites[r];
    }

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    
}
