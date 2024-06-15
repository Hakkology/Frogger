using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class representing an object that can be placed on a tile and has a direction.
/// </summary>
public abstract class DirectionObject : DynamicObject, IDirectionObject
{
    public enum Direction { Up, Down, Left, Right }
    public Direction facingDirection;
    public void Init() 
    {
        DetermineFacingDirection();
        RotateObjectToFaceDirection();
    }

    private void DetermineFacingDirection()
    {
        var possibleDirections = new List<Direction>();

        if (position.y < 6 - 1) possibleDirections.Add(Direction.Up);
        if (position.y > 0) possibleDirections.Add(Direction.Down);
        if (position.x < 6 - 1) possibleDirections.Add(Direction.Right);
        if (position.x > 0) possibleDirections.Add(Direction.Left);

        int randomIndex = Random.Range(0, possibleDirections.Count);
        facingDirection = possibleDirections[randomIndex];
    }

    protected virtual void RotateObjectToFaceDirection()
    {
        float angle = facingDirection switch
        {
            Direction.Up => 0,
            Direction.Down => 180,
            Direction.Left => 270,
            Direction.Right => 90,
            _ => 0,
        };

        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
