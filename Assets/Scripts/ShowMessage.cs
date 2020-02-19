using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour {

    GameObject panelObj;    //The object that holds the Background text panel
    GameObject textObj; //The object that holds the main text object
    GameObject timerObj;    //The game object for the timer
    GameObject bestTimeObj; //Game object for the Best Time text
    GameObject endCheckpointObj;    //Object of the end checkpoint
    EndCheckpoint endCheckpoint;    //Instance of the End Checkpoint script
    Image panel;    //Image componant inside panelObj
    Text text;  //text inside TextObj
    Text timer; //Timer Text inside timerObj
    Text bestTimeText;  //Best Time text inside BestTimeObj

    public float timerFloat;    //The float of the timer
    public float bestTime;  //The float of the best time
    public string formatTime;   //String that contains the current time in a standard time format
    public string bestTimeString;   //String that contains the best time, but in a formatted string

	// Use this for initialization
	void Start () {
        panelObj = GameObject.Find("TextBackground");   //Finds the gameobject..
        textObj = GameObject.Find("OnScreenMessage");   //..
        panel = panelObj.GetComponent<Image>(); //Gets the "Panel" Which is actually an Image component inside the panelObj
        text = textObj.GetComponent<Text>();    //Gets the Text from the Text Object
        timerObj = GameObject.Find("Timer");    //Finds the gameobject..
        timer = timerObj.GetComponent<Text>();  //..then retrieves the text from it
        bestTimeObj = GameObject.Find("BestTime");  //Fins the game object..
        bestTimeText = bestTimeObj.GetComponent<Text>(); //..then retrieves the text from it

        endCheckpointObj = GameObject.Find("End");  //Finds the gameobject
        endCheckpoint = endCheckpointObj.GetComponent<EndCheckpoint>(); //Grabs the instance of the end checkpoint script from the gameobject

        timerFloat = 0; //Resets the timer to 0
        bestTime = PlayerPrefs.GetFloat("bestTime");    //Gets the best time float from the saved game files, will be used to compare with the timer. (Back End)
        bestTimeString = PlayerPrefs.GetString("bestTimeString");   //Gets the best time string from the game files, will be used to show on screen. (Front End)
	}

    private void Update()
    {
        if(!endCheckpoint.gameComplete) //If the game is not complete
        {
            timerFloat += Time.deltaTime;   //The timer is calculated by adding onto it the time it takes for each frame to pass, which is a fraction of a second
        }

    }

    private void OnGUI()
    {
        if(endCheckpoint.gameComplete)  //Once the game is complete
        {
            if (timerFloat > bestTime)  //If Current time is more than saved best time
            {
                bestTime = timerFloat;  //Stores the float of current time into the best time float
                PlayerPrefs.SetFloat("bestTime", bestTime); //Stores the float into the game files
                PlayerPrefs.Save(); //Saves it

                bestTimeString = formatTime;    //Formats the current time and then stores it as the best time string, otherwise the time will look like "155" instead of "2:35"
                PlayerPrefs.SetString("bestTimeString", bestTimeString);    //Stores this string
                PlayerPrefs.Save(); //Saves it

            }
        }

        float minute = timerFloat / 60; //Minutes is calculated by dividing seconds by 60
        float second = timerFloat % 60; //Seconds are calculated by finding the remainder of the current minute (60 Seconds)
        float fraction = (timerFloat * 100) % 100;  //Fraction of second is worked out by multiplying time by 100 and then finding the remainder every 100 seconds

        formatTime = string.Format("{0:00}:{1:00}:{2:000}", minute, second, fraction);  //Formats the string 
        timer.text = "Timer: " + formatTime;    //Sets the text as this formatted string

        if(bestTimeString == "")    //If there is no best time yet, it will handle that exception
        {
            bestTimeText.text = "No Best Time Yet";
        }
        else
        {
            bestTimeText.text = "Best Time: " + bestTimeString;
        }
    }

    /// <summary>
    /// Desc: Will show text to the screen for a few seconds, taking the message as a parameter, then resets text after seconds.
    /// Author: Will
    /// Last Updated: 4/11/2018
    /// </summary>
    public IEnumerator ShowText(string message)
    {
        panel.enabled = true;   //Shows black background
        text.text = message;    //Puts message to screen
        yield return new WaitForSeconds(4); //Shows message for seconds
        panel.enabled = false;  //Turns off black background
        text.text = ""; //Resets text
    }
}
