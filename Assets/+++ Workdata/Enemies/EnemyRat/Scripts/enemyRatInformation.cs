using UnityEngine;

public class enemyRatInformation : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    [SerializeField] private int enemyMaxLifePoints;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _hitSound;
    
    public float _currentLifePoints;

    public GameObject enemyDrop;
    public GameObject player;
    
    private bool _isDead;

    private SpriteColorChanger _colorSpriteSetter;
    private Collider2D _coll;
    private Rigidbody2D _rb;
    
    private Animator _animator;
    
    private Vector2 _position;
    private int _random;

    private void Awake()
    {
        _currentLifePoints = enemyMaxLifePoints;
        
        _animator = GetComponent<Animator>();
        
        _colorSpriteSetter = GetComponent<SpriteColorChanger>();
        _coll = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        
        _isDead = false;
    }

    public void SetDamage(int dmg)                                                                                      // Ratte nimmt Schaden
    {
        _currentLifePoints -= dmg;
        AudioManager.instance.PlaySoundFXClip(_hitSound, transform, 1f);

        if (_currentLifePoints < 1)                                                                                     // Ratte tot
        {
            _coll.enabled = false;
            _rb.bodyType = RigidbodyType2D.Static;
            GetComponentInChildren<enemyRatAnimation>().SetEnemyDeath();                                    
            enemyRatPatrolMovement enemyPatrol = GetComponentInChildren<enemyRatPatrolMovement>();
            
            GetComponentInChildren<enemyRatPatrolMovement>().SetActionState(2);
            enemyPatrol.enabled = false;
            
            _animator.SetTrigger(Hash_ActionTrigger);
            _animator.SetInteger(Hash_ActionId, 10);
            AudioManager.instance.PlaySoundFXClip(_deathSound, transform, 1f);
            
            _isDead = true;

            gameObject.GetComponentInChildren<ContactDamage>().SetIsDead();
            
            _position = transform.position;
            _position.y = transform.position.y + 0.5f;
            
            if (player.GetComponent<PlayerController>().ReturnDirection())                                              // Spawn direction gegenueber vom Player damit nicht on spawn eingesammelt wird
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
        _animator.enabled = false;
        Destroy(gameObject);
    }

    public void Spawn()                                                                                                 // Chance auf Spawn von HealItem
    {
        _random = Random.Range(0, 100);
        Debug.Log(_random);

        if (_random < 75)
        {
            Instantiate(enemyDrop, _position, Quaternion.identity);
        }
    }
}