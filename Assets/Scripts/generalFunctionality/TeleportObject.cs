using System.Collections;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    [SerializeField] GameObject thisObject;
    private Vector3 initPos; 
    private Quaternion initRot;

    private bool resetting;
    Coroutine resetTime; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initPos = thisObject.transform.position;
        initRot = thisObject.transform.rotation;
        resetting = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (thisObject.transform.position.y < 0.5 && resetting == false)
        {
            resetting = true;
            resetTime = StartCoroutine(DelayTeleport());
        }
    }

    private IEnumerator DelayTeleport()
    {
        yield return new WaitForSeconds(3);
        ResetPosition();
        resetting = false;
    }

    private void ResetPosition()
    {
        thisObject.transform.SetPositionAndRotation(initPos, initRot);
    }
}
