using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

    /*TODO
    -Implement a Equip menu or way for the player to unequip the current equipment without having to equip something else
    -Try to make the Inventory better or at least more similar to the Shop Slots
    -Manage the inventory limit: either make it fixed or add a scrollbar to navigate it
    
    */
public class GameMenu : MonoBehaviour
{
    //UI References
    public GameObject menu;
    public GameObject[] windows;
    private CharStats[] playerStats;

    public TextMeshProUGUI[] nameText, hpText, mpText, levelText, expText;
    public Slider[] hpSlider, mpSlider, expSlider;
    public Image[] charImage;
    public GameObject[] charStatsHolder;
    public GameObject[] statusButtons;

    public TextMeshProUGUI charName, charLvl, charHp, charMp, charAtk, charDef, charWp, charArm, charWpPwr, charArmPwr, charExp;
    public Image charStatusImage;
    public GameObject invItemButton;
    public GameObject invItensHolder;

    public Item activeItem;
    public TextMeshProUGUI itemName, ItemDescription, useButtonTxt;
    public GameObject useButton, discardButton;
    public GameObject confirmationPanel;
    public GameObject itemCharChoicePanel;
    public TextMeshProUGUI itemCharChoiceText;
    public TextMeshProUGUI[] itemCharChoiceNames;

    public TextMeshProUGUI goldAmount;

