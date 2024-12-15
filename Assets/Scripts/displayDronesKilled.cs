using UnityEngine;
using TMPro;  // Import TextMesh Pro namespace

public class DisplayDroneKillCount : MonoBehaviour
{
    public TextMeshProUGUI killCountText;  // Reference to the TextMesh Pro Text component
    public DroneTracker droneTracker;      // Reference to the DroneTracker script

    void Update()
    {
        // Get the current count of destroyed drones from DroneTracker
        int destroyedCount = droneTracker.GetDestroyedDronesCount();

        // Update the TextMesh Pro text
        killCountText.text = "Drones killed: " + destroyedCount + "/10";  // Display the count and max value
    }
}
