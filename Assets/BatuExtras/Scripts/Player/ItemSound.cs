using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{

    [SerializeField] private AudioSource pickUpSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Health"))
        {
            pickUpSoundEffect.Play();
        }
        else if (collision.gameObject.CompareTag("DamagePotion"))
        {
            pickUpSoundEffect.Play();

        }

        else if (collision.gameObject.CompareTag("JumpPotion"))
        {
            pickUpSoundEffect.Play();

        }

        else if (collision.gameObject.CompareTag("SpeedPotion"))
        {
            pickUpSoundEffect.Play();

        }

        else
        {
            Debug.Log("No sound");
        }



    }

}
