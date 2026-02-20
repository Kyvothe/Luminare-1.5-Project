using UnityEngine;

public class SpawnHealItem : MonoBehaviour
{
   public int _random;
   
   public GameObject HealItem;

   public void DropItem()
   { 
      Debug.Log("Paw");
      _random = Random.Range(0, 100);

      if (_random < 50)
      {
         Instantiate(HealItem, gameObject.transform.position, gameObject.transform.rotation);
      }
   }
}
