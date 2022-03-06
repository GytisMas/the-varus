using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    private Bullet bullet;
    public GameObject bulletExplosion;

    private void Awake()
    {
        bullet = GetComponent<Bullet>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.CompareTag("Enemy"))
        {
            if (bullet.isFriendly)
            {
                Explode();
                collidedGameObject.GetComponent<Enemy>().TakeDamage(bullet.damage);
                Destroy(gameObject);
            }
        }
        else if (collidedGameObject.CompareTag("Player"))
        {
            if (!bullet.isFriendly)
            {
                Explode();
                collidedGameObject.GetComponent<Player>().TakeDamage(1);
                Destroy(gameObject);
            }
        }
        else if (collidedGameObject.CompareTag("Shield"))
        {
            if (bullet.isFriendly)
            {
                Explode();
                Destroy(gameObject);
            }
        }
    }

    public void Explode()
	{
        GameObject obj = Instantiate(bulletExplosion, transform.position, Quaternion.identity);
        Destroy(obj, 1f);
	}
}
