using Oculus.Interaction;
using UnityEngine;

public class InteractorLogging : MonoBehaviour
{
    GrabInteractable thisGrabInteractable; 
    string objectName; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisGrabInteractable = GetComponent<GrabInteractable>();
        objectName = thisGrabInteractable.transform.parent.gameObject.name;
        thisGrabInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded; 
        thisGrabInteractable.WhenSelectingInteractorRemoved.Action += OnInteractorRemoved;
    }

    private void OnInteractorAdded(GrabInteractor grabInteractor)
    {
        Logging.log.Information($",{objectName},-,grabbed");
    }
    private void OnInteractorRemoved(GrabInteractor grabInteractor)
    {
        Logging.log.Information($",{objectName},-,letgo");
    }
}
