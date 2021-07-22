using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    //Barely functional, need A LOT of improvements.

    public Quest[] quests;


    public static QuestManager instance;

    private void Start() 
    {
        instance = this;    

        //Reset quests for testing purposes
        foreach (Quest quest in quests)
        {
            quest.ResetQuest();
        }
    }

    //Check to see what state the quest is
    //State 0 - Has not been started yet
    //State 1 - Quest has been started
    //State 2 - Quest has been completed
    //State 3 - Quest has been delivered
    public int CheckForCompletion(Quest questToCheck)
    {
        if (questToCheck.QuestDeliveryStatus())
        {
            return 3; 
        } 
        else if (questToCheck.QuestCompletionStatus())
        {
            return 2;
        }
        else if (questToCheck.QuestStartedStatus())
        {
            return 1;
        }
        else 
        {
            return 0;
        }
    }

    public void AdvanceQuestStatus(Quest questToChange)
    {
        if (!questToChange.QuestStartedStatus())
        {
            questToChange.StartQuest();
            return;
        } 
        else if (!questToChange.QuestCompletionStatus())
        {
            questToChange.CompleteQuest();
            return;
        }
        else if (!questToChange.QuestDeliveryStatus())
        {
            questToChange.DeliverQuest();
            return;
        }
    }

    public void SaveQuestData()
    {
        //Save the state of each quest from 0 to 3
        foreach (Quest quest in quests)
        {
            PlayerPrefs.SetInt("Quest_" + quest.questName, CheckForCompletion(quest));
        }
    }

    public void LoadQuestData()
    {
        foreach (Quest quest in quests)
        {
            if (PlayerPrefs.HasKey("Quest_" + quest.questName))
            {
                switch (PlayerPrefs.GetInt("Quest_" + quest.questName))
                {
                    case 1:
                    quest.StartQuest();
                    break;
                    case 2:
                    quest.CompleteQuest();
                    break;
                    case 3:
                    quest.DeliverQuest();
                    break;
                    case 0:
                    quest.ResetQuest();
                    break;
                }
            }
        }
    }
}
