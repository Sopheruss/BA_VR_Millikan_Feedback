using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysWrongVisual : MonoBehaviour
{
    SnapInteractable thisInteractable;

    [SerializeField] Material wrongMaterial;

    Material[] initialMaterial;

    Renderer matRenderer; 
    Material[] materials;

    Coroutine makeRedRoutine; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.thisInteractable = GetComponent<SnapInteractable>();
        thisInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        thisInteractable.WhenSelectingInteractorAdded.Action -= OnInteractorRemoved;
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        initialMaterial = interactor.transform.parent.GetComponent<Renderer>().materials;

        makeRedRoutine = StartCoroutine(ChangeMaterial(wrongMaterial, interactor));
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        StopCoroutine(makeRedRoutine);
        matRenderer.materials = initialMaterial;
    }

    private IEnumerator ChangeMaterial(Material changeToMaterial, SnapInteractor interactor)
    {
        matRenderer = interactor.transform.parent.GetComponent<Renderer>();
        materials = matRenderer.materials;

        System.Array.Fill(materials, changeToMaterial);
        matRenderer.materials = materials; // replaces all of the material to wrong Material

        yield return new WaitForSeconds(0.5f);

        matRenderer.materials = initialMaterial; // changes materials back to initial materials 
    }
}
