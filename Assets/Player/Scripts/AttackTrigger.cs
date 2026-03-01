using System;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paw"))                                                                                    // Player schadet Katze im Bosskampf mit 5 Schaden
        { 
            other.GetComponent<PawInformation>().SetDamage(5);
        }

        if (other.CompareTag("Enemy"))                                                                          
        {
            Debug.Log("Found enemy");
            
            if (other.GetComponent<FlyingEnemy>())                                                                      // Schaden an Krähe
            {
                other.GetComponent<enemyInformation>().SetDamage(2);
            }
            else
            {
                other.GetComponent<enemyRatInformation>().SetDamage(2);                                            // Schaden an Ratte
                Debug.Log("Found rat");
            }
        }

        if (other.CompareTag("Door"))                                                                                   // Schaden an Door im Bosskampf
        {
            other.GetComponent<DoorBehaviour>().SetDamage(1);
        }
    }
}
