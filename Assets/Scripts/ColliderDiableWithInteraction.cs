using Oculus.Interaction;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ColliderDisableWithInteraction : MonoBehaviour
{
    Collider Collider;
    SnapInteractable snapInteractable;
 
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
        } else
        {
            Collider.enabled = true;
        }
    }
}
