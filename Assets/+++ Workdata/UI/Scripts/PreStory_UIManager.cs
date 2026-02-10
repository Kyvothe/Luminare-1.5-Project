using UnityEngine;

public class PreStory_UIManager : MonoBehaviour
{
    public GameObject[] frames = new GameObject[5]; 

    private GameObject _currentMenu;

    private int _count;

    private void Awake()
    {
        _currentMenu = frames[0];
        _count = 0;
    }

   public void Continue()
    {
        _currentMenu.SetActive(false);
        _count++;
        _currentMenu = frames[_count];
        _currentMenu.SetActive(true);
    }
   
}
