using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour
{
    public Item buttonItem;
    public Image buttonImage;
    public TextMeshProUGUI buttonText;
    public int buttonValue;

    public void Press()
    {
        if(GameMenu.instance.menu.activeInHierarchy)
        {
            GameMenu.instance.SelectItem(buttonItem);
        }
        if (Shop.instance.shopMenu.activeInHierarchy)
        {
            if (Shop.instance.buyMenu.activeInHierarchy)
            {
                Shop.instance.SelectItemToBuy(buttonItem);
            }
            if (Shop.instance.sellMenu.activeInHierarchy)
            {
                Shop.instance.SelectItemToSell(buttonItem);
            }
        }
        
    }

    

}
