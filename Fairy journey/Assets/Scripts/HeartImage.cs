using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartImage : MonoBehaviour
{
    public void ShowHeart()
    {
        GetComponent<Image>().color = Color.white;
    }

    public void HideHeart()
    {
        GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
    }

}
