using UnityEngine;

/// <summary>
/// Represents a frog on a tile.
/// </summary>
public class Frog : BaseObject
{
    public enum Direction { Up, Down, Left, Right }
    public Direction facingDirection;

    public override void Init()
    {

    }

    public override void Interact()
    {

    }

    /// <summary>
    /// Move the frog's tongue to eat berries.
    /// </summary>
    private void MoveTongue()
    {
        // Implement the logic to move the tongue in the facing direction.
    }
}
