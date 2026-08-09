using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class TeleportToOrigin : MonoBehaviour
{
    public GameObject Floor;
    Vector3 startPos;
    Quaternion rotation;

    [SerializeField] GameObject ObjectToTeleport;

    Coroutine resetTime;
    bool resetting; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = ObjectToTeleport.transform.position;
        rotation = ObjectToTeleport.transform.rotation;
    }

    // when collision with floor was detected, a timer starts 
    // after timer is finished object gets teleported back to original position

    private void OnTriggerEnter(Collider other)
    {
        // resetting bool checks wheter the timer was already started to avoid starting more than one process of position reset 
        if (other.gameObject == Floor && resetting == false)
        {
            resetting = true;
            resetTime = StartCoroutine(DelayTeleport());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // when the Collision with floor stops and the object is currently resetting, the coroutine is stopped 
        // resetting is also set to false, cuz reset is not happening anymore
        if (other.gameObject == Floor && resetting == true)
        {
            StopCoroutine(resetTime);
            resetting = false;
        }
    }

    // Coroutine that waits 3 seconds and resets position afterwards 
    // resetting bool is there as a check so that no two time can be started at the same time 
    private IEnumerator DelayTeleport()
    {
        yield return new WaitForSeconds(3);
        ResetPosition();
        resetting = false;
    }

    private void ResetPosition()
    {
        ObjectToTeleport.transform.position = startPos;
        ObjectToTeleport.transform.rotation = rotation;
    }
}
