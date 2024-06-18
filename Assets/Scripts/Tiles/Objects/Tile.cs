using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a tile on the map.
/// </summary>
public class Tile : BaseObject, ITileObject
{
    public int gridX, gridY;
    public List<BaseObject> ObjectsOnTile = new List<BaseObject>();
    protected override void Awake() 
    {
        base.Awake();
        textureRenderer = GetComponent<Renderer>();
    } 

    public void Initialize(int x, int y)
    {
        gridX = x;
        gridY = y;
    }
    public override void Interact()
    {
        
    }
    public void AddTileObject(BaseObject tileObject)
    {
        ObjectsOnTile.Add(tileObject);
        tileObject.transform.SetParent(this.transform);

        if (tileObject is DynamicObject dynamicObject)
            dynamicObject.SetCell(this);
        
        if (tileObject is DirectionObject directionObject)
            directionObject.Init();
        
    }
    public BaseObject GetTopmostObject()
    {
        if (ObjectsOnTile.Count > 0)
        {
            Debug.Log($"Topmost object on Tile {gridX}, {gridY} is {ObjectsOnTile[ObjectsOnTile.Count - 1].name}");
            return ObjectsOnTile[ObjectsOnTile.Count - 1];
        }
        else
        {
            Debug.Log($"No objects on Tile {gridX}, {gridY}");
            return null;
        }
    }
        
    public void RemoveTopmostObject()
    {
        if (ObjectsOnTile.Count > 0)
        {
            Destroy(ObjectsOnTile[ObjectsOnTile.Count - 1].gameObject);
            ObjectsOnTile.RemoveAt(ObjectsOnTile.Count - 1);
        }
    }
    public override void UpdateTexture(Texture2D newTexture) {
        if (textureRenderer != null && textureRenderer.materials.Length > 1)
        {
            Material[] materials = textureRenderer.materials;
            materials[0].mainTexture = newTexture;
            textureRenderer.materials = materials;
        }
        else
        {
            Debug.LogError("Renderer or materials array is not properly set up.");
        }
    }


}