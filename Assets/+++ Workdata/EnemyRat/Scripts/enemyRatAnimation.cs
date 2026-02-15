using System;
using TMPro;
using UnityEngine;

public class enemyRatAnimation : MonoBehaviour

{
    
   // public static readonly int Hash_MovementValue = Animator.StringToHash("MovementValue");
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");

    private Animator _animator;



    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    
    public void SetEnemyDeath()
    {
        AnimationSetActionId(100);
    }
    
    public void SetAttack()
    {
        AnimationSetActionId(10);
    }

    public void SetMovementValue(float value)
    {
      //  _animator.SetFloat(Hash_MovementValue, value);
    }
    
    private void AnimationSetActionId(int id)
    {
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionId, id);
    }

}
