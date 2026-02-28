using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour
{
   public void ResetHealth40()
   {
        PlayerPrefs.SetInt("Health", 40);
   }

   public void ResetHealth50()
   {
       PlayerPrefs.SetInt("Health", 50);
   }
}
