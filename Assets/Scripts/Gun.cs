using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] public float cooldown = 1.0f;
    [SerializeField] public float charge = 0.0f;
    [SerializeField] public float chargeGrowSpeed = 0.3f;
    public Vector3 OriginalPos;
    public AudioSource AudioSource;
    [SerializeField]
    public AudioClip Whoosh;
    //private float cooldownTimer;

    public void Start()
    {
        //cooldownTimer = 0.0f;
        AudioSource = GetComponent<AudioSource>();
    }
    public void Update()
    {
        //cooldownTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) )//& cooldownTimer > cooldown)
        {
            charge += Time.deltaTime;
            //cooldownTimer = 0.0f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Shoot(charge);
            charge = 0.0f;
        }
        transform.localScale = new Vector3(1 + charge*chargeGrowSpeed, 1+charge * chargeGrowSpeed, 1+charge * chargeGrowSpeed);
    }
    public void Shoot(float charge)
    {
        if (PlayerController.instance.canShoot) {
            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            var bulletComp = b.GetComponentInChildren<Bullet>();
            bulletComp.direction = PlayerController.playerCamera.transform.forward;
            bulletComp.BulletImage = PlayerController.instance.currentBullet;
            bulletComp.emojiAmmoType = PlayerController.instance.currentAmmoType;
            bulletComp.speed *= charge;
            PlayerController.instance.canShoot = false;
            AudioSource.PlayOneShot(Whoosh);
        }
    }
}
