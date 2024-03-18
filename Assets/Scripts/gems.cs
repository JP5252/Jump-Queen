using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gems : MonoBehaviour
{
    public Text gemCounterText; // Reference to the UI Text element for displaying gem count
    private int gemCount = 0;   // Counter for tracking gem count

    // Called when another collider enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Increment gem count
            gemCount++;
            UpdateGemCounter();

            // Destroy the gem GameObject
            Destroy(gameObject);
        }
    }

    // Update the gem counter UI Text
    private void UpdateGemCounter()
    {
        if (gemCounterText != null)
        {
            gemCounterText.text = "Gems: " + gemCount;
        }
    }

}
