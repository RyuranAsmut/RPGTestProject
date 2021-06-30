using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlots : MonoBehaviour
{
    public Item inventoryItem;
    [SerializeField] private int inventoryAmount;

    public void SetItem(Item item)
    {
        inventoryItem = item;
    }



    public Item GetItem()
    {
        return inventoryItem;
    }

    public int GetItemAmount()
    {
        return inventoryAmount;
    }
}
