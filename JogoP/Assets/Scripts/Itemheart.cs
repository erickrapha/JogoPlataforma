using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemheart : MonoBehaviour
{
    public int heathValue;
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sound.Play();
            collision.gameObject.GetComponent<Player>().IncreaseLife(heathValue);
            Destroy(gameObject, 0.1f);
        }
    }
}
