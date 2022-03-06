using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject player;
    public float parallexEffect;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
           startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        if (player != null)
        {
            transform.position += Vector3.left * Time.deltaTime;

            ///Debug.Log(player.transform.position.x);

            //Debug.Log(transform.position.x);

            if (player.transform.position.x -50 > transform.position.x)
            {
                transform.position = new Vector3(transform.position.x + length * 3-0.1f, transform.position.y, transform.position.z);
            }
        }
		else
		{
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
    }
}