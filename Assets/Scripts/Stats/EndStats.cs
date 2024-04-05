using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Justin Pearson
/// 
/// This script gets all the stats from playerprefs and puts them into the text for display on the end screen
/// </summary>
public class EndStats : MonoBehaviour
{
    public Text Stats;

    void Start()
    {
        float timeTaken = PlayerPrefs.GetFloat("TimeTaken");

        int numGems = PlayerPrefs.GetInt("numGems");

        int numJumps = PlayerPrefs.GetInt("numJumps");

        int numFalls = PlayerPrefs.GetInt("numFalls");

        Stats.text = "Time : " + FormatTime(timeTaken) +
                     "\nGems : " + numGems + " / 15" +
                     "\nJumps : " + numJumps +
                     "\nFalls : " + numFalls;        
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
