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

    public Battler[] playerParty;
    public Battler[] enemyParty;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BattleStart(new string[] {"Spider","Slime"});
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
        }
    }
}
