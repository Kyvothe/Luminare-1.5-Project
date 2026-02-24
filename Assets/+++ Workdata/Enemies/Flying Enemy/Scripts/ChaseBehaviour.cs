using System;
using UnityEngine;

public class ChaseBehaviour : MonoBehaviour
{
   public FlyingEnemy Enemy;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
         {
            Enemy.chase = true;
         }
   }
   
   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
            Enemy.chase = false;
            gameObject.GetComponent<FlyingEnemy>().ReturnToStart();
      }
   }
}
