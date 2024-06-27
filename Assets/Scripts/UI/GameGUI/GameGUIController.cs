using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGUIController : IUIController
{
    enum GameMenuState
    {
        Play,
        Pause,
        Settings
    }

    private GameMenuState currentState;
    private GameMenuComponent pauseMenu;
    private GameMenuComponent gameSettingsMenu;

    public GameGUIController(GameMenuComponent pause, GameMenuComponent settings)
    {
        pauseMenu = pause;
        gameSettingsMenu = settings;
    }

    public void Activate() => SetPlayMode();
    public void Deactivate() => CloseAllMenus();  
    private void UpdateMenuState()
    {
        CloseAllMenus();

        switch (currentState)
        {
            case GameMenuState.Pause:
                pauseMenu.Open();
                break;
            case GameMenuState.Settings:
                gameSettingsMenu.Open();
                break;
            case GameMenuState.Play:
                break;
        }
    }

    private void CloseAllMenus()
    {
        pauseMenu.Close();
        gameSettingsMenu.Close();
    }

    // Durum değişiklik metotları
    public void OpenSettingsMenu()
    {
        currentState = GameMenuState.Settings;
        UpdateMenuState();
    }

    public void OpenPauseMenu()
    {
        currentState = GameMenuState.Pause;
        UpdateMenuState();
    }
    public void SetPlayMode()
    {
        currentState = GameMenuState.Play;
        UpdateMenuState();
    }

    public void GoToMainMenu() => SceneManager.LoadScene("MainMenu");
    
}
