using Oculus.Interaction;
using System.Linq;
using UnityEngine;

public class FirstCapaThenLid : MonoBehaviour
{
    [SerializeField] private Collider snapInteractableLidCollider;
    SnapInteractable snapInteractableCapacitor; 

    void Start()
    {
        snapInteractableCapacitor = GetComponent<SnapInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckColliderActivity();
    }

    private void CheckColliderActivity()
    {
        if (snapInteractableCapacitor.SelectingInteractorViews.Any())
        {
            snapInteractableLidCollider.enabled = true;
        }
        else
        {
            snapInteractableLidCollider.enabled = false;
        }
    }
}
