using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a tile on the map.
/// </summary>
public class Tile : MonoBehaviour
{
    private List<BaseObject> tileObjects = new List<BaseObject>();

    /// <summary>
    /// Add a new object to the tile.
    /// </summary>
    /// <param name="tileObject">The object to add.</param>
    public void AddTileObject(BaseObject tileObject)
    {
        tileObjects.Add(tileObject);
        tileObject.transform.SetParent(this.transform);
        tileObject.Init();
    }

    /// <summary>
    /// Get the topmost object on the tile.
    /// </summary>
    /// <returns>The topmost object.</returns>
    public BaseObject GetTopmostObject()
    {
        return tileObjects.Count > 0 ? tileObjects[tileObjects.Count - 1] : null;
    }

    /// <summary>
    /// Remove the topmost object from the tile.
    /// </summary>
    public void RemoveTopmostObject()
    {
        if (tileObjects.Count > 0)
        {
            Destroy(tileObjects[tileObjects.Count - 1].gameObject);
            tileObjects.RemoveAt(tileObjects.Count - 1);
        }
    }
}