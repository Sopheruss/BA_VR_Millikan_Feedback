using Oculus.Interaction;
using UnityEngine;

public class AlwaysWrongAudio : MonoBehaviour
{
    SnapInteractable thisInteractable;

    [SerializeField] private AudioClip wrong;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.thisInteractable = GetComponent<SnapInteractable>();
        audioSource = thisInteractable.GetComponent<AudioSource>();
        thisInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        PlayWrong(); 
    }

    private void PlayWrong()
    {
        audioSource.PlayOneShot(wrong);
    }
}
