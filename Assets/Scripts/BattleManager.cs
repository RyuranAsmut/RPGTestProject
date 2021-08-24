using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*TODO
-Implement a Visual Turn Tracker
-Implement a Position Change Action
-Make the turns based on speed/Agility and character actions
-Improve the damage calulation formula to work in weakness and resistences
-Improve the damage display and effects
*/


public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private bool battleActive;

    public GameObject battleScene;
    public GameObject attackPanel;
    public TextMeshProUGUI attackTxt;
    public GameObject actionButtons;
    public GameObject enemyAtkVfx;

    public Transform[] playerPosition;
    public Transform[] enemyPosition;

    public Battler[] playerParty; //playable characer prefabs
    public Battler[] enemyParty; //enemy prefabs
    public List<Battler> activeBattler = new List<Battler>(); //List of all characters(player and enemy) active in battle

    public int currentTurn; //To use on the activeBattler list: "activeBattler[currentTurn]"
    public bool turnWaiting;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //Battle debugging
        if (Input.GetKeyDown(KeyCode.B))
        {
            BattleStart(new string[] { "Spider", "Slime" });
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
            Debug.Log(currentTurn);
        }

        //Check if there's an active battle
        if (battleActive)
        {
            if (turnWaiting)
            {
                //Player Turn
                if (activeBattler[currentTurn].isPlayer)
                {
                    actionButtons.SetActive(true);
                }
                //Enemy Turn
                else
                {
                    actionButtons.SetActive(false);
                    StartCoroutine(EnemyMoveCo());

                }
            }
        }
    }

    public void BattleStart(string[] enemiesTospawn)
    {
        turnWaiting = true;
        //This determins the first character to act in battle
        currentTurn = 0;
        if (!battleActive)
        {
            battleActive = true;
            GameManager.instance.battleActive = true;
            //To adjust the position of the Battle Background
            battleScene.transform.position = new Vector3(
                Camera.main.transform.position.x, (Camera.main.transform.position.y + 1), transform.position.z);

            battleScene.SetActive(true);
            //Play the correspondent BGM from the AudioManager
            AudioManager.instance.PlayBGM(6);

            //Check for each active character in the player party and instantiate it on the battle scene
            for (int i = 0; i < playerParty.Length; i++)
            {
                if (GameManager.instance.charStats[i].isActiveInParty)
                {
                    if (playerParty[i].name == GameManager.instance.charStats[i].name)
                    {
                        Battler newChar = Instantiate(playerParty[i], playerPosition[i].position, playerPosition[i].rotation);
                        newChar.transform.parent = playerPosition[i];
                        activeBattler.Add(newChar);

                        CharStats currentChar = GameManager.instance.charStats[i];

                        activeBattler[i].battlerName = currentChar.charName;
                        activeBattler[i].battlerIcon = currentChar.charImage;
                        activeBattler[i].currentHp = currentChar.currentHP;
                        activeBattler[i].maxHp = currentChar.maxHP;
                        activeBattler[i].currentMP = currentChar.currentMP;
                        activeBattler[i].power = currentChar.power;
                        activeBattler[i].defense = currentChar.defense;
                        activeBattler[i].wpnPwr = currentChar.weaponPower;
                        activeBattler[i].armDef = currentChar.armorDef;
                    }
                }
            }

            //Spawn the enemies given in the function parameters
            for (int i = 0; i < enemiesTospawn.Length; i++)
            {
                Debug.Log(enemiesTospawn[i]);
                if (enemiesTospawn[i] != "")
                {
                    foreach (Battler enemy in enemyParty)
                    {
                        if (enemy.battlerName == enemiesTospawn[i])
                        {
                            Battler newEnemy = Instantiate(enemyParty[i], enemyPosition[i].position, enemyPosition[i].rotation);
                            newEnemy.transform.parent = enemyPosition[i];
                            activeBattler.Add(newEnemy);

                        }
                    }
                }
            }
        }
    }

    public void NextTurn()
    {
        //Advance turn count
        currentTurn++;
        //If the turn cout is greater than the active Battlers list, reset it
        if (currentTurn >= activeBattler.Count)
        {
            currentTurn = 0;
        }
        turnWaiting = true;
        UpdateBattle();
    }

    public void UpdateBattle()
    {
        //Conditions to end the battle
        bool playerPartyWipedOut = true;
        bool enemyPartyWipedOut = true;

        //Check for the conditions to end the battle
        foreach (Battler characterInBattle in activeBattler)
        {
            //Check the chacters health, if bellow 0 set it to 0
            if (characterInBattle.currentHp < 0)
            {
                characterInBattle.currentHp = 0;
            }

            if (characterInBattle.currentHp == 0)
            {
                //Handles character death
            }
            else
            {
                //If one character still alive, the battle resumes
                if (characterInBattle.isPlayer)
                {
                    playerPartyWipedOut = false;
                }
                else
                {
                    enemyPartyWipedOut = false;
                }
            }
        }
        //If one of the partys is wiped out, end the battle
        if (playerPartyWipedOut || enemyPartyWipedOut)
        {
            //If it's the player party call Gamer Over
            if (playerPartyWipedOut)
            {
                //Game Over
            }
            //If it's the enemy party call victory screen
            else
            {
                //Battle Over/Victory
            }

            battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            battleActive = false;
        }
    }

    public IEnumerator EnemyMoveCo()
    {
        //This is to give some time to show the enemy's attack
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(2f);
        NextTurn();
    }

    public void EnemyAttack()
    {
        //Create a list with all active/alive characters from the player's party
        List<int> activePlayerparty = new List<int>();
        for (int i = 0; i < activeBattler.Count; i++)
        {
            if (activeBattler[i].isPlayer && activeBattler[i].currentHp > 0)
            {
                activePlayerparty.Add(i);
            }
        }
        //Select a random target from the avaible characters
        int attackTarget = activePlayerparty[Random.Range(0, activePlayerparty.Count)];
        //To improve code readability
        Battler target = activeBattler[attackTarget];
        Battler attacker = activeBattler[currentTurn];

        //Select a random attack from the active battler moves
        int selectAttack = Random.Range(0, attacker.moves.Length);

        //Show the name of the enemy and the move he is usig;
        StartCoroutine(AttackText(attacker.battlerName, attacker.moves[selectAttack].moveName));
        
        //VFX to show which enemy is attacking
        Instantiate(enemyAtkVfx, attacker.transform.position, attacker.transform.rotation);

        //Instantiate the visual effect on the target's position
        Instantiate(attacker.moves[selectAttack].visualEffect, target.transform.position, target.transform.rotation);

        //Pass on the target and the move power modifier 
        DealDamage(attackTarget, attacker.moves[selectAttack].modifier);
    }

    public IEnumerator AttackText(string attackerName, string enemyMove)
    {
        //This is to give some time to show the enemy's attack text
        attackPanel.SetActive(true);
        Debug.Log(attackerName + " used " + enemyMove);
        attackTxt.text = attackerName + " used " + enemyMove + "!";
        yield return new WaitForSeconds(1f);
        attackPanel.SetActive(false);
    }

    //Calculate the damage
    public void DealDamage(int target, int damagePower)
    {
        //Get the sum of powwr/def with the weaponPwr/ArmorDef as a float to do calculations
        float atkPower = activeBattler[currentTurn].power + activeBattler[currentTurn].wpnPwr;
        float defense = activeBattler[target].defense + activeBattler[target].armDef;

        //Damage formula to calculate the raw damage
        float rawDamage = (atkPower/defense) * damagePower * Random.Range(.9f, 1.2f);
        //Convert the damage to int before applyint it
        int actualDamage = Mathf.RoundToInt(rawDamage);
        //Apply the damage to the targer;
        activeBattler[target].currentHp -= actualDamage;

        //Display the Damage dealt by the attacker
        StartCoroutine(AttackerDamageText(activeBattler[currentTurn].battlerName, actualDamage));


       // Debug.Log(activeBattler[currentTurn].battlerName + " is dealing " + rawDamage + " converted to " + actualDamage + " hitting " + activeBattler[target].battlerName);
    }
      public IEnumerator AttackerDamageText(string attackerName, int damageNumber)
    {
        //This is to give some time to show the enemy's attack damage text
        yield return new WaitForSeconds(1f);
        attackPanel.SetActive(true);
        attackTxt.text = attackerName + " did " + damageNumber + " damage!";
        yield return new WaitForSeconds(1f);
        attackPanel.SetActive(false);
    }
}
