using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemheart : MonoBehaviour
{
    public int heathValue;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().IncreaseLife(heathValue);
            Destroy(gameObject);
        }
    }
}
