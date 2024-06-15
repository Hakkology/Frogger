using UnityEngine;
using UnityEngine.Events;

public class TextureManager : MonoBehaviour, ISingleton
{
    [SerializeField]
    private TextureData textureData;

    public void Init()
    {

    }

    public (Texture2D frogTexture, Texture2D cellTexture) GetRandomFrogTexture()
    {
        int index = Random.Range(0, textureData.frogTextureData.frogTextures.Length);
        Texture2D frogTexture = textureData.frogTextureData.frogTextures[index];
        Texture2D cellTexture = textureData.cellTextureData.cellTextures[index];
        return (frogTexture, cellTexture);
    }

    public (Texture2D grapeTexture, Texture2D cellTexture) GetRandomGrapeTexture()
    {
        int index = Random.Range(0, textureData.grapeTextureData.grapeTextures.Length);
        Texture2D grapeTexture = textureData.grapeTextureData.grapeTextures[index];
        Texture2D cellTexture = textureData.cellTextureData.cellTextures[index];
        return (grapeTexture, cellTexture);
    }
}