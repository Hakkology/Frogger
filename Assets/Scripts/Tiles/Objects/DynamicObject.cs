using UnityEngine;

public class DynamicObject : BaseObject, IDynamicObject
{
    protected Tile cell; 
    public override void Interact()
    {

    }

    public void SetCell(Tile cell)
    {
        this.cell = cell;
    }

    protected virtual void HandleTextureChange(Texture2D objTexture, Texture2D cellTexture)
    {
        if (textureRenderer != null)
            textureRenderer.material.mainTexture = objTexture;
        
        if (cell != null && cell.textureRenderer != null)
            cell.UpdateTexture(cellTexture);
        
    }
}