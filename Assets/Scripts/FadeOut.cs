using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image fadeScreen;
    public static FadeOut instance;

    private float fadeSpeed = 1.5f;
    [SerializeField] private bool fadeOut;
    [SerializeField] private bool fadeIn;

    private void Start() 
    {
        instance = this;   
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        CheckForTransition();
    }

    private void CheckForTransition()
    {
        if (fadeOut)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadeOut = false;
            }
        }

        if (fadeIn)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                fadeIn = false;
            }
        }
    }

    public void FadeToBlack()
    {
        fadeOut = true;
        fadeIn = false;
    }

    public void FadeFromBlack()
    {
        fadeIn = true;
        fadeOut = false;
    }
}
