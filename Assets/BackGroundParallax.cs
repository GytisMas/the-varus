using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParallax : MonoBehaviour
{

    private float length, startpos;
    [SerializeField] private GameObject cam;
    public float parallexEffect;
    

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
  
        cam = GameObject.FindGameObjectWithTag("MainCamera").gameObject;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float distance = (cam.transform.position.x * parallexEffect);
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);


        

        if (temp > startpos + length)
        {

            startpos += length * 3f;

        }
        /*else if (temp < startpos - length)
        {
            startpos -= length;

        }*/

    }
}
