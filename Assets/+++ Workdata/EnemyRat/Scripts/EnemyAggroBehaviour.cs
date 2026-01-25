using UnityEngine;

public class EnemyAggroBehaviour : MonoBehaviour
{
    private enemyRatPatrolMovement _enemyRatPatrolMovement;

    private void Awake()
    {
        _enemyRatPatrolMovement = GetComponentInParent<enemyRatPatrolMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyRatPatrolMovement.SetAggroMode(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyRatPatrolMovement.SetAggroModeToDefault();
        }
    }
}
