using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    public float life = 1.0f; // Number of seconds before deleting this request.
    public int requestedEmoji = 0; // The main emoji requested by the NPC.
    public int[] otherEmoji = new int[] {0};  // Other acceptable emoji. TBD if we'll use this.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life < 0) { 
            Object.Destroy(this.gameObject);
            NegativePenalty();
        }
    }

    private void NegativePenalty()
    {
        //What do we do here?
    }
}
