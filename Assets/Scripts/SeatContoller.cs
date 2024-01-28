using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Grid))]
public class SeatContoller : MonoBehaviour
{
    [SerializeField]
    public GameObject[] npcPrefabList;
    private static Vector2Int gridCloseRight = new Vector2Int(-2, 0);
    private static Vector2Int gridFarLeft = new Vector2Int(3, 4);
    // https://docs.unity3d.com/ScriptReference/Grid.html
    // https://docs.unity3d.com/ScriptReference/GridLayout.html
    private Grid g;
    
    // This flag determines whether we must use ALL advanced shapes before repeating any.
    private bool useUniqueShapesOnly = true;

    // Start is called before the first frame update
    void Start()
    {
        g = this.GetComponent<Grid>();
        int gridWidth = gridFarLeft.x - gridCloseRight.x;
        int gridHeight = gridFarLeft.y - gridCloseRight.y;
        int[,] gridRepresentation = RandomGrid(gridWidth, gridHeight);
        OverlayAdvancedShapes(2, gridRepresentation);
        InstantiateGrid(gridRepresentation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // RandomGrid will populate a grid representation with randomly chosen characters.
    public int[,] RandomGrid(int xSize, int ySize)
    {
        System.Random random = new System.Random();
        int npcTypeCount = npcPrefabList.Length;
        int[,] gridRepresentation = new int[xSize,ySize];
        for(int x = 0; x < xSize; x++) {
            for( int y = 0; y < ySize; y++) {
                gridRepresentation[x, y] = random.Next(npcTypeCount);
            }
        }
        return gridRepresentation;
    }

    // OverlayAdvancedShapes will overlay shapeCount shapes over an existing grid, populating it with predefined groups of similar characters.
    public void OverlayAdvancedShapes(int shapeCount, int[,] grid)
    {
        AudienceShape[] advancedShapes = ShapeConstants.ChooseRandomShapes(shapeCount, useUniqueShapesOnly);
        System.Random random = new System.Random();
        for (int i = 0; i < advancedShapes.Length; i++)
        {
            AudienceShape shape = advancedShapes[i];
            int npcType = random.Next(npcPrefabList.Length);
            int shapeOriginX = random.Next(grid.GetLength(0) - shape.width);
            int shapeOriginY = random.Next(grid.GetLength(1) - shape.height);
            
            for (int xMod = 0; xMod < shape.width; xMod++)
            {
                for (int yMod = 0; yMod < shape.height; yMod++)
                {
                    if (shape.layout[xMod, yMod]) { 
                        grid[shapeOriginX + xMod, shapeOriginY + yMod] = npcType;
                    }
                }
            }
        }
    }

    // InstantiateGrid will actually place the chosen characters on the map.
    public void InstantiateGrid(int[,] grid)
    {
        int xMod = -2;
        int zMod = 0;
        for (int x = 0; x < grid.GetLength(0); x++) {
            for (int z =  0; z < grid.GetLength(1); z++)
            {
                Vector3 pos = g.CellToWorld(new Vector3Int(x + xMod, 3+z, z + zMod));
                pos.x = pos.x + (float)((z % 2)); //Add slight horizontal offset to even rows.
                Debug.Log(pos);
                // https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
                Instantiate(npcPrefabList[grid[x,z]], pos, Quaternion.identity, this.transform);
            }
        }
        
    }

    private bool arraySafe(int arraySize, int val)
    {
        if (val >= 0 &&  val < arraySize)
        {
            return true;
        }
        return false;
    }
}
