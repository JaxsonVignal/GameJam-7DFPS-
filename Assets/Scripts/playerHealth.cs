using UnityEngine;
using UnityEngine.SceneManagement;  // To reload the scene
using TMPro;  // Import TextMeshPro namespace

public class PlayerHealthWithUI : MonoBehaviour
{
    public int health = 30;  // Initial health value
    public int damageAmount = 10;  // Damage to be taken on bullet collision
    public TextMeshProUGUI healthText;  // Reference to the TextMeshPro UI element

    void Start()
    {
        // Initialize player health at the start
        health = 30;
        UpdateHealthUI();  // Update the UI text at the start
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug: Log the name of the object collided with
        Debug.Log("Collided with: " + other.gameObject.name);

        // Check if the object collided with is tagged as "Bullet"
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Bullet hit detected!");

            // Decrease health by the damage amount
            health -= damageAmount;

            // If health reaches 0, reload the scene
            if (health <= 0)
            {
                Debug.Log("Player health is 0. Reloading scene...");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            UpdateHealthUI();  // Update the UI text after taking damage
        }
    }

    // Method to update the health text on the UI
    void UpdateHealthUI()
    {
        healthText.text = "Health: " + health.ToString();  // Set the text to display current health
    }
}


