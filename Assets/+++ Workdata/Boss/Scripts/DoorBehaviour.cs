using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public int _health = 15;

    public GameObject item;

    private Vector2 _spawnPosition;
    
    private SpriteColorChanger _spriteColorChanger;

    private void Awake()
    {
        _spawnPosition.x = 0;
        _spawnPosition.y = 6;
        
        _spriteColorChanger = GetComponent<SpriteColorChanger>();
    }
    
    public void SetDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Instantiate(item, _spawnPosition, Quaternion.identity);
            Destroy(gameObject);
        }
        
        _spriteColorChanger.ColorObject();
    }
}
