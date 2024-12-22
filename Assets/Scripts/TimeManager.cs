using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public List<float> bestTimes = new List<float>();  // List to store best times

    private void Start()
    {
        LoadTimes();  // Load the times when the game starts
    }

    // Save the player's time
    public void SaveTime(float time)
    {
        bestTimes.Add(time);  // Add the time to the list
        bestTimes.Sort();  // Sort the list in ascending order
        if (bestTimes.Count > 3)  // Keep only the top 3 times
        {
            bestTimes.RemoveAt(bestTimes.Count - 1);
        }

        // Save the best times to a file
        string filePath = Path.Combine(Application.persistentDataPath, "bestTimes.json");
        File.WriteAllText(filePath, JsonUtility.ToJson(new TimesData(bestTimes)));
    }

    // Load the best times from a file
    public void LoadTimes()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "bestTimes.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            TimesData data = JsonUtility.FromJson<TimesData>(json);
            bestTimes = data.times;
        }
    }

    // Class for saving the times in JSON format
    [System.Serializable]
    private class TimesData
    {
        public List<float> times;

        public TimesData(List<float> bestTimes)
        {
            times = bestTimes;
        }
    }
}
