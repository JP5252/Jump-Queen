using UnityEngine;
using UnityEngine.UI;

public class HeightCounter : MonoBehaviour
{
    public Text heightCounterText; 
    private float maxHeight = 0f; 

    void Update()
    {
        
        float currentHeight = transform.position.y;

        
        if (currentHeight > maxHeight)
        {
            maxHeight = currentHeight;
        }

        
        UpdateHeightCounter();
    }

    void UpdateHeightCounter()
    {
        if (heightCounterText != null)
        {
            heightCounterText.text = "Height: " + maxHeight.ToString("F1") + "m";
        }
    }
}