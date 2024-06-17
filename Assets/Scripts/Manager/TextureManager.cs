using UnityEngine;

/// <summary>
/// Enum representing different color sets.
/// </summary>
public enum ColorSet
{
    Blue,
    Green,
    Purple,
    Red,
    Yellow
}

/// <summary>
/// The <c>TextureManager</c> class is responsible for managing and providing textures for various game objects.
/// </summary>
public class TextureManager : MonoBehaviour, ISingleton
{
    /// <summary>
    /// The texture data containing textures for different game objects.
    /// </summary>
    [SerializeField]
    private TextureData textureData;

    /// <summary>
    /// Indicates whether the TextureManager is ready.
    /// </summary>
    private bool isReady = false;

    /// <summary>
    /// Gets a value indicating whether the TextureManager is ready.
    /// </summary>
    public bool IsReady => isReady;

    /// <summary>
    /// Initializes the TextureManager and sets it as ready.
    /// </summary>
    public void Init() => isReady = true;

    /// <summary>
    /// Gets a random frog texture along with its corresponding cell texture and color set.
    /// </summary>
    /// <returns>A tuple containing the frog texture, cell texture, and color set.</returns>
    public (Texture2D frogTexture, Texture2D cellTexture, ColorSet color) GetRandomFrogTexture()
    {
        int index = Random.Range(0, textureData.frogTextureData.frogTextures.Length);
        ColorSet color = (ColorSet)index;
        Texture2D frogTexture = textureData.frogTextureData.frogTextures[index];
        Texture2D cellTexture = textureData.cellTextureData.cellTextures[index];
        return (frogTexture, cellTexture, color);
    }

    /// <summary>
    /// Gets a random grape texture along with its corresponding cell texture and color set.
    /// </summary>
    /// <returns>A tuple containing the grape texture, cell texture, and color set.</returns>
    public (Texture2D grapeTexture, Texture2D cellTexture, ColorSet color) GetRandomGrapeTexture()
    {
        int index = Random.Range(0, textureData.grapeTextureData.grapeTextures.Length);
        ColorSet color = (ColorSet)index;
        Texture2D grapeTexture = textureData.grapeTextureData.grapeTextures[index];
        Texture2D cellTexture = textureData.cellTextureData.cellTextures[index];
        return (grapeTexture, cellTexture, color);
    }

    /// <summary>
    /// Gets a random arrow texture along with its corresponding cell texture and color set.
    /// </summary>
    /// <returns>A tuple containing the arrow texture, cell texture, and color set.</returns>
    public (Texture2D arrowTexture, Texture2D cellTexture, ColorSet color) GetRandomArrowTexture()
    {
        int index = Random.Range(0, textureData.cellTextureData.cellTextures.Length);
        ColorSet color = (ColorSet)index;
        Texture2D arrowTexture = textureData.cellTextureData.cellTextures[index];
        Texture2D cellTexture = textureData.cellTextureData.cellTextures[index];
        return (arrowTexture, cellTexture, color);
    }
}
