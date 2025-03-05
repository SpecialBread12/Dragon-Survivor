using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    private Grid m_Grid;

    private void OnEnable()
    {
        m_Grid = (Grid)target;
    }

    private void OnSceneGUI()
    {
        if (Event.current.type == EventType.MouseDown && Event.current.control)
        {
            //Debug.Log($"OnSceneGUI: {Event.current}");

            GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);

            if (m_Grid.SelectedTile >= m_Grid.AvailableTiles.Length)
            {
                throw new GridException("Selected tile is out of bounds");
            }

            Ray t_Rayon = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            Vector2Int t_GridPos = m_Grid.WorldToGrid(t_Rayon.origin);
            Vector3 t_CellCenter = m_Grid.GridToWorld(t_GridPos);

            //Supprimer ancienne tuile
            Tile[] t_Tiles = m_Grid.GetComponentsInChildren<Tile>();
            //Debug.Log(t_Tiles.Length);
            foreach (Tile t_Tile in t_Tiles)
            {
                if (t_Tile.transform.position == t_CellCenter)
                {
                    Undo.DestroyObjectImmediate(t_Tile.gameObject);
                    break;
                }
            }

            GameObject t_TilePrefab = m_Grid.AvailableTiles[m_Grid.SelectedTile];
            GameObject t_NewTile = (GameObject)PrefabUtility.InstantiatePrefab(t_TilePrefab, m_Grid.transform);
            Undo.RegisterCreatedObjectUndo(t_NewTile, "Tile created");
            t_NewTile.transform.position = t_CellCenter;


            Sprite t_Sprite = t_NewTile.GetComponent<SpriteRenderer>().sprite;
            float t_Scale = m_Grid.CellSize / t_Sprite.bounds.size.x;
            t_NewTile.transform.localScale = new Vector3(t_Scale, t_Scale, t_Scale);
        }

    }
}
