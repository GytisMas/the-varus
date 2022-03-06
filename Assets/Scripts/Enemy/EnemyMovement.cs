using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public bool docile = false;

   [SerializeField]private float speed = 0.5f;
    [SerializeField] private float acceleration = 0.01f;
    [SerializeField] private float maxAcceleration = 1f;
    [SerializeField] private float currentAcceleration = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        float speedMult = Random.Range(1f, 1.65f);
        speed *= speedMult;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }



    private void Move()
    {
        if (!docile)
            transform.position += Vector3.left * speed * Time.deltaTime;


    }

    public void MoveTowardsPlayerAndMove(Transform player)
    {
        if (transform.position.y < player.position.y)
        {
            if (currentAcceleration < maxAcceleration)
            {
                currentAcceleration += acceleration;
            }
                
        }
       else if (transform.position.y > player.position.y)
        {
            if (currentAcceleration > -maxAcceleration)
            {
                currentAcceleration -= acceleration;
            }
        }

        transform.position += Vector3.up * currentAcceleration;


    }

    public void MoveTowardsPlayerAndStop(Transform player)
    {
        if (transform.position.y < player.position.y+3f)
        {
            if (currentAcceleration < maxAcceleration)
            {
                currentAcceleration += acceleration;
            }

        }
        else if (transform.position.y > player.position.y-3f)
        {
            if (currentAcceleration > -maxAcceleration)
            {
                currentAcceleration -= acceleration;
            }
        }

        transform.position += Vector3.up * currentAcceleration;


    }

    public void MultiplySpeed()
    {
        speed = speed * 5;
    }


}
