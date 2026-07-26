using Oculus.Interaction;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTriggerColliderDisable : MonoBehaviour
{
    Collider interactableCollider;
    SnapInteractable snapInteractable; 
 
    void Start()
    {
        snapInteractable = GetComponent<SnapInteractable>();
        interactableCollider = GetComponentInChildren<Collider>();
    }

    /* Enables/Disabled Trigger Collider of Interactable when a snap interactor is snapped into the interactable. 
     * This is done to avoid, that another snap interactor can use the trigger box of the interactable to snap to another object. 
     * Would the collider not be disabled, the collider could be utilized to snap an interactor to another random interactable. 
     */
    void Update()
    {
        if (snapInteractable.SelectingInteractorViews.Any())
        { // when there is a collision happening and the collider is active, it is disabled. 
            if(interactableCollider.enabled == true)
            {
                DisableInteractableCollider();
            }
        } else
        { // when there is no interaction happening, the collider gets reactivated. 
            if(interactableCollider.enabled == false)
            {
                EnableInteractableCollider();
            }
        }
    }

    private void DisableInteractableCollider()
    {
        interactableCollider.enabled = false;
    }

    private void EnableInteractableCollider()
    {
        interactableCollider.enabled = true; 
    }
}
