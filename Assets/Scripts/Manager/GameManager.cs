using UnityEngine;

public class GameManager : MonoBehaviour, ISingleton
{
    public void Init()
    {
        Debug.Log("GameManager initialized");
    }
}