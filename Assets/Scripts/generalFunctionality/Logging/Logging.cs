using UnityEngine;
using Serilog;
using UnityEngine.SceneManagement;

public class Logging : MonoBehaviour
{
    public static Serilog.ILogger log;
    Scene scene; 
    string logName; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        logName = scene.name; 
        log = new LoggerConfiguration()
            .WriteTo.File($"Assets/StudyLogs/{logName}.txt", fileSizeLimitBytes: null)
            .CreateLogger();

        log.Information("LOG STARTED."); 
    }
}
