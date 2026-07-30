using Oculus.Interaction;
using System.Linq;
using UnityEngine;

/* Disables the child game object, which is responsable for physics collision */
public class InteractorColliderDisableOnSnap : MonoBehaviour
{
    SnapInteractable snapInteractable;
    SnapInteractor snapInteractorObject;
    GameObject snapInteractorParent;
    GameObject snapInteractorChildPhysicsCollider;
    bool colliderObjectActive; 

    void Start()
    {
        snapInteractable = GetComponent<SnapInteractable>();
        colliderObjectActive = true; 
    }

    void Update()
    {
        DisableInteractableCollider();
    }

    /* Checks whether a snap interactable has a interactor. 
       When there is an interaction happening and the collider is active, it will be disabled. 
       When there is no interaction happening, but the collider is still disabled, it will be reactivated. 
    */
    private void DisableInteractableCollider()
    {
        if (snapInteractable.SelectingInteractorViews.Any())
        {
            if (colliderObjectActive == true)
            {
                DisableInteractorCollision();
            }
        } else
        {
            //name is misleading and not representation for the actual state of the collision, since it is also deactivated when grabbing is active 
            //this bool is just for making sure the script logic is still working 
            if (colliderObjectActive == false)
            {
                colliderObjectActive = true;
            }      
        }
    }

    private void DisableInteractorCollision()
    {
        // accesses the snap interactor currently interacting with the interactable 
        foreach (SnapInteractor interactor in snapInteractable.SelectingInteractors)
        {
            snapInteractorObject = (SnapInteractor)interactor;
        }

        // gets the parent game object of the snap interactor
        snapInteractorParent = snapInteractorObject.transform.parent.gameObject;

        // accesses the child object with the name PhysicsCollider and disables it
        snapInteractorChildPhysicsCollider = snapInteractorParent.transform.Find("PhysicsCollider").gameObject;
        snapInteractorChildPhysicsCollider.SetActive(false);
        colliderObjectActive = false; 
    }
}
