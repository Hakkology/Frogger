using UnityEngine;

public class GameManager : MonoBehaviour, ISingleton
{
    private bool isReady = false;
    public bool IsReady => isReady;

    public void Init()
    {
        Debug.Log("GameManager initialized");
    }
}