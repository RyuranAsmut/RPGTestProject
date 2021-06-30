using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] string transitionName;
    public SceneLoader transition;

    private void Start() 
    {
        transitionName = transition.transitionName;
        if(transitionName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = transform.position;
        }     
    }
}
