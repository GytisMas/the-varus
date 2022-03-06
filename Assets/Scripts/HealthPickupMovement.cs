using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupMovement : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
