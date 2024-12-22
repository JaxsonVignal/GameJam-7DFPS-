using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    // Event or callback to notify when the item is collected
    public delegate void ItemCollected();
    public static event ItemCollected OnItemCollected;

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has collided with the collectible item
        if (other.CompareTag("Player"))
        {
            // If the player collides with this object, destroy it
            Destroy(gameObject);

            // Notify the listener that an item was collected
            OnItemCollected?.Invoke();
        }
    }
}
