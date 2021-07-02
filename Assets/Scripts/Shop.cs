using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
    public TextMeshProUGUI buyItemName, buyItemInfo, buyItemValue;
    public TextMeshProUGUI sellName, sellInfo, sellValue;

    public ShopSlots[] shopSlots = new ShopSlots[50];

    private void Start() 
    {
        instance = this;   
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.K) && !shopMenu.activeInHierarchy)
        {
            OpenShop();
        }    
    }

    public void OpenShop()
    {
        ClearActiveItem();
        shopMenu.SetActive(true);
        GameManager.instance.shopOpen = true;
        goldText.text = GameManager.instance.currentGold.ToString() + "G";
        OpenBuyMenu();
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
        DestroyOldShopButtons(buyItemsBtHolder.transform);
        foreach (ShopSlots slots in shopSlots)
        {
            if (slots != null)
            {
              GameObject newItemButton = Instantiate(ItemsBt) as GameObject;
              ItemButton buttonsBuy = newItemButton.GetComponent<ItemButton>();
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
            //Dynamically generates new shop slots and set this class as the parent
            ShopSlots newShopSlot = Instantiate(shopSlotObj) as ShopSlots;
            shopSlots[i] = newShopSlot.GetComponent<ShopSlots>();
            newShopSlot.transform.SetParent(shopSlotsParent.transform, false);
            shopSlots[i].SetId(i + 1);
            shopSlots[i].SetItem(items[i]);
            shopSlots[i].SetAmount(amount[i]);
        }
    }

    public void SelectItemToBuy(Item selectedItem)
    {
        //Change the text in the info panel to match the selected item
        activeItem = selectedItem;
        buyItemName.text = selectedItem.itemName;
        buyItemInfo.text = selectedItem.description;
        buyItemValue.text = selectedItem.monetaryValue.ToString() + "G";

    }
     public void SelectItemToSell(Item selectedItem)
    {
        //Change the text in the info panel to match the selected item
        activeItem = selectedItem;
        sellName.text = selectedItem.itemName;
        sellInfo.text = selectedItem.description;
        sellValue.text = selectedItem.monetaryValue.ToString() + "G";

    }

    private void ClearActiveItem()
    {
        activeItem = null;
        buyItemName.text = "";
        buyItemInfo.text = "";
        buyItemValue.text = "-G";
        sellName.text = "";
        sellInfo.text = "";
        sellValue.text = "-G";

    }

    public void Buy()
    {
        if(activeItem)
        {
            if (GameManager.instance.currentGold >= activeItem.monetaryValue)
            {
                GameManager.instance.currentGold -= activeItem.monetaryValue;
                //shopSlots[activeItem.itemId - 1];
            }
        }
    }


}
