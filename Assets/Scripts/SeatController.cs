using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Grid))]
public class SeatController : MonoBehaviour
{
    // Singleton :)
    public static SeatController instance;
    [SerializeField] public GameObject[] npcPrefabList;
    [SerializeField] public GameObject[] emojiPrefabList;
    private static Vector2Int gridCloseRight = new Vector2Int(-2, 0);
    private static Vector2Int gridFarLeft = new Vector2Int(3, 4);
    // https://docs.unity3d.com/ScriptReference/Grid.html
    // https://docs.unity3d.com/ScriptReference/GridLayout.html
    private Grid g;
    private Dictionary<Vector2Int, GameObject> SeatToNPC = new Dictionary<Vector2Int, GameObject>();

    private NPC[,] gridRepresentation; // This is an internal representation of which characters hold which seat.
    
    private float spawnTimer = 5f; // Don't spawn an emoji for 5 seconds after game launch
    private float spawnInterval = 5f; // Spawn an emoji every 5 seconds
    private float requestLifetime = 16f; // How long a request lasts for
    private int activeRequests = 0;
    private int maxRequests = 8;


    private System.Random random = new System.Random();
    
    // This flag determines whether we must use ALL advanced shapes before repeating any.
    private bool useUniqueShapesOnly = true;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        g = this.GetComponent<Grid>();
        int gridWidth = gridFarLeft.x - gridCloseRight.x;
        int gridHeight = gridFarLeft.y - gridCloseRight.y;
        RandomGrid(gridWidth, gridHeight);
        OverlayAdvancedShapes(2);
        InstantiateGrid();
    }

    // Called once per frame
    private void FixedUpdate()
    {
        UpdateSpawnTimer();
    }

    private void UpdateSpawnTimer()
    {
        if (spawnTimer <= 0)
        {
            spawnTimer = spawnInterval;
            if (activeRequests < maxRequests)
            {
                int newRequestX;
                int newRequestY;
                int tries = 5;
                do
                {
                    // Pick random seat
                    newRequestX = random.Next(0, gridRepresentation.GetLength(0));
                    newRequestY = random.Next(0, gridRepresentation.GetLength(1));
                    // check if seat is empty
                    // looks like this doesn't totally work :/
                    if (!SeatIsFilled(new Vector2Int(newRequestX, newRequestY)))
                    {
                        tries--;
                        continue;
                    }
                    // check for request
                    if (!gridRepresentation[newRequestX, newRequestY].HasActiveRequest())
                    {
                        break;
                    }
                    tries--;
                } while (tries > 0);
                SpawnEmojiRequest(newRequestX, newRequestY);
            }
        }
        spawnTimer -= Time.deltaTime;
    }

    private void SpawnEmojiRequest(int x, int z)
    {
        int[] possibleEmoji = gridRepresentation[x, z].acceptableEmoji;
        int primaryEmoji = possibleEmoji[random.Next(0, possibleEmoji.Length)];
        Vector3 pos = getPositionVector(x, 3+z, z);
        pos.x += 0.7f;
        pos.y += 1.4f;
        GameObject req = Instantiate(emojiPrefabList[primaryEmoji], pos, Quaternion.identity, this.transform);
        var reqComponent = req.GetComponent<Request>();
        reqComponent.life = requestLifetime;
        reqComponent.requestedEmoji = primaryEmoji;
        reqComponent.otherEmoji = possibleEmoji;
        reqComponent.parent = gridRepresentation[x, z];
        gridRepresentation[x, z].SetRequest(reqComponent);
    }


    // RandomGrid will populate a grid representation with randomly chosen characters.
    public void RandomGrid(int xSize, int ySize)
    {
        gridRepresentation = new NPC[xSize, ySize];
        int npcTypeCount = npcPrefabList.Length;
        for(int x = 0; x < xSize; x++) {
            for( int y = 0; y < ySize; y++) {
                gridRepresentation[x, y] = NPCConstants.GetNPC(random.Next(npcTypeCount));
            }
        }
    }

    // OverlayAdvancedShapes will overlay shapeCount shapes over an existing grid, populating it with predefined groups of similar characters.
    public void OverlayAdvancedShapes(int shapeCount)
    {
        AudienceShape[] advancedShapes = ShapeConstants.ChooseRandomShapes(shapeCount, useUniqueShapesOnly);
        for (int i = 0; i < advancedShapes.Length; i++)
        {
            AudienceShape shape = advancedShapes[i];
            int npcType = random.Next(npcPrefabList.Length);
            int shapeOriginX = random.Next(gridRepresentation.GetLength(0) - shape.width);
            int shapeOriginY = random.Next(gridRepresentation.GetLength(1) - shape.height);
            
            for (int xMod = 0; xMod < shape.width; xMod++)
            {
                for (int yMod = 0; yMod < shape.height; yMod++)
                {
                    if (shape.layout[xMod, yMod]) {
                        gridRepresentation[shapeOriginX + xMod, shapeOriginY + yMod] = NPCConstants.GetNPC(npcType);
                    }
                }
            }
        }
    }

    // InstantiateGrid will actually place the chosen characters on the map.
    public void InstantiateGrid()
    {
        for (int x = 0; x < gridRepresentation.GetLength(0); x++) {
            for (int z =  0; z < gridRepresentation.GetLength(1); z++)
            {
                Vector3 pos = getPositionVector(x, 3, z);
                Debug.Log(pos);
                // https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
                var go = Instantiate(npcPrefabList[gridRepresentation[x,z].prefabNum], pos, Quaternion.identity, this.transform) as GameObject;
                SeatToNPC[new Vector2Int(x, z)] = go;
            }
        }
    }

    public void RegisterNPCSeat(Vector2Int loc, GameObject go)
    {
        SeatToNPC[loc] = go;
    }

    // This may cause sync issues with gridRepresentation, possibly want to look more into this.
    public void RemoveNPCFromSeat(GameObject go)
    {
        foreach( KeyValuePair<Vector2Int, GameObject> kvp in SeatToNPC )
        {
            if (kvp.Value == go)
            {
                SeatToNPC.Remove(kvp.Key);
                return;
            }
        }
    }

    public bool SeatIsFilled(Vector2Int loc)
    {
        return SeatToNPC.ContainsKey(loc);
    }

    private Vector3 getPositionVector(int x, int y, int z)
    {
        int xMod = -2;
        int zMod = 0;
        Vector3 pos = g.CellToWorld(new Vector3Int(x + xMod, 3 + z, z + zMod));
        pos.x = pos.x + (float)((z % 2)); //Add slight horizontal offset to even rows.
        return pos;
    }
}
