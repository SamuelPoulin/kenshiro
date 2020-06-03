using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody playerBody;
    public GameObject playerCharacter;

    PlayerController playerController;
    Animator animator;

    public Camera playerCamera;

    CameraRotation playerCameraRotation;

    const float PLAYER_SPEED = 7;
    const float PLAYER_RUN_SPEED = 14;
    const float PLAYER_CROUCH_SPEED = 5;
    const float JUMP_SPEED = 10;
    const float SPEED_REDUCTION =0.95f;

    

    Vector3 direction;
    

    private void Start()
    {
        playerCameraRotation = playerCamera.GetComponent<CameraRotation>();
        animator = playerCharacter.GetComponent<Animator>();
        direction = new Vector3();
        playerController = playerBody.GetComponent<PlayerController>();

        
    }
    void Update()
    {
        

        playerBody.transform.eulerAngles = new Vector3(0, playerCameraRotation.x, 0);
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", true);
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isCrouching", false);
            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W)))
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalkingForwards", true);
            direction.z = 1;
        }
        else
        {
            animator.SetBool("isWalkingForwards", false);
            if (direction.z != -1)
            {
                direction.z = 0;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isWalkingBackwards", true);
            direction.z = -1;
        }
        else
        {
            animator.SetBool("isWalkingBackwards", false);
            if (direction.z != 1)
            {
                direction.z = 0;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isWalkingLeft", true);
            direction.x = -1;
        }
        else
        {
            animator.SetBool("isWalkingLeft", false);
            if (direction.x != 1)
            {
                direction.x = 0;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalkingRight", true);
            direction.x = 1;
        }
        else
        {
            animator.SetBool("isWalkingRight", false);
            if (direction.x != -1)
            {
                direction.x = 0;
            }
        }

        //APPLY POSITION
        if(!playerController.IsDead)
        {
            if (animator.GetBool("isCrouching"))
            {
                playerBody.position += Quaternion.Euler(0, playerCameraRotation.x % 360, 0) * direction.normalized * PLAYER_CROUCH_SPEED * Time.deltaTime;
            }
            else
            {
                if (animator.GetBool("isRunning") && !Input.GetKey(KeyCode.S))
                {
                    playerBody.position += Quaternion.Euler(0, playerCameraRotation.x % 360, 0) * direction.normalized * PLAYER_RUN_SPEED * Time.deltaTime;
                }
                else
                {
                    playerBody.position += Quaternion.Euler(0, playerCameraRotation.x % 360, 0) * direction.normalized * PLAYER_SPEED * Time.deltaTime;
                }
            }
        }
        else
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalkingForwards", false);
            animator.SetBool("isWalkingBackwards", false);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingRight", false);
        }
    }
}
