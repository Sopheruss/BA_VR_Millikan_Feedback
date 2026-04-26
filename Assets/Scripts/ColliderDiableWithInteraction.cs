using Oculus.Interaction;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ColliderDisableWithInteraction : MonoBehaviour
{
    BoxCollider Collider;
    SnapInteractable Interactable; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Collider = GetComponent<BoxCollider>();
       Interactable = GetComponent<SnapInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        // Disables the Collider of the Snap Interactable, so that no other Interactor can be added through the same collider
        // SelectingInteractors is a list of all Interactors currently connected to the Interactable 
        // Count checks wheter the list is empty, if it is not empty, meaning an interactor was added to the interactable, the collider is disabled 
        // Collider is activated if no Interactor is conntected to the interactable 

        if (Interactable.SelectingInteractors.Count > 0)
        {
            Collider.enabled = false;
        }
        else
        {
            Collider.enabled = true;
        }
    }
}
