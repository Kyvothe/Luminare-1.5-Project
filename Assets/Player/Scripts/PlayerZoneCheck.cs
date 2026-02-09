using System;
using UnityEngine;

public class PlayerZoneCheck : MonoBehaviour
{
    [SerializeField] private int itemCount = 0;
    public bool gotAllItems = false;
    
    public HealItem healitem;

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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isAttacking) return;
        
        if (other.CompareTag("PickupItem"))
        {
            if (other.GetComponent<HealItem>())
            {
                other.gameObject.SetActive(false);
                _playerInformation.SetHealth(healitem.ReturnHealthAmount());
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
    }
}