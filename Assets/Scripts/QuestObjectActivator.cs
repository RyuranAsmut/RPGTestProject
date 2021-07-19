using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{

    public GameObject objectToActivate;

    public Quest questToCheck;

    //This variables can be changed depending on the object. 
    //Some objects may appear or dissapear when a quest is started/completed.
    public bool activeOnQuestStart;
    public bool activeOnQuestFinish;

    private void Update() 
    {
        CheckQuestStatus();
        CheckQuestCompletion();    
    }

    public void CheckQuestStatus()
    {
        if (questToCheck.QuestStartedStatus())
        {
            objectToActivate.SetActive(activeOnQuestStart);
        }
    }

    public void CheckQuestCompletion()
    {
        if (questToCheck.QuestCompletionStatus())
        {
            objectToActivate.SetActive(activeOnQuestFinish);
        }
    }
}
