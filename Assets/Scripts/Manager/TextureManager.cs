using UnityEngine;
using UnityEngine.Events;

public enum ColorSet{
    Blue,
    Green,
    Purple,
    Red,
    Yellow
}

public class TextureManager : MonoBehaviour, ISingleton
{
    [SerializeField]
    private TextureData textureData;

    public void Init()
    {

    }

    public (Texture2D frogTexture, Texture2D cellTexture, ColorSet color) GetRandomFrogTexture()
    {
        int index = Random.Range(0, textureData.frogTextureData.frogTextures.Length);
        ColorSet color = (ColorSet)index;
        Texture2D frogTexture = textureData.frogTextureData.frogTextures[index];
        Texture2D cellTexture = textureData.cellTextureData.cellTextures[index];
        return (frogTexture, cellTexture, color);
    }

    public (Texture2D grapeTexture, Texture2D cellTexture, ColorSet color) GetRandomGrapeTexture()
    {
        int index = Random.Range(0, textureData.grapeTextureData.grapeTextures.Length);
        ColorSet color = (ColorSet)index;
        Texture2D grapeTexture = textureData.grapeTextureData.grapeTextures[index];
        Texture2D cellTexture = textureData.cellTextureData.cellTextures[index];
        return (grapeTexture, cellTexture, color);
    }
    public (Texture2D arrowTexture, Texture2D cellTexture, ColorSet color) GetRandomArrowTexture()
    {
        int index = Random.Range(0, textureData.cellTextureData.cellTextures.Length);
        ColorSet color = (ColorSet)index;
        Texture2D arrowTexture = textureData.cellTextureData.cellTextures[index];
        Texture2D cellTexture = textureData.cellTextureData.cellTextures[index];
        return (arrowTexture, cellTexture, color);
    }
}