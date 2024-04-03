using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Called when another collider enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("Stretch", true);
            music.Stop();
            meow.Play();
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
