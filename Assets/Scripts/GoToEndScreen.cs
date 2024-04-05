using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Justin Pearson
/// 
/// This script executes when the cat finishes their animation.
/// It takes all the stats from the statTracker script and sets them as playerprefs to send to then end scene.
/// then it resets the stats and sends the game to the end scene
/// </summary>
public class EndScreen : StateMachineBehaviour
{
    private statTracker StatTracker;

    private void OnEnable()
    {
        StatTracker = GameObject.FindObjectOfType<statTracker>();
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // set time for the end screen
        float timeTaken = StatTracker.GetElapsedTime();
        PlayerPrefs.SetFloat("TimeTaken", timeTaken);

        // set the jumps for the end screen
        int numJumps = StatTracker.getNumJumps();
        PlayerPrefs.SetInt("numJumps", numJumps);

        // set the falls for the end screen
        int numFalls = StatTracker.getNumFalls();
        PlayerPrefs.SetInt("numFalls", numFalls);

        // set the gems for the end screen
        int numGems = StatTracker.getNumGems();
        PlayerPrefs.SetInt("numGems", numGems);

        StatTracker.resetStats();

        SceneManager.LoadScene("End Screen");
    }
}
