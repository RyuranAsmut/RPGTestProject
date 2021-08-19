using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*TODO
-Implement a Visual Turn Tracker
-Implement a Position Change Action
-Make the turns based on speed/Agility and character actions
*/


public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private bool battleActive;

    public GameObject battleScene;
    public GameObject actionButtons;

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
        if (Input.GetKeyDown(KeyCode.B))
        {
            BattleStart(new string[] { "Spider", "Slime" });
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
            Debug.Log(currentTurn);
        }

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
        currentTurn = 0;
        if (!battleActive)
        {
            battleActive = true;
            GameManager.instance.battleActive = true;
            //To adjust the position of the Battle Background
            battleScene.transform.position = new Vector3(
                Camera.main.transform.position.x, (Camera.main.transform.position.y + 1), transform.position.z);

            battleScene.SetActive(true);
            AudioManager.instance.PlayBGM(6);

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
        currentTurn++;
        if (currentTurn >= activeBattler.Count)
        {
            currentTurn = 0;
        }
        turnWaiting = true;
        UpdateBattle();
    }

    public void UpdateBattle()
    {
        bool playerPartyWipedOut = true;
        bool enemyPartyWipedOut = true;

        foreach (Battler characterInBattle in activeBattler)
        {
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
        if (playerPartyWipedOut || enemyPartyWipedOut)
        {
            if (playerPartyWipedOut)
            {
                //Game Over
            }
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
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(1f);
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

        //Select a random attack from the active battler moves
        int selectAttack = Random.Range(0, activeBattler[currentTurn].moves.Length);

        //Instantiate the visual effect on the target's position
        Instantiate(activeBattler[currentTurn].moves[selectAttack].visualEffect, activeBattler[attackTarget].transform.position, activeBattler[attackTarget].transform.rotation);
    }
}
