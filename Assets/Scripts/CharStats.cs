using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int currentLevel = 1;
    public int currentExp;
    public int[] expToLevelUp;
    public int maxLevel = 100;
    public int baseExp = 1000;

    public int currentHP;
    public int maxHP = 1000;
    public int currentMP;
    public int maxMP = 100;
    public int mpBonus;
    public int power;
    public int defense;
    public int weaponPower;
    public int armorDef;
    public Item equippedWeapon;
    public Item equippedArmor;

    public Sprite charImage;


    private void Start()
    {
        CalculateExpCurve();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            AddExp(1000);
        }   
    }

    private void CalculateExpCurve()
    {
        expToLevelUp = new int[maxLevel];
        expToLevelUp[1] = baseExp;

        //i = expToLevelUp elements, starting from 2 since 1 is the base exp.
        for (int i = 2; i < expToLevelUp.Length; i++)
        {
            expToLevelUp[i] = Mathf.FloorToInt(expToLevelUp[i - 1] * 1.05f);
        }
    }

    public void AddExp(int expToAdd)
    {
 
        currentExp += expToAdd;
        while (currentLevel < maxLevel && currentExp >= expToLevelUp[currentLevel])
        {
            currentExp -= expToLevelUp[currentLevel];
            StatsUp();
            currentLevel++;
        }
        if(currentLevel >= maxLevel)
        {
            currentExp = 0;
        }
    }

    private void StatsUp()
    {
        if (currentLevel % 2 == 0)
        {
            power++;
        }
        else
        {
            defense++;
        }
        maxHP = Mathf.FloorToInt(maxHP * 1.02f);
        currentHP = maxHP;
        maxMP += mpBonus;
        currentMP = maxMP;
    }
}
