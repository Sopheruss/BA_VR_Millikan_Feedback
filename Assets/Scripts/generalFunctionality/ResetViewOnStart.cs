using System.Collections;
using UnityEngine;

public class ResetViewOnStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RecenterShortDelay()); 
    }

    IEnumerator RecenterShortDelay()
    {
        yield return new WaitForSeconds(0.3f);
        OVRManager.display.RecenterPose();
        Debug.Log("Should have recentered"); 
        
    }
}
