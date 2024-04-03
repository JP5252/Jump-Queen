using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpStats : MonoBehaviour
{
    public Text Jump;

    void Start()
    {
        int numJumps = PlayerPrefs.GetInt("numJumps");
        Jump.text = "Jumps : " + numJumps;
    }
}
