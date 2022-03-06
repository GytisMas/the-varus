using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public float moveSpeed;
    public float fireRate;
    public int damage;
    public float bulletSpeed;
    public int currentHealth;
    private HealthBar healthBar;

    private void Start()
    {
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        healthBar.SetHealth(currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();        
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        ScreenManager screenManager = GameObject.Find("Screen Manager").GetComponent<ScreenManager>();
        screenManager.OpenGameOverScreen();
    }

    public void Heal(int healAmmount)
    {
        currentHealth += healAmmount;
        if (currentHealth > 5) currentHealth = 5;
        healthBar.SetHealth(currentHealth);
    } 
}
