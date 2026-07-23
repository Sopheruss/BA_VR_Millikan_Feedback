using Oculus.Interaction;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTriggerColliderDisable : MonoBehaviour
{
    Collider Collider;
    SnapInteractable snapInteractable;
    SnapInteractor snapInteractorObject;
    //int snapInteractorIdentifier; 
 
    void Start()
    {
       Collider = GetComponentInChildren<Collider>();
       snapInteractable = GetComponent<SnapInteractable>();
    }

    void Update()
    {
        DisableInteractableCollider(); 
    }

    private void DisableInteractableCollider()
    {
        if (snapInteractable.SelectingInteractorViews.Any())
        {
            Collider.enabled = false;
            //DisableInteractorCollision();
        } else
        {
            Collider.enabled = true;
        }
    }

    /*private void DisableInteractorCollision()
    {
        foreach (IInteractorView interactor in snapInteractable.SelectingInteractorViews)
        {
            if(interactor is SnapInteractor snapInteractor)
            {
                Debug.Log("Snap Interactor found: " + snapInteractor.name.ToString());
                snapInteractorObject = (SnapInteractor) interactor;
            }
        }
    }*/
}
