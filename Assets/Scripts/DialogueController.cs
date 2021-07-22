using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueController : MonoBehaviour
{

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI dialogTxt;
    public GameObject nameBox;
    public GameObject dialogBox;
    public static DialogueController intance;
    public float txtDelay = 0.01f;
    public string[] dialog;
    [SerializeField] private int currentLine;
    private bool dialogStart;
    private bool isRunning;
    private string restOfSentence;

    private Quest currentQuest;

    private void Start()
    {
        intance = this;
    }
    private void Update()
    {
        CheckForDialog();
    }

    private void CheckForDialog()
    {
        if (dialogBox.activeInHierarchy && Input.GetButtonUp("Jump"))
        {
            if (!dialogStart) //Check if the dialog has just started
            {
                currentLine++;
                if (currentLine >= dialog.Length) //No more dialogue lines to display
                {
                    dialogBox.SetActive(false);
                    GameManager.instance.dialogActive = false;
                    if (currentQuest)
                    {
                        QuestHandler();
                    }
                }
                else
                {
                    CheckIfName();
                    dialogTxt.text = restOfSentence;
                }
            }
            else
            {
                dialogStart = false;
            }
        }
    }

    private void CheckIfName()
    {
        if (dialog[currentLine].StartsWith("n-"))
        {
            string sentence = dialog[currentLine];
            int firstSpace = sentence.IndexOf(' ');
            nameTxt.text = sentence.Substring(2, firstSpace - 1);
            restOfSentence = dialog[currentLine].Substring(firstSpace + 1);
        }
        else
        {
            restOfSentence = dialog[currentLine];
        }
    }

    public void ShowDialog(string[] newLines, bool namedNpc)
    {
        dialog = newLines;
        currentLine = 0;
        CheckIfName();
        dialogTxt.text = restOfSentence;
        dialogBox.SetActive(true);
        dialogStart = true;
        GameManager.instance.dialogActive = true;
        nameBox.SetActive(namedNpc);
    }

    public void QuestSetter(Quest quest)
    {
        if (quest)
        {
            currentQuest = quest;
        }
        else
        {
            currentQuest = null;
        }

    }

    public void QuestHandler()
    {
        QuestManager.instance.AdvanceQuestStatus(currentQuest);
    }

}
