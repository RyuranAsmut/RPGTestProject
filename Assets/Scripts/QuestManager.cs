using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    //Barely functional, need A LOT of improvements.

    public Quest[] quest;


    public static QuestManager instance;

    private void Start() 
    {
        instance = this;    
    }

    //Not sure yet if any of the bellow function is necessary
    public bool CheckForCompletion(Quest questToCheck)
    {
        if (questToCheck.QuestStartedStatus() == true)
        {
            return questToCheck.QuestStartedStatus(); 
        } 
        else if (questToCheck.QuestCompletionStatus() == true)
        {
            return questToCheck.QuestCompletionStatus();
        }
        else if (questToCheck.QuestDeliveryStatus() == true)
        {
            return questToCheck.QuestDeliveryStatus();
        }
        else 
        {
            return false;
        }
    }

    public void AdvanceQuestStatus(Quest questToChange)
    {
        if (questToChange.QuestStartedStatus() == false)
        {
            questToChange.StartQuest();
        } 
        else if (questToChange.QuestCompletionStatus() == false)
        {
            questToChange.CompleteQuest();
        }
        else if (questToChange.QuestDeliveryStatus() == false)
        {
            questToChange.DeliverQuest();
        }
    }
}
