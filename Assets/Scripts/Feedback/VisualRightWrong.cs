using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualRightOrWrong : MonoBehaviour
{
    [SerializeReference] List<SnapInteractor> rightInteractor = new();
    SnapInteractable thisInteractable;

    [SerializeField] Material correctMaterial;
    [SerializeField] Material wrongMaterial;

    Material[] initialMaterial;

    Renderer matRenderer;
    Material[] materials; 

    Coroutine routine;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();
        thisInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        thisInteractable.WhenSelectingInteractorRemoved.Action -= OnInteractorRemoved;  
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        initialMaterial = interactor.transform.parent.GetComponent<Renderer>().materials;

        if (!rightInteractor.Contains(interactor))
        {
            routine = StartCoroutine(ChangeMaterial(wrongMaterial, interactor)); 
            return;
        }

        routine = StartCoroutine(ChangeMaterial(correctMaterial, interactor));
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        StopCoroutine(routine);
        matRenderer.materials = initialMaterial;
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
