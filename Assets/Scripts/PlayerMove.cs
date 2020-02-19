using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private string horizontalInputName;    //Name of input buttons to affect Z axis
    [SerializeField] private string verticalInputName;  //Name of input buttons to affect X axis

    [SerializeField] private float walkSpeed;   //Walking speed of player
    [SerializeField] private float sprintSpeed; //Running speed of player
    [SerializeField] private float crouchSpeed;//Speed of player when crouching
    public float realSpeed; //The current speed of the player, depending if they are walking, running or crouching.

    private CharacterController charController; //Stores the character controller
    private Transform playerTransform;  //Stores the transform of the player
    private Vector3 standingHeight; //The height of the player when standing..
    private Vector3 crouchHeight;   //..when crouching
    private bool isCrouching;   //bool to detect if crouching

    [SerializeField] private AnimationCurve jumpFallOff;    //The curve of animation to store key frames when jumping
    [SerializeField] private float jumpMultiplier;  //Height of the jump
    [SerializeField] private KeyCode jumpKey;   //Key for jumping (Space)
    private bool isJumping; //bool to detect if jumping

    private void Awake()
    {
        charController = GetComponent<CharacterController>();   //Gets Character controller
        playerTransform = GetComponent<Transform>();    //Gets Transform
        standingHeight = playerTransform.localScale;    //The standing height is the height at the start of the game
        crouchHeight = new Vector3(0, playerTransform.localScale.y / 2);    //Crouching is just half of the height
        isCrouching = false;    //At the start of the script, the player is not crouching.
    }
	
	void Update () {
        PlayerMovement();   //Moves the player
        CheckForSprinting();    //Makes the player Spring
        CheckForCrouching();    //Makes the player crouch
        JumpInput();    //Detects for jumping
    }

    /// <summary>
    /// Desc: Script to check whether to enable the crouch function, and then return back to normal. and changes the speed
    /// Author: Will
    /// Last Updated: 29/10/2018
    /// </summary>
    private void CheckForCrouching()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            realSpeed = crouchSpeed;    //Sets speed to crouch speed
            playerTransform.localScale = crouchHeight;  //The players height is now crouch height.
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching)    //Will only do this if the player is crouching
        {
            realSpeed = walkSpeed;  //Sets speed back to walk speed
            playerTransform.localScale = standingHeight;
            isCrouching = false;
        }
    }

    /// <summary>
    /// Desc: Enables the Sprint function for the player, changes the speed to sprint speed
    /// Author: Will
    /// Last Updated: 29/10/2018
    /// </summary>
    private void CheckForSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)    //Will not let player run if they are crouching
        {
            //Debug.Log("Sprint");
            realSpeed = sprintSpeed;
        }
        else
            realSpeed = walkSpeed;
    }

    /// <summary>
    /// Desc: The main movement of the player is done in this script. Using the speed from other functions.
    /// Author: Will
    /// Last Updated: 29/10/2018
    /// </summary>
    private void PlayerMovement()
    {
        float vertInput = Input.GetAxis(verticalInputName) * realSpeed; //Value is either 1, 0 or -1, for example, if forward then it'll be 1 * 6,
                                                                                                //..or if backward then -1 * 6 = -6(Which is backwards)
        float horizInput = Input.GetAxis(horizontalInputName) * realSpeed;  //works the same way when going side to side

        Vector3 forwardMovement = transform.forward * vertInput;    //Times the players forward direction by the speed, which will be either forward 
                                                                                                                //..if positive or backward if negative.
        Vector3 rightMovement = transform.right * horizInput;   //Works the same way but side to side

        charController.SimpleMove(forwardMovement + rightMovement); //Uses method within Character Controller called Simple move, 
                                                                           //which moves the transform along both axis using deltaTime.
    }

    /// <summary>
    /// Desc: Detects the input for the jump function
    /// Author: Will
    /// Last Updated: 29/10/2018
    /// </summary>
    private void JumpInput()
    {
        if(Input.GetKeyDown(jumpKey) && !isJumping) //Will only work if not jumping; cannot jump in the air.
        {
            isJumping = true;
            StartCoroutine(JumpEvent());    //Starts a coroutine to enable jumpting if jump key is pressed.
        }
    }

    /// <summary>
    /// Desc: Runs a coroutine to start a jump animation.
    /// Author: Will
    /// Last Updated: 29/10/2018
    /// </summary>
    private IEnumerator JumpEvent()
    {
        float timeInAir = 0;    //Reset time in air to 0 each time the co routine starts.
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);  //Changes the force of the jump depending which point it is at during the animation curve.
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);  //Moves the controller Up, using the force of the curve toward the maximum height.
            timeInAir *= Time.deltaTime;    //The time in air is calculated using the time it takes between each frame in seconds. (I.e Unity's way of counting in seconds)
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);  //Will do this routine while the controller 
                                                        //is not grounded and is not touching something above them (so they don't go through the ceiling)
        isJumping = false;  //Once the ground is hit, it sets the jumping flag back to false.
    }
}
