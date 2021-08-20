using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


    /*TODO
    -Implement a buy/sell multiples/all items
    -Implent sold items by the player to be bought
    -Implement a confirmation
    -Implement a 'essential' tag to hide quest items from the shop page or just hide the sell button
    -Make the color of the gold price to change to red if it's higher than the current amount of gold the player has 
    */ 


public class Shop : MonoBehaviour
{
    public static Shop instance;

    public GameObject shopMenu, buyMenu, sellMenu;
    public TextMeshProUGUI goldText;
    public ShopSlots shopSlotObj;
    public GameObject shopSlotsParent;

    public GameObject ItemsBt;
    public GameObject buyItemsBtHolder;
    public GameObject sellItemsBtHolder;

    public Item activeItem;
    public ShopSlots activeSlot;
    public TextMeshProUGUI buyItemName, buyItemInfo, buyItemValue;
    public TextMeshProUGUI sellName, sellInfo, sellValue;

    public ShopSlots[] shopSlots = new ShopSlots[50];

    private void Start() 
    {
        instance = this;   
    }

    private void Update() 
    {
        //Gold debugging
        /* if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.instance.currentGold =+ 1000;
        }  */
          
    }

    public void OpenShop()
    {
        ClearActiveItem();
        shopMenu.SetActive(true);
        GameManager.instance.shopOpen = true;
        RefreshGoldDisplay();
        OpenBuyMenu();
    }

    private void RefreshGoldDisplay()
    {
        goldText.text = GameManager.instance.currentGold.ToString() + "G";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopOpen = false;
    }

    public void OpenBuyMenu()
    {
        ClearActiveItem();
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
        UpdateBuySlots();

    }

    private void UpdateBuySlots()
    {
        DestroyOldShopButtons(buyItemsBtHolder.transform);
        foreach (ShopSlots slots in shopSlots)
        {
            if (slots != null && slots.GetAmount() > 0)
            {
                GameObject newItemButton = Instantiate(ItemsBt) as GameObject;
                ItemButton buttonsBuy = newItemButton.GetComponent<ItemButton>();
                buttonsBuy.slot = slots;
                buttonsBuy.buttonItem = slots.shopItem;
                buttonsBuy.buttonImage.sprite = slots.shopItem.itemSprite;
                buttonsBuy.buttonText.text = slots.GetAmount().ToString();
                newItemButton.transform.SetParent(buyItemsBtHolder.transform, false);
            }
        }
    }

    private void DestroyOldShopButtons(Transform buttonHolderTransform)
    {
        //To stop creating multiple copies of the same buttons
        foreach (Transform buttons in buttonHolderTransform)
        {
            Destroy(buttons.gameObject);
        }
    }

    public void OpenSellMenu()
    {
        ClearActiveItem();
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);
        UpdateSellSlots();
    }

    private void UpdateSellSlots()
    {
        DestroyOldShopButtons(sellItemsBtHolder.transform);
        foreach (InventorySlots slots in GameManager.instance.inventory)
        {
            if (slots.GetAmount() > 0 && slots.inventoryItem != null)
            {
                GameObject newItemButton = Instantiate(ItemsBt) as GameObject;
                ItemButton buttonSell = newItemButton.GetComponent<ItemButton>();
                buttonSell.buttonItem = slots.inventoryItem;
                buttonSell.buttonImage.sprite = slots.inventoryItem.itemSprite;
                buttonSell.buttonText.text = slots.GetAmount().ToString();
                newItemButton.transform.SetParent(sellItemsBtHolder.transform, false);
            }
        }
    }

    public void GenerateShopSlots(Item[] items, int[] amount)
    {
        for(int i = 0; i < items.Length; i++)
        {
            //Gnerates new shop slots using a shopkeeper
            ShopSlots newShopSlot = Instantiate(shopSlotObj) as ShopSlots;
            shopSlots[i] = newShopSlot.GetComponent<ShopSlots>();
            newShopSlot.transform.SetParent(shopSlotsParent.transform, false);
            shopSlots[i].SetId(i + 1);
            shopSlots[i].SetItem(items[i]);
            shopSlots[i].SetAmount(amount[i]);
        }
    }

    public void SelectItemToBuy(ShopSlots slot)
    {
        //Change the text in the info panel to match the selected shop slot
        activeItem = slot.shopItem;
        activeSlot = slot;
        buyItemName.text = slot.shopItem.itemName;
        buyItemInfo.text = slot.shopItem.description;
        buyItemValue.text = slot.shopItem.monetaryValue.ToString() + "G";

    }
     public void SelectItemToSell(Item selectedItem)
    {
        //Change the text in the info panel to match the selected item
        activeItem = selectedItem;
        sellName.text = selectedItem.itemName;
        sellInfo.text = selectedItem.description;
        sellValue.text = Mathf.FloorToInt(selectedItem.monetaryValue/2).ToString() + "G";

    }

    private void ClearActiveItem()
    {
        activeItem = null;
        activeSlot = null;
        buyItemName.text = "";
        buyItemInfo.text = "";
        buyItemValue.text = "-G";
        sellName.text = "";
        sellInfo.text = "";
        sellValue.text = "-G";

    }

    public void Buy()
    {
        if (activeItem && GameManager.instance.currentGold >= activeItem.monetaryValue)
        {
            GameManager.instance.currentGold -= activeItem.monetaryValue;
            RefreshGoldDisplay();
            activeSlot.SetAmount(activeSlot.GetAmount() - 1);
            GameManager.instance.AddItem(activeItem.itemId, 1);
            if (activeSlot.GetAmount() <= 0)
            {
                ClearActiveItem();
                Destroy(activeSlot);
            }
            UpdateBuySlots();
        }
    }

    public void Sell()
    {
        if(activeItem)
        {
            //Making sure to sell the item for half the price and the value is a int
            GameManager.instance.currentGold += Mathf.FloorToInt(activeItem.monetaryValue/2);
            RefreshGoldDisplay();
            GameManager.instance.RemoveItem(activeItem.itemId, -1);
            if (GameManager.instance.GetItemAmount(activeItem.itemId) <= 0)
            {
                ClearActiveItem();
            }
            UpdateSellSlots(); 
        }
    }


}
