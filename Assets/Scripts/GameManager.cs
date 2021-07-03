using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharStats[] charStats;

    public Item[] items;
    public InventorySlots[] inventory;
    public int currentGold;

    public bool menuOpen, dialogActive, transtionActive, shopOpen;


    private void Start() 
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        GenerateEmptyInventory();

        //Starting items for test purposes;
        ChangeItemAmount(1, 5);
        ChangeItemAmount(4, 2);
        ChangeItemAmount(5, 3);
    }

    private void Update()
    {
        CheckForPlayerMovement();
    }
    private void CheckForPlayerMovement() 
    {
        if (menuOpen || dialogActive || transtionActive || shopOpen)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }

    private void GenerateEmptyInventory()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].SetAmount(0);
        }
    }
    public void ChangeItemAmount(int itemId, int amount)
    {
        inventory[itemId - 1].ChangeAmount(amount);
    }

    public int GetItemAmount(int itemId)
    {
        return inventory[itemId - 1].GetAmount();
    }

    public void TestItemManagement()
    {
        ChangeItemAmount(2, 3);
        Debug.Log("Added " + items[2].itemName);
        ChangeItemAmount(4, -2);
        Debug.Log("Removed " + items[4].itemName);
        GameMenu.instance.UpdateItems();
    }

   
}
