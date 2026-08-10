using UnityEngine;

public static class Counter
{
    private static int isDone;

    public static void UpdateCounter(bool inc)
    {
        if (inc == true)
        {
            isDone++;
        }

        if (inc == false)
        {
            isDone--;
        }

        Debug.Log($"Maus Counter: {isDone}"); 
    }

    public static bool GameFinished()
    {
        if(isDone == 3) //there are 17 interactables that have to be snapped correctly, 19 when counting the netzstecker
        {
            return true;
        }

        return false;
    }
}
