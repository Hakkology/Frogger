using UnityEngine;

public class DynamicObject : BaseObject, IDynamicObject
{
    protected ColorSet colorSet;
    protected Tile cell; 
    protected Vector2Int boardPosition;
    public override void Interact()
    {

    }

    public void SetCell(Tile cell)
    {
        this.cell = cell;
        this.boardPosition = new Vector2Int(cell.gridX, cell.gridY);
    }

    protected virtual void HandleTextureChange(Texture2D objTexture, Texture2D cellTexture, ColorSet color)
    {
        this.colorSet = color;

        if (textureRenderer != null)
            textureRenderer.material.mainTexture = objTexture;
        
        if (cell != null && cell.textureRenderer != null)
            cell.UpdateTexture(cellTexture);
        
    }
    protected bool IsColorMatch(BaseObject obj)
    {
        if (obj is DynamicObject dynObj)
            return dynObj.colorSet == this.colorSet;
        return false;
    }
}