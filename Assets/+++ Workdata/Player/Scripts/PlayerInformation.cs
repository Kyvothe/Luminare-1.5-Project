using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
   #region Inspector Variables

   [SerializeField] private int _maxHealth;
   [SerializeField] private int _currentHealth;

   #endregion

   public void SetDamage(int damage)
   {
      _currentHealth -= damage;

      if (_currentHealth < 1)
      {
        // dead stuff 
      }
   }

   public void SetHealth(int health)
   {
       if ((_currentHealth + health) <= _maxHealth)
       {
           _currentHealth += health;
       }
   }
}
