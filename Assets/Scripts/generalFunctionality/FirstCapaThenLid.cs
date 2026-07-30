using Oculus.Interaction;
using System.Linq;
using UnityEngine;

public class FirstCapaThenLid : MonoBehaviour
{
    [SerializeField ] private SnapInteractable snapInteractableLid;
    SnapInteractable snapInteractableCapacitor;

    void Start()
    {
        snapInteractableCapacitor = GetComponent<SnapInteractable>();
    }
    void Update()
    {
        CheckInteractorActivity();
    }

    private void CheckInteractorActivity()
    {
        if (snapInteractableCapacitor.SelectingInteractorViews.Any())
        {
            if(snapInteractableLid.enabled == false)
            {
                snapInteractableLid.enabled = true;
            }
        }
        else
        {
            if(snapInteractableLid.enabled == true)
            {
                snapInteractableLid.enabled = false;
            }
        }
    }
}
