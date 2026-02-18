using UnityEngine;

public class RatGangInteract : MonoBehaviour
{
    private bool _playerIn = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIn = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIn = false;
        }
    }

    public bool ReturnPlayerIn()
    {
        return _playerIn;
    }
}
