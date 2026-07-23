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

    // Update is called once per frame
    void Update()
    {
        CheckInteractorActivity();
    }

    private void CheckInteractorActivity()
    {
        if (snapInteractableCapacitor.SelectingInteractorViews.Any())
        {
            snapInteractableLid.enabled = true;
        }
        else
        {
            snapInteractableLid.enabled = false;
        }
    }
}
