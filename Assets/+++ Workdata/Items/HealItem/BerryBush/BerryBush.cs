using UnityEngine;
using UnityEngine.Events;

public class BerryBush : MonoBehaviour
{
    public int healAmount;
    public int _maxHealth;
    
    public PlayerInformation playerInformation;
    private int _playerHealth;
    
    public void Heal()
    {
        playerInformation.SetHealth(healAmount);
    }

    public bool CheckIfInteractable()
    {
        _playerHealth = playerInformation.ReturnHealth();

        if (_playerHealth < _maxHealth)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
