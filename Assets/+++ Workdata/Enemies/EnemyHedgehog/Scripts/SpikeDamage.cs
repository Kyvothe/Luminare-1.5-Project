using System;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
 [SerializeField] private int spikeDamage;
 
 private bool _hasHit = false;
 private void OnTriggerEnter2D(Collider2D other)
 {
  if (_hasHit) return;
  
  if (other.CompareTag("Player"))
  {
   other.GetComponent<PlayerInformation>().SetDamage(spikeDamage);
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
