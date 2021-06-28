using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    public float deathTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deathTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
