using UnityEngine;

public class EnemyEnviromentChecker : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private float wallCheckDistance = 0.2f;

    [SerializeField] private LayerMask groundAndWallMask;
    
    #region Private Variables

    private enemyRatPatrolMovement _enemyRatPatrolMovement;
    
    #endregion

    private void Awake()
    {
        _enemyRatPatrolMovement = GetComponent<enemyRatPatrolMovement>();
    }
    private void FixedUpdate()
    {
        if (_enemyRatPatrolMovement.enemyMovementState != enemyRatPatrolMovement.EnemyMovementState.Movement) return;
        
        if (CheckForWalls() || !CheckForGround())
        {
            Debug.Log(CheckForGround());
            _enemyRatPatrolMovement.ChangeDirection();
        }
    }


    bool CheckForWalls()
    {
        Vector2 direction = Vector2.right * _enemyRatPatrolMovement.FacingDirection;

        RaycastHit2D hit = Physics2D.Raycast(
            wallCheck.position,
            direction,
            wallCheckDistance,
            groundAndWallMask
        );
        return hit.collider;
    }
    
    
    bool CheckForGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(
        groundCheck.position,
        Vector2.down,
        groundCheckDistance,
        groundAndWallMask
            );
        return hit.collider;
    }
    
    #region Gizmos

    private void OnDrawGizmos()
    {
        float direction = _enemyRatPatrolMovement ? _enemyRatPatrolMovement.FacingDirection : 1;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position,
            wallCheck.position + Vector3.right * direction * wallCheckDistance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, 
            groundCheck.position + Vector3.down * groundCheckDistance);
    }

    #endregion
    
}
