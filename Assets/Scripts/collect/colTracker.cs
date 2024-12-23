using UnityEngine;
using TMPro;

public class ItemTracker : MonoBehaviour
{
    public int collectedItemCount = 0;  // Keeps track of how many items the player has collected
    public TextMeshProUGUI itemCountText;  // Reference to a TextMeshPro UI element to display the count

    void OnEnable()
    {
        // Subscribe to the item collected event
        CollectibleItem.OnItemCollected += IncrementItemCount;
    }

    void OnDisable()
    {
        // Unsubscribe from the event when this object is disabled
        CollectibleItem.OnItemCollected -= IncrementItemCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the text starts as "Golden Asad's: 0/5" when none have been collected
        UpdateItemCountUI();
    }

    // Method to increment the collected item count
    private void IncrementItemCount()
    {
        collectedItemCount++;
        UpdateItemCountUI();  // Update the UI text after collection
    }

    // Update the UI Text to display the current item count
    private void UpdateItemCountUI()
    {
        if (itemCountText != null)
        {
            itemCountText.text = "Golden Asad's: " + collectedItemCount.ToString() + "/5";
        }
    }

    // Call this method to save the collected item count to PlayerPrefs
    public void SaveCollectedItemCount()
    {
        PlayerPrefs.SetInt("CollectedItems", collectedItemCount);
        PlayerPrefs.Save();
    }
}
