using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField]
    public GameObject startLoc;
    [SerializeField]
    public GameObject endLoc;
    [SerializeField]
    public GameObject emojiPrefab;
    // Start is called before the first frame update
    private List<GameObject> emojis = new List<GameObject>();
    private float spawnTimer = 0f;
    private float spawnInterval = 2.5f;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        UpdateSpawnTimer();

        // Move all the emojis on the belt, delete when necessary.
        for (var i = emojis.Count - 1; i >= 0; i--)
        {
            if (emojis[i].transform.position.x < endLoc.transform.position.x)
            {
                var curr = emojis[i];
                emojis.RemoveAt(i);
                Destroy(curr);
                continue;
            }    
            emojis[i].transform.Translate(-1.2f * Time.deltaTime, 0, 0);
        }
    }

    private void UpdateSpawnTimer()
    {
        if (spawnTimer <= 0 )
        {
            spawnTimer = spawnInterval;
            SpawnEmoji();
        }
        spawnTimer -= Time.deltaTime;
    }

    private void SpawnEmoji() {
        var newEmoji = Instantiate(emojiPrefab, startLoc.transform.position, Quaternion.identity, this.transform);
        emojis.Add(newEmoji);
    }
}
