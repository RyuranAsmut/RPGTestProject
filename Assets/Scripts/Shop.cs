using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public GameObject shopMenu, buyMenu, sellMenu;
    public TextMeshProUGUI goldText;

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
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
    }
     public void OpenSellMenu()
    {
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);
    }




}
