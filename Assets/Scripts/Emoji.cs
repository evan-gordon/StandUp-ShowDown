using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoji : MonoBehaviour
{

    public SpriteRenderer emojiSpriteRenderer;
    [SerializeField]
    public Sprite[] newSprites;
    public Conveyor conveyor;

    private void OnTriggerEnter(Collider c)
    {
        if (c.name == "Player")
        {
            PlayerController.instance.AddAmmo(this.GetComponent<SpriteRenderer>().sprite);
            PlayerController.instance.canShoot = true;
            conveyor.RemoveEmoji(this.transform.parent.gameObject);
            Destroy(this.transform.parent.gameObject);
        }
    }
    void Start()
    {
        emojiSpriteRenderer = GetComponent<SpriteRenderer>();
        emojiSpriteRenderer.sprite = newSprites[Random.Range(0, newSprites.Length)]; ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
