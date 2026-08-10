using Oculus.Haptics;
using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    [SerializeField] private bool visual;
    [SerializeField] private bool auditiv;
    [SerializeField] private bool haptics;

    [SerializeField] private bool isCableHole;
    [SerializeField] private bool isAlwaysWrong; 

    [SerializeReference] List<SnapInteractor> rightInteractor = new();

    [SerializeField] SnapInteractable targetInteractable;

    SnapInteractable thisInteractable;

    // for haptics
    private HapticClipPlayer hapticClipPlayer;
    private HapticClip hapticRight; 
    private HapticClip hapticWrong;

    //for audio
    private AudioSource audioSource;
    private AudioClip audioRight; 
    private AudioClip audioWrong;

    //for visual
    Material[] initialMaterial;
    Renderer matRenderer;
    Material[] materials;
    Coroutine routine;
    private Material visualRight; 
    private Material visualWrong;

    bool correctInteractorAdded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();

        correctInteractorAdded = false; 

        if (auditiv)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialize = true;
            audioSource.spatialBlend = 1;

            audioRight = Resources.Load<AudioClip>("FeedbackStuff/Sound/dragon-studio-correct-472358");
            audioWrong = Resources.Load<AudioClip>("FeedbackStuff/Sound/freesound_community-wrong-47985");
        }

        if (haptics)
        {
            hapticRight = Resources.Load<HapticClip>("FeedbackStuff/Haptics/right");
            hapticWrong = Resources.Load<HapticClip>("FeedbackStuff/Haptics/wrong");

            hapticClipPlayer = new HapticClipPlayer(hapticRight);
        }

        if (visual)
        {
            visualRight = Resources.Load<Material>("FeedbackStuff/Visual/correctVisual");
            visualWrong = Resources.Load<Material>("FeedbackStuff/Visual/wrongVisual");
        }

        thisInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        thisInteractable.WhenSelectingInteractorRemoved.Action += OnInteractorRemoved; 
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        Logging.log.Information($"{interactor.name} got pluggin in.");

        if (isAlwaysWrong)
        {
            Wrong(interactor);
            return; 
        }

        bool isRightInteractor = IsRightInteractor(interactor);

        if (isRightInteractor)
        {
            Right(interactor); 
        } else 
        {
            Wrong(interactor); 
        }

    }

    private void Right(SnapInteractor interactor)
    {
        Logging.log.Information($"{interactor.name} added to CORRECT Interactable {thisInteractable.name}; Feedback given.");

        if (!interactor.transform.parent.CompareTag("Netzstecker")) 
        {
            //if a right element was aded, counter +1 
            // Netzstecker do not count into correct counter
            Counter.UpdateCounter(true);
            correctInteractorAdded = true;
        }

        if (visual)
        {
            initialMaterial = interactor.transform.parent.GetComponent<Renderer>().materials;
            CorrectVisual(interactor);
        }
        if (auditiv)
        {
            PlaySound(audioRight);
        }
        if (haptics)
        {
            PlayHaptics(hapticRight);
        }
    }

    private void Wrong(SnapInteractor interactor)
    {
        Logging.log.Information($"{interactor.name} added to WRONG Interactable {thisInteractable.name}. Feedback given.");

        if (visual)
        {
            initialMaterial = interactor.transform.parent.GetComponent<Renderer>().materials;
            WrongVisual(interactor);
        }
        if (auditiv)
        {
            PlaySound(audioWrong);
        }
        if (haptics)
        {
            PlayHaptics(hapticWrong);
        }
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        Logging.log.Information($"{interactor.name} got removed.");

        //Counter only has to be updated if a correct interactor is removed again 
        if(correctInteractorAdded == true)
        {
            Logging.log.Information($"{interactor.name} that was eigentlich correct got removed.");
            correctInteractorAdded = false; 
            Counter.UpdateCounter(false);
        }

        if (visual)
        {
            StopCoroutine(routine);
            matRenderer.materials = initialMaterial; 
        }

        if (auditiv)
        {
            audioSource.Stop();
        }

        if (haptics)
        {
            hapticClipPlayer.Stop(); 
        }

        // Is Interactor one of the allowed interactors
        if (!rightInteractor.Contains(interactor))
        {
            return;
        }

        // Are we a cable in a cable hole?
        if (isCableHole && interactor.transform.parent.CompareTag("Cable"))
        {
            var whereToGo = interactor.transform.parent.transform.parent.GetComponent<WhereToGo>();

            whereToGo.ChildrenPlugedIn--;

            for (int i = 0; i < whereToGo.targetPairs.Count; i++)
            {
                if (whereToGo.targetPairs[i].Item1 == interactor.Identifier)
                {
                    whereToGo.targetPairs.RemoveAt(i);
                    return;
                }
            }
        }
    }

    private bool IsRightInteractor(SnapInteractor interactor)
    {
        // Is Interactor one of the allowed interactors
        if (!rightInteractor.Contains(interactor))
        {
            //Debug.Log("Maus: Interactor not in list -> InCorrect.");
            return false;
        }

        if (isCableHole && interactor.transform.parent.CompareTag("Cable"))
        {

            // only reachable if interactor is cable 
            var whereToGo = interactor.transform.parent.transform.parent.GetComponent<WhereToGo>();

            whereToGo.targetPairs.Add((interactor.Identifier, targetInteractable));

            whereToGo.ChildrenPlugedIn++;

            if (whereToGo.ChildrenPlugedIn == 1)
            {
                //Debug.Log("Maus: First Child -> Correct.");
                return true;
            }

            //Debug.Log($"Maus: {whereToGo.ChildrenPlugedIn} - {whereToGo.targetPairs}");
            foreach ((int, SnapInteractable) tuple in whereToGo.targetPairs)
            {
                if (tuple.Item2 == thisInteractable)
                {
                    //Debug.Log("Maus: Second Child -> Correct.");
                    return true;
                }
            }

            //Debug.Log("Maus: Second Child -> InCorrect.");
            return false;
        }

        //Debug.Log("Maus: In List and Not cable in cable hole -> Correct.");
        return true;
    }

    private void CorrectVisual(SnapInteractor interactor)
    {
        routine = StartCoroutine(ChangeMaterial(visualRight, interactor)); 
    }

    private void WrongVisual(SnapInteractor interactor)
    {
        routine = StartCoroutine(ChangeMaterial(visualWrong, interactor)); 
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

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void PlayHaptics(HapticClip clip)
    {
        hapticClipPlayer.clip = clip;
        hapticClipPlayer.Play(Controller.Both);
    }
}
