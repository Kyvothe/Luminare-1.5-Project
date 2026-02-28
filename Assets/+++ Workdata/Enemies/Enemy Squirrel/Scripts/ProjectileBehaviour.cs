using System;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    private Animator _animator;
    private bool _isGrounded;
    private bool _notDestroyed = true;
    private BoxCollider2D _coll;

    public int damage;

    private bool _hasHit = false;
    
    [Header("Ground Setup")] 
    [SerializeField] private Vector2 groundBoxPos;
    [SerializeField] private Vector2 groundBoxSize;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _coll = GetComponent< BoxCollider2D>();
    }
    
    private void FixedUpdate()
    {
        CheckIsGrounded();

        if (_isGrounded)
        {
            _animator.SetTrigger(Hash_ActionTrigger);
            _animator.SetInteger(Hash_ActionId, 1);
            
        }
    }
    
    public void DestroyProjectile()                                                                                     // aufgerufen über AnimationEndAcorn am Ende von Shatter Animantion
    {
        _hasHit = false;
        Destroy(gameObject);
    }

    public void DisableDamage()
    {
        _coll.enabled = false;
    }
    
    
    private void CheckIsGrounded()
    {
        _isGrounded = Physics2D.OverlapBox((Vector2)transform.position + groundBoxPos, groundBoxSize, 0, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasHit) return;
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInformation>().SetDamage(damage);
            
            _animator.SetTrigger(Hash_ActionTrigger);
            _animator.SetInteger(Hash_ActionId, 1);
            
            _hasHit = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + groundBoxPos, groundBoxSize);
    }
}
