using Oculus.Interaction;
using System.Linq;
using UnityEngine;

public class AllowedTags : MonoBehaviour, IGameObjectFilter
{
    [SerializeField] private string specificInteractorTag;

    public bool Filter(GameObject gameObject)
    {
        Debug.Log("Maus: Filter reached"); 
        return gameObject.CompareTag(specificInteractorTag);
    }
}