    public static GameMenu instance;

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
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //Check to see if it's not in the middle of a area transaction
            if (!GameManager.instance.transtionActive)
            {
                //If the menu is already open(in the hierarchy)
                if (menu.activeInHierarchy)
                {
                    CloseMenu();
                }
                //If the menu isn't open.
                else
                {
                    menu.SetActive(true);
                    UpdateMainStats();
                    GameManager.instance.menuOpen = true;
                }
            }
        }
    }

    private void CheckForActiveItem()
    {
        if (!activeItem)
        {
            useButton.SetActive(false);
            discardButton.SetActive(false);

        }
        else
        {
            useButton.SetActive(true);
            discardButton.SetActive(true);
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.charStats;
        goldAmount.text = GameManager.instance.currentGold.ToString() + "G";

        for(int i = 0; i < playerStats.Length; i++)
        {
            //If the character is active in the hierarchy
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                //Show and update each stats in the array
                charStatsHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                levelText[i].text = "Lvl: " + playerStats[i].currentLevel;
                expText[i].text = "" + playerStats[i].currentExp + "/" + playerStats[i].expToLevelUp[playerStats[i].currentLevel];
                hpSlider[i].maxValue = playerStats[i].maxHP;
                hpSlider[i].value = playerStats[i].currentHP;
                mpSlider[i].maxValue = playerStats[i].maxMP;
                mpSlider[i].value = playerStats[i].currentMP;
                expSlider[i].maxValue = playerStats[i].expToLevelUp[playerStats[i].currentLevel];
                expSlider[i].value = playerStats[i].currentExp;
                charImage[i].sprite = playerStats[i].charImage;
            }
            else
            {
                //Don't show the character stats
                charStatsHolder[i].SetActive(false);
            }
        }
    }


    public void ToggleWindows(int windowNumber)
    {
        UpdateMainStats();
        //Check for every window when the button is clicked
        for(int i = 0; i < windows.Length; i++)
        {
            //if the number of the window is the same as the button
            if (i == windowNumber)
            {
                //Change the status window to the opposite of its current status
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                //If the number of the window is not the same as the button
                windows[i].SetActive(false);
            }
        }
        itemCharChoicePanel.SetActive(false);
        confirmationPanel.SetActive(false);
        ClearActiveItem();
        CheckForActiveItem();
    }

    public void CloseMenu()
    {
        foreach(GameObject window in windows)
        {
            window.SetActive(false);
        }

        menu.SetActive(false);
        itemCharChoicePanel.SetActive(false);
        confirmationPanel.SetActive(false);
        ClearActiveItem();

        GameManager.instance.menuOpen = false;
    }

    public void OpenStatus()
    {
        UpdateMainStats();
        CharStats(0);
        for (int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].charName;
        }
    }

    public void CharStats(int selected)
    {
        charName.text = playerStats[selected].charName;
        charLvl.text = playerStats[selected].currentLevel.ToString();
        charHp.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        charMp.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        charAtk.text = playerStats[selected].power.ToString();
        charDef.text = playerStats[selected].power.ToString();
        if(playerStats[selected].equippedWeapon)
        {
            charWp.text = playerStats[selected].equippedWeapon.itemName;
            charWpPwr.text = playerStats[selected].weaponPower.ToString(); 
        }
        else
        {
            charWp.text = "None";
            charWpPwr.text = "0";
        }
        if(playerStats[selected].equippedArmor)
        {
            charArm.text = playerStats[selected].equippedArmor.itemName;
            charArmPwr.text = playerStats[selected].armorDef.ToString(); 
        }
        else
        {
            charArm.text = "None";
            charArmPwr.text = "0";
        }
        //To show the exact value to the next level
        charExp.text = (playerStats[selected].expToLevelUp[playerStats[selected].currentLevel] - playerStats[selected].currentExp).ToString();
        charStatusImage.sprite = playerStats[selected].charImage;

    }

    public void UpdateItems()
    {
        DestroyOldItemsButtons();
        foreach (InventorySlots slots in GameManager.instance.inventory)
        {
            if (slots.GetAmount() > 0 && slots.inventoryItem != null)
            {
              GameObject newItemButton = Instantiate(invItemButton) as GameObject;
              ItemButton buttonInv = newItemButton.GetComponent<ItemButton>();
              buttonInv.buttonItem = slots.inventoryItem;
              buttonInv.buttonImage.sprite = slots.inventoryItem.itemSprite;
              buttonInv.buttonText.text = slots.GetAmount().ToString();
              newItemButton.transform.SetParent(invItensHolder.transform, false);
            }
        }
    }

    private void DestroyOldItemsButtons()
    {
        //To stop creating multiple copies of the same buttons
        foreach (Transform buttons in invItensHolder.transform)
        {
            Destroy(buttons.gameObject);
        }
    }

    public void SelectItem(Item selectedItem)
    {
        //Change the text in the info panel to match the selected item
        activeItem = selectedItem;
        itemName.text = selectedItem.itemName;
        ItemDescription.text = selectedItem.description;

        //Check and activate/deactivate the Use/Equip button for the correct text
        if (selectedItem.isUsable)
        {
            useButton.SetActive(true);
            useButtonTxt.text = "Use";

        }
        else if (selectedItem.isArmor || selectedItem.isWeapon)
        {
            useButton.SetActive(true);
            useButtonTxt.text = "Equip";
        }
        else
        {
            useButton.SetActive(false);
        }
        CheckForActiveItem();
    }

    private void ClearActiveItem()
    {
        activeItem = null;
        itemName.text = "";
        ItemDescription.text = "";

    }

    public void DiscardItem()
    {
        if(activeItem)
        {
            confirmationPanel.SetActive(true);
        }

    }

    public void DiscardConfirmAction()
    {
        GameManager.instance.inventory[activeItem.itemId - 1].SetAmount(0);
        UpdateItems();
        confirmationPanel.SetActive(false);
        activeItem = null;

    }

    public void CancelAction()
    {
        confirmationPanel.SetActive(false);
    }

    public void OpenCharChoice()
    {
        if(activeItem.isArmor || activeItem.isWeapon)
        {
            itemCharChoiceText.text = "Choose character to equip it:";
        }
        else
        {
            itemCharChoiceText.text = "Choose character to use it:";
        }
        itemCharChoicePanel.SetActive(true);
        //Assign the correct character names to the buttons if they are active
        for (int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.charStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.charStats[i].gameObject.activeInHierarchy);

        }
    }

    public void CloseCharChoice()
    {
        itemCharChoicePanel.SetActive(false);
    }

    public void UseItem(int selectedChar)
    {
        if(activeItem)
        {
            activeItem.UseItem(selectedChar);
            CloseCharChoice();
            UpdateItems();
        }

    }

}
