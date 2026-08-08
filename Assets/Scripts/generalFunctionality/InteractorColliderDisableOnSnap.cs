using Oculus.Interaction;
using System.Linq;
using UnityEngine;

/* Disables the child game object, which is responsable for physics collision */
public class InteractorColliderDisableOnSnap : MonoBehaviour
{
    SnapInteractable snapInteractable;

    void Start()
    {
        snapInteractable = GetComponent<SnapInteractable>();
        snapInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        snapInteractable.WhenSelectingInteractorRemoved.Action += OnInteractorRemoved;
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        DisableCollider(false, interactor);
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        //DisableCollider(true, interactor);
    }

    private void DisableCollider(bool disable, SnapInteractor interactor)
    {
        var snapInteractorParent = interactor.transform.parent.gameObject;
        var snapInteractorChildPhysicsCollider = snapInteractorParent.transform.Find("PhysicsCollider").gameObject;
        snapInteractorChildPhysicsCollider.SetActive(disable);
        Debug.Log($"Maus: Collider active {disable}"); 
    }
}
