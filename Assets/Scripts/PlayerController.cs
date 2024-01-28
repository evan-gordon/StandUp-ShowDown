using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Used for getting global references to the player
    public static PlayerController instance;
    public static Camera playerCamera;
    private Rigidbody rb;

    [SerializeField]
    public GameObject gun;

    public float speed = 10f;
    public bool canShoot =false;
    private int score = 0;

    public Sprite currentBullet;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = GetComponentInChildren<Camera>();
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.position = transform.position + playerCamera.transform.right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.position = transform.position + playerCamera.transform.forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
    }

    // Used by Pickup.cs
    // We can change this later to add different ammo types.
    public void AddAmmo(Sprite BulletType)
    {
        currentBullet = BulletType;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}
