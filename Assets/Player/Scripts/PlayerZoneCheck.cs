using System;
using UnityEngine;

public class PlayerZoneCheck : MonoBehaviour
{
    [SerializeField] private int itemCount = 0;
    public bool gotAllItems = false;
    public bool gotSunGlasses = false;

    public GameObject _healItem;

    private PlayerInformation _playerInformation;
    private PlayerController _playerController;

    private bool _isAttacking;

    private void Awake()
    {
        _playerInformation = GetComponent<PlayerInformation>();
        _playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        _isAttacking = _playerController.ReturnIsAttacking();

        ReturnGotItems();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isAttacking) return;
        
        if (other.CompareTag("PickupItem"))
        {
            if (other.GetComponent<HealItem>())
            {
                other.gameObject.SetActive(false);
                _playerInformation.SetHealth(_healItem.GetComponent<HealItem>().ReturnHealthAmount());
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