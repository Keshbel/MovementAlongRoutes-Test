using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    private Grid _grid;
    [SerializeField] private Tilemap interactiveMap;
    [SerializeField] private Tilemap pathMap;
    [SerializeField] private Tilemap terrainMap;
    [SerializeField] private TileBase hoverTile;
    [SerializeField] private TileBase pathTile;
    [SerializeField] private TileBase blackTile;
    [SerializeField] private TileBase goalTile;
    
    private Vector3Int _previousMousePos;

    // Start is called before the first frame update
    void Start() 
    {
        _grid = gameObject.GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update() {
        // Mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        
        if (terrainMap.GetTile(mousePos) == blackTile) return;
        
        if (!mousePos.Equals(_previousMousePos)) {
            interactiveMap.SetTile(_previousMousePos, null); // Remove old hoverTile
            interactiveMap.SetTile(mousePos, hoverTile);
            
            _previousMousePos = mousePos;
        }

        // Left mouse click -> add path tile
        if (Input.GetMouseButton(0)) 
        {
            AddPathTile(mousePos);
        }

        // Right mouse click -> remove path tile
        if (Input.GetMouseButton(1)) 
        {
            RemovePathTile(mousePos, true);
        }
       
    }

    public void ToggleInteractiveMap(bool isOn)
    {
        terrainMap.gameObject.SetActive(isOn);
    }
    
    private void AddPathTile(Vector3Int position)
    {
        pathMap.SetTile(position, pathTile);
            
        var pos = GetCellCenterToWorldPosition(position);
        var pathList = AllSingleton.Instance.cubeController.currentCube.pathList;
            
        if (!pathList.Contains(pos)) pathList.Add(pos);
    }
    
    public void RemovePathTile(Vector3Int position, bool isRemovingPath)
    {
        pathMap.SetTile(position, null);

        if (isRemovingPath)
        {
            var pos = GetCellCenterToWorldPosition(position);
            var pathList = AllSingleton.Instance.cubeController.currentCube.pathList;

            if (pathList.Contains(pos)) pathList.Remove(pos);
        }
    }

    private Vector3Int GetMousePosition () //получаем позицию мыши в мировых координатах с помощью raycast
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
            return _grid.WorldToCell(hit.point);
        
        return new Vector3Int(100, 100, 100);
    }

    public Vector3Int GetCellCenterFromWorldPosition(Vector3 worldPos) //получаем позицию клетки из мировой
    {
        return _grid.WorldToCell(worldPos);
    }
    
    private Vector3 GetCellCenterToWorldPosition(Vector3Int cellPos) //получаем мировую позицию центра клетки
    {
        return _grid.GetCellCenterWorld(cellPos);
    }

    public bool CheckGoal(Vector3 position, Cube cube) //проверяем на достижение цели
    {
        if (terrainMap.GetTile(GetCellCenterFromWorldPosition(position)) != goalTile) return false;
        
        cube.isGoal = true;
        return true;
    }
}
