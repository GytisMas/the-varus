using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldModule : MonoBehaviour
{
    public float rotationSpeed;
    public Transform pivotObject;

    private void Update()
    {
        transform.RotateAround(pivotObject.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
