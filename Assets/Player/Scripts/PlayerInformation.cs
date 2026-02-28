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
    
    public GameObject extraHeart1;
    public GameObject extraHeart2;
    
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _healSound;

    #endregion
    
    private Animator _animator;

    private bool _playerDead = false;

    public bool isFirstLevel;

    public bool hasHealthUpgrade;

    private string _currentTrigger = "ActionTrigger";

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

        if (hasHealthUpgrade)
        {
            UpgradeHealthWithoutHeal();
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
        AudioManager.instance.PlaySoundFXClip(_hurtSound, transform, 1f);
        
        SetActionId(30);

        if (_currentHealth < 5)
        {
            AudioManager.instance.PlaySoundFXClip(_deathSound, transform, 1f);
            SetActionId(40); 
            _playerDead = true;
            GetComponent<PlayerController>().enabled = false;
        }
        
        PlayerPrefs.SetInt("Health", _currentHealth);
    }

    public void SetHealth(int health)
    {
        if ((_currentHealth + health) <= _maxHealth)
        {
            _currentHealth += health;
            AudioManager.instance.PlaySoundFXClip(_healSound, transform, 1f);
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
        
        Debug.Log("Dead");
        
        MenuManagerInGame.GetComponent<OpenDialogueInGame>().OpenGameOverScreen();
    }

    public void UpgradeHealth()
    {
        _maxHealth = 50;
        _currentHealth = _maxHealth;
        
        PlayerPrefs.SetInt("Health", _currentHealth);
        
        extraHeart1.SetActive(true);
        extraHeart2.SetActive(true);
    }

    private void UpgradeHealthWithoutHeal()
    {
        extraHeart1.SetActive(true);
        extraHeart2.SetActive(true);
    }

    public void SetSock2()
    {
        _currentTrigger = "ActionTriggerSock";
    }

    private void SetActionId(int id)
    {
        _animator.SetTrigger(_currentTrigger);
        _animator.SetInteger(Hash_ActionId, id);
    }
}