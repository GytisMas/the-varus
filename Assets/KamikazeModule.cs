using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeModule : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       transform.parent.parent.GetComponent<Enemy>().SetSpeed();


    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
