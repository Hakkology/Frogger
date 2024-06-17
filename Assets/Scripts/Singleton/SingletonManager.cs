using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>SingletonManager</c> class is responsible for managing singleton instances in the game.
/// Loads all singleton prefabs from the Resources/Prefabs/Singletons directory.
/// </summary>
public class SingletonManager : MonoBehaviour
{
    private static Dictionary<System.Type, ISingleton> singletons = new Dictionary<System.Type, ISingleton>();
    /// <summary>
    /// Ensures the GameObject persists across scene loads and initiates the loading and instantiation of singleton prefabs.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadAndInstantiateSingletons();
    }

    /// <summary>
    /// Gets a specific type of singleton.
    /// </summary>
    public static T GetSingleton<T>() where T : class, ISingleton
    {
        if (singletons.TryGetValue(typeof(T), out ISingleton singleton))
        {
            return singleton as T;
        }
        return null;
    }

    /// <summary>
    /// Load all singleton prefabs, instantiate them under this gameobject and initialize them.
    /// </summary>
    private void LoadAndInstantiateSingletons()
    {
        GameObject[] singletonPrefabs = Resources.LoadAll<GameObject>("Prefabs/Singleton");
        foreach (var prefab in singletonPrefabs)
        {
            GameObject instance = Instantiate(prefab, this.transform);
            ISingleton singleton = instance.GetComponent<ISingleton>();
            if (singleton != null)
            {
                singletons.Add(singleton.GetType(), singleton);
                singleton.Init();
            }
        }
    }
}
