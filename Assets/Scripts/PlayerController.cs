using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Used for getting global references to the player
    public static PlayerController instance;
    public static Camera playerCamera;

    [SerializeField]
    public GameObject gun;

    public float speed = 10f;
    public int ammo = 0;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + playerCamera.transform.right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.position = transform.position + playerCamera.transform.forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
    }

    // Used by Pickup.cs
    // We can change this later to add different ammo types.
    public void AddAmmo(int amount)
    {
        ammo += amount;
    }
}
