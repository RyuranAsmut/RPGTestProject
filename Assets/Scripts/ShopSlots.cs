using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopSlots : MonoBehaviour
{
    public int slotId;
    public Item shopItem;
    [SerializeField] private int shopAmount;

    public void SetItem(Item item)
    {
        shopItem = item;
    }

    public void SetId(int id)
    {
        slotId = id;
    }

    public void SetAmount(int amount)
    {
        shopAmount = amount;
    }

    public Item GetItem()
    {
        return shopItem;
    }

    public int GetAmount()
    {
        return shopAmount;
    }

    public void ChangeAmount(int amountToChange)
    {
        shopAmount += amountToChange; 
    }
    
}
