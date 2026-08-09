using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereToGo : MonoBehaviour
{
    // public SnapInteractable target;
    public int ChildrenPlugedIn;
    public List<(int, SnapInteractable)> targetPairs = new(); // (kabel_start, loch5), (kabel_end, loch3)
}
