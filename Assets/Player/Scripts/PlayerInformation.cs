using System;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    #region Inspector Variables

    private int _maxHealth = 40;
    [SerializeField] private int _currentHealth;

    #endregion
    
    private Animator _animator;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        
        _animator = GetComponent<Animator>();
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
            // dead stuff 
            SetActionId(40);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
           // player noch movable!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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

    private void SetActionId(int id)
    {
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionId, id);
    }
}