using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoji : MonoBehaviour
{

    public SpriteRenderer emojiSpriteRenderer;
    [SerializeField]
    public Sprite[] newSprites;
    public Conveyor conveyor;
    private EmojiEnum e;

    private void OnTriggerEnter(Collider c)
    {
        if (c.name == "Player")
        {
            PlayerController.instance.AddAmmo(e);
            conveyor.RemoveEmoji(this.transform.parent.gameObject);
            Destroy(this.transform.parent.gameObject);
        }
    }
    void Start()
    {
        emojiSpriteRenderer = GetComponent<SpriteRenderer>();
        var emojiData = SeatController.instance.GetRandomEmoji();
        e = emojiData.id;
        emojiSpriteRenderer.sprite = emojiData.img;
    }
}
