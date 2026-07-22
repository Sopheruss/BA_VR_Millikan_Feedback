using Oculus.Interaction;
using System.Linq;
using UnityEngine;

public class IsSnapped : MonoBehaviour
{
    private SnapInteractable snapInteractable;

    public void Start()
    {
        snapInteractable = GetComponent<SnapInteractable>();
    }

    public void Update()
    {
        GetSelectingInteractorID();
    }

    public void GetSelectingInteractorID()
    {
        if (snapInteractable.SelectingInteractorViews.Any())
        {
            printID();
        }
    }

    public void printID()
    {
        foreach (IInteractorView interactorView in snapInteractable.SelectingInteractorViews)
        {
            Debug.Log("Interactor ID: " + interactorView.Identifier.ToString());
        }
    }
}
