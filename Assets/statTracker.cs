using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statTracker : MonoBehaviour
{
    private float startTime = 0;

    private int numJumps = 0;

    private int numFalls = 0;

    private int numGems = 0;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    //to get the elapsed time for the end screen
    public float GetElapsedTime()
    {
        return Time.time - startTime;
    }

    //for adding a jump
    public void addJump()
    {
        numJumps++;
    }

    // getter for num jumps
    public int getNumJumps()
    {
        return numJumps;
    }

    //for adding a fall
    public void addFall()
    {
        numFalls++;
    }

    // getter for num falls
    public int getNumFalls()
    {
        return numFalls;
    }

    //for adding a gem
    public void addGem()
    {
        numGems++;
    }

    // getter for num gems
    public int getNumGems()
    {
        return numGems;
    }
}
