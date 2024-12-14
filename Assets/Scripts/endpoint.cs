using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    // This is the name of the scene to load when the player collides with the trigger
    public string nextSceneName;

    // This function is called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided with the trigger is the player
        if (other.CompareTag("Player"))
        {
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
}