using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    public FlyingEnemy _crow;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _crow.SetAttackInfos(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _crow.SetAttackInfos(false);
        }
    }
}
