using UnityEngine;

public class MainMenu_ButtonManager : MonoBehaviour
{
    public MainMenu_UiManager uiManager;
    public SceneLoader sceneLoader;
    
    // Gibt alles and MainMenu_UIManager weiter

    public void Button_OpenMainMenu()
    {
        uiManager.OpenMainMenu();
    }

    public void Button_OpenOptionsMenu()
    {
        uiManager.OpenOptionsMenu();
    }
    
    public void Button_OpenCreditsMenu()
    {
        uiManager.OpenCreditsMenu();
    }

    public void Button_LoadSceneByName(string sceneName)
    {
        sceneLoader.LoadSceneByName(sceneName);
    }

    public void Button_LoadSceneByIndex(int sceneIndex)
    {
        sceneLoader.LoadSceneByIndex(sceneIndex);
    }

    public void Button_QuitGame()
    {
        uiManager.QuitGame();
    }
}