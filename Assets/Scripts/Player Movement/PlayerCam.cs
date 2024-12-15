using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camHolder;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Adjust initial camera rotation by 90 degrees
        yRotation = 90f;  // Set the starting rotation to 90 degrees on the Y axis
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);  // Apply the rotation to the camera holder
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);  // Apply the same to the orientation
    }

    private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Update rotations
        yRotation += mouseX;  // Rotate around the Y axis (left/right)
        xRotation -= mouseY;  // Rotate around the X axis (up/down)

        // Clamp the X rotation to prevent flipping upside down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the rotations
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void FOV(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void tilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
}

