using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private Transform Player;
    public float Smooth;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Player.position.x >= 0 && Player.position.x < 24.5)
        {
            Vector3 following = new Vector3(Player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, following, Smooth * Time.deltaTime);
        }
        
    }
}
