using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCable : MonoBehaviour
{
    [SerializeReference] List<SnapInteractor> rightInteractor = new();
    SnapInteractable thisInteractable;

    [SerializeField] SnapInteractable targetInteractable;

    Material[] initialMaterial;
    Renderer matRenderer;
    Material[] materials;
    Coroutine routine;

    [SerializeField] Material right; 
    [SerializeField] Material wrong;


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

        initialMaterial = interactor.transform.parent.GetComponent<Renderer>().materials;

        if (!rightInteractor.Contains(interactor))
        {
            Debug.Log("Maus: Wrong Interactor");
            routine = StartCoroutine(ChangeMaterial(wrong, interactor));
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
            routine = StartCoroutine(ChangeMaterial(right, interactor));
        }
        else
        {
            if (thisInteractable != interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target)
            {
                Debug.Log("Maus: Wrong Second Interactor");
                routine = StartCoroutine(ChangeMaterial(wrong, interactor));
                return;
            }

            Debug.Log("Maus: Right Second Interactor");
            routine = StartCoroutine(ChangeMaterial(right, interactor));
        }        
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        Debug.Log($"Maus: Interacor removed: {interactor.name}");

        if (!rightInteractor.Contains(interactor))
        {
            Debug.Log("Maus: Wrong Interactor removed!");
            StopCoroutine(routine);
            return;
        }

        StopCoroutine(routine);

        interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn--;

        if(interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn == 0)
        {
            interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target = null;
        }

        Debug.Log($"Maus: Parent Stuff: {interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target} und {interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn}");

    }

    private IEnumerator ChangeMaterial(Material changeToMaterial, SnapInteractor interactor)
    {
        matRenderer = interactor.transform.parent.GetComponent<Renderer>();
        materials = matRenderer.materials;

        System.Array.Fill(materials, changeToMaterial);
        matRenderer.materials = materials; // replaces all of the material to green materials 

        yield return new WaitForSeconds(0.5f);

        matRenderer.materials = initialMaterial; // changes materials back to initial materials 
    }
}
