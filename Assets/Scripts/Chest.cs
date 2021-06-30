using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item item;
    public int amount;
    //Serialized for testing
    [SerializeField] private bool canOpen; 
    [SerializeField] private bool isEmpty = false;

    private void Update()
    {
        OpenChest();
    }

    private void OpenChest()
    {
        if (canOpen && PlayerController.instance.canMove && Input.GetButtonDown("Fire1"))
        {
            GameManager.instance.ChangeItemAmount(item.itemId, amount);
            isEmpty = true;
            canOpen = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !isEmpty)
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            canOpen = false;
        }   
    }
}
