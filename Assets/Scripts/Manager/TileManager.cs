using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction { Up, Down, Left, Right }
public class TileManager : MonoBehaviour, ISingleton
{
    public Dictionary<Vector2Int, Tile> tiles;
    public void Init() => tiles = new Dictionary<Vector2Int, Tile>();
    private void Start() {
        
    }
    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => ClearTiles();
    public void ClearTiles() => tiles.Clear();
    
    public void RegisterTile(Tile tile)
    {
        Vector2Int position = new Vector2Int(tile.gridX, tile.gridY);
        if (tiles.ContainsKey(position))
        {
            Debug.LogError($"Tile at {position} is already registered.");
            return;
        }
        tiles[position] = tile;
        Debug.Log($"Registered tile at {position.x}, {position.y}");
    }

    public Tile GetTileAt(int x, int y)
    {
        Vector2Int position = new Vector2Int(x, y);
        if (tiles.TryGetValue(position, out Tile tile))
        {
            return tile;
        }
        return null;
    }
}
