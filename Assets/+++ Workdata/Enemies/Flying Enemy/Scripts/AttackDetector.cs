using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    public FlyingEnemy _crow;

    private bool _hasHit = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasHit) return;
        
        if (other.CompareTag("Player"))                                                                                 // Player in AttackRange
        {
            _crow.SetAttackInfos(true);
            _hasHit = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _crow.SetAttackInfos(false);
            _hasHit = false;
        }
    }
}