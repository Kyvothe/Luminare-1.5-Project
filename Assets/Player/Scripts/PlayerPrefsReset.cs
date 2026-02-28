using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour
{
   public void ResetHealth40()                                                                                          // fuellt health wieder auf, damit nicht 0 helath on try again after death
   {
        PlayerPrefs.SetInt("Health", 40);                                                                               // fuer Level ohne health upgrade
   }

   public void ResetHealth50()
   {
       PlayerPrefs.SetInt("Health", 50);                                                                                // fuer level mit health upgrade
   }
}
