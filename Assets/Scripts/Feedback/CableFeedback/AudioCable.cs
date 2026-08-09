using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class AudioCable : MonoBehaviour
{
    [SerializeReference] List<SnapInteractor> rightInteractor = new();
    SnapInteractable thisInteractable;

    [SerializeField] SnapInteractable targetInteractable;

    [SerializeField] AudioClip right; 
    [SerializeField] AudioClip wrong;

    private AudioSource audioSource; 
    /*

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();
        audioSource = GetComponent<AudioSource>();
        thisInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        thisInteractable.WhenSelectingInteractorRemoved.Action += OnInteractorRemoved;
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        Debug.Log($"Maus: Interactor added: {interactor.name}");

        if (!rightInteractor.Contains(interactor))
        {
            Debug.Log("Maus: Wrong Interactor");
            PlaySound(wrong); 
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
            PlaySound(right);
        }
        else
        {
            if (thisInteractable != interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target)
            {
                Debug.Log("Maus: Wrong Second Interactor");
                PlaySound(wrong); 
                return;
            }

            Debug.Log("Maus: Right Second Interactor");
            PlaySound(right);
        }        
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        Debug.Log($"Maus: Interacor removed: {interactor.name}");

        if (!rightInteractor.Contains(interactor))
        {
            Debug.Log("Maus: Wrong Interactor removed!");
            audioSource.Stop();
            return;
        }

        audioSource.Stop();

        interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn--;

        if(interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn == 0)
        {
            interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target = null;
        }

        Debug.Log($"Maus: Parent Stuff: {interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target} und {interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn}");

    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    */
}
