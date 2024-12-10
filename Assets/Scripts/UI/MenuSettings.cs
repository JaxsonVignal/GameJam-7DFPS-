using UnityEngine;
using UnityEngine.SceneManagement;  // To load scenes
using UnityEngine.UI;  // To interact with UI elements

public class MenuSettings : MonoBehaviour
{
    
    public Button mutesoundsButton;
    public Button backButton;
    public Slider volumeSlider;
    public AudioSource audioSource;
    public GameObject settingsMenu;
    public GameObject mainMenu;
 
   
   
    public void muteSounds()
    {

    }

    // Method to show options (for now it just prints a message)
    public void mainBack()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    // Method to quit the game
    public void setVolume(float volume)
    {
        audioSource.volume = volume;
    }

    // Method to initialize the buttons (called when the script starts)
    void Start()
    {
        // Set button listeners if they're assigned in the inspector
        if (mutesoundsButton != null) mutesoundsButton.onClick.AddListener(muteSounds);
        if (backButton != null) backButton.onClick.AddListener(mainBack);

        volumeSlider.value = audioSource.volume;

        volumeSlider.onValueChanged.AddListener(setVolume);
    }
}