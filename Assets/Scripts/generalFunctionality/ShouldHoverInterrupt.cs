using Oculus.Interaction;
using Oculus.Interaction.Editor.Generated;
using System.Linq;
using UnityEngine;

public class ShouldHoverInterrupt : MonoBehaviour
{
    private SnapInteractable snapInteractable;

    [SerializeField] private Collider interactableColliderAssigned;
    Vector3 initColliderPos;
    Vector3 newColliderPos; 

    void Start()
    {
        snapInteractable = GetComponent<SnapInteractable>();
        initColliderPos = interactableColliderAssigned.transform.position; 
    }

    /* Enables/Disabled Trigger Collider of Interactable when a snap interactor is snapped into the interactable. 
     * This is done to avoid, that another snap interactor can use the trigger box of the interactable to snap to another object. 
     * Would the collider not be disabled, the collider could be utilized to snap an interactor to another random interactable. 
     */
    void Update()
    {
        /*if (snapInteractable.SelectingInteractorViews.Any())
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
        }*/

        if (snapInteractable.SelectingInteractorViews.Any())
        { // when there is a collision happening and the collider is active, it is disabled. 
            if (interactableColliderAssigned.enabled == true)
            {
                Debug.Log("Maus: initPos: " + initColliderPos.ToString());
                DisableInteractableColliderA();
                newColliderPos = interactableColliderAssigned.transform.position;
                newColliderPos.y = -10; 
                Debug.Log("Maus: new Pos " +  newColliderPos.ToString());
            }
        }
        else
        { // when there is no interaction happening, the collider gets reactivated. 
            if (interactableColliderAssigned.enabled == false)
            {
                EnableInteractableColliderA();
                //snapInteractable.enabled = true;
            }
        }
    }

    /*private void DisableInteractableCollider()
    {
        interactableCollider.enabled = false;
    }

    private void EnableInteractableCollider()
    {
        interactableCollider.enabled = true; 
    } */

    private void DisableInteractableColliderA()
    {
        interactableColliderAssigned.enabled = false;
    }

    private void EnableInteractableColliderA()
    {
        interactableColliderAssigned.enabled = true;
    }
}
