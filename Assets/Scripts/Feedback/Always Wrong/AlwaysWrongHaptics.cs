using Oculus.Haptics;
using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysWrongHaptics : MonoBehaviour
{
    SnapInteractable thisInteractable;

    [SerializeField] private HapticClip wrong;

    private HapticClipPlayer hapticClipPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.thisInteractable = GetComponent<SnapInteractable>();
        hapticClipPlayer = new HapticClipPlayer(wrong);
        thisInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
    }

    private void OnInteractorAdded(SnapInteractor interactor)
    {
        PlayWrong(); 
    }

    public void PlayWrong()
    {
        hapticClipPlayer.clip = wrong;
        hapticClipPlayer.Play(Controller.Both);
        //Debug.Log("Correct haptic played");
    }
}
