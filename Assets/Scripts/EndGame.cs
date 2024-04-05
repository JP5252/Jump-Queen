using UnityEngine;

/// <summary>
/// Author: Justin Pearson
/// 
/// This script sets in actrion the end scene of the game.
/// </summary>
public class EndGame : MonoBehaviour
{
    private Animator anim;
    public GameObject Audio;
    private AudioSource meow;
    private AudioSource music;

    public GameObject player;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        meow = Audio.transform.Find("Meow").GetComponent<AudioSource>();
        music = Audio.transform.Find("song").GetComponent<AudioSource>();

        rb = player.transform.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// called when a boxcollider collides with the cats collider
    /// we will then turn off the music and begin the cats animationa and sound as well as freeze player movement
    /// </summary>
    /// <param name="other">the other boxcollider</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // maker sure the collider is the player
        if (other.CompareTag("Player"))
        {
            anim.SetBool("Stretch", true);
            music.Stop();
            meow.Play();
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
