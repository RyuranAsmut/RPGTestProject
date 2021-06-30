using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharStats[] charStats;

    public Item[] items;
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
        AddItem(1, 5);
        AddItem(4, 2);
        AddItem(5, 3);
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
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetAmount(0);
        }
    }
    public void AddItem(int itemId, int amount)
    {
        items[itemId - 1].AddAmount(amount);
    }
    public void RemoveItem(int itemId, int amount)
    {
        items[itemId - 1].RemoveAmount(amount);
    }

    public void TestItemManagement()
    {
        AddItem(2, 3);
        Debug.Log("Added " + items[2].itemName);
        RemoveItem(4, 2);
        Debug.Log("Removed " + items[4].itemName);
        GameMenu.instance.UpdateItems();
    }

   
}
