using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int id { get; private set; }
    // We might be able to use this to make sure the grid syncs content.
    public Vector2Int seatLoc { get; private set; }
    private SpriteRenderer sr;
    void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    public void SetData(NPCData data, Vector2Int seat)
    {
        sr.sprite = data.img;
        id = data.id;
        seatLoc = seat;
    }
}
