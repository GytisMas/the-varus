using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{

    public GameObject redBloodCell;
    bool flag = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag) 
        StartCoroutine(Spawn());
    }



	IEnumerator Spawn()
	{
        flag = false;
        Vector3 randomOffset = new Vector3(0, Random.Range(10, -10), -8f);
        GameObject newCloud = Instantiate(redBloodCell, transform.position + randomOffset, Quaternion.identity);
        float randomScale = Random.Range(0.5f, 1.5f);
        newCloud.transform.localScale = new Vector2(randomScale, randomScale);
       // newCloud.GetComponent<SpriteRenderer>().flipX = spawner.CalculateChances(0, 3);
       // newCloud.GetComponent<SpriteRenderer>().flipY = spawner.CalculateChances(0, 3);


        //Alpha change
        /*Color cloudColor = newCloud.GetComponent<SpriteRenderer>().color;
        cloudColor.a = spawner.FCalculateRandomNumbers(0.6f, 1f);
        newCloud.GetComponent<SpriteRenderer>().color = cloudColor;*/
        //Alpha change of the cloau


        Destroy(newCloud, 40f);
        yield return new WaitForSeconds(3);
        flag = true;
    }


}
