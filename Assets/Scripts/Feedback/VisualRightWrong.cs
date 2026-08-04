using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualRightOrWrong : MonoBehaviour
{
    [SerializeField] SnapInteractor rightInteractor;
    SnapInteractable thisInteractable;

    SnapInteractor interactingInteractor; 

    [SerializeField] Material correctMaterial;
    [SerializeField] Material wrongMaterial;

    Material[] initialMaterial; 

    Coroutine makeGreenRoutine;
    Coroutine makeRedRoutine; 

    bool hasPrinted; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();
        hasPrinted = false; 
    }

    // Update is called once per frame
    void Update()
    {
            if (thisInteractable.SelectingInteractors.Any())
            {
                if (hasPrinted == false)
                {
                    // get initial Material of the current interactor 
                    foreach (SnapInteractor interactor in thisInteractable.SelectingInteractors)
                    {
                        interactingInteractor = interactor;
                    }

                    initialMaterial = interactingInteractor.transform.parent.GetComponent<Renderer>().materials; 

                    // if the interactor is the right interactor, the color is changed to green; if not the color is changes to red
                    if (thisInteractable.SelectingInteractors.Contains(rightInteractor))
                    {
                        makeGreenRoutine = StartCoroutine(ChangeMaterial(correctMaterial));  
                    }
                    else
                    {
                        makeRedRoutine = StartCoroutine(ChangeMaterial(wrongMaterial));
                    }

                    hasPrinted = true;
                }
            } else
            {
                hasPrinted = false; 
            }
    }

    private IEnumerator ChangeMaterial(Material changeToMaterial)
    {
        Renderer renderer = interactingInteractor.transform.parent.GetComponent<Renderer>();
        Material[] materials = renderer.materials;

        System.Array.Fill(materials, changeToMaterial);
        renderer.materials = materials; // replaces all of the material to green materials 

        yield return new WaitForSeconds(0.5f);

        renderer.materials = initialMaterial; // changes materials back to initial materials 
    }
}
