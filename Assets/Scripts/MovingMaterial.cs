using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMaterial : MonoBehaviour {

    Renderer mainMaterial;  //Material that is to be moved.
    [SerializeField] float scrollSpeed; //The speed of the scrolling

	// Use this for initialization
	void Start () {

        mainMaterial = GetComponent<Renderer>();    //Gets the Renderer of the material
	}
	
	// Update is called once per frame
	void Update () {

        float offset = Time.time * scrollSpeed; //Calculated the materaisl offset by multiplying the time it takes for each frame to pass by the speed
        mainMaterial.material.mainTextureOffset = new Vector2(offset, offset);  //Sets the offset to this calculated offset.
		
	}
}
