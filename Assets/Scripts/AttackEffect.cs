using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public int sfx;

    private void Start() 
    {
        AudioManager.instance.PlaySFX(sfx);    
    }

    public void DestroyOnceDone() 
    {
        Destroy(gameObject);   
    }
}
