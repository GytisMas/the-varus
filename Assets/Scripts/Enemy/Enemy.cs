using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    public UnityAction<GameObject, int> removeFromList;
    int Health = 100;
    bool docile = false;


    private Mutation virusType;
   [SerializeField] private EnemyMovement enemyMovement;
   [SerializeField] private EnemyAttack enemyAttack;
    private GameObject player;
    [SerializeField] public Transform bulletsFolder;

    [SerializeField] private List<GameObject> allModules;
    [SerializeField] private GameObject ModulesPlace;

    [SerializeField] private GameObject Explosion;

    [SerializeField] private Animator animator;

    [SerializeField] private Virus virus;


    public void Setup(Virus virusColor, Mutation mutations, bool _docile = false)
    {
        docile = _docile;
        enemyMovement.docile = docile;
        enemyAttack.onDeath = Die;
        bulletsFolder = GameObject.Find("BulletsFolder").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        virusType = mutations;
        virus = virusColor;
        gameObject.GetComponent<SpriteRenderer>().sprite = virus.sprite;
        Explosion = virus.ExplosionPrefab;

        if ((virusType & Mutation.Shooter) != 0)
        {
            GameObject shooterModule = Instantiate(allModules[0], transform.position, Quaternion.identity, ModulesPlace.transform);
            if (docile)
                shooterModule.GetComponent<ShooterModule>().allowfire = false;
        }
        if ((virusType & Mutation.Rocket) != 0)
        {
            GameObject rocketsModule = Instantiate(allModules[1], transform.position, Quaternion.identity, ModulesPlace.transform);
            rocketsModule.GetComponent<RocketModule>().Setup(player.transform);
        }
        if ((virusType & Mutation.Kamikaze) != 0)
        {
            Instantiate(allModules[2], transform.position, Quaternion.identity, ModulesPlace.transform);
        }
        if ((virusType & Mutation.Shield) != 0)
        {
            Instantiate(allModules[3], transform.position, Quaternion.identity, ModulesPlace.transform);
        }
        if ((virusType & Mutation.Melee) != 0)
        {
            Instantiate(allModules[4], transform.position, Quaternion.identity, ModulesPlace.transform);
        }
        if ((virusType & Mutation.TriShooter) != 0)
        {
            GameObject tri = Instantiate(allModules[5], transform.position, Quaternion.identity, ModulesPlace.transform);
            if (docile)
            {
                ShooterModule[] shooters = tri.GetComponentsInChildren<ShooterModule>();
                for (int i = 0; i < shooters.Length; i++)
                {
                    shooters[i].allowfire = false;
                }
            }
        }
        if ((virusType & Mutation.HexShooter) != 0)
        {
            GameObject hex = Instantiate(allModules[6], transform.position, Quaternion.identity, ModulesPlace.transform);
            if (docile)
            {
                ShooterModule[] shooters = hex.GetComponentsInChildren<ShooterModule>();
                for (int i = 0; i < shooters.Length; i++)
                {
                    shooters[i].allowfire = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((virusType & Mutation.Kamikaze) != 0 && !docile)
        {
            enemyMovement.MoveTowardsPlayerAndMove(player.transform);
        }
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit");
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }

    }
    public void Die(bool selfDestruct = false)
    {
        int score = 0;
        if (!selfDestruct)
        {
            score += 10;
            for (int i = 0; i < (int)Mathf.Log(GameManager.maxMutationValue, 2); i++)
            {
                score += GetBitFromRight((byte)virusType, i) ? GameManager.mutationScores[i] : 0;
            }
        }
        
        GameObject explosion=Instantiate(Explosion, transform.position, Quaternion.identity);
        SoundManager.PlaySound(SoundManager.Sound.Explosion);
        removeFromList?.Invoke(gameObject, score);
        Destroy(gameObject);
        Destroy(explosion, 2f);
    }

    private bool GetBitFromRight(byte b, int index)
    {
        return (b & (byte)Mathf.Pow(2, index)) != 0; 
    }


    public void SetSpeed()
    {
        enemyMovement.MultiplySpeed();
    }


}


