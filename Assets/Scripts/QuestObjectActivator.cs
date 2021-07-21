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
            //This is different because there's no "QuestHasntStarted" variable for the first state
            if (questToCheck.QuestStartedStatus())
            {
                objectToActivate[0].SetActive(false);
            }
        }
        if (objectToActivate[1])
        {
            objectToActivate[1].SetActive(questToCheck.QuestStartedStatus());
        }
        if (objectToActivate[2])
        {
            objectToActivate[2].SetActive(questToCheck.QuestCompletionStatus());
        }
        if (objectToActivate[3])
        {
            objectToActivate[3].SetActive(questToCheck.QuestDeliveryStatus());
        }
        

    }
}
