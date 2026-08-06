using Oculus.Haptics;
using Oculus.Interaction;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class AudioRightWrong : MonoBehaviour
{
    [SerializeReference] List<SnapInteractor> rightInteractor = new();
    SnapInteractable thisInteractable;

    bool hasPrinted;

    [SerializeField] private AudioClip correct; 
    [SerializeField] private AudioClip wrong;

    private AudioSource audioSource; 

    //public bool leftController; 
    //public bool rightController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();
        hasPrinted = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
            if (thisInteractable.SelectingInteractors.Any())
            {
                if (hasPrinted == false)
                {
                    if (rightInteractor.Contains(thisInteractable.SelectingInteractors.First())) { 
                        PlayCorrect();
                        //Debug.Log("Maus: right interactor");
                    }
                    else
                    {
                        PlayWrong();
                        //Debug.Log("Maus: wrong interactor");
                    }

                    hasPrinted = true;
                }
            } else
            {
                hasPrinted = false; 
            }
    }   

    private void PlayCorrect()
    {
        audioSource.PlayOneShot(correct);
    }

    private void PlayWrong()
    {
        audioSource.PlayOneShot(wrong);
    }
}
