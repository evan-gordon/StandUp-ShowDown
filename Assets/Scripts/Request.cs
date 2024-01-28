using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Request : MonoBehaviour
{
    public float life = 1.0f; // Number of seconds before deleting this request.
    public int requestedEmoji = 0; // The main emoji requested by the NPC.
    public int[] otherEmoji = new int[] {0};  // Other acceptable emoji. TBD if we'll use this.
    public NPC parent = null;

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
            parent.ClearRequest();
            NegativePenalty();
        }
    }

    private void NegativePenalty()
    {
        //What do we do here?
    }

    // Returns 0 if the emoji is requested, 1 if the emoji is acceptable, and 0 if the emoji is not wanted.
    public int ApplyJoke(int emojiId)
    {
        if (emojiId == requestedEmoji)
        {
            return 0;
        }
        if (otherEmoji.Contains(emojiId))
        {
            return 1;
        }
        return 2;
    }
}
