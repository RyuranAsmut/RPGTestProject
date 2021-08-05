using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public int musicToPlay;

    private bool musicStaterd;

    private void Start() 
    {
        //make the camera follow the player
        target = PlayerController.instance.transform;
        var vcam =  GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = target;
    }

    private void Update() 
    {
        if (!musicStaterd)
        {
            musicStaterd = true;
            AudioManager.instance.PlayBGM(musicToPlay);
        }    
    }

}
