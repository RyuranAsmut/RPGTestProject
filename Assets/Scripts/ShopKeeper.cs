using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private bool canOpenShop;
    public Item[] itemForSale;
    public int[] amountForSale;

    private void Update() 
    {
        if(canOpenShop && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove && !Shop.instance.shopMenu.activeInHierarchy)
        {
            Shop.instance.GenerateShopSlots(itemForSale, amountForSale);
            Shop.instance.OpenShop();
        }    
    }



    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            canOpenShop = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            canOpenShop = false;
        }    
    }
}
