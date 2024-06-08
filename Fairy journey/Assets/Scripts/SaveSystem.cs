using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static GameData data;

    public static void Save()
    {
        FindObjectOfType<ChestsSaver>().Save();
        string str = JsonUtility.ToJson(data);
        print(str);
        PlayerPrefs.SetString("data", str);
    }

    public static void Load()
    {
        if (PlayerPrefs.HasKey("data") == false) return;
        string str = PlayerPrefs.GetString("data");
        data = JsonUtility.FromJson<GameData>(str);
        FindObjectOfType<ChestsSaver>().Load();
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
