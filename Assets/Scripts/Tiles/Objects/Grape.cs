using UnityEngine;

/// <summary>
/// Represents a Grape on a tile.
/// </summary>
public class Grape : DynamicObject
{
    protected override void Awake() 
    {
        base.Awake();
        textureRenderer = GetComponentInChildren<Renderer>();
    } 

    private void Start() 
    {
        var textures = textureManager.GetRandomGrapeTexture();
        HandleTextureChange(textures.grapeTexture, textures.cellTexture);
    }
    public override void Interact()
    {

    }
}