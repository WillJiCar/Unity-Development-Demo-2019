using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {
    //This script is instanced to each key

    public bool isCollected;    // Flags if this specfic key has been collected
    public ShowMessage showMessage; //Reference of Show Message script
    public GameManagerScript gameManager;   //...and Game Manager Script
    public GameObject gameManagerObj;   //Object of the game manager
    Renderer rend;  //The renderer of this game object, will be used to show and hide object

	// Use this for initialization
	void Start () {
        gameManagerObj = GameObject.Find("GameManager");    //Finds the game manager object
        gameManager = gameManagerObj.GetComponent<GameManagerScript>(); //Gets the instance of this script from that object
        showMessage = gameManagerObj.GetComponent<ShowMessage>();   //...
        rend = GetComponent<Renderer>();    //Gets the renderer from this game object attacked
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.transform.tag == "Player" && !isCollected) //If the player has collided with it and it is not collected yet
        {   
            rend.enabled = false;   //Disabled the render, hiding it
            isCollected = true; //Flags that it has been collected
            gameManager.currentKey++;   //Increments the current key in the game manager script
            StartCoroutine(showMessage.ShowText("Collected " + gameManager.currentKey + " out of 4 keys!"));    //Shows a message to the player
            //Debug.Log("Collected " + gameManager.currentKey + " out of 4 Keys!");   
        }
    }
}
