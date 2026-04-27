using System.Collections;
using UnityEngine;

public class TeleportToOrigin : MonoBehaviour
{
    public GameObject Floor;
    Vector3 startPos;
    Quaternion rotation;

    Coroutine resetTime;
    bool resetting; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        rotation = transform.rotation;
    }

    // when collision with floor was detected, a timer starts 
    // after timer is finished object gets teleported back to original position

    private void OnCollisionEnter(Collision collision)
    {
        // resetting bool checks wheter the timer was already started to avoid starting more than one process of position reset 
        if (collision.gameObject == Floor && !resetting)
        {
            resetTime = StartCoroutine(DelayTeleport());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // when the Collision with floor stops and the object is currently resetting, the coroutine is stopped 
        // resetting is also set to false, cuz reset is not happening anymore 

        if (collision.gameObject == Floor && resetting)
        {
            StopCoroutine(resetTime);
            resetting = false;
        }
    }

    // Coroutine that waits 10 seconds and resets position afterwards 
    // resetting bool is there as a check so that no two time can be started at the same time 
    private IEnumerator DelayTeleport()
    {
        resetting = true; 
        yield return new WaitForSeconds(10);
        ResetPosition();
    }

    private void ResetPosition()
    {
        transform.position = startPos;
        transform.rotation = rotation;
    }
}
