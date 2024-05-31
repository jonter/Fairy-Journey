using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CheckPoint : MonoBehaviour
{
    TMP_Text aboveText;
    bool inZone = false;
    // Start is called before the first frame update
    void Start()
    {
        aboveText = GetComponentInChildren<TMP_Text>();
        aboveText.color = new Color(1, 1, 1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player == null) return;
        Color c = new Color(1, 1, 1, 1);
        aboveText.DOColor(c, 0.5f);
        inZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player == null) return;
        Color c = new Color(1, 1, 1, 0);
        aboveText.DOColor(c, 0.5f);
        inZone = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (inZone == false) return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            SaveSystem.Save();
            Hint.instance.ShowHint("Сохрание удалось", 0.3f, 2f);
        }
        
    }
}
