using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour {

    bool colorDisabled = false; //Flag to tell the script to not color the wall

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject mazeWall = hit.transform.parent.gameObject;  //Attaches the parent game object of the hit into this Game Object.
            //Uses parent game object because the parent object has the tag "Maze Wall"    

        if (mazeWall.tag == "MazeWalls" && !colorDisabled)  //If the parent has the tag "MazeWalls" and the coloring isn't disabled
        {
            Debug.Log("Hit Wall!"); //Sends a message to the console to show that you have hit a wall
            hit.transform.GetComponent<Renderer>().material.color = Random.ColorHSV(0, 1, 1, 1, 0.5f, 1);   //Gets the material of the wall and changes it to a random color
            colorDisabled = true;   //Disabled the color
            Invoke("SetBoolBack", 0.5f);    //After half a second it will let the wall allowed to be colored again
        }
    }

    /// <summary>
    /// Desc: Resets the walls flag of disabling color to allow the wall to be colored again.
    /// Author: Will
    /// Last Updated: 4/11/2018
    /// </summary>
    private void SetBoolBack()
    {
        colorDisabled = false;
    }
}
