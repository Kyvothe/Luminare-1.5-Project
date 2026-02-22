using UnityEngine;

public class RatGangDialogue : MonoBehaviour
{
    public GameObject tedDialogue;
    public GameObject shadowDialogue;
    public GameObject feliciaDialogue;
    public GameObject bobDialogue;
    public GameObject swaggerDialogue;

    private GameObject _currentDialogue;

    private void Awake()
    {
        _currentDialogue = tedDialogue;
    }
    
    public void OpenTed()
    {
        _currentDialogue = tedDialogue;
        _currentDialogue.SetActive(true);
    }
    
    public void OpenShadow()
    {
        _currentDialogue.SetActive(false);
        _currentDialogue = shadowDialogue;
        _currentDialogue.SetActive(true);
    }
    
    public void OpenFelicia()
    {
        _currentDialogue.SetActive(false);
        _currentDialogue = feliciaDialogue;
        _currentDialogue.SetActive(true);
    }
    
    public void OpenBob()
    {
        _currentDialogue.SetActive(false);
        _currentDialogue = bobDialogue;
        _currentDialogue.SetActive(true);
    }
    
    public void OpenSwagger()
    {
        _currentDialogue.SetActive(false);
        _currentDialogue = swaggerDialogue;
        _currentDialogue.SetActive(true);
    }
}
