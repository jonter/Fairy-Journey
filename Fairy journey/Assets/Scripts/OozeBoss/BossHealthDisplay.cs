using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BossHealthDisplay : MonoBehaviour
{
    Slider healthBar;
    RectTransform rect;

    // Start is called before the first frame update
    void Awake()
    {
        healthBar = GetComponent<Slider>();
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, -100);
        healthBar.value = 1;
        rect.DOAnchorPosY(150, 1).SetEase(Ease.OutBack);
    }

    public void SetHealth(float percentage)
    {
        healthBar.DOKill();
        healthBar.DOValue(percentage, 0.1f);
    }

    public void DisableDisplay()
    {
        rect.DOAnchorPosY(-150, 1).SetEase(Ease.OutBack);
    }

    
}
