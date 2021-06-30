using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Animator playerAnim;
    [SerializeField] float moveSpeed;
    public string areaTransitionName;
    public bool canMove = true;
    public static PlayerController instance;

    private void Awake() 
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canMove)
        {
            playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else
        {
            playerRB.velocity = Vector2.zero;
        }
        
        playerAnim.SetFloat("moveX", playerRB.velocity.x);
        playerAnim.SetFloat("moveY", playerRB.velocity.y);

        //Check to see which axis the player is moving to display the correct idle animation
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 
        || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                playerAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                playerAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }

        } 
    }
}
