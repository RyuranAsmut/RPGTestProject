using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public Quest[] quest;


    public static QuestManager instance;

    private void Start() 
    {
        instance = this;    
    }

    //Not sure yet if any of the 2 bellow functions are necessary
    public bool CheckForCompletion(Quest questToCheck)
    {
        return questToCheck.QuestCompletionStatus();
    }

    public void ChangeCompletionStatus(Quest questToChange)
    {
        questToChange.ChangeQuestStatus();
    }
}
