using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Hint : MonoBehaviour
{
    public static Hint instance;

    RectTransform rect;
    TMP_Text mytext;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        rect = GetComponent<RectTransform>();
        mytext = GetComponent<TMP_Text>();
    }

    public void ShowHint(string str, float animTime, float duration)
    {
        rect.DOKill();
        mytext.DOKill();

        rect.anchoredPosition = new Vector2(0, -200);
        mytext.color = new Color(1, 1, 1, 0);
        mytext.text = str;

        rect.DOAnchorPosY(200, animTime).SetEase(Ease.OutBack);
        mytext.DOColor(Color.white, animTime).SetEase(Ease.OutSine);

        rect.DOAnchorPosY(-200, animTime).SetEase(Ease.InBack).SetDelay(duration);
        mytext.DOColor(new Color(1,1,1,0), animTime)
            .SetEase(Ease.InOutSine).SetDelay(duration);
    }

    
}
