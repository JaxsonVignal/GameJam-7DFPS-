using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDuringReload : MonoBehaviour
{
    public PistolShoot pistolShoot;   // Reference to the PistolShoot script
    public float loweringSpeed = .03f;  // Speed at which the gun lowers
    public float loweredAmount = 3f;  // How much to lower the gun (adjust this based on how far you want to lower it)

    private Vector3 originalPosition;  // To store the original position of the gun
    public bool isReloading = false;  // To track if the reload state is active

    void Start()
    {
        // Get the PistolShoot component if not assigned
        if (pistolShoot == null)
        {
            pistolShoot = GetComponentInParent<PistolShoot>();
        }

        // Store the original position of the gun
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Check if the PistolShoot script is currently reloading
        if (pistolShoot != null && pistolShoot.isReloading)
        {
            // If reloading, lower the gun
            LowerGun();
        }
        else
        {
            // If not reloading, return the gun to its original position
            ReturnGunToOriginalPosition();
        }
    }

    private void LowerGun()
    {
        // Smoothly lower the gun by adjusting its localPosition
        float targetYPosition = originalPosition.y - loweredAmount;
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(originalPosition.x, targetYPosition, originalPosition.z), .02f);
    }

    private void ReturnGunToOriginalPosition()
    {
        // Smoothly return the gun to its original position when the reload is finished
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, .04f);
    }
}
