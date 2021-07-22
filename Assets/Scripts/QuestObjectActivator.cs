using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{

    /*
    The 4 states are:
    0 - Before Accepting the Quest
    1 - After the Quest has been accepted
    2 - After the quest completion
    3 - After the quest delivery
    */
    public GameObject[] objectToActivate = new GameObject[4];

    public Quest questToCheck;

    //This variables can be changed depending on the object. 
    //Some objects may appear or dissapear when a quest is started/completed.
    //The below variables are alternatives for a possible different approach.
    // public bool activeOnQuestStart;
    // public bool activeOnQuestFinish;
    // public bool activeOnQuestDelivered;

    private void Update()
    {
        CheckQuestStatus();
    }

    public void CheckQuestStatus()
    {
        //The if statement are here so ther's no need to always fill all the states
        if (objectToActivate[0])
        {
            //If the quest has started deactivate the State 0
            if (questToCheck.QuestStartedStatus())
            {
                objectToActivate[0].SetActive(false);
            }
        }
        if (objectToActivate[1])
        {
            //If the quest hasn't been completed yet activate State 1
            if (!questToCheck.QuestCompletionStatus())
            {
                objectToActivate[1].SetActive(questToCheck.QuestStartedStatus());
            }
            //If the quest has been completed deactivate the State 1
            else if (questToCheck.QuestCompletionStatus())
            {
                objectToActivate[1].SetActive(false);
            }

        }
        if (objectToActivate[2])
        {
            //If the quest hasn't been delivered yet activate State 2
            if (!questToCheck.QuestDeliveryStatus())
            {
                objectToActivate[2].SetActive(questToCheck.QuestCompletionStatus());
            }
            //If the quest has been delivered deactivate State 2
            else if (questToCheck.QuestDeliveryStatus())
            {
                objectToActivate[2].SetActive(false);
            }
        }
        if (objectToActivate[3])
        {
            //If the quest has been delivered activate State 3
            objectToActivate[3].SetActive(questToCheck.QuestDeliveryStatus());
        }

        //Switch approach with less code lines but more issues to tackle on
        // switch(QuestManager.instance.CheckForCompletion(questToCheck))
        // {
        //     case 1:
        //     objectToActivate[1].SetActive(true);
        //     break;

        //     case 2:
        //     objectToActivate[1].SetActive(false);
        //     objectToActivate[2].SetActive(true);
        //     break;

        //     case 3:
        //     objectToActivate[1].SetActive(false);
        //     objectToActivate[2].SetActive(false);
        //     objectToActivate[3].SetActive(true);
        //     break;

        //     case 0:
        //     break;
        // }



    }
}
