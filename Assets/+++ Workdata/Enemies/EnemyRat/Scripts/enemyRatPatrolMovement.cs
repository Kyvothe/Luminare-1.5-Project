using System;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class enemyRatPatrolMovement : MonoBehaviour
{
    public enum EnemyMovementState{Idle, Movement, Chase, ChangeDirection}
    
    public enum EnemyActionState{Default, Attack, Dead}
    
    public enum EnemyAggroState {Default, Aggro}
    
    #region Inspector

    [Header("States")]
    public EnemyMovementState enemyMovementState;
    public EnemyActionState enemyActionState;
    public EnemyAggroState enemyAggroState;
    
    
    [Header("Movement")]
    [SerializeField] private bool isFacingRight = false;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float changeDirectionTime;
    
    [Header("Attack")]
    [SerializeField] private float attackDistance = 1;
    
    #endregion
    
    #region Private Variables
    
    private Rigidbody2D _rb;
    private enemyRatAnimation _enemyRatAnimation;
    public int _facingDirection;
    public int FacingDirection => _facingDirection;
    
    public Transform _chaseTarget;

    private float _lastDirectionChangeTime;
    
    #endregion
    
    #region Unity Events

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyRatAnimation = GetComponentInChildren<enemyRatAnimation>();
        _facingDirection = isFacingRight ? 1 : -1;
        SetDirection();
    }
    
    private void FixedUpdate()
    {
        if (enemyMovementState == EnemyMovementState.ChangeDirection)
        {
            ChangeDirection();
            Debug.Log("IM CHANGING");
            return;
        }
        
        if (enemyActionState != EnemyActionState.Default) return;

        if (enemyMovementState == EnemyMovementState.Chase)                                                             // Gibt es bei Ratte nicht
        {
            if (_chaseTarget == null)
            {
                Debug.LogError("Chase target is null");
                return;
            }

            if (Vector2.Distance(transform.position, _chaseTarget.position) < attackDistance)
            {
                _enemyRatAnimation.SetAttack();
                enemyActionState = EnemyActionState.Attack;
            }

            if ((transform.position.x < _chaseTarget.position.x && _facingDirection == -1) ||
                (transform.position.x > _chaseTarget.position.x && _facingDirection == 1))
            {
                ChangeDirection();
            }
        }
        
        _rb.linearVelocityX = movementSpeed * _facingDirection;
    }

    public void LateUpdate()
    {
        UpdateAnimator();
        CheckMovementState();
    }

    #endregion
    
    #region State
    public void CheckMovementState()
    {
        enemyMovementState = Mathf.Abs(_rb.linearVelocityX) > 0 ?
            (enemyAggroState == EnemyAggroState.Aggro ? EnemyMovementState.Chase: EnemyMovementState.Movement)
            : EnemyMovementState.Idle;
    }

    public void SetAggroMode(Transform target)
    {
        enemyMovementState = EnemyMovementState.Chase;
        enemyAggroState = EnemyAggroState.Aggro;
        
        _chaseTarget = target;
    }

    public void SetAggroModeToDefault()
    {
        enemyAggroState = EnemyAggroState.Default;
        _chaseTarget = null;
    }

    public void SetMovementState(int state)
    {
        enemyMovementState = (EnemyMovementState)state;
    }

    public void SetActionState(int state)
    {
        enemyActionState = (EnemyActionState)state;

        if (enemyMovementState != EnemyMovementState.Chase);
        {
            _chaseTarget = null;
        }
    }

    public void SetActionStateToDefault()
    {
        enemyActionState = EnemyActionState.Default;
    }
    
    #endregion


    #region Movement

    public void ChangeDirection()                                                                                       
    {
        
        if (Time.time - _lastDirectionChangeTime > changeDirectionTime)                                                 // Kein Jitter-Change-Direction
        {
            _lastDirectionChangeTime = Time.time;
            _facingDirection *= -1;
            transform.rotation = Quaternion.Euler(0, _facingDirection == 1 ? 180 : 0, 0);
            enemyMovementState = EnemyMovementState.Idle;
        }
        else
        {
            enemyMovementState = EnemyMovementState.ChangeDirection;
        }
    }
    
    public void SetDirection()
    {
        transform.rotation = Quaternion.Euler(0, _facingDirection == 1? 180 : 0, 0);
    }

    public void BumpChangeDirection()
    {
        enemyMovementState = EnemyMovementState.ChangeDirection;

    }
    
    #endregion
    
    #region Animation

    private void UpdateAnimator()
    {
        //_animator.SetFloat("MovementValue", _rb.linearVelocity.magnitude);
        _enemyRatAnimation.SetMovementValue(Mathf.Abs(_rb.linearVelocityX));
    }
    
    #endregion
    
}
