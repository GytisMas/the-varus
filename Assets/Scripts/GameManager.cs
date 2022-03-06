using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Random = UnityEngine.Random;
public enum Mutation
{
    Kamikaze = 1,
    Shooter = 2,
    Rocket = 4,
    Shield = 8,
    Melee = 16,
    
    TriShooter = 32,
    HexShooter = 64
    
}
public class GameManager : MonoBehaviour
{
    private const string HIGH_SCORE = "High_Score";                 // PlayerPrefs.GetInt(HIGH_SCORE)
    private const Mutation startMutations = (Mutation)0b00000000;
    private const int fiveWavesAfterThis = 25;

    public static int maxMutationValue = (int)Enum.GetValues(typeof(Mutation)).Cast<Mutation>().Max();
    public static int[] mutationScores = { 6, 4, 5, 6, 7, 8, 9 };

    private string[] virusColors = { "Green", "Blue", "Yellow", "Red", "Dark" };

    private int maxWaves = 2;

    [SerializeField] private GameObject enemiesParent;
    [SerializeField] private GameObject cutsceneCamera;
    [SerializeField] private List<GameObject> enemyPrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private CutSceneEnter cutsceneEnter;
    [SerializeField] private Player player;

    private List<GameObject> liveEnemies = new List<GameObject>();
    private List<List<int>> unusedMutations = new List<List<int>>();
    Mutation[] possibleMutations;

    private int totalScore = 0;
    private int roundNumber = 1;
    private int waveCount = 1;
    private int enemiesToSpawnInWave = 5;
    private bool timeIsRunning = true;

    private void Awake()
    {
        cutsceneCamera.SetActive(false);
    }

    private void DisplayMutation(int colorIndex, Mutation mutation)
    {
        int index = (int)Mathf.Log((int)mutation, 2);
        Debug.Log(colorIndex);
        Virus virusColor = (Virus)Resources.Load(virusColors[colorIndex] + "Virus");
        string text = $"{mutation}";
        GameObject newEnemy = Instantiate(enemyPrefab[0],
                    transform.position, Quaternion.identity, enemiesParent.transform);
        newEnemy.GetComponent<Enemy>().Setup(virusColor, mutation, true);
        cutsceneCamera.SetActive(true);
        cutsceneEnter.EnterCutSceneCamera(text, newEnemy);
    }

    private int highScore
    {
        get
        {
            return PlayerPrefs.GetInt(HIGH_SCORE);
        }
        set
        {
            PlayerPrefs.SetInt(HIGH_SCORE, value);
        }
    }

    void Start()
    {
        StartOrStopTime();
        UpdateScore();
        SetUnusedMutations();
    }

    public void StartOrStopTime()
    {
        Time.timeScale = timeIsRunning ? 1 : 0;
        timeIsRunning = !timeIsRunning;
    }

    private void SetHighScore()
    {
        highScore = totalScore;
    }

    public void RestartGame()
    {
        waveCount = 1;
        roundNumber = 1;
        totalScore = 0;
        UpdateScore();
        SetUnusedMutations();
        liveEnemies = new List<GameObject>();
        StartRound();
    }

    private void StartRound(int enemyIncrease = 2)
    {
        waveCount = 1;
        enemiesToSpawnInWave = (roundNumber - 1) * 2 + 6;
        StartWave(enemyIncrease);
        Debug.Log($"Next round: {roundNumber}");
    }

    private void StartWave(int enemyIncrease = 2)
    {
        StartCoroutine(WaveSpawn(enemiesToSpawnInWave, enemyIncrease));
        Debug.Log($"Next wave: {waveCount}");
    }

    private void NextWave()
    {
        if (waveCount < maxWaves)
        {
            waveCount++;
            StartWave();
        }
        else
        {
            roundNumber++;
            AddMutation();
            if (roundNumber > fiveWavesAfterThis)
                maxWaves = 5;
            StartCoroutine(StartRoundWithDelay(5));
        }            
    }

