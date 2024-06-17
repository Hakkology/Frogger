using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Represents a frog on a tile.
/// </summary>
public class Frog : DirectionObject
{
    public Transform tongueBone;
    private TongueMesh tongueMesh;
    private bool isAnimating = false;
    
    protected override void Awake() 
    {
        base.Awake();
        Debug.Log("Frog Awake called, subscribing to Texture Change.");
        textureRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        tongueMesh = GetComponentInChildren<TongueMesh>();
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
            Debug.Log("Starting tongue animation.");
            tongueMesh.ExtendTongue(facingDirection, 2.0f, 0.5f, () => {isAnimating = false;});
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
            Tile nextTile = tileManager.GetTileAt(currentPosition.x, currentPosition.y);
            if (nextTile == null || !IsColorMatch(nextTile.GetTopmostObject()))
                break;

            path.Add(currentPosition);

            // Check for arrows and change direction if needed
            Arrow arrow = nextTile.GetComponent<Arrow>();
            if (arrow != null && arrow.GetColorSet() == this.colorSet)
                currentDirection = arrow.facingDirection;
            
        }

        return path;
    }
}
