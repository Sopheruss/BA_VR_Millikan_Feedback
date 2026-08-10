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
        var snapInteractorParentRigidbody = interactor.transform.parent.gameObject.GetComponent<Rigidbody>();

        //Debug.Log($"{snapInteractorParentRigidbody.name} is kinematic active: {snapInteractorParentRigidbody.isKinematic}");
        //Debug.Log($"{snapInteractorParentRigidbody.name} use gravity active: {snapInteractorParentRigidbody.useGravity}");

        EnableKinematic(true, interactor);

        if (interactor.transform.parent.CompareTag("Cable"))
        {
            DisableCollider(false, interactor);
        }
    }
    private void EnableKinematic(bool active, SnapInteractor interactor)
    {
        var snapInteractorParentRigidbody = interactor.transform.parent.gameObject.GetComponent<Rigidbody>();
        snapInteractorParentRigidbody.isKinematic = active;
        snapInteractorParentRigidbody.useGravity = !active;
        //Debug.Log($"{snapInteractorParentRigidbody.name} is kinematic active: {snapInteractorParentRigidbody.isKinematic}");
        //Debug.Log($"{snapInteractorParentRigidbody.name} use gravity active: {snapInteractorParentRigidbody.useGravity}");
    }

    private void DisableCollider(bool disable, SnapInteractor interactor)
    {
        var snapInteractorParent = interactor.transform.parent.gameObject;
        var snapInteractorChildPhysicsCollider = snapInteractorParent.transform.Find("PhysicsCollider").gameObject;
        snapInteractorChildPhysicsCollider.SetActive(disable);
        //Debug.Log($"Maus: Collider active {disable}");
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        //is kinematic has to be disabled again after removal; use gravity does not have to be truned on since that is handles by the hand grab component 
        var snapInteractorParentRigidbody = interactor.transform.parent.gameObject.GetComponent<Rigidbody>();
        snapInteractorParentRigidbody.isKinematic = false;

        if (interactor.transform.parent.CompareTag("Cable"))
        {
            DisableCollider(true, interactor);
        }
    }
}
