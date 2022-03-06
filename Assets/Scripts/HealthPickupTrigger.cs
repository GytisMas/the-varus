using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.CompareTag("Player"))
        {
            collidedGameObject.GetComponent<Player>().Heal(1);
            Destroy(gameObject);
        }
        if (collidedGameObject.CompareTag("Shredder"))
        {
            Destroy(gameObject);
        }
    }
}
