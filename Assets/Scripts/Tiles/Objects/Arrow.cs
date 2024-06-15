using UnityEngine;
using DG.Tweening;

/// <summary>
/// Represents an arrow on a tile.
/// </summary>
public class Arrow : DirectionObject
{
    public Texture2D arrowTexture;
    protected override void Awake() 
    {
        base.Awake();
        textureRenderer = GetComponent<Renderer>();
    }

    private void Start() 
    {
        var textures = textureManager.GetRandomFrogTexture();
        HandleTextureChange(textures.frogTexture, textures.cellTexture);
    }
    public override void Interact()
    {
        
    }

    protected override void HandleTextureChange(Texture2D objTexture, Texture2D cellTexture)
    {
        if (textureRenderer != null)
            textureRenderer.material.mainTexture = objTexture;
        
        if (cell != null && cell.textureRenderer != null)
            cell.UpdateTexture(cellTexture);
            cell.textureRenderer.materials[1].mainTexture = arrowTexture;
    }

    protected override void RotateObjectToFaceDirection()
    {
        float angle = facingDirection switch
        {
            Direction.Up => 0,
            Direction.Down => 180,
            Direction.Left => 270,
            Direction.Right => 90,
            _ => 0,
        };

        transform.rotation = Quaternion.Euler(90, angle, 0);
    }
}