using DG.Tweening;
using UnityEngine;

public enum LevelSize
{
    FiveByFive,
    FiveBySix,
    SixBySix
}

/// <summary>
/// The <c>LevelManager</c> class is responsible for generating levels based on the given size.
/// It uses prefabs from the Resources/Prefabs/Objects directory to create the level grid.
/// </summary>
public class LevelManager : MonoBehaviour, ISingleton
{
    private GameObject cellPrefab;
    private GameObject tilePrefab;

    /// <summary>
    /// Initializes the LevelManager by loading the square tile prefab.
    /// </summary>
    public void Init()
    {
        cellPrefab = Resources.Load<GameObject>("Prefabs/Objects/Cell");
        tilePrefab = Resources.Load<GameObject>("Prefabs/Objects/TileObject");
        if (cellPrefab == null)
        {
            Debug.LogError("Cell prefab could not be loaded from Resources/Prefabs/Objects/Cell");
        }
        if (tilePrefab == null)
        {
            Debug.LogError("Tile prefab could not be loaded from Resources/Prefabs/Objects/TileObject");
        }

        GenerateLevel(LevelSize.SixBySix);
    }

    /// <summary>
    /// Generates a level based on the specified size.
    /// </summary>
    /// <param name="size">The size of the level to generate.</param>
    public void GenerateLevel(LevelSize size)
    {
        (int rows, int cols) = GetLevelDimensions(size);

        ClearExistingLevel();

        CreateNewLevel(rows, cols);
    }

    /// <summary>
    /// Determines the number of rows and columns for the specified level size.
    /// </summary>
    /// <param name="size">The size of the level.</param>
    /// <returns>A tuple containing the number of rows and columns.</returns>
    private (int, int) GetLevelDimensions(LevelSize size)
    {
        int rows = 0, cols = 0;
        switch (size)
        {
            case LevelSize.FiveByFive:
                rows = 5;
                cols = 5;
                break;
            case LevelSize.FiveBySix:
                rows = 5;
                cols = 6;
                break;
            case LevelSize.SixBySix:
                rows = 6;
                cols = 6;
                break;
        }
        return (rows, cols);
    }

    /// <summary>
    /// Clears the existing level by destroying all child objects.
    /// </summary>
    private void ClearExistingLevel()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Creates a new level with the specified number of rows and columns.
    /// </summary>
    /// <param name="rows">The number of rows in the level.</param>
    /// <param name="cols">The number of columns in the level.</param>
    private void CreateNewLevel(int rows, int cols)
    {

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 position = new Vector3(col, 0, row);
                GameObject cellInstance = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                Tile tile = cellInstance.GetComponent<Tile>();
                if (tile != null)
                {
                    tile.Initialize(col, row);
                    AddRandomTileObject(tile);
                }
                else
                {
                    Debug.LogError("Tile component could not be found on the instantiated cellPrefab.");
                }
            }
        }
    }


    /// <summary>
    /// Adds a random tile object to the specified tile.
    /// </summary>
    /// <param name="tile">The tile to add the object to.</param>
    private void AddRandomTileObject(Tile tile)
    {
        if (tile == null)
        {
            Debug.LogError("Tile is null in AddRandomTileObject");
            return;
        }

        GameObject tileObjectPrefab = SelectRandomTileObject();

        if (tileObjectPrefab != null)
        {
            SpawnTileObject(tile, tileObjectPrefab);
        }
    }

    /// <summary>
    /// Selects a random tile object prefab (Frog, Arrow, or Grape).
    /// </summary>
    /// <returns>The selected tile object prefab.</returns>
    private GameObject SelectRandomTileObject()
    {
        int randomObject = Random.Range(0, 3);
        GameObject tileObjectPrefab = null;

        switch (randomObject)
        {
            case 0:
                tileObjectPrefab = Resources.Load<GameObject>("Prefabs/Objects/Frog");
                break;
            case 1:
                tileObjectPrefab = Resources.Load<GameObject>("Prefabs/Objects/Grape");
                break;
            case 2:
                tileObjectPrefab = Resources.Load<GameObject>("Prefabs/Objects/Arrow");
                break;
        }

        if (tileObjectPrefab == null)
        {
            Debug.LogError($"Tile object prefab could not be loaded for randomObject: {randomObject}");
        }

        return tileObjectPrefab;
    }

    /// <summary>
    /// Spawns a tile object on the specified tile with a growth animation.
    /// </summary>
    /// <param name="tile">The tile to add the object to.</param>
    /// <param name="tileObjectPrefab">The tile object prefab to instantiate.</param>
    private void SpawnTileObject(Tile tile, GameObject tileObjectPrefab)
    {
        Vector3 position = tile.transform.position + Vector3.up*.5f;
        GameObject tileObjectInstance = Instantiate(tileObjectPrefab, position, Quaternion.identity, tile.transform);
        tileObjectInstance.transform.localScale = Vector3.zero;
        tileObjectInstance.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        BaseObject tileObject = tileObjectInstance.GetComponent<BaseObject>();
        if (tileObject == null)
        {
            Debug.LogError("BaseObject component could not be found on the instantiated tileObjectPrefab.");
            return;
        }

        tile.AddTileObject(tileObject);
    }
}