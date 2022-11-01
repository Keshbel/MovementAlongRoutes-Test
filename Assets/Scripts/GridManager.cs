using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private Tilemap interactiveMap = null;
    [SerializeField] private TileBase whiteTile = null;
    [SerializeField] private TileBase yellowTile = null;
    [SerializeField] private TileBase orangeTile = null;


    private Vector3Int previousMousePos = new Vector3Int();

    // Start is called before the first frame update
    void Start() {
        grid = gameObject.GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update() {
        // Mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos)) {
            interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile
            interactiveMap.SetTile(mousePos, yellowTile);
            previousMousePos = mousePos;
        }

        // Left mouse click -> add path tile
        if (Input.GetMouseButton(0)) {
            interactiveMap.SetTile(mousePos, orangeTile);
        }

        // Right mouse click -> remove path tile
        if (Input.GetMouseButton(1)) {
            interactiveMap.SetTile(mousePos, whiteTile);
        }
    }

    Vector3Int GetMousePosition () 
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}
