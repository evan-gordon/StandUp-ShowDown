using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public float life = 5.0f; // Number of seconds before deleting this bullet
    public Vector3 direction;
    private Transform parent;
    [SerializeField]
    public Sprite BulletImage;

    void Start()
    {
        parent = transform.parent.transform;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = BulletImage;
    }

    void FixedUpdate()
    {
        life -= Time.fixedDeltaTime;
        if (life < 0 ) { Object.Destroy(parent.gameObject); }

        parent.position += direction * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.name);
        if (c.tag != "NPC")
        {
            return;
        }

        SeatController.instance.RemoveNPCFromSeat(c.gameObject);
        Destroy(c.gameObject);
        PlayerController.instance.AddScore(1);
        // TODO call some 'onDeath' code here instead.
        Destroy(parent.gameObject);
    }
}
