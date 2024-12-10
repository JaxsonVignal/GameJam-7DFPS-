using UnityEngine;
using UnityEngine.SceneManagement;  // To load scenes
using UnityEngine.UI;  // To interact with UI elements

public class MainMenu : MonoBehaviour
{
    // Reference to buttons (optional if you prefer dragging directly in inspector)
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public Button creditsButton;
    public GameObject mainMenu;
    public GameObject optionsMenu;

    // Method to start the game
    public void PlayGame()
    {
        // Loads the scene with index 1 (assuming the main game scene is at index 1 in the Build Settings)
        SceneManager.LoadScene("HouseRunner1");  // Change to your game's scene index or name
    }

    // Method to show options (for now it just prints a message)
    public void ShowOptions()
    {
        Debug.Log("Options button clicked!");

        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        
    }

    public void ShowCredits()
    {
        Debug.Log("Credits Button clicked!");
    }
      

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();

        // For Unity editor, play mode must be stopped manually:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Method to initialize the buttons (called when the script starts)
    void Start()
    {
        // Set button listeners if they're assigned in the inspector
        if (playButton != null) playButton.onClick.AddListener(PlayGame);
        if (optionsButton != null) optionsButton.onClick.AddListener(ShowOptions);
        if (quitButton != null) quitButton.onClick.AddListener(QuitGame);
        if (creditsButton !=null) creditsButton.onClick.AddListener(ShowCredits);
    }
}