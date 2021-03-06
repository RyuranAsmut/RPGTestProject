using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    public bool namedNpc = true;
    public string[] lines;
    private bool canActivate;


    private void Update() 
    {
        if (canActivate && Input.GetButtonDown("Jump") && !DialogueController.intance.dialogBox.activeInHierarchy)
        {
            DialogueController.intance.ShowDialog(lines, namedNpc);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            canActivate = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }    
    }
}
