using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Create Quest")]
public class Quest : ScriptableObject
{

    [Header("Quest Info")]
    public int questId;
    public string questName;

    [TextArea(10,14)]public string questDescription;

    public bool isStarted;
    public bool isCompleted;

    [Header("Quest Reward")]

    public int expReward;

    public int moneyReward;

    public Item[] itemReward;


    public bool QuestStartedStatus()
    {
        return isStarted;
    }

    public void QuestStart()
    {
        isStarted = true;
    }


    public bool QuestCompletionStatus()
    {
        return isCompleted;
    }

    public void ChangeQuestStatus()
    {
        if(!isCompleted)
        {
            isCompleted = true;
            isStarted = false;
        } 
        else if (isCompleted)
        {
            isCompleted = false;
        }
    }

}
