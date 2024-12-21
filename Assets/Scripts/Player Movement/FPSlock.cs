using UnityEngine;

public class FPSLock : MonoBehaviour
{
    public int targetFrameRate = 60; // Set your desired framerate (e.g., 60 FPS)

    void Start()
    {
        // Lock the frame rate to the desired value
        Application.targetFrameRate = targetFrameRate;
    }
}
