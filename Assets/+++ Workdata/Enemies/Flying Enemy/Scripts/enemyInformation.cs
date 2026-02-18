using UnityEngine;

public class enemyInformation : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    [SerializeField] private int enemyMaxLifePoints;
    
    public float _currentLifePoints;

    public GameObject enemyDrop;
    
    private Animator _animator;
    
    private Collider2D _coll;
    private Rigidbody2D _rb;
    
    private bool _isGrounded;

    private Vector2 _position;
    private int _random;
    
    [Header("Ground Setup")] 
    [SerializeField] private Vector2 groundBoxPos;
    [SerializeField] private Vector2 groundBoxSize;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        _currentLifePoints = enemyMaxLifePoints;
        _coll = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapBox((Vector2)transform.position + groundBoxPos, groundBoxSize, 0, groundLayer);

        if (_isGrounded)
        {
            _animator.SetBool("isGrounded", true);
        }
    }

    public void SetDamage(int dmg)
    {
        _currentLifePoints -= dmg;

        if (_currentLifePoints < 1)
        {
            Debug.Log("Dead");
            
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.gravityScale = 5;
            
            FlyingEnemy flyingEnemy = GetComponentInChildren<FlyingEnemy>();
            flyingEnemy.enabled = false;
            
            ChaseBehaviour chaseBehaviour = GetComponentInChildren<ChaseBehaviour>();
            chaseBehaviour.enabled = false;
                
            _animator.SetBool("InAttackRange", false);
            
            _animator.SetTrigger("ActionTrigger");
            _animator.SetInteger("ActionId", 10);
            
            _position = transform.position;
            
        }
    }

    public void DestroyEnemey()
    {
        Destroy(gameObject);
    }

    public void Spawn()
    {
        _random = Random.Range(0, 2);

        if (_random == 0)
        { 
            Instantiate(enemyDrop, _position, Quaternion.identity);
        }
    }
}
