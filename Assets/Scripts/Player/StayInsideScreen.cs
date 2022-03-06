using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInsideScreen: MonoBehaviour
{
    public float xboundaryL;
    public float xboundaryR;
    public float yboundaryT;
    public float yboundaryB;

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(
            transform.position.x, xboundaryL, xboundaryR),
            Mathf.Clamp(transform.position.y, yboundaryB, yboundaryT),
            transform.position.z
            );
    }
}
