using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction { Up, Down, Left, Right }
public class TileManager : MonoBehaviour, ISingleton
{
    public Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();
    public void Init() {}
    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => ClearTiles();
    public void ClearTiles() => tiles.Clear();
    
    public void RegisterTile(Tile tile)
    {
        Vector2Int position = new Vector2Int(tile.gridX, tile.gridY);
        if (tiles.ContainsKey(position))
            return;
        
        tiles[position] = tile;
        Debug.Log($"Tile registered at position {position}");
    }

    public Tile GetTileAt(int x, int y)
    {
        Vector2Int position = new Vector2Int(x, y);
        if (tiles.TryGetValue(position, out Tile tile))
        {
            Debug.Log($"Tile found at position {position}");
            return tile;
        }
        else
        {
            Debug.LogError($"No tile found at position {position}");
            return null; 
        }
    }
}
