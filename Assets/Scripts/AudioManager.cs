using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] bgmArray;
    public AudioSource[] sfxArray;

    public static AudioManager instance;



    private void Start()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        /*  if (Input.GetKeyDown(KeyCode.P))
         {
             PlayBGM(2);
         }
         if (Input.GetKeyDown(KeyCode.O))
         {
             PlayBGM(0);
         }      
         if (Input.GetKeyDown(KeyCode.L))
         {
             PlaySFX(3);
         }    */
    }

    public void PlaySFX(int sfxIndex)
    {
        if (sfxIndex < sfxArray.Length)
        {
            sfxArray[sfxIndex].Play();
        }
    }

    public void PlayBGM(int bgmIndex)
    {
        //Check if the wanted BGM is already not playing to not reset the music
        if (!bgmArray[bgmIndex].isPlaying)
        {
            StopBGM();
            if (bgmIndex < bgmArray.Length)
            {
                bgmArray[bgmIndex].Play();
            }
        }

    }

    public void StopBGM()
    {
        foreach (AudioSource bgm in bgmArray)
        {
            bgm.Stop();
        }
    }
}
