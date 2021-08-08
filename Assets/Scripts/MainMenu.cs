using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string newGameScene;
    public string loadGameScene;
    public GameObject loadGameBt;

    private void Start() 
    {
       if (PlayerPrefs.HasKey("Current_Scene"))
        {
            loadGameBt.SetActive(true);
        }
        else
        {
            loadGameBt.SetActive(false);
        }    
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(loadGameScene);
    }

    public void LoadSettingsMenu()
    {
        //TODO
    }

    /*Confirmation Panel not in use   
    public void HiddeConfirmationPanel()
      {
          confimationPanel.SetActive(false);
          confirmationPanelOpen = false;
      } */

    public void QuitGame()
    {

        Application.Quit();


    }
}
