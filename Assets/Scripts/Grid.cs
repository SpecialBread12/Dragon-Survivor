using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.EventSystems.EventTrigger;

public class Grid : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] AvailableWallTiles;
    public float WallProbability = 0.2f;
    public uint ColumnCount = 10;
    public uint RowCount = 10;
    public float CellSize = 1;
    public GameObject[] AvailableGems;
    public float GemProbability = 0.1f;
    public GameObject[] AvailableTiles;
    public int SelectedTile;

    private int EntryX;
    private int ExitX;
    [SerializeField]
    private Color m_GridColor = Color.white;
    private List<Tile> m_Tiles;
    [SerializeField]
    private bool m_ProceduralMap = false;
    [SerializeField]
    private Tile m_TileEntry;
    [SerializeField]
    private Tile m_TileExit;


    private void Awake()
    {
        m_Tiles = GetComponentsInChildren<Tile>().ToList();

        foreach (var t_Tile in m_Tiles)
        {
            Vector2Int t_GridPos = WorldToGrid(t_Tile.transform.position);
            t_Tile.x = (uint)t_GridPos.x;
            t_Tile.y = (uint)t_GridPos.y;
        }
    }

    private void Start()
    {
        if (m_ProceduralMap)
        {
            GenerateRandomMap();
            Player.transform.position = new Vector3(15.5f, 1.5f, 0);
        }
    }

    private void GenerateRandomMap()
    {
        ClearGrid(); // Clear existing tiles

        for (int x = 0; x < ColumnCount; x++)
        {
            for (int y = 0; y < RowCount; y++)
            {
                if (x == 0 || x == ColumnCount - 1 || y == 0 || y == RowCount - 1)
                {
                    // If on the border, instantiate a wall
                    InstantiateWallTile(x, y);
                }
                else
                {
                    // For non-border cells, determine whether to instantiate a ground or wall tile
                    InstantiateGroundOrWallTile(x, y);
                }
            }
        }

        // Set entry and exit points

        // Add pathfinding logic here
        List<Vector2Int> path = FindPath(new Vector2Int(EntryX, 0), new Vector2Int(ExitX, (int)(RowCount - 1)));

        // Clear walls along the path
        foreach (Vector2Int pathPosition in path)
        {
            ClearWallAtPosition(pathPosition);
        }
        //Create the entry for the player
        OpenEntry(15, 0);
        OpenExit(15, 29);
        //Protect the entry to prevent a wall from being on them
        DestroyWall(new Vector2Int(15, 0));
        DestroyWall(new Vector2Int(15, 29));
        //Protect the entry from having a wall right in front of them
        DestroyWall(new Vector2Int(15, 1));
        DestroyWall(new Vector2Int(14, 1));
        DestroyWall(new Vector2Int(16, 1));

        DestroyWall(new Vector2Int(14, 28));
        DestroyWall(new Vector2Int(15, 28));
        DestroyWall(new Vector2Int(16, 28));



        //Since we removed the wall, there will be a gap
        InstantiateGroundTile(14, 1);
        InstantiateGroundTile(15, 1);
        InstantiateGroundTile(16, 1);

        InstantiateGroundTile(14, 28);
        InstantiateGroundTile(15, 28);
        InstantiateGroundTile(16, 28);

        GenerateGems();
    }

    private void GenerateGems()
    {
        for (int x = 1; x < ColumnCount - 1; x++)
        {
            for (int y = 1; y < RowCount - 1; y++)
            {
                if (UnityEngine.Random.value < GemProbability)
                {
                    // Determine which gem prefab to instantiate
                    GameObject gemPrefab = AvailableGems[UnityEngine.Random.Range(0, AvailableGems.Length)];

                    // Instantiate the gem at the current position
                    Vector2Int gridPos = new Vector2Int(x, y);
                    Vector3 worldPos = GridToWorld(gridPos);
                    Instantiate(gemPrefab, worldPos, Quaternion.identity);
                }
            }
        }
    }
    private List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
    {
        // Implement your pathfinding algorithm here (e.g., A*)

        // Placeholder: For simplicity, return a straight path for now
        List<Vector2Int> path = new List<Vector2Int>();
        for (int x = start.x; x <= end.x; x++)
        {
            path.Add(new Vector2Int(x, start.y));
        }
        return path;
    }

    private void DestroyWall(Vector2Int position)
    {
        Tile tile = GetTile(new Vector2(position.x, position.y));
        if (tile != null)
        {
            // Try to get the wall script
            Wall wallScript = tile.GetComponent<Wall>();
            Destroy(tile.gameObject);
        }
        }
    private void ClearWallAtPosition(Vector2Int position)
    {
        // Clear the wall at the specified grid position
        Tile tile = GetTile(new Vector2(position.x, position.y));
        if (tile != null && Array.Exists(AvailableWallTiles, wallTile => wallTile == tile.gameObject))
        {
            Destroy(tile.gameObject);
        }
    }
    private void InstantiateGroundOrWallTile(int x, int y)
    {
        Vector2Int gridPos = new Vector2Int(x, y);
        Vector3 worldPos = GridToWorld(gridPos);

        // Determine whether to instantiate a ground or wall tile
        GameObject tilePrefab;
        if (UnityEngine.Random.value < WallProbability)
        {
            tilePrefab = AvailableWallTiles[UnityEngine.Random.Range(0, AvailableWallTiles.Length)];
        }
        else
        {
            tilePrefab = AvailableTiles[UnityEngine.Random.Range(0, AvailableTiles.Length)];
        }

        // Instantiate the selected tile
        GameObject tileGO = Instantiate(tilePrefab, worldPos, Quaternion.identity);
        tileGO.transform.parent = transform;

        // Set the tile's grid coordinates
        Tile tile = tileGO.GetComponent<Tile>();
        tile.x = (uint)x;
        tile.y = (uint)y;
        m_Tiles.Add(tile);
    }

    private void InstantiateWallTile(int x, int y)
    {
        Vector2Int gridPos = new Vector2Int(x, y);
        Vector3 worldPos = GridToWorld(gridPos);

        // Instantiate a wall tile
        GameObject tilePrefab = AvailableWallTiles[UnityEngine.Random.Range(0, AvailableWallTiles.Length)];
        GameObject tileGO = Instantiate(tilePrefab, worldPos, Quaternion.identity);
        tileGO.transform.parent = transform;

        // Set the tile's grid coordinates
        Tile tile = tileGO.GetComponent<Tile>();
        tile.x = (uint)x;
        tile.y = (uint)y;
        m_Tiles.Add(tile);
    }
    private int FindOpenPosition(int startX, int y, int range)
    {
        int x = startX;

        for (int i = 0; i < range; i++)
        {
            // Check if the current position is not occupied by a wall
            if (!IsWall(x, y))
            {
                return x;
            }

            // Move to the next position
            x = (x + 1) % (int)(ColumnCount);
        }

        // If no open position is found within the range, return the original position
        return startX;
    }

    private void SetEntryAndExitPoints()
    {
        // Assuming that exit is on the opposite side of the grid (bottom)
        int exitX = UnityEngine.Random.Range(1, (int)(ColumnCount - 1));

        // Set entry point (fixed to the top row)
        int entryX = (int)(ColumnCount) / 2;  // You can adjust this to be the desired fixed entry point
        Debug.Log("Entry point coordinates: " + entryX + ", 0");

        // Manually set entry point
        InstantiateGroundTile(entryX, 0);

        // Manually set exit point
        InstantiateGroundTile(exitX, (int)(RowCount - 1));
    }
    
    private bool IsWall(int x, int y)
    {
        // Check if the specified grid position contains a wall
        Tile tile = GetTile(new Vector2(x, y));
        return tile != null && Array.Exists(AvailableWallTiles, wallTile => wallTile == tile.gameObject);
    }

    private void InstantiateGroundTile(int x, int y)
    {
        Vector2Int gridPos = new Vector2Int(x, y);
        Vector3 worldPos = GridToWorld(gridPos);

        // Instantiate a ground tile
        GameObject tilePrefab = AvailableTiles[UnityEngine.Random.Range(0, AvailableTiles.Length)];
        GameObject tileGO = Instantiate(tilePrefab, worldPos, Quaternion.identity);
        tileGO.transform.parent = transform;

        // Set the tile's grid coordinates
        Tile tile = tileGO.GetComponent<Tile>();
        tile.x = (uint)x;
        tile.y = (uint)y;
        m_Tiles.Add(tile);
        ClearWallAtPosition(gridPos);
    }


    private void ClearGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        m_Tiles.Clear();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = m_GridColor;
        //Gizmos.DrawLine(new Vector3(0,0,0), new Vector3(0,1,0));


        // Dessiner ligne vertical
        for (int i = 0; i < ColumnCount + 1; i++)
        {
            float t_lenght = RowCount * CellSize;
            Vector3 t_From = new Vector3(i * CellSize, 0, 0) + transform.position;
            Vector3 t_To = new Vector3(i * CellSize, t_lenght, 0) + transform.position;

            Gizmos.DrawLine(t_From, t_To);
        }
        for (int i = 0; i < RowCount + 1; i++)
        {
            float t_lenght = ColumnCount * CellSize;
            Vector3 t_From = new Vector3(0, i * CellSize, 0) + transform.position;
            Vector3 t_To = new Vector3(t_lenght, i * CellSize, 0) + transform.position;
            Gizmos.DrawLine(t_From, t_To);
        }
    }

    public Vector3 GridToWorld(Vector2Int a_GridPos)
    {
        if (a_GridPos.x >= ColumnCount || a_GridPos.x < 0)
        {
            throw new GridException("X Is Out Of Grid");
        }
        else if (a_GridPos.y >= RowCount || a_GridPos.y < 0)
        {
            throw new GridException("Y Is Out Of Grid");
        }

        float t_PosX = (a_GridPos.x + 0.5f) * CellSize;
        float t_PosY = (a_GridPos.y + 0.5f) * CellSize;

        return new Vector3(t_PosX, t_PosY, 0) + transform.position;
    }

    public Vector2Int WorldToGrid(Vector3 a_WorldPos)
    {
        //Ramène la grille à 0,0
        a_WorldPos -= transform.position;

        //Ramène le CellSize à 1
        a_WorldPos /= CellSize;

        int t_PosX = Mathf.FloorToInt(a_WorldPos.x);
        int t_PosY = Mathf.FloorToInt(a_WorldPos.y);

        if (t_PosX >= ColumnCount || t_PosX < 0)
        {
            throw new GridException("X Is Out Of Grid");
        }
        else if (t_PosY >= RowCount || t_PosY < 0)
        {
            throw new GridException("Y Is Out Of Grid");
        }

        return new Vector2Int(t_PosX, t_PosY);
    }
    public Tile GetTile(Vector2 a_Pos)
    {

        return m_Tiles.FirstOrDefault(t => t.x == a_Pos.x && t.y == a_Pos.y);
    }

    private void OpenEntry(int x, int y)
    {
        Vector2Int gridPos = new Vector2Int(x, y);
        Vector3 worldPos = GridToWorld(gridPos);


        // Instantiate a wall tile
        GameObject tilePrefab = m_TileEntry.gameObject;
        GameObject tileGO = Instantiate(tilePrefab, worldPos, Quaternion.identity);
        tileGO.transform.parent = transform;

        // Set the tile's grid coordinates
        Tile tile = tileGO.GetComponent<Tile>();
        tile.x = (uint)x;
        tile.y = (uint)y;
        m_Tiles.Add(tile);
    }
    private void OpenExit(int x, int y)
    {
        Vector2Int gridPos = new Vector2Int(x, y);
        Vector3 worldPos = GridToWorld(gridPos);


        // Instantiate a wall tile
        GameObject tilePrefab = m_TileExit.gameObject;
        GameObject tileGO = Instantiate(tilePrefab, worldPos, Quaternion.identity);
        tileGO.transform.parent = transform;

        // Set the tile's grid coordinates
        Tile tile = tileGO.GetComponent<Tile>();
        tile.x = (uint)x;
        tile.y = (uint)y;
        m_Tiles.Add(tile);
    }
}
