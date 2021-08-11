using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Create Battle Move")]
public class BattleMove : ScriptableObject
{
    [Header("Move Type")]
    public bool isPhysical, isMagical, isHealing, isBuff, isDebuff;

    [Header("General Move Deteails")]
    public int MoveId;
    public string moveName, description;
    public Sprite moveIcon;

    [Header("Move Values")]
    public int modifier;
    public bool affectHp, affectMP, affectStr, affectDef;

}
