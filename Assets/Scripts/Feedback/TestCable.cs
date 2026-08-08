using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class TestCable : MonoBehaviour
{
    [SerializeReference] List<SnapInteractor> rightInteractor = new();
    SnapInteractable thisInteractable;

    [SerializeField] SnapInteractable targetInteractable; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();
        thisInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        thisInteractable.WhenSelectingInteractorRemoved.Action += OnInteractorRemoved;
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        Debug.Log($"Maus: Interactor added: {interactor.name}");

        if (!rightInteractor.Contains(interactor))
        {
            Debug.Log("Maus: Wrong Interactor");
            return;
        }

        interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn++;

        if (interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target == null) {
            interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target = targetInteractable;
        }

        Debug.Log($"Maus: Parent Stuff: {interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target.name} und {interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn}");

        if (interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn == 1)
        {
            Debug.Log("Maus: Right First Interactor");
        }
        else
        {
            if (thisInteractable != interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target)
            {
                Debug.Log("Maus: Wrong Second Interactor");
                return;
            }

            Debug.Log("Maus: Right Second Interactor");
        }        
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        Debug.Log($"Maus: Interacor removed: {interactor.name}");

        if (!rightInteractor.Contains(interactor))
        {
            Debug.Log("Maus: Wrong Interactor removed!");
            return;
        }

        interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn--;

        if(interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn == 0)
        {
            interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target = null;
        }

        Debug.Log($"Maus: Parent Stuff: {interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target} und {interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn}");

    }
}
