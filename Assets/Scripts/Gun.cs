using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] public float cooldown = 1.0f;
    private float cooldownTimer;

    public void Start()
    {
        cooldownTimer = 0.0f;
    }
    public void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) & cooldownTimer > cooldown)
        {
            Shoot();
            cooldownTimer = 0.0f;
        }
    }
    public void Shoot()
    {
        if (PlayerController.instance.canShoot) {
            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            b.GetComponentInChildren<Bullet>().direction = PlayerController.playerCamera.transform.forward;
            b.GetComponentInChildren<Bullet>().BulletImage = PlayerController.instance.currentBullet;
            PlayerController.instance.canShoot = false;
        }
    }
}
