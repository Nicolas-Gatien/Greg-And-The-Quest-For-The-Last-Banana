using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraObj;

    public Collider2D pastCollider;
    public Collider2D currentCollider;
    public Collider2D newCollider;

    public Transform curPos;
    public float smoothTime;

    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if(curPos != null)
        {
            cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, curPos.position + offset,smoothTime);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Room"))
        {
            currentCollider = other;
            if (currentCollider != pastCollider)
            {
                newCollider = currentCollider;
            }
            curPos = currentCollider.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            if (collision == pastCollider)
            {
                pastCollider = newCollider;
            }
        }
    }
}
