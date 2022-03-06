using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    public List<GameObject> prefabs;


    [SerializeField] private float spawnInterval = 20f;
    [SerializeField] private int spawnAmount = 1;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        Timer();
    }



    void SpawnEnemies(int enemiersCount)
    {
        

        StartCoroutine(SpawnRoutine());

    }

    public IEnumerator SpawnRoutine()
    {
        
         GameObject obj= Instantiate(prefabs[Random.Range(0, prefabs.Count)], transform.position + new Vector3(0, Random.Range(-10f, +10f), 0), Quaternion.identity);
        obj.GetComponent<MoveAndSpin>().Setup(Random.Range(0.001f,0.05f),40f );
        yield return new WaitForSeconds(5f);
        




    }

    void Timer()
    {

        if (timer <= 0)
        {
            StartCoroutine( SpawnRoutine());
            timer = spawnInterval;
        }
        else
        {
            timer -= Time.deltaTime;
        }


    }
}
