using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerScript playerScript;
    public Score score;
    public GameController gameController;
    public AudioSource audioSource;     // collectible sound
    public AudioSource audioSource1;    // obstacle/death sound

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectables")
        {
            audioSource.Play();
            score.AddScore(0.5);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacles")
        {
            audioSource1.Play();
            gameController.GameOver();
            playerScript.enabled = false;
        }
    }
}
