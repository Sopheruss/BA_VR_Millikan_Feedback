using Oculus.Haptics;
using Oculus.Interaction;
using System.Linq;
using UnityEngine;

public class RightOrWrongHaptics : MonoBehaviour
{
    [SerializeField] SnapInteractor rightInteractor;
    SnapInteractable thisInteractable;

    bool hasPrinted;

    [SerializeField] private HapticClip correct; 
    [SerializeField] private HapticClip wrong;

    private HapticClipPlayer hapticClipPlayer;

    //public bool leftController; 
    //public bool rightController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();
        hasPrinted = false;

        hapticClipPlayer = new HapticClipPlayer(correct); 
    }

    // Update is called once per frame
    void Update()
    {
            if (thisInteractable.SelectingInteractors.Any())
            {
                if (hasPrinted == false)
                {
                    if (thisInteractable.SelectingInteractors.Contains(rightInteractor))
                    {
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

    public void PlayCorrect()
    {
        hapticClipPlayer.clip = correct;
        hapticClipPlayer.Play(Controller.Both);
        //Debug.Log("Correct haptic played"); 
    }

    public void PlayWrong()
    {
        hapticClipPlayer.clip = wrong;
        hapticClipPlayer.Play(Controller.Both);
        //Debug.Log("Correct haptic played");
    }
}
