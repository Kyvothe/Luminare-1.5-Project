using UnityEngine;

public class enemyInformation : MonoBehaviour
{
    [SerializeField] private int enemyMaxLifePoints;
    
    public float _currentLifePoints;
    
    private Collider2D _coll;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _currentLifePoints = enemyMaxLifePoints;
        _coll = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetDamage(int dmg)
    {
        _currentLifePoints -= dmg;

        if (_currentLifePoints < 1)
        {
            _coll.enabled = false;
            Destroy(gameObject);
        }
    }
}
