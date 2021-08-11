using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    public string battlerName;
    public bool isPlayer;
    public bool isDead;
    public int currentHp, maxHp, currentMP, maxMP, power, defense, wpnPwr, armDef;
    public BattleMove[] moves;
    
}
