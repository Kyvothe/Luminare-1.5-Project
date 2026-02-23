using UnityEngine;

public class enemyRatInformation : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    [SerializeField] private int enemyMaxLifePoints;
    
    public float _currentLifePoints;

    public GameObject enemyDrop;
    public GameObject player;
    
    private bool _isDead;

    private ColorSpriteSetter _colorSpriteSetter;
    private Collider2D _coll;
    private Rigidbody2D _rb;
    
    private Animator _animator;
    
    private Vector2 _position;
    private int _random;

    private void Awake()
    {
        _currentLifePoints = enemyMaxLifePoints;
        
        _animator = GetComponent<Animator>();
        
        _colorSpriteSetter = GetComponent<ColorSpriteSetter>();
        _coll = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        
        _isDead = false;
    }

    public void SetDamage(int dmg)
    {
        _currentLifePoints -= dmg;

        if (_currentLifePoints < 1)
        {
            _coll.enabled = false;
            _rb.bodyType = RigidbodyType2D.Static;
            GetComponentInChildren<enemyRatAnimation>().SetEnemyDeath();                                                // maybe rausnehmen???????????
            enemyRatPatrolMovement enemyPatrol = GetComponentInChildren<enemyRatPatrolMovement>();
            
            GetComponentInChildren<enemyRatPatrolMovement>().SetActionState(2);
            enemyPatrol.enabled = false;
            
            _animator.SetTrigger(Hash_ActionTrigger);
            _animator.SetInteger(Hash_ActionId, 10);
            
            _isDead = true;

            gameObject.GetComponentInChildren<ContactDamage>().SetIsDead();
            
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
        Debug.Log(_random);

        if (_random < 75)
        {
            Instantiate(enemyDrop, _position, Quaternion.identity);
        }
    }
}
