using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public int currentKey;  //The amount of keys that have been collected
    public int maximumKey = 4;  //Maximum keys, hard coded to 4, but could be automated by finding all game objects with "Key" tag

    [SerializeField] GameObject endCheckpointObj;   //The end checkpoint game object
    EndCheckpoint endCheck; //Reference of end checkpoint script
    ShowMessage showMessage;    //Reference of show message script

	// Use this for initialization
	void Start () {
        currentKey = 0; //Resets the current keys
        endCheck = endCheckpointObj.GetComponent<EndCheckpoint>();  //Grabs instance of end checkpoint script from end check object
        showMessage = GetComponent<ShowMessage>();  //Grabs instance of show message script
        StartCoroutine(showMessage.ShowText("Collect the 4 keys and reach the end of the maze to win!"));   //Shows the starting message
    }
	
	// Update is called once per frame
	void Update () {
        CheckForEndGame();  //Checks for when the game has ended
	}

    /// <summary>
    /// Desc: Checks to see if all of the keys have been collected, resulting in the end game
    /// Author: Will
    /// Last Updated: 4/11/2018
    /// </summary>
    private void CheckForEndGame()
    {
        if(currentKey == maximumKey)
        {
            endCheck.endGame = true;
        }
    }
}
