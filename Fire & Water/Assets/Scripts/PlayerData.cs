using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coinsNumber;
    
    public PlayerData(GameMaster _gameMaster)
    {
        coinsNumber = _gameMaster.coinsNumber;
    }
}
