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
    
    private int _maxHealth;
    private int _currentHealth;
    private bool _gotHealthUpgrade;

    private void Awake()
    {
        _playerInformation = GetComponent<PlayerInformation>();
        _playerController = GetComponent<PlayerController>();
        
        _gotHealthUpgrade = false;
    }

    private void FixedUpdate()
    {
        _isAttacking = _playerController.ReturnIsAttacking();
        
        _currentHealth = _playerInformation.ReturnHealth();

        if (_gotHealthUpgrade)
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
            if (other.GetComponent<HealItem>())
            {
                if (_currentHealth >= _maxHealth) return;
                
                other.gameObject.SetActive(false);
                _playerInformation.SetHealth(other.GetComponent<HealItem>().ReturnHealthAmount());
                
                PickedUpItem.Invoke();
            }
            else
            {
                itemCount++;
                other.gameObject.SetActive(false);
            
                if (itemCount == 5)
                { 
                    gotAllItems = true;
                } 
            }
        }

        if (other.CompareTag("SunGlasses"))
        {
            other.gameObject.SetActive(false);
            gotSunGlasses = true;

        }
        
        if (other.CompareTag("HealthUpgrade"))
        {
            _playerInformation.UpgradeHealth(); 
            _gotHealthUpgrade = true;
            other.gameObject.SetActive(false);
        }
    }

    public bool ReturnGotItems()
    {
        return gotAllItems;
    }

    public bool ReturnSunGlasses()
    {
        return gotSunGlasses;
    }
    
    public int ReturnItemCount()
    {
        return itemCount;
    }
}