using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPlayer : MonoBehaviour
{
    //place this script on the player gameobject

    public GameObject followingMe; // in the inspector drag the gameobject the will be following the player to this field
    public float followDistance;
    private List<Vector3> storedPositions;


    void Awake()
    {
        storedPositions = new List<Vector3>(); //create a blank list

        if (!followingMe)
        {
            Debug.Log("The FollowingMe gameobject was not set");
        }

        if (followDistance == 0)
        {
            Debug.Log("Please set distance higher then 0");
        }
    }

    void Start()
    {

    }

    void Update()
    {
        storedPositions.Add(transform.position); //store the position every frame

        if (storedPositions.Count > followDistance)
        {
            followingMe.transform.position = storedPositions[0]; //move the player
            storedPositions.RemoveAt(0); //delete the position that player just move to
        }
    }
}