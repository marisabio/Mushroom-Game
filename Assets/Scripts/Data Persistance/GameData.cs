using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentScene;
    public Vector3 playerPosition;
    public List<string> itemsCollected;
    public List<string> runesCollected;

    public GameData()
    {
        currentScene = 0;
        playerPosition = Vector3.zero;
        itemsCollected = new List<string>();
        runesCollected = new List<string>();
    }
}
