using UnityEngine;

public class OneWayPlatformBehaviour : MonoBehaviour
{
   [SerializeField] private PlatformEffector2D effector2D;
   [SerializeField] private float setBackTimer;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         other.GetComponent<PlayerOneWay>().SetPlatformEffector(effector2D);
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         other.GetComponent<PlayerOneWay>().SetPlatformEffector(null);
      }
   }

   public void SetEffectorBack()
   {
      effector2D.surfaceArc = 180;
   }

   public void StartSetBackTimer()
   {
      Invoke("SetEffectorBack", setBackTimer);
   }
}
