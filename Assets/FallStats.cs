using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallStats : MonoBehaviour
{
    public Text Fall;

    void Start()
    {
        int numFalls = PlayerPrefs.GetInt("numFalls");
        Fall.text = "Falls : " + numFalls;
    }
}