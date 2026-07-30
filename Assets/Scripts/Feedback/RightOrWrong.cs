using Oculus.Interaction;
using System.Linq;
using UnityEngine;

public class RightOrWrong : MonoBehaviour
{
    [SerializeField] SnapInteractor rightInteractor;
    SnapInteractable thisInteractable;

    bool hasPrinted; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisInteractable = GetComponent<SnapInteractable>();
        hasPrinted = false; 
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
                        Debug.Log("Maus: right interactor");
                    }
                    else
                    {
                        Debug.Log("Maus: wrong interactor");
                    }

                    hasPrinted = true;
                }
            } else
            {
                hasPrinted = false; 
            }
    }
}
