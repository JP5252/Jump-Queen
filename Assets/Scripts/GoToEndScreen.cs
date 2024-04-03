using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        SceneManager.LoadScene("End Screen");
    }
}
