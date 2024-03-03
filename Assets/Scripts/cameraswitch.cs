using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraswitch : MonoBehaviour
{
    public Camera associatedCamera; // Reference to the camera associated with this zone

    // Called when another collider enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player entered camera zone: " + gameObject.name);
        if (other.CompareTag("Player"))
        {
            // Disable all cameras
            Camera[] allCameras = Camera.allCameras;
            foreach (Camera cam in allCameras)
            {
                cam.enabled = false;
            }

            // Enable the associated camera
            associatedCamera.enabled = true;
        }
    }
}
