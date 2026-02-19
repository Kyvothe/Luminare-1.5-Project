using System;
using UnityEngine;

public class CatInformation : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    public int _pawsAlive;
    private bool _catDied;

    private Animator _animator;

    private void Awake()
    {
        _pawsAlive = 2;
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
        if (_pawsAlive < 1)
        {
            _catDied = true;
            Debug.Log("Boss defeated");
        }
    }
}
