using UnityEngine;

public class ActivateCongrats : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    bool hasPrinted;

    private void Start()
    {
        hasPrinted = false; 
        canvas.SetActive(false); 
    }

    private void Update()
    {
        if (Counter.GameFinished() && hasPrinted == false)
        {
            Debug.Log("Maus: FINISHED!!");
            Logging.log.Information("APPLICATION FINISHED.");
            canvas.SetActive(true);
            hasPrinted = true; 
        }
        
    }
}
