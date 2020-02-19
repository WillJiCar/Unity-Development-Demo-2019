using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachMaterial : MonoBehaviour {

    [SerializeField] List<GameObject> mazeWalls;    //A list to hold every maze wall,
    [SerializeField] Material mat;  //The material that will be attached to each wall

    // Use this for initialization
    void Start()
    {
        foreach (Transform mazeWall in transform)   //Each sub folder inside the folder of the walls
        {
            foreach (Transform child in mazeWall)   //Each game object inside every sub folder.
            {
                if(child.GetComponent<Renderer>() != null)  //If the game object doesn't have a renderer then skip
                {
                    mazeWalls.Add(mazeWall.gameObject); //Add this wall to the list
                    child.GetComponent<Renderer>().material = mat;  //Attach the materail to the wall.
                }             
            }
        }
    }
}
