using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Bullet bullet;

    private void Awake()
    {
        bullet = GetComponent<Bullet>();
    }

    private void FixedUpdate()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        transform.position += transform.right * bullet.speed * Time.deltaTime;
    }
}
