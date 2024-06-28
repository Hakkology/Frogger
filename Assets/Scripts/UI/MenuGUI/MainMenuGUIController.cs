using UnityEngine;

public class MainMenuGUIController : IUIController
{
    enum MenuState
    {
        EntryMenu,
        LevelSelect,
        Settings
    }

    private MenuState currentState;
    private MainMenuComponent entryMenu;
    private MainMenuComponent levelSelectMenu;
    private MainMenuComponent settingsMenu;

    public MainMenuGUIController(MainMenuComponent entry, MainMenuComponent levelSelect, MainMenuComponent settings)
    {
        entryMenu = entry;
        levelSelectMenu = levelSelect;
        settingsMenu = settings;
    }

    public void Activate()
    {
        GoToMainMenu();  // Aktivasyon sırasında ana menüye git
    }

    public void Deactivate()
    {
        CloseAllMenus();  // Deaktivasyon sırasında tüm menüleri kapat
    }

    private void UpdateMenuState()
    {
        // İlgili menüyü açmadan önce tüm menüleri kapat
        switch (currentState)
        {
            case MenuState.EntryMenu:
                CloseAllMenusExcept(entryMenu);
                entryMenu.Open();
                break;
            case MenuState.LevelSelect:
                CloseAllMenusExcept(levelSelectMenu);
                levelSelectMenu.Open();
                break;
            case MenuState.Settings:
                CloseAllMenusExcept(settingsMenu);
                settingsMenu.Open();
                break;
        }
    }

    // Tüm menüleri kapatma işlemi
    private void CloseAllMenus()
    {
        entryMenu.Close();
        levelSelectMenu.Close();
        settingsMenu.Close();
    }

    private void CloseAllMenusExcept(MainMenuComponent menuToExclude)
    {
        if (menuToExclude != entryMenu) entryMenu.Close();
        if (menuToExclude != levelSelectMenu) levelSelectMenu.Close();
        if (menuToExclude != settingsMenu) settingsMenu.Close();
    }

    // Durum değişiklik metotları
    public void GoToSettings()
    {
        currentState = MenuState.Settings;
        UpdateMenuState();
    }

    public void GoToMainMenu()
    {
        currentState = MenuState.EntryMenu;
        UpdateMenuState();
    }

    public void GoToLevelSelect()
    {
        currentState = MenuState.LevelSelect;
        UpdateMenuState();
    }
}
