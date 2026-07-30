using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<string> itemList;

    public void AddItem(string item)
    {
        itemList.Add(item);
    }

    public void RemoveItem(string item)
    {
        itemList.Remove(item);
    }
}
