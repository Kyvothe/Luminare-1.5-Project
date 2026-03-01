using System;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    #region Inspector Variables

    public int _maxHealth = 40;
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
        if (isFirstLevel)                                                                                               // PlayerPrefs auf maxHealth wenn FirstLevel, weil keine Weitergabe von health
        {
            _currentHealth = _maxHealth;
            PlayerPrefs.SetInt("Health", _currentHealth);
        }
        else
        {
            _currentHealth = PlayerPrefs.GetInt("Health");                                                          // Alle anderen Level sollen helath uebernehmen
        }

        if (hasHealthUpgrade)
        {
            UpgradeHealthWithoutHeal();                                                                                 // Upgrade Health wenn Level nach Boss
        }
        
        _animator = GetComponent<Animator>();

        _openDialogueInGame = MenuManagerInGame.GetComponent<OpenDialogueInGame>();
    }

    private void FixedUpdate()
    {
        ReturnHealth();
    }

    public void SetDamage(int damage)                                                                                   // Player nimmt Schaden
    {
        _currentHealth -= damage;
        AudioManager.instance.PlaySoundFXClip(_hurtSound, transform, 1f);
        
        SetActionId(30);

        if (_currentHealth < 5)                                                                                         // Player dead
        {
            AudioManager.instance.PlaySoundFXClip(_deathSound, transform, 1f);
            SetActionId(40); 
            _playerDead = true;
            GetComponent<PlayerController>().enabled = false;
        }
        
        PlayerPrefs.SetInt("Health", _currentHealth);
    }

    public void SetHealth(int health)                                                                                   // Player wird geheilt
    {
        if ((_currentHealth + health) <= _maxHealth)                                                                    // Nicht ueberheilen
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

    private void GameOver()                                                                                             // Aufgerufen ueber AnimationsEvent am Ende der death Animation; kann abgeborchen werden -> GameOverFailSafe
    {
        if (!_playerDead) return;
        
        Debug.Log("Dead");
        
        MenuManagerInGame.GetComponent<OpenDialogueInGame>().OpenGameOverScreen();
    }

    public void UpgradeHealth()                                                                                         // Upgrade health auf 5 Herzen mit volle Heilung durch Soggy Pizza
    {
        _maxHealth = 50;
        _currentHealth = _maxHealth;
        
        PlayerPrefs.SetInt("Health", _currentHealth);
        
        extraHeart1.SetActive(true);                                                                                    // graue Herzen anschalten in HUD
        extraHeart2.SetActive(true);
    }

    private void UpgradeHealthWithoutHeal()                                                                             // Upgrade health auf 5 Herzen ohne Heilung fuer neue Szenen nach Boss
    {
        _maxHealth = 50;
        extraHeart1.SetActive(true);
        extraHeart2.SetActive(true);
    }

    public void SetSock2()                                                                                              // Animationswechsel fuer hurt and death animation
    {
        _currentTrigger = "ActionTriggerSock";
    }

    private void SetActionId(int id)                                                                                    // Animator setzen
    {
        _animator.SetTrigger(_currentTrigger);
        _animator.SetInteger(Hash_ActionId, id);
    }
}