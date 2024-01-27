using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class SeatContoller : MonoBehaviour
{
    [SerializeField]
    public GameObject npcPrefab;
    private static Vector2Int gridCloseRight = new Vector2Int(-2, 0);
    private static Vector2Int gridFarLeft = new Vector2Int(3, 4);
    private Grid g;
    
    // This flag determines whether we must use ALL advanced shapes before repeating any.
    private bool useUniqueShapesOnly = true;

    // Start is called before the first frame update
    void Start()
    {
        g = this.GetComponent<Grid>();
        FillGrid();
        OverlayAdvancedShapes(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FillGrid will populate the audience grid with randomly chosen characters.
    public void FillGrid()
    {
        for(int x = gridCloseRight.x; x < gridFarLeft.x; x++) {
            for( int z = gridCloseRight.y; z < gridFarLeft.y; z++) {
                Vector3 pos = g.CellToWorld(new Vector3Int(x, 3, z));
                Debug.Log(pos);
                Instantiate(npcPrefab, pos, Quaternion.identity, this.transform);    
            }
        }
    }

    // OverlayAdvancedShapes will overlay shapeCount shapes over an existing grid, populating it with predefined groups of similar characters.
    public void OverlayAdvancedShapes(int shapeCount)
    {
        AudienceShape[] advancedShapes = ShapeConstants.ChooseRandomShapes(shapeCount, useUniqueShapesOnly);
        for (int i = 0; i < advancedShapes.Length; i++)
        {
            Debug.Log(advancedShapes[i].name);
        }
    }
}
