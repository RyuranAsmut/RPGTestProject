using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleMove
{
    [Header("Move Type")]
    public bool isPhysical, isMagical, isHealing, isBuff, isDebuff;

    [Header("General Move Deteails")]
    public int moveCost;
    public string moveName;
    [TextArea(5, 7)]public string description;
    public Sprite moveIcon;
    public AttackEffect visualEffect;

    [Header("Move Values")]
    public int modifier;
    public bool affectHp, affectMP, affectStr, affectDef;

}
