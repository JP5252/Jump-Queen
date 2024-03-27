using UnityEngine;
using UnityEngine.UI;

public class gems : MonoBehaviour
{
    public Text gemCounterText;
    public static int gemCount = 0;
    public GameObject Audio;
    private AudioSource gemsound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gemCount++;
            UpdateGemCounter();
            gemsound = Audio.transform.Find("gemsound").GetComponent<AudioSource>();
            gemsound.Play();
            Destroy(gameObject);
        }
    }

    private void UpdateGemCounter()
    {
        if (gemCounterText != null)
        {
            gemCounterText.text = "Gems: " + gemCount;
        }
    }
}
