using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private void Start() 
    {
        //make the camera follow the player
        target = PlayerController.instance.transform;
        var vcam =  GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = target;


    }

}
