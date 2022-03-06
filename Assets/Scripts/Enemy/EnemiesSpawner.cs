using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemiesParent;



   [SerializeField] private List<GameObject> enemyPrefab;
    [SerializeField] private float spawnChance = 0.5f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int spawnAmount = 1;

    private float timer=0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    

    void SpawnEnemies(int enemiersCount)
    {
        /*for(int i=0; i<enemiersCount; i++)
        {
            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], transform.position + new Vector3(0, Random.Range(-10f, +10f), 0), Quaternion.identity);
        }*/

        StartCoroutine(SpawnRoutine());

    }

    public IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], transform.position + new Vector3(0, Random.Range(-10f, +10f), 0), Quaternion.identity, enemiesParent.transform);
            yield return new WaitForSeconds(1f);
        }

       
        

    }

    void Timer()
    {

        if (timer <= 0)
        {
            SpawnEnemies(spawnAmount);
            timer = spawnInterval;
        }
        else
        {
            timer -= Time.deltaTime;
        }


    }



}
