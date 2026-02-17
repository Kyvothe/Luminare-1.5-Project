using System;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    private Animator _animator;
    private bool _isGrounded;
    private bool _notDestroyed = true;

    public int damage;
    
    [Header("Ground Setup")] 
    [SerializeField] private Vector2 groundBoxPos;
    [SerializeField] private Vector2 groundBoxSize;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
        Destroy(gameObject);
    }
    
    private void CheckIsGrounded()
    {
        _isGrounded = Physics2D.OverlapBox((Vector2)transform.position + groundBoxPos, groundBoxSize, 0, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInformation>().SetDamage(damage);
            
            _animator.SetTrigger(Hash_ActionTrigger);
            _animator.SetInteger(Hash_ActionId, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + groundBoxPos, groundBoxSize);
    }
}
