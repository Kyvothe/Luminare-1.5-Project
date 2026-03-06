using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public int _health = 15;

    public GameObject item;

    private Vector2 _spawnPosition;
    
    private SpriteColorChanger _spriteColorChanger;

    private void Awake()
    {
        _spawnPosition.x = 14f;
        _spawnPosition.y = -2f;
        
        _spriteColorChanger = GetComponent<SpriteColorChanger>();
    }
    
    public void SetDamage(int damage)
    {
        _health -= damage;                                                                                              // Door nimmt Schaden

        if (_health <= 0)                                                                                               // Door tot
        {
            Instantiate(item, _spawnPosition, Quaternion.identity);                                                  // Spawn Soggy Pizza
            Destroy(gameObject);
        }
        
        _spriteColorChanger.ColorObject();
    }
}
