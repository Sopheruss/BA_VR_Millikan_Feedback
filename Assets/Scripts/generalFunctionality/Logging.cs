using UnityEngine;
using Serilog; 

public class Logging : MonoBehaviour
{
    Serilog.ILogger logger; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logger = new LoggerConfiguration()
            .WriteTo.File("Assets/StudyLogs/testLog.txt")
            .CreateLogger();
    }
}
