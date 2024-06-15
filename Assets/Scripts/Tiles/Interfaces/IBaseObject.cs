using UnityEngine;

/// <summary>
/// Defines the basic functionalities for an object that can be placed on a tile.
/// </summary>
public interface IBaseObject
{
    /// <summary>
    /// Handles the interaction with the tile object.
    /// </summary>
    void Interact();

    /// <summary>
    /// Sets the position of the object on the map.
    /// </summary>
    /// <param name="position">The Vector2Int representing the position of the object on the map.</param>
    void SetPosition(Vector2Int position);
}
