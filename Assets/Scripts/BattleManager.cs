using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private bool battleActive;

    public GameObject battleScene;

    public Transform[] playerPosition;
    public Transform[] enemyPosition;

    public Battler[] playerParty; //playable characer prefabs
    public Battler[] enemyParty; //enemy prefabs

    public List<Battler> activeBattler = new List<Battler>();

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
    }

    public void BattleStart(string[] enemiesTospawn)
    {
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
}
