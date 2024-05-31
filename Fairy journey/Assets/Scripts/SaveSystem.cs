using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static GameData data;

    public static void Save()
    {
        // записать в data все необходимые переменные
        string str = JsonUtility.ToJson(data);
        print(str);
        PlayerPrefs.SetString("data", str);
    }

    public static void Load()
    {
        if (PlayerPrefs.HasKey("data") == false) return;
        string str = PlayerPrefs.GetString("data");
        data = JsonUtility.FromJson<GameData>(str);
    }

    // Start is called before the first frame update
    void Awake()
    {
        Load();
        if(data == null)
        {
            data = new GameData();
            data.playerPos = FindObjectOfType<PlayerController>()
                .transform.position;
        }
    }

    
}
