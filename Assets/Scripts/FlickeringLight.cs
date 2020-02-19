using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    private Light mainLight; //Light that will flicker
    [SerializeField] float min; //Min amount of seconds until next flicker
    [SerializeField] float max; //...

	// Use this for initialization
	void Start () {

        mainLight = GetComponent<Light>();  //Gets the light component of the Light game object
        StartCoroutine(Flicker());  //Starts a co routine to start flicker
	}

    /// <summary>
    /// Desc: A coroutine to flicker a light every few seconds based on a random min and max value.
    /// Author: Will
    /// Last Updated: 06/11/2018
    /// </summary>
    IEnumerator Flicker()
    {
        while (true)    //Infinate loop
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(min, max));  //Every X seconds it will run the code below
            mainLight.enabled = !mainLight.enabled; //Alternates states of true and false
        }
    }
}
