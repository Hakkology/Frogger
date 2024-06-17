using UnityEngine;
using DG.Tweening;

/// <summary>
/// Represents an arrow on a tile.
/// </summary>
public class Arrow : DirectionObject
{
    protected override void Awake() 
    {
        base.Awake();
        textureRenderer = GetComponentInChildren<Renderer>();
    }

    private void Start() 
    {
        var textures = textureManager.GetRandomArrowTexture();
        HandleTextureChange(textures.cellTexture, textures.cellTexture, textures.color);
    }
    public override void Interact()
    {
        
    }

    public ColorSet GetColorSet()
    {
        return this.colorSet;
    }
}