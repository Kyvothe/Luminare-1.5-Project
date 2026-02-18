using System;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
 [SerializeField] private int contactDamage;

 private bool _isDead;

 private void Awake()
 {
  _isDead = false;
 }

 private void OnTriggerEnter2D(Collider2D other)
 {
  if (_isDead) return;

  if (other.CompareTag("Player"))
  {
   other.GetComponent<PlayerInformation>().SetDamage(contactDamage);

   Debug.Log("Player damaged");
  }
  
  /*
  if (other.CompareTag("Enemy"))
  {
   Debug.Log("Hello there");
   other.GetComponent<enemyRatPatrolMovement>().BumpChangeDirection();
  }*/

 }
 
 public void SetIsDead()
 {
  _isDead = true;
 }
}