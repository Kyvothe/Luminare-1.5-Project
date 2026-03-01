using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    public int clawDamage;

    private bool _hasHit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasHit) return;                                                                                            // Nur ein Hit
        
        if (other.CompareTag("Player"))                                                                                 // Player nimmt Schaden
        {
            other.GetComponent<PlayerInformation>().SetDamage(clawDamage); 
            _hasHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            _hasHit = false;
        }
    }
}