using TMPro;
using UnityEngine;

public enum GameState {
    MainMenu,
    Game
}
public class UIManager : MonoBehaviour, ISingleton
{
    private bool isReady = false;
    public bool IsReady => isReady;
    
    [Header("Canvases")]
    public GameObject MainMenuCanvas;
    public GameObject GameMenuCanvas;

    [Header("Main Menu Components")]
    public MainMenuComponent entryMenu;
    public MainMenuComponent levelSelectMenu;
    public MainMenuComponent settingsMenu;

    [Header("Game Menu Components")]
    public GameMenuComponent pauseMenu;
    public GameMenuComponent gameSettingsMenu;

    [Header("HUD Components")]
    public GameHUDScoreCounter scoreText;  
    public GameHUDMoveCounter movesText; 

    private MainMenuGUIController mainMenuGUIController;
    private GameGUIController gameGUIController;
    private GameHUDController gameHUDController;
    private GameState currentGameState;

    public void Init()
    {
        InitializeControllers();
        ChangeGameState(GameState.MainMenu);
        
    }

    private void InitializeControllers(){
        gameHUDController = new GameHUDController(scoreText, movesText);
        mainMenuGUIController = new MainMenuGUIController(entryMenu, levelSelectMenu, settingsMenu);
        gameGUIController = new GameGUIController(pauseMenu, gameSettingsMenu);

        entryMenu.SetupController(mainMenuGUIController);
        levelSelectMenu.SetupController(mainMenuGUIController);
        settingsMenu.SetupController(mainMenuGUIController);

        pauseMenu.SetupController(gameGUIController);
        gameSettingsMenu.SetupController(gameGUIController);
    }
    public void ChangeGameState(GameState newState)
    {
        currentGameState = newState;
        UpdateUIState();
    }

    private void UpdateUIState()
    {
        switch (currentGameState)
        {
            case GameState.MainMenu:
                MainMenuCanvas.SetActive(true);
                GameMenuCanvas.SetActive(false);
                mainMenuGUIController.Activate();
                gameGUIController.Deactivate();
                gameHUDController.Deactivate();
                break;
            case GameState.Game:
                MainMenuCanvas.SetActive(false);
                GameMenuCanvas.SetActive(true);
                mainMenuGUIController.Deactivate();
                gameGUIController.Activate();
                gameHUDController.Activate();
                break;
        }
    }
}

