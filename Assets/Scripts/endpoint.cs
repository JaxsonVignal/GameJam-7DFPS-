using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    // This is the name of the scene to load when the player collides with the trigger
    public string nextSceneName;
    public DroneTracker dt;

    // Track the time spent in the level
    private float timeSpent = 0f;
    private bool levelCompleted = false; // Flag to check if level has been completed

    // This function is called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        int destroyedCount = dt.GetDestroyedDronesCount();

        // Check if the object that collided with the trigger is the player and if they have destroyed 10 drones
        if (other.CompareTag("Player") && destroyedCount == 10 && !levelCompleted)
        {
            // Stop the timer when level is completed
            levelCompleted = true;

            // Save the time the player took to finish the level
            PlayerPrefs.SetFloat("TimeSpent", timeSpent);
            PlayerPrefs.Save();

            // Check if the next scene name is provided and valid
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                // Load the next scene by name
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Next scene name is not set!");
            }
        }
    }

    // Update the timer as long as the level is not completed
    private void Update()
    {
        if (!levelCompleted)
        {
            timeSpent += Time.deltaTime;  // Increment the time spent in the level
        }
    }
}
