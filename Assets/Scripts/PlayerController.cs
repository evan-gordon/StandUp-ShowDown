using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Used for getting global references to the player
    public static PlayerController instance;

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
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            Input.GetAxis("Horizontal") * speed * Time.deltaTime,
            0,
            Input.GetAxis("Vertical") * speed * Time.deltaTime
        );

        if (Input.GetMouseButton(0)){
            gun.GetComponent<Gun>().Shoot();
         }
    }

    // Used by Pickup.cs
    // We can change this later to add different ammo types.
    public void AddAmmo(int amount)
    {
        ammo += amount;
    }
}
