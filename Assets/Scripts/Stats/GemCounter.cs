using UnityEngine;
using UnityEngine.UI;

public class GemCounter : MonoBehaviour
{
    public Text gemCounterText; // Reference to the UI Text element for displaying gem count
    public statTracker StatTracker; // added by Justin so we use the stats tracker for the display

    void Start()
    {
        UpdateGemCounter(); // Call this method to update the UI Text element with the initial gem count
    }

    void UpdateGemCounter()
    {
        gemCounterText.text = "Gems: " + StatTracker.getNumGems();  //Update UI Text element with the gem count from the gems script
    }
}