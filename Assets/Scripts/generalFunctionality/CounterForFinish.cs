using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public static class Counter
{
    private static int isDone; 

    public static void UpdateCounter(bool inc)
    {
        if (inc)
        {
            isDone++;
        }

        if (!inc)
        {
            isDone--;
        }
    }

    public static bool GameFinished()
    {
        if(isDone == 17) //there are 17 interactables that have to be snapped correctly 
        {
            return true;
        }

        return false;
    }
}
