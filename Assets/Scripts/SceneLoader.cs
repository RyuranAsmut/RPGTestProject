using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    public string transitionName;
    private float waitLoad = 0.5f;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        FadeOut.instance.FadeToBlack();
        GameManager.instance.transtionActive = true;
        PlayerController.instance.areaTransitionName = transitionName;
        yield return new WaitForSeconds(waitLoad);
        SceneManager.LoadScene(sceneToLoad);
        FadeOut.instance.FadeFromBlack();
        GameManager.instance.transtionActive = false;
    }
}
