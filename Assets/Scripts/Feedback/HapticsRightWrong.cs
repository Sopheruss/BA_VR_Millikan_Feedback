using Oculus.Haptics;
using Oculus.Interaction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RightOrWrongHaptics : MonoBehaviour
{
    [SerializeReference] List<SnapInteractor> rightInteractor = new();
    SnapInteractable thisInteractable;

    [SerializeField] private HapticClip correct; 
    [SerializeField] private HapticClip wrong;

    private HapticClipPlayer hapticClipPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();
        hapticClipPlayer = new HapticClipPlayer(correct);
        thisInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        thisInteractable.WhenSelectingInteractorRemoved.Action -= OnInteractorRemoved; 
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        if (!rightInteractor.Contains(interactor)) {
            PlayHaptics(wrong);
            return; 
        }

        PlayHaptics(correct); 
    }

    private void OnInteractorRemoved(SnapInteractor interactor)
    {
        hapticClipPlayer.Stop(); 
    }

    private void PlayHaptics(HapticClip clip)
    {
        hapticClipPlayer.clip = clip;
        hapticClipPlayer.Play(Controller.Both);
        Debug.Log("Correct haptic played");
    }
}
