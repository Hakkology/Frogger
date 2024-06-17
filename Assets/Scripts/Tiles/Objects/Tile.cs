using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a tile on the map.
/// </summary>
public class Tile : BaseObject, ITileObject
{
    public int gridX, gridY;
    private List<BaseObject> tileObjects = new List<BaseObject>();
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
        tileObjects.Add(tileObject);
        tileObject.transform.SetParent(this.transform);

        if (tileObject is DynamicObject dynamicObject)
            dynamicObject.SetCell(this);
        
        if (tileObject is DirectionObject directionObject)
            directionObject.Init();
        
    }
    public BaseObject GetTopmostObject()
    {
        if (tileObjects.Count > 0)
        {
            Debug.Log($"Topmost object on Tile {gridX}, {gridY} is {tileObjects[tileObjects.Count - 1].name}");
            return tileObjects[tileObjects.Count - 1];
        }
        else
        {
            Debug.Log($"No objects on Tile {gridX}, {gridY}");
            return null;
        }
    }
        
    public void RemoveTopmostObject()
    {
        if (tileObjects.Count > 0)
        {
            Destroy(tileObjects[tileObjects.Count - 1].gameObject);
            tileObjects.RemoveAt(tileObjects.Count - 1);
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