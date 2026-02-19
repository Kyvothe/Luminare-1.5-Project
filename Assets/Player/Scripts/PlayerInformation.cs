using System;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    #region Inspector Variables

    private int _maxHealth = 40;
    [SerializeField] private int _currentHealth;

    public GameObject MenuManagerInGame;

    #endregion
    
    private Animator _animator;

    private bool _playerDead = false;

    public bool isFirstLevel;

    private OpenDialogueInGame _openDialogueInGame;

    private void Awake()
    {
        if (isFirstLevel)
        {
            _currentHealth = _maxHealth;
            PlayerPrefs.SetInt("Health", _currentHealth);
        }
        else
        {
            _currentHealth = PlayerPrefs.GetInt("Health");
        }
        
        _animator = GetComponent<Animator>();

        _openDialogueInGame = MenuManagerInGame.GetComponent<OpenDialogueInGame>();
    }

    private void FixedUpdate()
    {
        ReturnHealth();
    }

    public void SetDamage(int damage)
    {
        _currentHealth -= damage;
        
        SetActionId(30);

        if (_currentHealth < 5)
        {
            SetActionId(40); 
            _playerDead = true;
        }
        
        PlayerPrefs.SetInt("Health", _currentHealth);
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
        
        PlayerPrefs.SetInt("Health", _currentHealth);
    }
   
    public int ReturnHealth()
    {
        return _currentHealth;
    }

    private void GameOver()                                                                                             // Aufgerufen ueber AnimationsEvent am Ende der death Animation
    {
        if (!_playerDead) return;
        
        _openDialogueInGame.OpenGameOverScreen();
    }

    private void SetActionId(int id)
    {
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionId, id);
    }
}