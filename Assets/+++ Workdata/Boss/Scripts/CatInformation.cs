using System;
using UnityEngine;
using UnityEngine.Events;

public class CatInformation : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    public int _pawsAlive;

    public GameObject item;
    
    private bool _catDied;

    private Animator _animator;
    
    private Vector2 _spawnPosition;
    
    public UnityEvent OnDeath;

    private void Awake()
    {
        _pawsAlive = 3;
        
        _animator = GetComponent<Animator>();

        _spawnPosition.x = -4.39f;
        _spawnPosition.y = 6;
    }

    public void StartFight()
    {
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionId, 100);
    }
    
    public void PawDied()
    {
        _pawsAlive--;
    }

    public void TakesDamage()
    {
        Debug.Log("Hurt");
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionId, 10);
    }

    private void FixedUpdate()
    {
        if (_pawsAlive <= 1 && !_catDied)
        {
            _catDied = true;
            _animator.SetInteger(Hash_ActionId, 20);
            OnDeath.Invoke();
        }
    }

    public void SpawnHealthUpgrade()
    {
        Instantiate(item, _spawnPosition, Quaternion.identity);
    }
}
