using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndSpin : MonoBehaviour
{
   float speed= 5f;
    float spinSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(speed * 0.5f, speed * 2);

        spinSpeed = Random.Range(spinSpeed * -1, spinSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

   public void Setup(float _speed, float _spinSpeed)
    {
        Debug.Log(speed);
        Debug.Log(_speed);
        speed = _speed;
        spinSpeed = Random.Range(_spinSpeed * -1, _spinSpeed);
        Debug.Log(speed);
    }

    private void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);

    }




}
