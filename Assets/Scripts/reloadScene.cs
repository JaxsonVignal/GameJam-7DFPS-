using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Needed to reload the scene

public class KillBoxTrigger : MonoBehaviour
{
    // Trigger the scene reload when the player enters the kill box
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the kill box is the player
        if (other.CompareTag("Player"))  // Ensure the player object has the "Player" tag
        {
            ReloadScene();
        }
    }

    // Function to reload the current scene
    void ReloadScene()
    {
        // Get the name of the current scene
        string currentScene = SceneManager.GetActiveScene().name;

        // Reload the scene
        SceneManager.LoadScene(currentScene);
    }
}