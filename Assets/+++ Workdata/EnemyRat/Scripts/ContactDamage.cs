using System;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
 [SerializeField] private int contactDamage;

 private void OnTriggerEnter2D(Collider2D other)
 {
  if (other.CompareTag("Player"))
  {
   other.GetComponent<PlayerInformation>().SetDamage(contactDamage);
  }
  
  if (other.CompareTag("Enemy"))
  {
   Debug.Log("Hello there");
   other.GetComponent<enemyRatPatrolMovement>().BumpChangeDirection();
  }
  
 }
}