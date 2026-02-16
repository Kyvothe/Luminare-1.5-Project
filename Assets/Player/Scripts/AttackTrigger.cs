using System;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paw"))
        { 
            other.GetComponent<PawInformation>().SetDamage(5);
        }
    }
}
