using System;
using UnityEngine;

public class ChaseBehaviour : MonoBehaviour
{
    public FlyingEnemy Enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))                                                                                 // Player in ChaseContainer
        {
            Enemy.PlayerDetected(true);
        }
    }
   
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy.PlayerDetected(false);
            Enemy.StopChase();

        }
    }
}