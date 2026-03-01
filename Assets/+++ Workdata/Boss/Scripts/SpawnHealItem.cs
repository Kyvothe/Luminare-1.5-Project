using Unity.VisualScripting;
using UnityEngine;

public class SpawnHealItem : MonoBehaviour
{
   public int _random;
   
   public GameObject HealItem;
   
   private bool _spawned = false;

   public void DropItem()
   { 
      Debug.Log("Paw");
      _random = Random.Range(0, 100);

      if (_random < 40)                                                                                                 // Spawn Wahrscheinlichkeit
      {
         if (_spawned == false)                                                                                         // Kein Stacking von HealItems
         { 
            Instantiate(HealItem, gameObject.transform.position, gameObject.transform.rotation);
            _spawned = true;
         }
      }
   }

   public void ResetSpawn()
   {
      if (_spawned == true)
      { 
         _spawned = false;
      }
   }
}
