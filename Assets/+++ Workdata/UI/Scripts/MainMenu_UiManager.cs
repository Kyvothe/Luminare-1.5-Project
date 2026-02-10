using System;
using UnityEngine;

public class MainMenu_UiManager : MonoBehaviour
{
    public GameObject mainMenuContainer;
    public GameObject optionsMenuContainer;
    public GameObject creditsMenuContainer;

    private GameObject _currentMenu;
    
    // Methoden werden über entsprechende Methoden von MainMenu_ButtpManager aufgerufen

    private void Awake()
    {
        _currentMenu = mainMenuContainer;
    }

    public void OpenOptionsMenu()
    {
        _currentMenu.SetActive(false);
        optionsMenuContainer.SetActive(true);
        
        _currentMenu = optionsMenuContainer;
    }
    
    public void OpenCreditsMenu()
    {
        _currentMenu.SetActive(false);
        creditsMenuContainer.SetActive(true);
        
        _currentMenu = creditsMenuContainer;
    }

    public void OpenMainMenu()
    {
        _currentMenu.SetActive(false);
        mainMenuContainer.SetActive(true);
        
        _currentMenu = mainMenuContainer;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    
}