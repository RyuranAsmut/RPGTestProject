using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/*TODO
-Implement Multiple Save slots
*/

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharStats[] charStats;

    public Item[] item;
    public InventorySlots[] inventory;
    public int currentGold;

    public bool menuOpen, dialogActive, transtionActive, shopOpen, battleActive;


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
        /* AddItem(1, 5);
        AddItem(4, 2);
        AddItem(5, 3); */
    }

    private void Update()
    {
        CheckForPlayerMovement();

        /*  if (Input.GetKeyDown(KeyCode.J))
         {
             SaveData();
         }

         if (Input.GetKeyDown(KeyCode.L))
         {
             LoadData();
         } */
    }
    private void CheckForPlayerMovement()
    {
        if (menuOpen || dialogActive || transtionActive || shopOpen || battleActive)
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
        foreach (InventorySlots slot in inventory)
        {
            slot.SetItem(null);
            slot.SetAmount(0);
        }
    }

    public void AddItem(int itemId, int amount)
    {
        //Variable to check if the item is already on the inventory or not
        bool newItem = true;
        //Iterate for each inventory slot
        foreach (InventorySlots slot in inventory)
        {
            //check if the slot has an item
            if (slot.inventoryItem)
            {
                //check if the item id matches
                if (slot.inventoryItem.itemId == itemId)
                {
                    slot.ChangeAmount(amount);
                    newItem = false;
                }
            }
        }
        //If the itemid is not found, runs this
        if (newItem)
        {
            foreach (InventorySlots slot in inventory)
            {
                //Find the first empty slot and save the item
                if (slot.GetItem() == null)
                {
                    //The item IDs start at 1 and the Item array starts at 0
                    slot.SetItem(item[itemId - 1]);
                    slot.SetAmount(amount);
                    break; //Stop the forach loop after saving the item in an empty spot
                }
            }
        }
        else
        {
            Debug.Log("Cannot add item: " + item[itemId - 1].itemName);
        }
    }

    public void RemoveItem(int itemId, int amount)
    {
        foreach (InventorySlots slot in inventory)
        {
            if (slot.inventoryItem)
            {
                if (slot.inventoryItem.itemId == itemId)
                {
                    slot.ChangeAmount(amount);
                    if (slot.GetAmount() <= 0)
                    {
                        slot.SetAmount(0);
                        slot.SetItem(null);
                    }
                }
            }
        }
        SortEmptySlots();
    }

    public void SortEmptySlots()
    {
        // Variable to keep track of what's the next empty slot
        int emptySlot = 0;

        // Iterate through each inventory slot
        for (int i = 0; i < inventory.Length; i++)
        {
            // If we see an available item in the inventory, move it to an empty slot
            if (inventory[i].inventoryItem != null)
            {
                inventory[emptySlot].SetItem(inventory[i].inventoryItem);
                inventory[emptySlot].SetAmount(inventory[i].GetAmount());
                emptySlot++;
            }
        }
        // Populate the rest of inventory with empty slots
        // Start at the next empty slot, and just iterate through the remaining slots left
        for (int i = emptySlot; i < inventory.Length; i++)
        {
            // Reset the values
            inventory[i].SetItem(null);
            inventory[i].SetAmount(0);
        }
    }

    public int GetItemAmount(int itemId)
    {
        foreach (InventorySlots slot in inventory)
        {
            if(slot.GetItem())
            {
                if (slot.inventoryItem.itemId == itemId)
                {
                    return slot.GetAmount();
                }
            }
            
        }
        Debug.LogError("Item ID not found");
        return 0;
    }

    public void TestItemManagement()
    {
        AddItem(2, 3);
        Debug.Log("Added " + item[2].itemName);
        AddItem(4, 2);
        Debug.Log("Removed " + item[4].itemName);
        GameMenu.instance.UpdateItems();
    }

    public void SaveData()
    {
        //Save Scene and playr position
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);

        //Save Char Info
        for (int i = 0; i < charStats.Length; i++)
        {
            if (charStats[i].isActiveInParty)
            {
                PlayerPrefs.SetInt("Active_Status_" + charStats[i].charName, 1);
            }
            else
            {
                PlayerPrefs.SetInt("Active_Status_" + charStats[i].charName, 0);
            }

            PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_Level", charStats[i].currentLevel);
            PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_Exp", charStats[i].currentExp);
            PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_currentHP", charStats[i].currentHP);
            PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_currentMP", charStats[i].currentMP);
            PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_maxHP", charStats[i].maxHP);
            PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_maxMP", charStats[i].maxMP);
            PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_Power", charStats[i].power);
            PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_Def", charStats[i].defense);
            PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_WeaponPwr", charStats[i].weaponPower);
            PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_ArmorDef", charStats[i].armorDef);
            //Check if there are a equipped weapon and armor to get the itemID
            //If not set ID to 0
            if (charStats[i].equippedWeapon)
            {
                PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_Equip_Weapon_ID", charStats[i].equippedWeapon.itemId);
            }
            else
            {
                PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_Equip_Weapon_ID", 0);
            }
            if (charStats[i].equippedArmor)
            {
                PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_Equip_Armor_ID", charStats[i].equippedArmor.itemId);
            }
            else
            {
                PlayerPrefs.SetInt("Char_" + charStats[i].charName + "_Equip_Armor_ID", 0);
            }

        }

        //Save Inventory Data
        for (int i = 0; i < item.Length; i++)
        {
            PlayerPrefs.SetInt("Item_" + inventory[i].inventoryItem.itemName + "_Inventory_Amount", inventory[i].GetAmount());
        }
    }

    public void LoadData()
    {
        //Load Player Position
        PlayerController.instance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

        //Load Char Info
        for (int i = 0; i < charStats.Length; i++)
        {
            if (PlayerPrefs.GetInt("Active_Status_" + charStats[i].charName) == 0)
            {
                charStats[i].isActiveInParty = false;
            }
            else
            {
                charStats[i].isActiveInParty = true;
            }

            charStats[i].currentLevel = PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_Level");
            charStats[i].currentExp = PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_Exp");
            charStats[i].currentHP = PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_currentHP");
            charStats[i].currentMP = PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_currentMP");
            charStats[i].maxHP = PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_maxHP");
            charStats[i].maxMP = PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_maxMP");
            charStats[i].power = PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_Power");
            charStats[i].defense = PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_Def");
            charStats[i].weaponPower = PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_WeaponPwr");
            charStats[i].armorDef = PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_ArmorDef");
            //The items IDs start at 1 while the item array starts at 0
            //If the ID is 0 then there's no item equiped
            if (PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_Equip_Weapon_ID") > 0)
            {
                charStats[i].equippedWeapon = item[PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_Equip_Weapon_ID") - 1];
            }
            else
            {
                charStats[i].equippedWeapon = null;
            }
            if (PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_Equip_Armor_ID") > 0)
            {
                charStats[i].equippedArmor = item[PlayerPrefs.GetInt("Char_" + charStats[i].charName + "_Equip_Armor_ID") - 1];
            }
            else
            {
                charStats[i].equippedArmor = null;
            }


        }

        //Load Inventory Data
        for (int i = 0; i < item.Length; i++)
        {
            inventory[i].SetAmount(PlayerPrefs.GetInt("Item_" + inventory[i].inventoryItem.itemName + "_Inventory_Amount"));
        }

    }


}
