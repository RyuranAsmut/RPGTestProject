using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Create Quest")]
public class Quest : ScriptableObject
{

    [Header("Quest Info")]
    public int questId;
    public string questName;

    [TextArea(10, 14)] public string questDescription;
    public bool isStarted;
    public bool isCompleted;
    public bool isDelivered; //If it requires you to talk to a NPC to finish the quest

    [Header("Quest Reward")]
    public int expReward;
    public int moneyReward;
    public Item[] itemReward;


    public bool QuestStartedStatus()
    {
        return isStarted;
    }

    public void StartQuest()
    {
        if (!isDelivered && !isCompleted)
        {
            isStarted = true;
        }
    }


    public bool QuestCompletionStatus()
    {
        return isCompleted;
    }

    public void CompleteQuest()
    {
        isStarted = true;
        isCompleted = true;
    }

    public bool QuestDeliveryStatus()
    {
        return isDelivered;
    }

    public void DeliverQuest()
    {
        isStarted = true;
        isCompleted = true;
        isDelivered = true;
    }

    public void ResetQuest()
    {
        isStarted = false;
        isCompleted = false;
        isDelivered = false;
    }

}
