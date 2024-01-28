using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public float life = 5.0f; // Number of seconds before deleting this bullet
    public Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life < 0 ) { Object.Destroy(this.gameObject); }

        transform.position += direction * speed * Time.deltaTime;
    }

}
