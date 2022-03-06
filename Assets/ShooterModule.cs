using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterModule : MonoBehaviour
{

    [SerializeField] private Transform firePoint;
    [SerializeField] float fireRate=1f;
    [SerializeField] float sprayFireRate = 0.5f;
    [SerializeField] float bulletSpeed = 0.05f;
   public  bool allowfire = true;
    [SerializeField] private GameObject bullet;

    
    [SerializeField] private Enemy enemy;

    [SerializeField] private Animator animator;

    [SerializeField] private int bulletsAmountInSeries=3;

    [SerializeField] private bool multipleCannons;


    // Start is called before the first frame update
    void Start()
    {
        if(!multipleCannons) enemy = gameObject.transform.parent.parent.gameObject.GetComponent<Enemy>();
        else enemy = gameObject.transform.parent.parent.parent.gameObject.GetComponent<Enemy>();
        if (allowfire && enemy != null)
            StartCoroutine(Shoot(bulletsAmountInSeries));
    }

    public void EndAnimation()
    {
        animator.SetBool("Fire", false);
    }

    private IEnumerator Shoot(int bulletCount)
    {
        while (true)
        {
            if (bulletCount > 0)
            {
                bulletCount--;
                animator.SetBool("Fire", true);
                GameObject bullet1 = Instantiate(bullet, firePoint.position, firePoint.rotation, enemy.bulletsFolder);
                bullet1.GetComponent<Bullet>().SetBulletParameters(bulletSpeed, 10, false);
                yield return new WaitForSeconds(sprayFireRate);
            }
            else
            {
                bulletCount = bulletsAmountInSeries;
              
                yield return new WaitForSeconds(fireRate);
            }
        }
    }




}
