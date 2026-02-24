using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.EventSystems;

public class PreStory_UIManager : MonoBehaviour
{
    public GameObject[] frames = new GameObject[5]; 

    private GameObject _currentMenu;

    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    public GameObject Button5;

    private int _count;

    private void Awake()
    {
        _currentMenu = frames[0];
        _count = 0;
    }
    
    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame && EventSystem.current.currentSelectedGameObject == null)
        { 
            //EventSystem.current.SetSelectedGameObject(_currentMenu.GetComponent<DefaultButtonSetter>().ReturnButton());

            if (_count == 0)
            {
                EventSystem.current.SetSelectedGameObject(Button1);
            }
            
            if (_count == 1)
            {
                EventSystem.current.SetSelectedGameObject(Button2);
            }
            
            if (_count == 2)
            {
                EventSystem.current.SetSelectedGameObject(Button3);
            }
            
            if (_count == 3)
            {
                EventSystem.current.SetSelectedGameObject(Button4);
            }
            
            if (_count == 4)
            {
                EventSystem.current.SetSelectedGameObject(Button5);
            }
        }
    }

    public void Continue()
    {
        _currentMenu.SetActive(false);
        _count++;
        _currentMenu = frames[_count];
        _currentMenu.SetActive(true);
    }
   
}