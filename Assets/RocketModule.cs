using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketModule : MonoBehaviour
{

    [SerializeField] private float firingRange=1f;
    Transform player;
    bool isFired = false;


    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float acceleration = 0.01f;
    [SerializeField] private float maxAcceleration = 1f;
    [SerializeField] private float currentAcceleration = 0.01f;

    [SerializeField] GameObject flames;
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void Setup(Transform player)
    {
        this.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (!isFired)
            {
                CheckDistance();
            }
            else
            {
                MoveTowards();
            }
        }
        
    }


    void Fire()
    {
        flames.SetActive(true);
        transform.parent = transform.root;
        isFired = true;
    }

    void MoveTowards()
    {
        transform.position -= transform.right * speed * Time.deltaTime;
         if (transform.position.y < player.position.y)
         {
             if (currentAcceleration < maxAcceleration)
             {
                 currentAcceleration += acceleration;
             }

            transform.Rotate(0,0,-0.1f);



         }
         else if (transform.position.y > player.position.y)
         {
             if (currentAcceleration > -maxAcceleration)
             {
                 currentAcceleration -= acceleration;
                transform.Rotate(0, 0, +0.1f);
            }
         }

        // transform.position += Vector3.up * currentAcceleration;

        //transform.LookAt(player);
       
    }

    void CheckDistance()
    {
        if( Vector2.Distance(transform.position, player.position) < firingRange)
        {
            Fire();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.PlaySound(SoundManager.Sound.Explosion);
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
            GameObject exp=Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, 2f);
            Destroy(gameObject);
        }
    }
}
