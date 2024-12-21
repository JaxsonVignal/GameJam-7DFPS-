using UnityEngine;
using TMPro;  // Reference to TextMeshPro namespace

public class AmmoDisplay : MonoBehaviour
{
    public PistolShoot pistolShoot;       // Reference to the PistolShoot script
    public TextMeshProUGUI ammoText;      // Reference to the TMP text component

    void Update()
    {
        // Update the ammo display each frame
        UpdateAmmoDisplay();
    }

    void UpdateAmmoDisplay()
    {
        // Display the current shot count and max shots in the format "Current/Max"
        ammoText.text = pistolShoot.shotCount + "/" + pistolShoot.maxShots;
    }
}

