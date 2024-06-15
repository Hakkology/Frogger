using UnityEngine;

/// <summary>
/// Represents a frog on a tile.
/// </summary>
public class Frog : DirectionObject
{
    protected override void Awake() 
    {
        base.Awake();
        Debug.Log("Frog Awake called, subscribing to Texture Change.");
        textureRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    } 

    private void Start() 
    {
        var textures = textureManager.GetRandomFrogTexture();
        HandleTextureChange(textures.frogTexture, textures.cellTexture);
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
