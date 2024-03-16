using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] GameObject heartImage;

    HeartImage[] hearts;
    
    public void SetupHealth(int maxHP)
    {
        maxHP--;
        for(int i = 0; i < maxHP; i++)
        {
            GameObject clone = Instantiate(heartImage, transform);
        }
        hearts = GetComponentsInChildren<HeartImage>();
    }

    public void Display(int hp)
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < hp) hearts[i].ShowHeart();
            else hearts[i].HideHeart(); 
        }
    }

    
}
