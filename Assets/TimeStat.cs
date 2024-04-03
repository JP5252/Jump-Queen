using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeStat : MonoBehaviour
{
    public Text Time;

    void Start()
    {
        float timeTaken = PlayerPrefs.GetFloat("TimeTaken");
        Time.text = "Time : " + FormatTime(timeTaken);
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
