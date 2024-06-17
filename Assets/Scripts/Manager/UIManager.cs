using UnityEngine;

public class UIManager : MonoBehaviour, ISingleton
{
    private bool isReady = false;
    public bool IsReady => isReady;

    public void Init()
    {
        Debug.Log("UIManager initialized");
    }
}