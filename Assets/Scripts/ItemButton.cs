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
        GameMenu.instance.SelectItem(buttonItem);
    }

    

}
