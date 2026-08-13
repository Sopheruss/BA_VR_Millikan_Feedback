using Oculus.Interaction;
using UnityEngine;

public class SnapActivateIsTrigger : MonoBehaviour
{
    SnapInteractable interactable; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactable = GetComponent<SnapInteractable>();
        interactable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        interactable.WhenSelectingInteractorRemoved.Action += OnInteractorRemoved; 
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        var physicsCollider = interactor.transform.parent.Find("PhysicsCollider");

        Debug.Log("phyiscs collider acquired: " +  physicsCollider.ToString());

        var childCount = physicsCollider.childCount;

        for (int i = 0; i < childCount; i++)
        {
            physicsCollider.GetChild(i).GetComponent<Collider>().isTrigger = true;
            Debug.Log($"{physicsCollider.GetChild(i).ToString()} was set to isTrigger"); 
        }
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        var physicsCollider = interactor.transform.parent.Find("PhysicsCollider");

        var childCount = physicsCollider.childCount;

        for (int i = 0; i < childCount; i++)
        {
            physicsCollider.GetChild(i).GetComponent<Collider>().isTrigger = false;
        }
    }
}
