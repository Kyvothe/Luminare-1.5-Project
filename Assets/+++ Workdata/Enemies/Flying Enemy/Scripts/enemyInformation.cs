using UnityEngine;

public class enemyInformation : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    [SerializeField] private int enemyMaxLifePoints;
    
    public float _currentLifePoints;

    public GameObject enemyDrop;
    public GameObject player;
    
    private Animator _animator;
    
    private Collider2D _coll;
    private Rigidbody2D _rb;
    
    private bool _isGrounded;

    private Vector2 _position;
    private int _random;
    
    private SpriteColorChangerChildren _colorSpriteSetter;
    
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
        _colorSpriteSetter = GetComponentInChildren<SpriteColorChangerChildren>();
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
            
            if (player.GetComponent<PlayerController>().ReturnDirection())
            {
                _position.x = transform.position.x - 1.5f;
            }
            else
            {
                _position.x = transform.position.x + 1.5f;
            }
        }
        _colorSpriteSetter.ColorObject();
    }

    public void DestroyEnemey()
    {
        Destroy(gameObject);
    }

    public void Spawn()
    {
        _random = Random.Range(0, 100);

        if (_random < 75)
        { 
            Instantiate(enemyDrop, _position, Quaternion.identity);
        }
    }
}
