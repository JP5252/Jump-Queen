using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemStats : MonoBehaviour
{
    public Text Gem;

    void Start()
    {
        int numGems = PlayerPrefs.GetInt("numGems");
        Gem.text = "Gems : " + numGems + " / 15";
    }
}
