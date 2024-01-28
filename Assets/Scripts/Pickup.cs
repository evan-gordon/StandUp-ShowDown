using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Conveyor conveyor;
    
    private void OnTriggerEnter(Collider c)
    {
        if (c.name == "Player")
        {
            PlayerController.instance.AddAmmo(1);
            conveyor.RemoveEmoji(this.transform.parent.gameObject);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
