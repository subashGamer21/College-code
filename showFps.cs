using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showFps : MonoBehaviour
{
    


    private float deltaTime = 0.0f;

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        int fps = Mathf.RoundToInt(1.0f / deltaTime);
        string text = "FPS: " + fps;
        GUI.Label(new Rect(10, 10, 100, 20), text);
    }
    private void Awake()
    {
        // Disable VSync
        QualitySettings.vSyncCount = 0;
        // Set the target frame rate to the maximum
        Application.targetFrameRate = -1;
    }
}


