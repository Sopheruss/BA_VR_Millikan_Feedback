using Oculus.Interaction;
using System.Linq;
using UnityEngine;

/* Disables the child game object, which is responsable for physics collision */
public class InteractorColliderDisableOnSnap : MonoBehaviour
{
    SnapInteractable snapInteractable;
    SnapInteractor snapInteractorObject;
    GameObject snapInteractorParent;
    GameObject snapInteractorChild;
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
        } else if (colliderObjectActive == false)
        {
            // Child Object that is responsible for physics collision is reactivated 
            snapInteractorChild.SetActive(true); 
            colliderObjectActive = true;
        }
    }

    private void DisableInteractorCollision()
    {
        // accesses the snap interactor currently interacting with the interactable 
        foreach (IInteractorView interactor in snapInteractable.SelectingInteractorViews)
        {
            if (interactor is SnapInteractor snapInteractor)
            {
                snapInteractorObject = (SnapInteractor) interactor;
            }
        }

        // gets the parent game object of the snap interactor
        snapInteractorParent = snapInteractorObject.transform.parent.gameObject;

        // accesses the child object with the name PhysicsCollider and disables it
        snapInteractorChild = snapInteractorParent.transform.Find("PhysicsCollider").gameObject;
        snapInteractorChild.SetActive(false);
        colliderObjectActive = false; 
    }
}
