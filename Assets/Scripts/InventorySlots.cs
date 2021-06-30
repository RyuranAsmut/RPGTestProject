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

    public void SetAmount(int amount)
    {
        inventoryAmount = amount;
    }

    public Item GetItem()
    {
        return inventoryItem;
    }

    public int GetAmount()
    {
        return inventoryAmount;
    }

    public void ChangeAmount(int amountToChange)
    {
        inventoryAmount += amountToChange; 
    }
}
