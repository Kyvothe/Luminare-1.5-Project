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

      if (_random < 50)
      {
         if (_spawned == false)
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
