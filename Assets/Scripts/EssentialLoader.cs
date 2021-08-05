using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    public GameObject gameManager;
    public GameObject audioManager;

    private void Awake() 
    {
        //If theres no intance of the objects in the scene, instatiate it
        if (!PlayerController.instance)
        {
            Instantiate(player);
        }
        if (!FadeOut.instance)
        {
            Instantiate(canvas);
        }
        if (!GameManager.instance)
        {
            Instantiate(gameManager);
        }
        if (!AudioManager.instance)
        {
            Instantiate(audioManager);
        } 
    }
}
