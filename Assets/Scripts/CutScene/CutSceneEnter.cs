using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutSceneEnter : MonoBehaviour
{
    [SerializeField]private GameObject mainCamera;
    [SerializeField] private GameObject cutSceneCamera;

    //[SerializeField] private GameManager manager;

    [SerializeField] private Animator animator;


    [SerializeField] private Transform placeForVirusPrefab;
    [SerializeField] private TextMeshProUGUI mutationText;


    public void EnterCutSceneCamera(string _mutationText, GameObject virus)
    {

        GameObject obj = Instantiate(virus, placeForVirusPrefab.position, Quaternion.identity, placeForVirusPrefab);
        Destroy(virus);
        mutationText.text = _mutationText;
        Destroy(obj, 6f);
        //manager.StartOrStopTime();
        cutSceneCamera.SetActive(true);
        mainCamera.SetActive(false);
        animator.SetTrigger("Enter");

        float rnd = Random.value;
        Debug.Log(rnd);
        if (rnd < 0.3)
        {
            SoundManager.PlaySound(SoundManager.Sound.Mutation1);
        }
        else if ( rnd < 0.6)
		{
            SoundManager.PlaySound(SoundManager.Sound.Mutation2);
        }
		else
		{
            SoundManager.PlaySound(SoundManager.Sound.Mutation3);
        }

       

        StartCoroutine(ResumeGame());



	}

    IEnumerator ResumeGame()
	{
        yield return new WaitForSeconds(5f);
        //manager.StartOrStopTime();
        mainCamera.SetActive(true);
        cutSceneCamera.SetActive(false);
        gameObject.SetActive(false);

    }



}