    private IEnumerator StartRoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartRound();
        yield break;
    }

    private IEnumerator EndRoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextWave();
        yield break;
    }

    private IEnumerator WaveSpawn(int remainingEnemies, int enemyIncrease)
    {
        while (true)
        {
            if (remainingEnemies > 0)
            {
                remainingEnemies--;
                int colorIndex = Random.Range(0, virusColors.Length);
                Virus virusColor = (Virus)Resources.Load(virusColors[colorIndex] + "Virus");
                GameObject newEnemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)],
                    transform.position + new Vector3(0, Random.Range(-10f, 5f), 0), Quaternion.identity, enemiesParent.transform);
                newEnemy.GetComponent<Enemy>().removeFromList += RemoveEnemyFromList;
                newEnemy.GetComponent<Enemy>().Setup(virusColor, SelectMutations(colorIndex));
                liveEnemies.Add(newEnemy);
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                enemiesToSpawnInWave += enemyIncrease;
                yield break;
            }
        }
    }

    private void RemoveEnemyFromList(GameObject removeThis, int score)
    {
        liveEnemies.Remove(removeThis);
        if (liveEnemies.Count == 0)
            StartCoroutine(EndRoundWithDelay(2f));
        totalScore += score;
        UpdateScore();
    }

    private void AddMutation()
    {
        int mutationListIndex = 0;
        if (!UnusedMutationsEmpty())
        {
            do
            {
                mutationListIndex = Random.Range(0, unusedMutations.Count);
            } while (unusedMutations[mutationListIndex].Count == 0);
            int indexToAdd = Random.Range(0, unusedMutations[mutationListIndex].Count);
            Mutation mutToAdd = (Mutation)unusedMutations[mutationListIndex][indexToAdd];
            possibleMutations[mutationListIndex] = possibleMutations[mutationListIndex]
                | mutToAdd;
            unusedMutations[mutationListIndex].RemoveAt(indexToAdd);
            Debug.Log($"Mutation added: {virusColors[mutationListIndex]} virus, {mutToAdd} mutation.");
            DisplayMutation(mutationListIndex, mutToAdd);
        }
        else
        {
            Debug.Log($"All mutations already added to {virusColors[mutationListIndex]} virus.");
        }        
    }
    private void SetUnusedMutations()
    {
        possibleMutations = new Mutation[virusColors.Length];
        for (int i = 0; i < virusColors.Length; i++)
        {
            possibleMutations[i] = startMutations;
            unusedMutations.Add(new List<int>());
            for (int j = 0; j <= (int)Mathf.Log(maxMutationValue, 2); j++)
            {
                unusedMutations[i].Add((int)Mathf.Pow(2, j));
            }
        }
    }

    private Mutation SelectMutations(int index)
    {
        Mutation usedMutation = 0;
        if ((possibleMutations[index] & Mutation.Kamikaze) != 0)
        {
            int followHeightRandom = Random.Range(0, 9);
            if (followHeightRandom >= 6)
                usedMutation = usedMutation | Mutation.Kamikaze;
        }
        if ((usedMutation & Mutation.Kamikaze) == 0 && (possibleMutations[index] & Mutation.Shooter) != 0)
        {
            int shooterRandom = Random.Range(0, 2);
            if (shooterRandom == 1)
                usedMutation = usedMutation | Mutation.Shooter;
        }
        if ((possibleMutations[index] & Mutation.Rocket) != 0)
        {
            int rocketRandom = Random.Range(0, 2);
            if (rocketRandom == 1)
                usedMutation = usedMutation | Mutation.Rocket;
        }
        if ((possibleMutations[index] & Mutation.Shield) != 0)
        {
            int shieldRandom = Random.Range(0, 2);
            if (shieldRandom == 1)
                usedMutation = usedMutation | Mutation.Shield;
        }
        if ((possibleMutations[index] & Mutation.Melee) != 0)
        {
            int meleeRandom = Random.Range(0, 2);
            if (meleeRandom == 1)
                usedMutation = usedMutation | Mutation.Melee;
        }
        
        if ((possibleMutations[index] & Mutation.TriShooter) != 0)
        {
            int triRandom = Random.Range(0, 2);
            if (triRandom == 1)
                usedMutation = usedMutation | Mutation.TriShooter;
        }
        if ((possibleMutations[index] & Mutation.HexShooter) != 0)
        {
            int hexRandom = Random.Range(0, 2);
            if (hexRandom == 1)
                usedMutation = usedMutation | Mutation.HexShooter;
        }
        
        return usedMutation;
    }

    private void UpdateScore()
    {
        if (!PlayerPrefs.HasKey(HIGH_SCORE))
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
        else if (highScore < totalScore)
            highScore = totalScore;

        scoreText.text = "Score:\n" + totalScore;
        highScoreText.text = "High Score:\n" + PlayerPrefs.GetInt(HIGH_SCORE, 0);
    }

    private bool UnusedMutationsEmpty()
    {
        bool result = true;
        for (int i = 0; i < unusedMutations.Count; i++)
        {
            if (unusedMutations[i].Count > 0)
                result = false;
        }
        return result;
    }

    void Update()
    {
        //Debug.Log($"Current score: {totalScore}");
        if (Input.GetKeyDown(KeyCode.G))
            StartOrStopTime();
        //    if (Input.GetKeyDown(KeyCode.H))
        //        cutsceneCamera.SetActive(!cutsceneCamera.activeInHierarchy);
    }
}
