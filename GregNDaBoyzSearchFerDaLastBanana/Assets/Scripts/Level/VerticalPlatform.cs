using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    PlatformEffector2D effector;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") > -0.1)
        {
            waitTime = 0.2f;
        }

        if(Input.GetAxis("Vertical") < -0.1)
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.2f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Vertical") > 0.1f || Input.GetAxis("Vertical") == 0)
        {
            effector.rotationalOffset = 0f;

        }
    }


}
