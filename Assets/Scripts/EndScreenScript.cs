using UnityEngine;
using TMPro;  // Make sure to include the TextMesh Pro namespace

public class EndScreenScript : MonoBehaviour
{
    public TextMeshProUGUI timeText;  // Reference to the TextMesh Pro Text component that will display the time

    void Start()
    {
        // Load the saved time from PlayerPrefs and display it
        float timeSpent = PlayerPrefs.GetFloat("TimeSpent", 0f);  // Default value is 0 if no time is saved
        timeText.text = "Time: " + timeSpent.ToString("F2") + " seconds";  // Show time with 2 decimal places
    }

    public void RestartLevel()
    {
        // Restart the current level (replace "CurrentSceneName" with the actual scene name)
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

    public void GoToMainMenu()
    {
        // Load the main menu scene (replace "MainMenu" with the actual scene name)
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}

