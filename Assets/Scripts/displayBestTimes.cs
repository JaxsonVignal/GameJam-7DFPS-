using UnityEngine;
using TMPro;  // Make sure to include the TextMeshPro namespace

public class DisplayBestTimes : MonoBehaviour
{
    public TextMeshProUGUI[] timeTexts;  // Array to hold references to the TextMeshPro UI elements

    private TimeManager timeManager;

    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();  // Find the TimeManager script in the scene

        // Load the best times and display them
        DisplayTimes();
    }

    // Display the top 3 times on the UI
    private void DisplayTimes()
    {
        for (int i = 0; i < timeManager.bestTimes.Count; i++)
        {
            timeTexts[i].text = $"#{i + 1}: {timeManager.bestTimes[i]:F2} seconds";  // Format and display the times
        }
    }
}
