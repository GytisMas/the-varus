using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Player player;
    public GameObject bullet;
    public Transform firePoint1;
    public Transform firePoint2;
    public bool allowfire = true;
    public bool topTurn = true;
    private Transform bulletsFolder;

    public ParticleSystem topGunMuzzleFlash;
    public ParticleSystem bottomGunMuzzleFlash;

    private void Awake()
    {
        bulletsFolder = GameObject.Find("BulletsFolder").transform;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) && allowfire) 
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        allowfire = false;
        SoundManager.PlaySound(SoundManager.Sound.playerFire);
        GameObject bulletShot;
        if (topTurn) { bulletShot = Instantiate(bullet, firePoint1.position, firePoint1.rotation, bulletsFolder); topTurn = false;
            topGunMuzzleFlash.Play();
        }
        else { bulletShot = Instantiate(bullet, firePoint2.position, firePoint2.rotation, bulletsFolder); topTurn = true;
            bottomGunMuzzleFlash.Play();
        }
        bulletShot.GetComponent<Bullet>().SetBulletParameters(player.bulletSpeed, player.damage, true);
        yield return new WaitForSeconds(player.fireRate);
        allowfire = true;
    }
}
