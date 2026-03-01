using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.EventSystems;

public class OpenDialogueInGame : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    private InputAction _pauseAction;
    
    public GameObject pauseMenu;
    public GameObject GameOverScreen;
    public GameObject EndOfGameMenu;
    public GameObject SureScreen;
    
    public GameObject Player;
    
    private GameObject _currentMenu;

    private bool _isPaused = false;
    private bool _noOtherMenuActive = true;
    private bool _anyMenuActive = false;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _pauseAction = _inputActions.UI.Pause;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _pauseAction.performed += Pause;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _pauseAction.performed -= Pause;
    }
    
    private void Pause(InputAction.CallbackContext ctx)                                                                 // PauseMenu       
    {
        if (!_isPaused && _noOtherMenuActive)                                                                           // Erster Druck auf Escape --> Oeffnen; es darf kein anderes Menu offen sein
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;                                                                                         // Spiel gefreezed
            _isPaused = true;
            Player.GetComponent<PlayerController>().SetPaused(true);
            FreezePlayer(true);
            
            _anyMenuActive = true;
            _currentMenu = pauseMenu;
        }
       else if (_isPaused && _noOtherMenuActive)                                                                        // Zweiter Druck auf Escape --> Schließen; es darf kein anderes Menu offen sein
        {
            pauseMenu.SetActive(false); 
            Time.timeScale = 1;                                                                                         // Spiel läuft wieder
            _isPaused = false;
            _anyMenuActive = false;
            _currentMenu = null;
            Player.GetComponent<PlayerController>().SetPaused(false);
            FreezePlayer(false);
        } 
    }
    
    public void Button_Resume()                                                                                         // Button im Pause Menu als zweiter Weg zum Schließen
    {
        _currentMenu.SetActive(false); 
        Time.timeScale = 1; 
        _isPaused = false;
        _anyMenuActive = false;
        _currentMenu = null;
        _noOtherMenuActive = true;
        Player.GetComponent<PlayerController>().SetPaused(false);
        FreezePlayer(false);
    }
    
    public void OpenGameOverScreen()                                                                                    // Aufgerufen wenn Player dead über GameOver() in PlayerInformation
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0; 
        _isPaused = true;
        _noOtherMenuActive = false;
        _anyMenuActive = true;
        _currentMenu = GameOverScreen;
        FreezePlayer(true);
    }

    public void OpenEndOfGameMenu()                                                                                     // Aufgerufen wenn Finish Zone erreicht über PlayerZoneCheck
    {
        EndOfGameMenu.SetActive(true);
        _noOtherMenuActive = false;
        Time.timeScale = 0; 
        Player.GetComponent<PlayerController>().SetPaused(true);
        _currentMenu = EndOfGameMenu;
        _anyMenuActive = true;
        FreezePlayer(true);
    }
    
    public void OpenAreYouSure()                                                                                        // Aufgerufen wenn Player dead über GameOver() in PlayerInformation
    {
        _currentMenu.SetActive(false);
        SureScreen.SetActive(true);
        Time.timeScale = 0; 
        _isPaused = true;
        _noOtherMenuActive = false;
        _anyMenuActive = true;
        _currentMenu = SureScreen;
    }

    private void FreezePlayer(bool value)                                                                               // Freeze Bewegung von Player
    {
        Player.GetComponent<PlayerController>().ToggleInput(value);
    }
}