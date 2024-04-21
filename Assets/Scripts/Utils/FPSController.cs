using UnityEngine;
using UnityEngine.UI;

public class FPSCheckerScript : MonoBehaviour
{
    [SerializeField] private Text fpsText;
    private float deltaTime = 0.0f;
    private int frames = 0;
    private float currentFPS = 0.0f;

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        DisplayFPS();
    }

    void DisplayFPS() {
        frames++;
        deltaTime += Time.deltaTime;

        if (deltaTime >= 1.0f)
        {
            currentFPS = frames / deltaTime;
            fpsText.text = currentFPS.ToString("F1") + " FPS";

            frames = 0;
            deltaTime = 0.0f;
        }
    }
}
