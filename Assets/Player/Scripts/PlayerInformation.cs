using System;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    #region Inspector Variables

    private int _maxHealth = 40;
    [SerializeField] private int _currentHealth;

    #endregion

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void FixedUpdate()
    {
        ReturnHealth();
    }


    public void SetDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth < 5)
        {
            // dead stuff 
        }
    }

    public void SetHealth(int health)
    {
        if ((_currentHealth + health) <= _maxHealth)
        {
            _currentHealth += health;
        }
        else
        {
            _currentHealth = _maxHealth;
        }
    }
   
    public int ReturnHealth()
    {
        return _currentHealth;
    }
}