using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gremlins : MonoBehaviour
{
    public GameObject gremlin;
    public int gremlins;
    public float followRange = 10;

    public float followSpace; // amount of space between gremlins

    // Start is called before the first frame update
    void Start()
    {
        while (gremlins > 0)
        {
            AddGremlin();
        }
    }


    void AddGremlin()
    {
        FollowPlayer followPlayer = gameObject.AddComponent<FollowPlayer>();
        followPlayer.followingMe = Instantiate(gremlin, transform.position, Quaternion.identity);
        followPlayer.followDistance = followRange;
        followRange += followSpace;
        gremlins--;
    }
}
