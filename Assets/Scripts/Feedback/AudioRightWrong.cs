using Oculus.Haptics;
using Oculus.Interaction;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class AudioRightWrong : MonoBehaviour
{
    [SerializeReference] List<SnapInteractor> rightInteractor = new();
    SnapInteractable thisInteractable;

    [SerializeField] private AudioClip correct; 
    [SerializeField] private AudioClip wrong;

    private AudioSource audioSource; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();
        audioSource = GetComponent<AudioSource>();
        thisInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        thisInteractable.WhenSelectingInteractorRemoved.Action -= OnInteractorRemoved; 
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        if (!rightInteractor.Contains(interactor))
        {
            PlaySound(wrong);
            Debug.Log("Maus: Wrong interactor");
            return;
        }

        PlaySound(correct); 
        Debug.Log("Maus: correct interactor"); 
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        audioSource.Stop();
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip); 
    }
}
