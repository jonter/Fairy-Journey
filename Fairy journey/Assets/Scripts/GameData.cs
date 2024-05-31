using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public int hp = 5;
    public int maxHp = 5;

    public Vector3 playerPos = new Vector3(0,0,0);
    public Quaternion playerRot = Quaternion.identity;

    public bool oozeBossKilled = false;
    public int coins = 0;
    public bool doubleJump = false;

    
}
