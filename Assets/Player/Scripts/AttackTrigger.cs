using System;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paw"))
        { 
            // other.GetComponent<PawInformation>().SetDamage(5);
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("FOund enemy");
            
            if (other.GetComponent<FlyingEnemy>())
            {
                other.GetComponent<enemyInformation>().SetDamage(2);
            }
            else
            {
                other.GetComponent<enemyRatInformation>().SetDamage(2);
                Debug.Log("Found rat");
            }
        }
    }
}