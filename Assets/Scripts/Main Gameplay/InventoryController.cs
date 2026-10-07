using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour, IDataPersistance
{
    public List<string> itemList;
    public List<string> runeList;

    public void AddItem(string item)
    {
        itemList.Add(item);
    }

    public void AddRune(string rune)
    {
        runeList.Add(rune);
    }

    public void RemoveItem(string item)
    {
        itemList.Remove(item);
    }

    public void LoadData(GameData data)
    {
        itemList = data.itemsCollected;
        runeList = data.runesCollected;
    }

    public void SaveData(GameData data)
    {
        data.itemsCollected = itemList;
        data.runesCollected = runeList;
    }

}
