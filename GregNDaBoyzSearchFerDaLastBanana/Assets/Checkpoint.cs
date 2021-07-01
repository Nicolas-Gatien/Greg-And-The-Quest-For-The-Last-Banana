using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Animator anim;
    public bool isActivated;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            other.GetComponent<PlayerMovement>().currentCheckpoint = this.transform.position;
            anim.SetTrigger("activate");
            isActivated = true;
        }
    }
}
