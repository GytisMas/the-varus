using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public bool isFriendly;
    

    public void SetBulletParameters(float speed, int damage, bool isFriendly)
    {
        this.speed = speed;
        this.damage = damage;
        this.isFriendly = isFriendly;
        Destroy(gameObject, 6);
    }
}
