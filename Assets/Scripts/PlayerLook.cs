using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    [SerializeField] private string mouseXInputName, mouseYInputName;   //Stores names of Inputs for moving mouse up and right.
    [SerializeField] private float mouseSensitivity;    //how fast the camera will move.
    [SerializeField] private Transform playerBody;  //Parent of Camera.

    private float xAxisClamp;   //Used to make sure camera does not go behind player on X axis(The front of player).

    private void Awake()
    {
        LockCurser();   //Locks the cursor to screen and hides it.
        xAxisClamp = 0; //Sets clamp to 0;
    }

    /// <summary>
    /// Desc: Locks and hides the cursor.
    /// Author: Will
    /// Last Updated: 29/10/2018
    /// </summary>
    private void LockCurser()
    {
        Cursor.visible = false; //Sets cursor to invisible
        Cursor.lockState = CursorLockMode.Locked;   //Locks cursor to middle.
    }
	
	void Update () {
        CameraRotation();   //Rotates camera along with body.
		
	}

    /// <summary>
    /// Desc: Rptates the camera using the mouse and rotates the parent transform.
    /// Author: Will
    /// Last Updated: 29/10/2018
    /// </summary>
    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;  //Moves the mouse along the X axis using the mouse sensitivity in relation 
                                                                                                                                         //to time between each frame..
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;  //..Same with Y axis

        xAxisClamp += mouseY;   //Stores the float calculated to store mouse location along the camera Matrix.

        if( xAxisClamp > 90)    //If this float goes above 90 (Directly above player)
        {
            xAxisClamp = 90;    //Sets the clamp back to 90
            mouseY = 0; //Makes the mouse not move.
            ClampXAxisRotation(270); //The rotation of the camera in angles is stuck to this maximum value along the X axis.
        }
        else if (xAxisClamp < -90)  //If the float goes below -90 (Directly below)
        {
            xAxisClamp = -90;   
            mouseY = 0;
            ClampXAxisRotation(90);
        }

        transform.Rotate(Vector3.left * mouseY);    //Rotates the left and right of camera using the mouseY value
        playerBody.Rotate(Vector3.up * mouseX); //Rotates the parent of the camera using the mouse X value.
    }

    /// <summary>
    /// Desc: Prevents the camera going behind the player using a clamp.
    /// Author: Will
    /// Last Updated: 29/10/2018
    /// </summary>
    private void ClampXAxisRotation(float value) //Clamps the X axis to a certain angle using the float value parsed in.
    {
        Vector3 eulerRotation = transform.eulerAngles;  //Stores a rotation using the rotation of the camera.
        eulerRotation.x = value;    //The x of this rotation is set to the value that is parsed in (The clamp value)
        transform.eulerAngles = eulerRotation;  //Reverse the previous assignment, so the angle of the transform is now the Rotation.
    }
}
