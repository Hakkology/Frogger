using UnityEngine;

/// <summary>
/// Abstract class representing an object that can be placed on a tile.
/// </summary>
public abstract class BaseObject : MonoBehaviour
{
    public Color objectColor;

    /// <summary>
    /// Initialize the tile object with its specific properties.
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// Handles the interaction with the tile object.
    /// </summary>
    public abstract void Interact();
}