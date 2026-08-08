using Oculus.Interaction;
using Oculus.Haptics; 
using System.Collections.Generic;
using UnityEngine;

public class HapticCable : MonoBehaviour
{
    [SerializeReference] List<SnapInteractor> rightInteractor = new();
    SnapInteractable thisInteractable;

    [SerializeField] SnapInteractable targetInteractable;

    [SerializeField] HapticClip right; 
    [SerializeField] HapticClip wrong;

    private HapticClipPlayer hapticClipPlayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();
        hapticClipPlayer = new HapticClipPlayer(right);
        thisInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        thisInteractable.WhenSelectingInteractorRemoved.Action += OnInteractorRemoved;
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        Debug.Log($"Maus: Interactor added: {interactor.name}");

        if (!rightInteractor.Contains(interactor))
        {
            Debug.Log("Maus: Wrong Interactor");
            PlayHaptics(wrong); 
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
            PlayHaptics(right);
        }
        else
        {
            if (thisInteractable != interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target)
            {
                Debug.Log("Maus: Wrong Second Interactor");
                PlayHaptics(wrong); 
                return;
            }

            Debug.Log("Maus: Right Second Interactor");
            PlayHaptics(right);
        }        
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        Debug.Log($"Maus: Interacor removed: {interactor.name}");

        if (!rightInteractor.Contains(interactor))
        {
            Debug.Log("Maus: Wrong Interactor removed!");
            hapticClipPlayer.Stop();
            return;
        }

        hapticClipPlayer.Stop();

        interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn--;

        if(interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn == 0)
        {
            interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target = null;
        }

        Debug.Log($"Maus: Parent Stuff: {interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().target} und {interactor.transform.parent.transform.parent.GetComponent<WhereToGo>().ChildrenPlugedIn}");

    }

    private void PlayHaptics(HapticClip clip)
    {
        hapticClipPlayer.clip = clip;
        hapticClipPlayer.Play(Controller.Both);
        Debug.Log("Correct haptic played");
    }
}
