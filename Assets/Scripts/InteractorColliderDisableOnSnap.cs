using Oculus.Interaction;
using System.Linq;
using UnityEngine;

public class InteractorColliderDisableOnSnap : MonoBehaviour
{
    SnapInteractable snapInteractable;
    SnapInteractor snapInteractorObject;
    GameObject snapInteractorParent;
    Collider objectCollider;
    bool disableCollider; 
    //int snapInteractorIdentifier;

    void Start()
    {
        snapInteractable = GetComponent<SnapInteractable>();
        disableCollider = false; 
    }

    void Update()
    {
        DisableInteractableCollider();
    }

    private void DisableInteractableCollider()
    {
        if (snapInteractable.SelectingInteractorViews.Any())
        {
            DisableInteractorCollision();
        } else
        {
            //objectCollider.enabled = true; 
        }
    }

    private void DisableInteractorCollision()
    {
        foreach (IInteractorView interactor in snapInteractable.SelectingInteractorViews)
        {
            if (interactor is SnapInteractor snapInteractor)
            {
                Debug.Log("Snap Interactor found: " + snapInteractor.name.ToString());
                snapInteractorObject = (SnapInteractor)interactor;
                disableCollider = true; 
            }
        }

        snapInteractorParent = snapInteractorObject.transform.parent.gameObject;

        objectCollider = snapInteractorParent.GetComponentInChildren<Collider>();

        objectCollider.enabled = false;
        Debug.Log("Parent of snapInteractor: " + snapInteractorParent.name.ToString());


    }
}
