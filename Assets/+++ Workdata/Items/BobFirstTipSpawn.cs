using System;
using UnityEngine;

public class BobFirstTipSpawn : MonoBehaviour
{
   public GameObject manager;
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         gameObject.GetComponent<SpriteRenderer>().enabled = false;
         gameObject.GetComponent<CircleCollider2D>().enabled = false;

         manager.GetComponent<OpenDialogueInGame>().OpenBobTip();
      }
   }
}
