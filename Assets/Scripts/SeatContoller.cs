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
    // https://docs.unity3d.com/ScriptReference/Grid.html
    // https://docs.unity3d.com/ScriptReference/GridLayout.html
    private Grid g;

    // Start is called before the first frame update
    void Start()
    {
        g = this.GetComponent<Grid>();
        FillGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillGrid()
    {
        for(int x = gridCloseRight.x; x < gridFarLeft.x; x++) {
            for( int z = gridCloseRight.y; z < gridFarLeft.y; z++) {
                Vector3 pos = g.CellToWorld(new Vector3Int(x, 3, z));
                Debug.Log(pos);
                // https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
                Instantiate(npcPrefab, pos, Quaternion.identity, this.transform);    
            }
        }
    }
}
