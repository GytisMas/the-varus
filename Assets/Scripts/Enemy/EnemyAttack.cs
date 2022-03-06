using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    public UnityAction<bool> onDeath;


   [SerializeField]private GameObject bullet;

    [SerializeField] private GameObject firePoint;

    [SerializeField] private GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {

      // gameObject projectile= Instantiate(bullet, firePoint)


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
            onDeath?.Invoke(false);
        } else if (collision.gameObject.tag == "Shredder")
        {
            onDeath?.Invoke(true);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }

    private void Explode()
    {




    }
}
