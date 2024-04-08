using UnityEngine;

/// <summary>
/// Author: Justin Pearson
/// 
/// This script keeps track of all the stats of the game with getter and setter methods for each stat
/// this script also includes a reset method to reset the stats at the end of the game.
/// </summary>
public class statTracker : MonoBehaviour
{
    private float startTime;

    private int numJumps;

    private int numFalls;

    private int numGems;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        numJumps = 0;
        numFalls = 0;
        numGems = 0;
    }

    //to get the elapsed time for the end screen
    public float GetElapsedTime()
    {
        return Time.time - startTime;
    }

    // to reset stats when game is finished for restart
    public void resetStats()
    {
        startTime = Time.time;
        numJumps = 0;
        numFalls = 0;
        numGems = 0;
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
