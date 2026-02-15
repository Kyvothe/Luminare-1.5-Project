using System;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
 [SerializeField] private int spikeDamage;
 private void OnTriggerEnter2D(Collider2D other)
 {
  if (other.CompareTag("Player"))
  {
   other.GetComponent<PlayerInformation>().SetDamage(spikeDamage);  
  }
 }
}
