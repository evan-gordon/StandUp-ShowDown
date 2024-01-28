using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoji : MonoBehaviour
{

    public SpriteRenderer emojiSpriteRenderer;
    [SerializeField]
    public Sprite[] newSprites;
    // Start is called before the first frame update
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
