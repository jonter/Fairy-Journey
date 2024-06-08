using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestsSaver : MonoBehaviour
{

    public void Save()
    {
        Chest[] chests = FindObjectsOfType<Chest>();
        SaveSystem.data.openChests = new bool[chests.Length];
        for (int i = 0; i < chests.Length; i++)
        {
            SaveSystem.data.openChests[i] = chests[i].GetIsOpen();
        }
    }

    public void Load()
    {
        Chest[] chests = FindObjectsOfType<Chest>();
        if (SaveSystem.data.openChests == null) return;

        for (int i = 0; i < chests.Length;i++)
        {
            if (i >= SaveSystem.data.openChests.Length) return;
            chests[i].SetState(SaveSystem.data.openChests[i]);
        }
    }


    
}
