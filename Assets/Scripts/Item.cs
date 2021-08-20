using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Create Item")]
public class Item : ScriptableObject
{
    [Header("Item Type")]
    public bool isUsable;
    public bool isWeapon;
    public bool isArmor;

    [Header("General Item Deteails")]
    public int itemId, monetaryValue;
    public string itemName, description;
    public Sprite itemSprite;

    [Header("Item Values")]
    public int amountToChange;
    public bool affectHp, affectMP, affectStr;

    [Header("Weapon/Armor Detais")]
    public int weaponStr;
    public int armorDef;

    public void UseItem(int charToUse)
    {
        var selectedChar = GameManager.instance.charStats[charToUse];

        if (isUsable)
        {
            if(affectHp)
            {
                //Mathf.Clamp is to make sure the currentHP/MP isn't bigger than the maxHP
                selectedChar.currentHP += Mathf.Clamp(selectedChar.currentHP += amountToChange, 0, selectedChar.maxHP);
            }
            if (affectMP)
            {
                selectedChar.currentMP += Mathf.Clamp(selectedChar.currentMP += amountToChange, 0, selectedChar.maxMP);
            }
            if (affectStr)
            {
                //To implement at some point
            }
        } 
        else if (isArmor)
        {
            if(selectedChar.equippedArmor)
            {
                GameManager.instance.AddItem(selectedChar.equippedArmor.itemId, 1);
            }
                selectedChar.equippedArmor = this;
                selectedChar.armorDef = armorDef;
                
        }
        else if (isWeapon)
        {
            if(selectedChar.equippedWeapon)
            {
                GameManager.instance.AddItem(selectedChar.equippedWeapon.itemId, 1);
            }
                selectedChar.equippedWeapon = this;
                selectedChar.weaponPower = weaponStr;
        }

        GameManager.instance.RemoveItem(itemId, -1);
    }

}
