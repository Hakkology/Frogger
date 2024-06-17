using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Represents a frog on a tile.
/// </summary>
public class Frog : DirectionObject
{
    public Transform tongueBone;
    private TongueManager tongue;
    private bool isAnimating = false;
    
    protected override void Awake() 
    {
        base.Awake();
        Debug.Log("Frog Awake called, subscribing to Texture Change.");
        textureRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        tongue = GetComponentInChildren<TongueManager>();
    } 

    private void Start() 
    {
        var textures = textureManager.GetRandomFrogTexture();
        HandleTextureChange(textures.frogTexture, textures.cellTexture, textures.color);
    }

    public override void Interact()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            List<Vector2Int> travelCoordinates = GetPath();
            Debug.Log("Starting tongue animation.");
            tongue.ExtendTongue(travelCoordinates, () => {isAnimating = false;});
            tongueBone.DOLocalRotate(new Vector3(0, 0, -100), 0.5f).SetLoops(2, LoopType.Yoyo);
        }
    }

    private List<Vector2Int> GetPath()
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int currentPosition = new Vector2Int(cell.gridX, cell.gridY);
        Direction currentDirection = facingDirection;

        while (true)
        {
            currentPosition += DirectionToVector(currentDirection);

            if (tileManager == null)
                tileManager = SingletonManager.GetSingleton<TileManager>();
            
            Tile nextTile = tileManager.GetTileAt(currentPosition.x, currentPosition.y);
            if (nextTile == null)
            {
                Debug.Log("Reached a boundary or non-existent tile.");
                break;
            }
            
            BaseObject topmostObject = nextTile.GetTopmostObject();
            // If its a grape of the same colour.
            if (topmostObject is Grape grape && IsColorMatch(grape))
            {
                Debug.Log($"Adding position {currentPosition} with matching grape to path.");
                path.Add(currentPosition);
            }
            // If its an arrow with the same colour and it needs to continue on a different path.
            else if (topmostObject is Arrow arrow && arrow.GetColorSet() == this.colorSet)
            {
                Debug.Log($"Direction change to {arrow.facingDirection} due to arrow at position {currentPosition}");
                currentDirection = arrow.facingDirection;
                path.Add(currentPosition); 
                continue; 
            }
            else
            {
                Debug.Log($"No matching grape or color mismatch at position {currentPosition}");
                break;
            }
        }

        Debug.Log($"Path calculation complete. Path length: {path.Count}");
        return path;
    }
}
