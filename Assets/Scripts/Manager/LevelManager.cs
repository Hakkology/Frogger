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
    private GameObject squareTilePrefab;

    /// <summary>
    /// Initializes the LevelManager by loading the square tile prefab.
    /// </summary>
    public void Init()
    {
        squareTilePrefab = Resources.Load<GameObject>("Prefabs/Objects/Cell");
        if (squareTilePrefab == null)
        {
            Debug.LogError("Cell prefab could not be loaded from Resources/Prefabs/Objects/Cell");
        }

        //GenerateLevel(LevelSize.FiveByFive);
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
                Instantiate(squareTilePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}