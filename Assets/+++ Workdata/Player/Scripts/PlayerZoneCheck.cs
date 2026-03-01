using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerZoneCheck : MonoBehaviour
{
    [SerializeField] private int itemCount = 0;
    public bool gotAllItems = false;
    public bool gotSunGlasses = false;

    public UnityEvent PickedUpItem;

    private PlayerInformation _playerInformation;
    private PlayerController _playerController;

    private bool _isAttacking;
    
    public int _maxHealth;
    private int _currentHealth;
    private bool _gotHealthUpgrade;
    
    [SerializeField] private AudioClip _crumbSound;
    [SerializeField] private AudioClip _triumphSound;

    private void Awake()
    {
        _playerInformation = GetComponent<PlayerInformation>();
        _playerController = GetComponent<PlayerController>();
        _gotHealthUpgrade = GetComponent<PlayerInformation>().ReturnUpgrade();
        
        if (_gotHealthUpgrade)                                                                                          // Health Upgrade fuer Szenen nach Boss
        {
            _maxHealth = 50;
        }
        else
        {
            _maxHealth = 40;
        }
    }

    private void FixedUpdate()
    {
        _isAttacking = _playerController.ReturnIsAttacking();
        
        _currentHealth = _playerInformation.ReturnHealth();
    
        if (_gotHealthUpgrade)                                                                                          // Health Upgrade on Soggy Pizza
        {
            _maxHealth = 50;
        }
        else
        {
            _maxHealth = 40;
        }

        ReturnGotItems();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isAttacking) return;
        
        if (other.CompareTag("PickupItem"))
        {
            if (other.GetComponent<HealItem>())                                                                         // Hat Apfel zum Heilen eingesammelt
            {
                if (_currentHealth >= _maxHealth) return;
                other.gameObject.SetActive(false);
                _playerInformation.SetHealth(other.GetComponent<HealItem>().ReturnHealthAmount());
                
                PickedUpItem.Invoke();
            }
            else
            {                                                                                                           // Hat Pizza Crumb eingesammelt
                itemCount++;
                other.gameObject.SetActive(false);
                AudioManager.instance.PlaySoundFXClip(_crumbSound, transform, 0.5f);
            
                if (itemCount >= 10)                                                                                    // Alle Pizza Crumbs eingesammelt
                { 
                    AudioManager.instance.PlaySoundFXClip(_triumphSound, transform, 1f);
                    gotAllItems = true;
                } 
            }
        }

        if (other.CompareTag("SunGlasses"))                                                                             // SunGlasses eingesammelt
        {
            other.gameObject.SetActive(false);
            gotSunGlasses = true;
            AudioManager.instance.PlaySoundFXClip(_triumphSound, transform, 1f);

        }
        
        if (other.CompareTag("HealthUpgrade"))                                                                          // Soggy Pizza eingesammelt
        {
            _playerInformation.UpgradeHealth(); 
            _gotHealthUpgrade = true;
            other.gameObject.SetActive(false);
            AudioManager.instance.PlaySoundFXClip(_triumphSound, transform, 1f);
        }
    }

    public bool ReturnGotItems()                                                                                        // Fuer andere Scripte
    {
        return gotAllItems;
    }

    public bool ReturnSunGlasses()                                                                                      // Fuer andere Scripte
    {
        return gotSunGlasses;
    }
    
    public int ReturnItemCount()                                                                                        // Fuer andere Scripte
    {
        return itemCount;
    }
}