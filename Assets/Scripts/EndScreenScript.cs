using UnityEngine;
using TMPro;

public class EndScreenScript : MonoBehaviour
{
    public TextMeshProUGUI timeText;  // Reference to display the time
    private TimeManager timeManager;

    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();  // Find the TimeManager script

        // Load the player's time from PlayerPrefs and display it
        float timeSpent = PlayerPrefs.GetFloat("TimeSpent", 0f);
        timeText.text = $"Time: {timeSpent:F2} seconds";  // Show time with 2 decimal places

        // Save the time to the TimeManager
        timeManager.SaveTime(timeSpent);
    }

    public void RestartLevel()
    {
        // Restart the current level
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

    public void GoToMainMenu()
    {
        // Go to the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
