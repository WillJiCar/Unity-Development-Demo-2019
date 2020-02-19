using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCheckpoint : MonoBehaviour {

    public bool endGame;    //A flag to show that the game is able to be ended  (Player collected all keys)
    public bool gameComplete;   //A flag to show that the game has in fact been completed (Player walked into end checkpoint)
    public GameObject gameManObj;   //The Game Manager object
    public ShowMessage showMessage; //Script to show message

	// Use this for initialization
	void Start () {
        endGame = false;    //At the start of the game, the end game is false.
        gameComplete = false;   //...

        gameManObj = GameObject.Find("GameManager");    //Finds the Game Manager object
        showMessage = gameManObj.GetComponent<ShowMessage>();   //Gets the Show Message script inside the game manager
	}

    private void Update()
    {
        if (gameComplete && Input.GetKey(KeyCode.Return))   //If the player has completed the game and presses Enter
        {
            //Debug.Log("Load Scene");    
            SceneManager.LoadScene("Task45");   //Re loads scene
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.transform.tag == "Player" && endGame)   //If the player reaches the end checkpoint and has ended the game
        {
            Debug.Log("You have reached the end of the Maze!");
            gameComplete = true;    //The game is now complete
            StartCoroutine(showMessage.ShowText("You have reached the end of the maze! Press Enter to Restart"));   //Shows the text to notify the player
            
        }
        else if (collision.gameObject.transform.tag == "Player" && !endGame)    //If the game isn't ended
        {
            //Debug.Log("Not Enough Keys Collected!");
            StartCoroutine(showMessage.ShowText("Not enough keys collected!")); //Will notify the player
        }
    }
}
