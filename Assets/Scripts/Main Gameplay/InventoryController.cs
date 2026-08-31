using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
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

}
