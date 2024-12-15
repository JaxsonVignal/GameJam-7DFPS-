using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTracker : MonoBehaviour
{
    public int destroyedDronesCount = 0;  // Variable to track the number of destroyed drones

    // This method should be called whenever a drone is destroyed
    public void IncrementDestroyedDrones()
    {
        destroyedDronesCount++;
        Debug.Log("Drones destroyed: " + destroyedDronesCount);  // Optional: Print count to console
    }

    // Optional: You can expose this count to be used elsewhere in your game (e.g. UI)
    public int GetDestroyedDronesCount()
    {
        return destroyedDronesCount;
    }
}